using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using GIAMultimediaSystemV2.Methods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GIAMultimediaSystemV2.Views.Setting
{
    public partial class ScreenSettingUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 設定按鈕視窗
        /// </summary>
        private SettingButtonUserControl SettingButtonUserControl { get; set; }
        /// <summary>
        /// Loading物件繼承
        /// </summary>
        private IOverlaySplashScreenHandle handle = null;
        /// <summary>
        /// 開啟檔案瀏覽
        /// </summary>
        private OpenFileDialog openFileDialog = new OpenFileDialog();
        /// <summary>
        /// 影片路徑物件
        /// </summary>
        private FolderBrowserDialog VedioFileDialog = new FolderBrowserDialog();
        /// <summary>
        /// 影片路徑物件
        /// </summary>
        private FolderBrowserDialog PictureFileDialog = new FolderBrowserDialog();
        /// 關閉Loading視窗
        /// </summary>
        /// <param name="handle"></param>
        private void CloseProgressPanel(IOverlaySplashScreenHandle handle)
        {
            if (handle != null)
                SplashScreenManager.CloseOverlayForm(handle);
        }
        public ScreenSettingUserControl(SettingButtonUserControl settingButtonUserControl)
        {
            InitializeComponent();
            SettingButtonUserControl = settingButtonUserControl;
            #region 畫面設定
            if (settingButtonUserControl.SenserForm != null)
            {
                if (settingButtonUserControl.SenserForm.ScreenMediaSetting.LogoPath != null)
                {
                    LogolabelControl.Text = Path.GetFileName(settingButtonUserControl.SenserForm.ScreenMediaSetting.LogoPath);
                }
                if (settingButtonUserControl.SenserForm.MediaPlaySetting != null)
                {
                    VediolabelControl.Text = $"{settingButtonUserControl.SenserForm.MediaPlaySetting.VideoPath}";
                    PicturelabelControl.Text = $"{settingButtonUserControl.SenserForm.MediaPlaySetting.PicturePath}";
                }
                SectextEdit.Text = settingButtonUserControl.SenserForm.ScreenMediaSetting.ChangePageSec.ToString();
                SenserItem(comboBoxEdit1);
                SenserItem(comboBoxEdit2);
                SenserItem(comboBoxEdit3);
                SenserItem(comboBoxEdit4);
                SenserItem(comboBoxEdit5);
                SenserItem(comboBoxEdit6);
                SenserItem(comboBoxEdit7);
                SenserItem(comboBoxEdit8);
                SenserItem(comboBoxEdit9);
                SenserItem(comboBoxEdit10);
                SenserItem(comboBoxEdit11);
                SenserItem(comboBoxEdit12);
                SenserItem(comboBoxEdit13);
                SenserItem(comboBoxEdit14);
                toggleSwitch1.IsOn = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[0].VisibleFlag1;
                toggleSwitch2.IsOn = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[0].VisibleFlag2;
                toggleSwitch3.IsOn = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[1].VisibleFlag1;
                toggleSwitch4.IsOn = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[1].VisibleFlag2;
                toggleSwitch5.IsOn = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[3].VisibleFlag1;
                toggleSwitch6.IsOn = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[3].VisibleFlag2;
                toggleSwitch7.IsOn = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[5].VisibleFlag1;
                toggleSwitch8.IsOn = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[5].VisibleFlag2;
                toggleSwitch9.IsOn = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[2].VisibleFlag1;
                toggleSwitch10.IsOn = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[2].VisibleFlag2;
                toggleSwitch11.IsOn = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[4].VisibleFlag1;
                toggleSwitch12.IsOn = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[4].VisibleFlag2;
                toggleSwitch13.IsOn = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[6].VisibleFlag1;
                toggleSwitch14.IsOn = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[6].VisibleFlag2;
                comboBoxEdit1.SelectedIndex = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[0].SenserTypeEnum1;
                comboBoxEdit2.SelectedIndex = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[0].SenserTypeEnum2;
                comboBoxEdit3.SelectedIndex = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[1].SenserTypeEnum1;
                comboBoxEdit4.SelectedIndex = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[1].SenserTypeEnum2;
                comboBoxEdit5.SelectedIndex = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[3].SenserTypeEnum1;
                comboBoxEdit6.SelectedIndex = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[3].SenserTypeEnum2;
                comboBoxEdit7.SelectedIndex = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[5].SenserTypeEnum1;
                comboBoxEdit8.SelectedIndex = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[5].SenserTypeEnum2;
                comboBoxEdit9.SelectedIndex = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[2].SenserTypeEnum1;
                comboBoxEdit10.SelectedIndex = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[2].SenserTypeEnum2;
                comboBoxEdit11.SelectedIndex = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[4].SenserTypeEnum1;
                comboBoxEdit12.SelectedIndex = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[4].SenserTypeEnum2;
                comboBoxEdit13.SelectedIndex = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[6].SenserTypeEnum1;
                comboBoxEdit14.SelectedIndex = settingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[6].SenserTypeEnum2;
            }
            else if (settingButtonUserControl.ElectricForm != null)
            {
                //groupControl1.Text = "圖片設定";
                //VediosimpleButton.Text = "更改圖片路徑";
                if (settingButtonUserControl.ElectricForm.ScreenMediaSetting.LogoPath != null)
                {
                    LogolabelControl.Text = Path.GetFileName(settingButtonUserControl.ElectricForm.ScreenMediaSetting.LogoPath);
                }
                if (settingButtonUserControl.ElectricForm.MediaPlaySetting != null)
                {
                    VediolabelControl.Text = $"{settingButtonUserControl.ElectricForm.MediaPlaySetting.VideoPath}";
                    PicturelabelControl.Text = $"{settingButtonUserControl.ElectricForm.MediaPlaySetting.PicturePath}";
                }
                SectextEdit.Text = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ChangePageSec.ToString();
                SenserItem(comboBoxEdit1);
                SenserItem(comboBoxEdit2);
                SenserItem(comboBoxEdit3);
                SenserItem(comboBoxEdit4);
                SenserItem(comboBoxEdit5);
                SenserItem(comboBoxEdit6);
                SenserItem(comboBoxEdit7);
                SenserItem(comboBoxEdit8);
                SenserItem(comboBoxEdit9);
                SenserItem(comboBoxEdit10);
                SenserItem(comboBoxEdit11);
                SenserItem(comboBoxEdit12);
                SenserItem(comboBoxEdit13);
                SenserItem(comboBoxEdit14);
                toggleSwitch1.IsOn = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[0].VisibleFlag1;
                toggleSwitch2.IsOn = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[0].VisibleFlag2;
                toggleSwitch3.IsOn = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[1].VisibleFlag1;
                toggleSwitch4.IsOn = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[1].VisibleFlag2;
                toggleSwitch5.IsOn = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[3].VisibleFlag1;
                toggleSwitch6.IsOn = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[3].VisibleFlag2;
                toggleSwitch7.IsOn = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[5].VisibleFlag1;
                toggleSwitch8.IsOn = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[5].VisibleFlag2;
                toggleSwitch9.IsOn = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[2].VisibleFlag1;
                toggleSwitch10.IsOn = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[2].VisibleFlag2;
                toggleSwitch11.IsOn = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[4].VisibleFlag1;
                toggleSwitch12.IsOn = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[4].VisibleFlag2;
                toggleSwitch13.IsOn = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[6].VisibleFlag1;
                toggleSwitch14.IsOn = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[6].VisibleFlag2;
                comboBoxEdit1.SelectedIndex = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[0].SenserTypeEnum1;
                comboBoxEdit2.SelectedIndex = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[0].SenserTypeEnum2;
                comboBoxEdit3.SelectedIndex = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[1].SenserTypeEnum1;
                comboBoxEdit4.SelectedIndex = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[1].SenserTypeEnum2;
                comboBoxEdit5.SelectedIndex = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[3].SenserTypeEnum1;
                comboBoxEdit6.SelectedIndex = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[3].SenserTypeEnum2;
                comboBoxEdit7.SelectedIndex = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[5].SenserTypeEnum1;
                comboBoxEdit8.SelectedIndex = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[5].SenserTypeEnum2;
                comboBoxEdit9.SelectedIndex = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[2].SenserTypeEnum1;
                comboBoxEdit10.SelectedIndex = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[2].SenserTypeEnum2;
                comboBoxEdit11.SelectedIndex = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[4].SenserTypeEnum1;
                comboBoxEdit12.SelectedIndex = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[4].SenserTypeEnum2;
                comboBoxEdit13.SelectedIndex = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[6].SenserTypeEnum1;
                comboBoxEdit14.SelectedIndex = settingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[6].SenserTypeEnum2;
            }
            else if (settingButtonUserControl.StraightSenserForm != null)
            {
                if (settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.LogoPath != null)
                {
                    LogolabelControl.Text = Path.GetFileName(settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.LogoPath);
                }
                if (settingButtonUserControl.StraightSenserForm.MediaPlaySetting != null)
                {
                    VediolabelControl.Text = $"{settingButtonUserControl.StraightSenserForm.MediaPlaySetting.VideoPath}";
                    PicturelabelControl.Text = $"{settingButtonUserControl.StraightSenserForm.MediaPlaySetting.PicturePath}";
                }
                SectextEdit.Text = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ChangePageSec.ToString();
                SenserItem(comboBoxEdit1);
                SenserItem(comboBoxEdit2);
                SenserItem(comboBoxEdit3);
                SenserItem(comboBoxEdit4);
                SenserItem(comboBoxEdit5);
                SenserItem(comboBoxEdit6);
                SenserItem(comboBoxEdit7);
                SenserItem(comboBoxEdit8);
                SenserItem(comboBoxEdit9);
                SenserItem(comboBoxEdit10);
                SenserItem(comboBoxEdit11);
                SenserItem(comboBoxEdit12);
                SenserItem(comboBoxEdit13);
                SenserItem(comboBoxEdit14);
                toggleSwitch1.IsOn = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[0].VisibleFlag1;
                toggleSwitch2.IsOn = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[0].VisibleFlag2;
                toggleSwitch3.IsOn = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[1].VisibleFlag1;
                toggleSwitch4.IsOn = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[1].VisibleFlag2;
                toggleSwitch5.IsOn = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[3].VisibleFlag1;
                toggleSwitch6.IsOn = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[3].VisibleFlag2;
                toggleSwitch7.IsOn = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[5].VisibleFlag1;
                toggleSwitch8.IsOn = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[5].VisibleFlag2;
                toggleSwitch9.IsOn = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[2].VisibleFlag1;
                toggleSwitch10.IsOn = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[2].VisibleFlag2;
                toggleSwitch11.IsOn = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[4].VisibleFlag1;
                toggleSwitch12.IsOn = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[4].VisibleFlag2;
                toggleSwitch13.IsOn = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[6].VisibleFlag1;
                toggleSwitch14.IsOn = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[6].VisibleFlag2;
                comboBoxEdit1.SelectedIndex = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[0].SenserTypeEnum1;
                comboBoxEdit2.SelectedIndex = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[0].SenserTypeEnum2;
                comboBoxEdit3.SelectedIndex = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[1].SenserTypeEnum1;
                comboBoxEdit4.SelectedIndex = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[1].SenserTypeEnum2;
                comboBoxEdit5.SelectedIndex = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[3].SenserTypeEnum1;
                comboBoxEdit6.SelectedIndex = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[3].SenserTypeEnum2;
                comboBoxEdit7.SelectedIndex = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[5].SenserTypeEnum1;
                comboBoxEdit8.SelectedIndex = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[5].SenserTypeEnum2;
                comboBoxEdit9.SelectedIndex = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[2].SenserTypeEnum1;
                comboBoxEdit10.SelectedIndex = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[2].SenserTypeEnum2;
                comboBoxEdit11.SelectedIndex = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[4].SenserTypeEnum1;
                comboBoxEdit12.SelectedIndex = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[4].SenserTypeEnum2;
                comboBoxEdit13.SelectedIndex = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[6].SenserTypeEnum1;
                comboBoxEdit14.SelectedIndex = settingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[6].SenserTypeEnum2;
            }
            #endregion
        }
        #region 新增項目
        /// <summary>
        /// 感測器項目
        /// </summary>
        /// <param name="comboBox"></param>
        private void SenserItem(ComboBoxEdit comboBox)
        {
            comboBox.Properties.Items.Add("IAQ 室內指數");
            comboBox.Properties.Items.Add("PM2.5 細懸浮微粒");
            comboBox.Properties.Items.Add("PM10 懸浮微粒");
            comboBox.Properties.Items.Add("CO" + "\xb2" + " 二氧化碳");
            comboBox.Properties.Items.Add("TVOC 揮發性有機物");
            comboBox.Properties.Items.Add("HUMI 濕度");
            comboBox.Properties.Items.Add("TEMP 溫度");
            comboBox.Properties.Items.Add("HCHO 甲醛");
            comboBox.Properties.Items.Add("O" + "\xb3" + "臭氧");
            comboBox.Properties.Items.Add("CO 一氧化碳");
            comboBox.Properties.Items.Add("Mold 黴菌");
            comboBox.Properties.Items.Add("PM1 超細懸浮微粒");
            comboBox.Properties.Items.Add("PH 氫離子");
            comboBox.Properties.Items.Add("CL 氯氣");
            comboBox.Properties.Items.Add("TEMP 感測器溫度");
        }
        /// <summary>
        /// 天氣項目
        /// </summary>
        /// <param name="comboBox"></param>
        private void WeatherItem(ComboBoxEdit comboBox)
        {
            comboBox.Properties.Items.Add("基隆市");
            comboBox.Properties.Items.Add("臺北市");
            comboBox.Properties.Items.Add("新北市");
            comboBox.Properties.Items.Add("桃園市");
            comboBox.Properties.Items.Add("新竹市");
            comboBox.Properties.Items.Add("新竹縣");
            comboBox.Properties.Items.Add("苗栗縣");
            comboBox.Properties.Items.Add("臺中市");
            comboBox.Properties.Items.Add("彰化縣");
            comboBox.Properties.Items.Add("南投縣");
            comboBox.Properties.Items.Add("雲林縣");
            comboBox.Properties.Items.Add("嘉義市");
            comboBox.Properties.Items.Add("嘉義縣");
            comboBox.Properties.Items.Add("臺南市");
            comboBox.Properties.Items.Add("高雄市");
            comboBox.Properties.Items.Add("屏東縣");
            comboBox.Properties.Items.Add("臺東縣");
            comboBox.Properties.Items.Add("花蓮縣");
            comboBox.Properties.Items.Add("宜蘭縣");
            comboBox.Properties.Items.Add("澎湖縣");
            comboBox.Properties.Items.Add("金門縣");
            comboBox.Properties.Items.Add("連江縣");
        }
        #endregion

        #region 背景圖片變更
        /// <summary>
        /// 背景圖片變更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogosimpleButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog.OpenFile() != null)
                {
                    LogolabelControl.Text = Path.GetFileName(openFileDialog.FileName);
                }
            }
        }
        #endregion
        #region 影片路徑變更
        /// <summary>
        /// 影片路徑變更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VediosimpleButton_Click(object sender, EventArgs e)
        {
            if (VedioFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (VedioFileDialog.Description != null)
                {
                    VediolabelControl.Text = Path.GetFullPath(VedioFileDialog.SelectedPath);
                }
            }
        }
        #endregion
        #region 圖片路徑變更
        /// <summary>
        /// 圖片路徑變更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicturesimpleButton_Click(object sender, EventArgs e)
        {
            if (PictureFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (PictureFileDialog.Description != null)
                {
                    PicturelabelControl.Text = Path.GetFullPath(PictureFileDialog.SelectedPath);
                }
            }
        }
        #endregion
        #region 取消按鈕
        /// <summary>
        /// 取消按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelsimpleButton_Click(object sender, EventArgs e)
        {
            if (SettingButtonUserControl.SenserForm != null)
            {
                SettingButtonUserControl.SenserForm.GIAScreenUserControl1.LockFlag = SettingButtonUserControl.AfterLockFlag;
            }
            else if (SettingButtonUserControl.ElectricForm != null)
            {
                SettingButtonUserControl.ElectricForm.GIAScreenUserControl1.LockFlag = SettingButtonUserControl.AfterLockFlag;
            }
            else if (SettingButtonUserControl.StraightSenserForm != null)
            {
                SettingButtonUserControl.StraightSenserForm.GIAScreenUserControl.LockFlag = SettingButtonUserControl.AfterLockFlag;
            }
            SettingButtonUserControl.FlyoutFlag = false;
            SettingButtonUserControl.flyout.Close();
        }
        #endregion
        #region 確定按鈕
        /// <summary>
        /// 確定按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKsimpleButton_Click(object sender, EventArgs e)
        {
            handle = SplashScreenManager.ShowOverlayForm(FindForm());
            if (SettingButtonUserControl.SenserForm != null)
            {
                #region 畫面
                if (openFileDialog.FileName != "")
                {
                    SettingButtonUserControl.SenserForm.ScreenMediaSetting.LogoPath = Path.GetFullPath(openFileDialog.FileName);
                }
                if (VedioFileDialog.SelectedPath != "")
                {
                    SettingButtonUserControl.SenserForm.MediaPlaySetting.VideoPath = VedioFileDialog.SelectedPath;
                }
                if (PictureFileDialog.SelectedPath != "")
                {
                    SettingButtonUserControl.SenserForm.MediaPlaySetting.PicturePath = PictureFileDialog.SelectedPath;
                }
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ChangePageSec = Convert.ToInt32(SectextEdit.Text);
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[0].VisibleFlag1 = toggleSwitch1.IsOn;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[0].VisibleFlag2 = toggleSwitch2.IsOn;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[1].VisibleFlag1 = toggleSwitch3.IsOn;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[1].VisibleFlag2 = toggleSwitch4.IsOn;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[3].VisibleFlag1 = toggleSwitch5.IsOn;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[3].VisibleFlag2 = toggleSwitch6.IsOn;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[5].VisibleFlag1 = toggleSwitch7.IsOn;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[5].VisibleFlag2 = toggleSwitch8.IsOn;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[2].VisibleFlag1 = toggleSwitch9.IsOn;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[2].VisibleFlag2 = toggleSwitch10.IsOn;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[4].VisibleFlag1 = toggleSwitch11.IsOn;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[4].VisibleFlag2 = toggleSwitch12.IsOn;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[6].VisibleFlag1 = toggleSwitch13.IsOn;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[6].VisibleFlag2 = toggleSwitch14.IsOn;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[0].SenserTypeEnum1 = comboBoxEdit1.SelectedIndex;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[0].SenserTypeEnum2 = comboBoxEdit2.SelectedIndex;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[1].SenserTypeEnum1 = comboBoxEdit3.SelectedIndex;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[1].SenserTypeEnum2 = comboBoxEdit4.SelectedIndex;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[3].SenserTypeEnum1 = comboBoxEdit5.SelectedIndex;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[3].SenserTypeEnum2 = comboBoxEdit6.SelectedIndex;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[5].SenserTypeEnum1 = comboBoxEdit7.SelectedIndex;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[5].SenserTypeEnum2 = comboBoxEdit8.SelectedIndex;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[2].SenserTypeEnum1 = comboBoxEdit9.SelectedIndex;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[2].SenserTypeEnum2 = comboBoxEdit10.SelectedIndex;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[4].SenserTypeEnum1 = comboBoxEdit11.SelectedIndex;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[4].SenserTypeEnum2 = comboBoxEdit12.SelectedIndex;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[6].SenserTypeEnum1 = comboBoxEdit13.SelectedIndex;
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.ScreenSwitches[6].SenserTypeEnum2 = comboBoxEdit14.SelectedIndex;
                #endregion
                SettingButtonUserControl.SenserForm.GIAScreenUserControl1.Change_ScreenMedia(SettingButtonUserControl.SenserForm.ScreenMediaSetting);
                InitialMethod.Save_ScreenMedia(SettingButtonUserControl.SenserForm.ScreenMediaSetting);
                InitialMethod.Save_MediaPlay(SettingButtonUserControl.SenserForm.MediaPlaySetting);
                SettingButtonUserControl.SenserForm.Change_BackgroundImage();
                SettingButtonUserControl.SenserForm.GIAScreenUserControl1.LockFlag = SettingButtonUserControl.AfterLockFlag;
            }
            else if (SettingButtonUserControl.ElectricForm != null)
            {
                #region 畫面
                if (openFileDialog.FileName != "")
                {
                    SettingButtonUserControl.ElectricForm.ScreenMediaSetting.LogoPath = Path.GetFullPath(openFileDialog.FileName);
                }
                if (VedioFileDialog.SelectedPath != "")
                {
                    SettingButtonUserControl.ElectricForm.MediaPlaySetting.VideoPath = VedioFileDialog.SelectedPath;
                }
                if (PictureFileDialog.SelectedPath != "")
                {
                    SettingButtonUserControl.ElectricForm.MediaPlaySetting.PicturePath = PictureFileDialog.SelectedPath;
                }
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ChangePageSec = Convert.ToInt32(SectextEdit.Text);
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[0].VisibleFlag1 = toggleSwitch1.IsOn;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[0].VisibleFlag2 = toggleSwitch2.IsOn;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[1].VisibleFlag1 = toggleSwitch3.IsOn;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[1].VisibleFlag2 = toggleSwitch4.IsOn;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[3].VisibleFlag1 = toggleSwitch5.IsOn;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[3].VisibleFlag2 = toggleSwitch6.IsOn;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[5].VisibleFlag1 = toggleSwitch7.IsOn;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[5].VisibleFlag2 = toggleSwitch8.IsOn;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[2].VisibleFlag1 = toggleSwitch9.IsOn;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[2].VisibleFlag2 = toggleSwitch10.IsOn;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[4].VisibleFlag1 = toggleSwitch11.IsOn;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[4].VisibleFlag2 = toggleSwitch12.IsOn;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[6].VisibleFlag1 = toggleSwitch13.IsOn;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[6].VisibleFlag2 = toggleSwitch14.IsOn;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[0].SenserTypeEnum1 = comboBoxEdit1.SelectedIndex;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[0].SenserTypeEnum2 = comboBoxEdit2.SelectedIndex;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[1].SenserTypeEnum1 = comboBoxEdit3.SelectedIndex;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[1].SenserTypeEnum2 = comboBoxEdit4.SelectedIndex;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[3].SenserTypeEnum1 = comboBoxEdit5.SelectedIndex;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[3].SenserTypeEnum2 = comboBoxEdit6.SelectedIndex;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[5].SenserTypeEnum1 = comboBoxEdit7.SelectedIndex;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[5].SenserTypeEnum2 = comboBoxEdit8.SelectedIndex;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[2].SenserTypeEnum1 = comboBoxEdit9.SelectedIndex;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[2].SenserTypeEnum2 = comboBoxEdit10.SelectedIndex;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[4].SenserTypeEnum1 = comboBoxEdit11.SelectedIndex;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[4].SenserTypeEnum2 = comboBoxEdit12.SelectedIndex;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[6].SenserTypeEnum1 = comboBoxEdit13.SelectedIndex;
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.ScreenSwitches[6].SenserTypeEnum2 = comboBoxEdit14.SelectedIndex;
                #endregion
                SettingButtonUserControl.ElectricForm.GIAScreenUserControl1.Change_ScreenMedia(SettingButtonUserControl.ElectricForm.ScreenMediaSetting);
                InitialMethod.Save_ScreenMedia(SettingButtonUserControl.ElectricForm.ScreenMediaSetting);
                InitialMethod.Save_MediaPlay(SettingButtonUserControl.ElectricForm.MediaPlaySetting);
                SettingButtonUserControl.ElectricForm.Change_BackgroundImage();
                SettingButtonUserControl.ElectricForm.GIAScreenUserControl1.LockFlag = SettingButtonUserControl.AfterLockFlag;
            }
            else if (SettingButtonUserControl.StraightSenserForm != null)
            {
                #region 畫面
                if (openFileDialog.FileName != "")
                {
                    SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.LogoPath = Path.GetFullPath(openFileDialog.FileName);
                }
                if (VedioFileDialog.SelectedPath != "")
                {
                    SettingButtonUserControl.StraightSenserForm.MediaPlaySetting.VideoPath = VedioFileDialog.SelectedPath;

                }
                if (PictureFileDialog.SelectedPath != "")
                {
                    SettingButtonUserControl.StraightSenserForm.MediaPlaySetting.PicturePath = PictureFileDialog.SelectedPath;
                }
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ChangePageSec = Convert.ToInt32(SectextEdit.Text);
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[0].VisibleFlag1 = toggleSwitch1.IsOn;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[0].VisibleFlag2 = toggleSwitch2.IsOn;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[1].VisibleFlag1 = toggleSwitch3.IsOn;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[1].VisibleFlag2 = toggleSwitch4.IsOn;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[3].VisibleFlag1 = toggleSwitch5.IsOn;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[3].VisibleFlag2 = toggleSwitch6.IsOn;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[5].VisibleFlag1 = toggleSwitch7.IsOn;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[5].VisibleFlag2 = toggleSwitch8.IsOn;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[2].VisibleFlag1 = toggleSwitch9.IsOn;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[2].VisibleFlag2 = toggleSwitch10.IsOn;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[4].VisibleFlag1 = toggleSwitch11.IsOn;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[4].VisibleFlag2 = toggleSwitch12.IsOn;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[6].VisibleFlag1 = toggleSwitch13.IsOn;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[6].VisibleFlag2 = toggleSwitch14.IsOn;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[0].SenserTypeEnum1 = comboBoxEdit1.SelectedIndex;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[0].SenserTypeEnum2 = comboBoxEdit2.SelectedIndex;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[1].SenserTypeEnum1 = comboBoxEdit3.SelectedIndex;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[1].SenserTypeEnum2 = comboBoxEdit4.SelectedIndex;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[3].SenserTypeEnum1 = comboBoxEdit5.SelectedIndex;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[3].SenserTypeEnum2 = comboBoxEdit6.SelectedIndex;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[5].SenserTypeEnum1 = comboBoxEdit7.SelectedIndex;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[5].SenserTypeEnum2 = comboBoxEdit8.SelectedIndex;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[2].SenserTypeEnum1 = comboBoxEdit9.SelectedIndex;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[2].SenserTypeEnum2 = comboBoxEdit10.SelectedIndex;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[4].SenserTypeEnum1 = comboBoxEdit11.SelectedIndex;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[4].SenserTypeEnum2 = comboBoxEdit12.SelectedIndex;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[6].SenserTypeEnum1 = comboBoxEdit13.SelectedIndex;
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.ScreenSwitches[6].SenserTypeEnum2 = comboBoxEdit14.SelectedIndex;
                #endregion
                SettingButtonUserControl.StraightSenserForm.GIAScreenUserControl.Change_ScreenMedia(SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting);
                InitialMethod.Save_ScreenMedia(SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting);
                InitialMethod.Save_MediaPlay(SettingButtonUserControl.StraightSenserForm.MediaPlaySetting);
                SettingButtonUserControl.StraightSenserForm.GIAScreenUserControl.LockFlag = SettingButtonUserControl.AfterLockFlag;
            }
            SettingButtonUserControl.FlyoutFlag = false;
            SettingButtonUserControl.flyout.Close();
            /*結束等待畫面*/
            CloseProgressPanel(handle);
        }
        #endregion
    }
}
