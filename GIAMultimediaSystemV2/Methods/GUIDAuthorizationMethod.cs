using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using GIAMultimediaSystemV2.Configuration;
using Serilog;
using System;
using System.IO;
using System.Management;
using System.Threading;
using System.Windows.Forms;

namespace GIAMultimediaSystemV2.Methods
{
    /// <summary>
    /// 授權使用
    /// </summary>
    public class GUIDAuthorizationMethod
    {
        /// <summary>
        /// 初始路徑
        /// </summary>
        private string WorkPath = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 起始時間
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 結束時間
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 最後關閉系統時間
        /// </summary>
        public string FromTime { get; set; }
        /// <summary>
        /// 整合後系統號碼
        /// </summary>
        public string FinalCheckValue = null;
        /// <summary>
        /// 設備ID
        /// </summary>
        public string DeviceID { get; set; }
        /// <summary>
        /// 系統號碼
        /// </summary>
        public string SystemNumber { get; set; }
        /// <summary>
        /// 權杖號碼
        /// </summary>
        public string LicenseCode { get; set; }
        /// <summary>
        /// 授權旗標 True = 已授權，False = 未授權
        /// </summary>
        public bool LicenseBool { get; set; }
        /// <summary>
        /// 時間旗標
        /// </summary>
        public bool TimeError { get; set; } = true;
        /// <summary>
        /// 授權JSON
        /// </summary>
        public TokenSetting setting { get; set; }
        /// <summary>
        /// 主畫面物件
        /// </summary>
        //public Form1 Form1 { get; set; }
        /// <summary>
        /// GUID產生物件
        /// </summary>
        private Guid gid;
        /// <summary>
        /// 未授權啟動時間
        /// </summary>
        private DateTime NTimeEnd = DateTime.Now.AddSeconds(7200);
        /// <summary>
        /// 系統開起時間
        /// </summary>
        private DateTime NTimeStart = DateTime.Now;
        private FlyoutAction action = new FlyoutAction();


        #region 確認是否註冊，(否)建立設備ID與系統號碼
        /// <summary>
        /// 確認是否註冊，(否)建立設備ID與系統號碼
        /// </summary>
        public void Create_Multimedia()
        {
            if (!LicenseRead())
            {
                gid = Guid.NewGuid();   //啟動GUID
                #region GUID顯示數量
                long id = 1;
                foreach (byte b in gid.ToByteArray())
                {
                    id *= ((int)b + 1);
                }
                string guid;
                guid = string.Format("{0:x}", id - DateTime.Now.Ticks).ToUpper();
                #endregion
                #region 產生註冊碼
                int CheckValue = 0;
                //ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration"); //網卡
                ManagementClass mc = new ManagementClass("Win32_Processor"); //CPU ID
                ManagementObjectCollection moc = mc.GetInstances();
                try
                {
                    foreach (ManagementObject mo in moc)
                    {
                        //var MacAddress = mo.Properties["MACAddress"].Value; //網卡
                        var MacAddress = mo.Properties["ProcessorId"].Value; //CPU ID
                        if (MacAddress != null)
                        {
                            string macaddress = MacAddress.ToString().Replace(":", "");
                            for (int i = 0; i <= macaddress.Length - 1; i++)
                            {
                                CheckValue += Convert.ToInt16(macaddress.Substring(i, 1), 16) * (i + 1) * 256;
                            }
                        }
                    }

                    string[] ChekVlue1 = new string[CheckValue.ToString().Length];
                    string CheckValue1 = CheckValue.ToString();
                    for (int i = 0; i < CheckValue.ToString().Length; i++)
                    {
                        ChekVlue1[i] = CheckValue.ToString().Substring(i, 1);
                    }
                    for (int i = ChekVlue1.Length - 1; i > -1; i--)
                    {
                        CheckValue1 += ChekVlue1[i];
                    }
                    for (int i = 0; i < CheckValue1.Length; i++)
                    {
                        FinalCheckValue += guid.Substring(Convert.ToUInt16(CheckValue1.Substring(i, 1)) + 2, 1);
                    }
                    SystemNumber = strAdd(guid);
                    DeviceID = strAdd(CheckValue1);
                }
                catch (Exception ex) { Log.Error("註冊碼產生有問題\r\n" + ex); }
                #endregion
            }
            else
            {
                /*永久*/
                if (EndTime == "永久")
                {
                    LicenseBool = true;
                    TimeError = true;
                }
                else
                {
                    /*時間大於最後關閉系統時間*/
                    if (DateTime.Now > Convert.ToDateTime(FromTime))
                    {
                        LicenseBool = true;
                        TimeError = true;
                    }
                    /*時間大於最後關閉系統時間-直接關閉系統*/
                    else
                    {
                        TimeError = false;
                        //Form1.Close();
                    }
                }
            }
        }
        #endregion 

        #region 讀取是否有授權檔
        /// <summary>
        /// 讀取是否有授權檔
        /// </summary>
        /// <returns></returns>
        public bool LicenseRead()
        {
             setting = InitialMethod.TokenLoad();
            if (setting != null)
            {
                DeviceID = setting.DeviceID;
                LicenseCode = setting.LicenseCode;
                SystemNumber = setting.SystemNumber;
                StartTime = setting.StartTime;
                FromTime = setting.FromTime;
                EndTime = setting.EndTime;
                LicenseBool = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 啟動註冊
        /// <summary>
        /// 啟動註冊
        /// </summary>
        /// <param name="Token">權杖</param>
        /// <returns></returns>
        public bool Checked_Token(string Token)
        {
            string checkvalue = null;
            string DayTimeRandom = null;
            string DayTime = null;
            if (Token != "")
            {
                string value = Token.Replace("-", "");
                checkvalue = value.Substring(0, 3) + value.Substring(4, 3) + value.Substring(8, 3) + value.Substring(12, 3);
                DayTimeRandom = value.Substring(3, 1) + value.Substring(7, 1) + value.Substring(11, 1) + value.Substring(15, 1);
                if (checkvalue == FinalCheckValue)
                {
                    string Random = "FUCKYOMNSTQALXZVBDH";
                    if (DayTimeRandom == "9999")
                    {
                        EndTime = "永久";
                    }
                    else
                    {
                        for (int i = 0; i < DayTimeRandom.Length; i++)
                        {
                            DayTime += Random.IndexOf(DayTimeRandom.Substring(i, 1)).ToString();
                        }
                        EndTime = DateTime.Now.AddDays(Convert.ToDouble(DayTime)).ToString();
                    }
                    StartTime = DateTime.Now.ToString();
                    LicenseCode = Token;
                    setting = new TokenSetting()
                    {
                        DeviceID = DeviceID,
                        LicenseCode = LicenseCode,
                        SystemNumber = SystemNumber,
                        StartTime = StartTime,
                        EndTime = EndTime
                    };
                    InitialMethod.Save_Token(setting);
                    LicenseBool = true;
                    return true;
                }
                else
                {
                    action.Caption = "系統訊息";
                    action.Description = "註冊碼錯誤請與軟體工程師確認";
                    action.Commands.Add(FlyoutCommand.OK);
                    //if (FlyoutDialog.Show(Form1, action) == DialogResult.OK)
                    //{
                    //    return false;
                    //}
                    //else
                    //{
                    return false;
                    //}
                }
            }
            else
            {
                action.Caption = "系統訊息";
                action.Description = "註冊碼不可空白";
                action.Commands.Add(FlyoutCommand.OK);
                //if (FlyoutDialog.Show(Form1, action) == DialogResult.OK)
                //{
                //    return false;
                //}
                //else
                //{
                    return false;
                //}
                
            }
        }
        #endregion

        #region 視窗關閉使用
        /// <summary>
        /// 視窗關閉使用
        /// </summary>
        public void Close_System()
        {
            if (TimeError)
            {
                if (EndTime == "永久")
                {
                    InitialMethod.Save_Token(setting);
                }
                else
                {
                    InitialMethod.Save_Token(setting);
                }
            }
        }
        #endregion

        #region 授權時間比較
        /// <summary>
        /// 授權時間比較
        /// </summary>
        public void Timer_Token()
        {
            if (LicenseBool)
            {
                string regedit = $"C:\\ProgramData\\GIAMultimedia.dll";
                if (EndTime != "永久")
                {
                    if (DateTime.Now > Convert.ToDateTime(EndTime))
                    {
                        TimeError = false;
                        action.Caption = "系統訊息";
                        action.Description = "超過授權時間時間";
                        action.Commands.Add(FlyoutCommand.OK);
                        //if (FlyoutDialog.Show(Form1,action) == DialogResult.OK)
                        //{
                        //    Thread.Sleep(80);
                        //    File.Delete(regedit);
                        //    Form1.Close();
                        //}
                    }
                    else if (Convert.ToDateTime(FromTime) > DateTime.Now)
                    {
                        TimeError = false;
                        action.Caption = "系統訊息";
                        action.Description = "系統時間異常無法驗證";
                        action.Commands.Add(FlyoutCommand.OK);
                        //if (FlyoutDialog.Show(Form1, action) == DialogResult.OK)
                        //{
                        //    Thread.Sleep(80);
                        //    Form1.Close();
                        //}
                    }
                }
            }
            else
            {
                TimeSpan timeSpan = NTimeEnd.Subtract(DateTime.Now);
                if (NTimeStart < DateTime.Now)
                {
                    if (timeSpan.TotalSeconds < 0)
                    {
                        TimeError = false;
                        action.Caption = "系統訊息";
                        action.Description = "超過試用時間";
                        action.Commands.Add(FlyoutCommand.OK);
                        //if (FlyoutDialog.Show(Form1, action) == DialogResult.OK)
                        //{
                        //    Thread.Sleep(80);
                        //    Form1.Close();
                        //}
                    }
                }
                else
                {
                    TimeError = false;
                    action.Caption = "系統訊息";
                    action.Description = "系統時間異常無法驗證";
                    action.Commands.Add(FlyoutCommand.OK);
                    //if (FlyoutDialog.Show(Form1, action) == DialogResult.OK)
                    //{
                    //    Thread.Sleep(80);
                    //    Form1.Close();
                    //}
                }
            }
        }
        #endregion

        #region 字串增加 "-"
        /// <summary>
        /// 字串增加 "-"
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string strAdd(string str)
        {
            string stradd = null;
            if (str.Length < 12)
            {
                stradd = str.Substring(0, 4) + "-" + str.Substring(4, 4) + "-" + str.Substring(8, 4) + "-" + str.Substring(12, 4);
            }
            else
            {
                stradd = str.Substring(0, 4) + "-" + str.Substring(4, 4) + "-" + str.Substring(8, 4);
            }
            return stradd;
        }
        #endregion
    }
}
