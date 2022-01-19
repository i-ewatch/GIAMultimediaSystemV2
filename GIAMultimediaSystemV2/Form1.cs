using DevExpress.Utils;
using GIAMultimediaSystemV2.Components;
using GIAMultimediaSystemV2.Configuration;
using GIAMultimediaSystemV2.Enums;
using GIAMultimediaSystemV2.Methods;
using GIAMultimediaSystemV2.Protocols;
using GIAMultimediaSystemV2.Protocols.Senser;
using GIAMultimediaSystemV2.Views;
using GIAMultimediaSystemV2.Views.ElectricViews;
using GIAMultimediaSystemV2.Views.GIAViews;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GIAMultimediaSystemV2
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        #region 泡泡視窗
        /// <summary>
        /// 設定泡泡視窗
        /// </summary>
        public FlyoutPanel SettingflyoutPanel;
        /// <summary>
        /// 錯誤泡泡視窗
        /// </summary>
        public FlyoutPanel ErrorflyoutPanel;
        #endregion
        #region JSON資訊
        /// <summary>
        /// 群組資訊
        /// </summary>
        public GroupSetting GroupSetting { get; set; }
        /// <summary>
        /// 設備通訊設定
        /// </summary>
        public GateWaySetting GateWaySetting { get; set; }
        /// <summary>
        /// 跑馬燈設定
        /// </summary>
        public MarqueeSetting MarqueeSetting { get; set; }
        /// <summary>
        /// 台灣縣市區設定
        /// </summary>
        public List<Taiwan_DistricsSetting> Taiwan_DistricsSetting { get; set; } = new List<Taiwan_DistricsSetting>();
        /// <summary>
        /// 資料庫連接設定
        /// </summary>
        public SqlDBSetting SqlDBSetting { get; set; }
        /// <summary>
        /// 上傳設定
        /// </summary>
        public UploadSetting UploadSetting { get; set; }
        /// <summary>
        /// 畫面資訊
        /// </summary>
        public ScreenMediaSetting ScreenMediaSetting { get; set; }
        #endregion
        #region 方法
        /// <summary>
        /// 資料庫方法
        /// </summary>
        private SqlMethod SqlMethod { get; set; }
        /// <summary>
        /// 上傳方法
        /// </summary>
        private UploadMethod UploadMethod { get; set; }
        #endregion
        #region 通訊
        /// <summary>
        /// 通訊類型
        /// </summary>
        private GatewayEnumType GatewayEnumType { get; set; }
        /// <summary>
        /// 總通訊數值
        /// </summary>
        private List<AbsProtocol> AbsProtocols { get; set; } = new List<AbsProtocol>();
        /// <summary>
        /// 總通訊物件
        /// </summary>
        private List<Field4Component> Field4Components { get; set; } = new List<Field4Component>();

        /// <summary>
        /// 紀錄物件
        /// </summary>
        private List<Field4Component> RecordComponents { get; set; } = new List<Field4Component>();
        #endregion
        #region 畫面
        /// <summary>
        /// 畫面切換鎖定 True = 不鎖定 ,False = 鎖定
        /// </summary>
        public bool LockFlag { get; set; } = true;
        /// <summary>
        /// 跑馬燈
        /// </summary>
        public MarqueeUserControl MarqueeUserControl { get; set; }
        /// <summary>
        /// GIA感測器畫面
        /// </summary>
        public GIAScreenUserControl GIAScreenUserControl { get; set; }
        /// <summary>
        /// 圖表畫面
        /// </summary>
        public ChartUserControl ChartUserControl { get; set; }
        /// <summary>
        /// 其他畫面
        /// </summary>
        public OtherUserControl OtherUserControl { get; set; }
        /// <summary>
        /// 設定泡泡視窗
        /// </summary>
        //public SettingButtonUserControl SettingButtonUserControl { get; set; }
        #endregion
        public Form1()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File($"{AppDomain.CurrentDomain.BaseDirectory}\\log\\log-.txt",
                rollingInterval: RollingInterval.Day,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();        //宣告Serilog初始化

            GateWaySetting = InitialMethod.GateWayLoad();
            MarqueeSetting = InitialMethod.MarqueeLoad();
            Taiwan_DistricsSetting = InitialMethod.Taiwan_DistricsLoad();
            SqlDBSetting = InitialMethod.SqlDBLoad();
            UploadSetting = InitialMethod.UploadLoad();
            ScreenMediaSetting = InitialMethod.ScreenMediaLoad();
            GroupSetting = InitialMethod.GroupLoad();

            if (GateWaySetting.ControlFlag)//使用通訊
            {
                foreach (var item in GateWaySetting.GateWays)
                {
                    GatewayEnumType = (GatewayEnumType)item.GatewayEnumType;
                    switch (GatewayEnumType)
                    {
                        case GatewayEnumType.ModbusRTU:
                            {
                                SerialportComponent component = new SerialportComponent(GateWaySetting, item, SqlMethod);
                                component.MyWorkState = GateWaySetting.ControlFlag;
                                Field4Components.Add(component);
                                AbsProtocols.AddRange(component.AbsProtocols);
                            }
                            break;
                        case GatewayEnumType.ModbusTCP:
                            {
                                TCPComponent component = new TCPComponent(GateWaySetting, item, SqlMethod);
                                component.MyWorkState = GateWaySetting.ControlFlag;
                                Field4Components.Add(component);
                                AbsProtocols.AddRange(component.AbsProtocols);
                            }
                            break;
                        case GatewayEnumType.API:
                            break;
                        case GatewayEnumType.EMS:
                            break;
                    }
                }
            }

            if (GateWaySetting.RecordFlag & GateWaySetting.ControlFlag)//使用紀錄
            {
                SqlMethod = new SqlMethod() { setting = SqlDBSetting };
                try
                {
                    SqlMethod.SQLConnect();
                    SqlMethod.Insert_ElectricConfig(GateWaySetting.GateWays);//電表基本資訊
                    SqlMethod.Insert_SenserConfig(GateWaySetting.GateWays);//感測器基本資訊
                    if (SqlMethod.Check_Datebase())
                    {
                        SqlComponent component = new SqlComponent(AbsProtocols) { SqlMethod = SqlMethod };
                        component.MyWorkState = GateWaySetting.RecordFlag;
                        RecordComponents.Add(component);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "沒有安裝資料庫");
                }
            }
            if (GateWaySetting.UploadFlag & GateWaySetting.ControlFlag)//使用上傳
            {
                UploadMethod = new UploadMethod() { UploadSetting = UploadSetting };
                //UploadComponent component = new UploadComponent() { UploadMethod = UploadMethod };
                //component.MyWorkState = GateWaySetting.UploadFlag;
                //RecordComponents.Add(component);
            }
            InitializeComponent();
            #region 畫面      
            MarqueeUserControl = new MarqueeUserControl(MarqueeSetting, ScreenMediaSetting) { Parent = MarqueepanelControl, MarqueeSetting = MarqueeSetting };//跑馬燈
            foreach (var Gateitem in GateWaySetting.GateWays)
            {
                foreach (var item in Gateitem.GateWaySenserIDs)
                {
                    SenserEnumType senserEnumType = (SenserEnumType)item.SenserEnumType;
                    switch (senserEnumType)
                    {
                        case SenserEnumType.BlackSenser:
                        case SenserEnumType.WhiteSenser:
                        case SenserEnumType.WeatherAPI:
                            break;
                        case SenserEnumType.GIAAPI:
                        case SenserEnumType.GIA:
                            {
                                GIAScreenUserControl = new GIAScreenUserControl(Gateitem, Taiwan_DistricsSetting, ScreenMediaSetting, AbsProtocols) { Dock = DockStyle.Fill, Parent = SenserpanelControl };
                            }
                            break;
                    }
                }
            }
            ChartUserControl = new ChartUserControl(GroupSetting, GateWaySetting, SqlMethod) { Dock = DockStyle.Fill, Parent = ChartpanelControl };
            OtherUserControl = new OtherUserControl(GateWaySetting, SqlDBSetting, GroupSetting, SqlMethod) { Dock = DockStyle.Fill, Parent = OtherpanelControl };
            GIAScreenUserControl.TextChange();
            #endregion
            timer1.Interval = 1000;
            timer1.Enabled = true;
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            GIAScreenUserControl.TextChange();
            ChartUserControl.TextChange();
            OtherUserControl.TextChange();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Location = new Point(0, 0);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var item in Field4Components)
            {
                item.MyWorkState = false;
            }
            foreach (var item in RecordComponents)
            {
                item.MyWorkState = false;
            }
            timer1.Enabled = false;
            MarqueeUserControl.timer1.Enabled = false;
        }
    }
}
