using DevExpress.XtraEditors;
using GIAMultimediaSystemV2.Components;
using GIAMultimediaSystemV2.Configuration;
using GIAMultimediaSystemV2.Enums;
using GIAMultimediaSystemV2.Methods;
using GIAMultimediaSystemV2.Protocols;
using GIAMultimediaSystemV2.Views;
using GIAMultimediaSystemV2.Views.GIAViews;
using GIAMultimediaSystemV2.Views.WeathcrViews;
using Serilog;
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

namespace GIAMultimediaSystemV2
{
    public partial class SenserForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 初始路徑
        /// </summary>
        public string MyWorkPath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
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
        /// <summary>
        /// 影片資訊
        /// </summary>
        public MediaPlaySetting MediaPlaySetting { get; set; }
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
        /// 跑馬燈
        /// </summary>
        public MarqueeUserControl MarqueeUserControl { get; set; }
        /// <summary>
        /// 影片
        /// </summary>
        public VideoUserControl VideoUserControl { get; set; }
        /// <summary>
        /// 天氣資訊
        /// </summary>
        public WeatherUserControl1 WeatherUserControl1 { get; set; }
        /// <summary>
        /// 感測器畫面
        /// </summary>
        public GIAScreenUserControl1 GIAScreenUserControl1 { get; set; }
        #endregion
        public SenserForm()
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
            MediaPlaySetting = InitialMethod.MediaPlayLoad();

            if (GateWaySetting.ControlFlag)//使用通訊
            {
                foreach (var Gateitem in GateWaySetting.GateWays)
                {
                    GatewayEnumType = (GatewayEnumType)Gateitem.GatewayEnumType;
                    switch (GatewayEnumType)
                    {
                        case GatewayEnumType.ModbusRTU:
                            {
                                SerialportComponent component = new SerialportComponent(GateWaySetting, Gateitem, SqlMethod);
                                component.MyWorkState = GateWaySetting.ControlFlag;
                                Field4Components.Add(component);
                                AbsProtocols.AddRange(component.AbsProtocols);
                            }
                            break;
                        case GatewayEnumType.ModbusTCP:
                            {
                                TCPComponent component = new TCPComponent(GateWaySetting, Gateitem, SqlMethod);
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
            InitializeComponent();
            if (File.Exists($"{MyWorkPath}\\Images\\欣寶-空氣品質看板UI底圖1.png"))
            {
                pictureEdit1.Image = Image.FromFile($"{MyWorkPath}\\Images\\欣寶-空氣品質看板UI底圖1.png");
            }
            MarqueeUserControl = new MarqueeUserControl(MarqueeSetting) { Dock = DockStyle.Fill, Parent = MarqueepanelControl };
            VideoUserControl = new VideoUserControl(MediaPlaySetting) { Dock = DockStyle.Fill, Parent = VediopanelControl };
            WeatherpanelControl.Parent = pictureEdit1;
            foreach (var GateWay in GateWaySetting.GateWays)
            {
                foreach (var item in GateWay.GateWaySenserIDs)
                {
                    SenserEnumType senserEnumType = (SenserEnumType)item.SenserEnumType;
                    switch (senserEnumType)
                    {
                        case SenserEnumType.WeatherAPI:
                            {
                                WeatherUserControl1 = new WeatherUserControl1(GateWay, Taiwan_DistricsSetting, item, AbsProtocols) { Dock = DockStyle.Fill, Parent = WeatherpanelControl };
                            }
                            break;
                    }
                }
                foreach (var item in GateWay.GateWaySenserIDs)
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
                                GIAScreenUserControl1 = new GIAScreenUserControl1(GateWay, Taiwan_DistricsSetting, ScreenMediaSetting, AbsProtocols) { Dock = DockStyle.Fill, Parent = SenserpanelControl };
                            }
                            break;
                    }
                }
            }
            timer1.Interval = 1000;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            VideoUserControl.TextChange();
            WeatherUserControl1.TextChange();
            GIAScreenUserControl1.TextChange();
        }

        private void SenserForm_Load(object sender, EventArgs e)
        {
            Location = new Point(0, 0);
        }
    }
}