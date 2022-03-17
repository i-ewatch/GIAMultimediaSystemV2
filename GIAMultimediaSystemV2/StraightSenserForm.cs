﻿using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors;
using GIAMultimediaSystemV2.Components;
using GIAMultimediaSystemV2.Configuration;
using GIAMultimediaSystemV2.Enums;
using GIAMultimediaSystemV2.Methods;
using GIAMultimediaSystemV2.Protocols;
using GIAMultimediaSystemV2.Views;
using GIAMultimediaSystemV2.Views.GIAViews;
using GIAMultimediaSystemV2.Views.Setting;
using GIAMultimediaSystemV2.Views.WeathcrViews;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GIAMultimediaSystemV2
{
    public partial class StraightSenserForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 軟體被開啟旗標
        /// </summary>
        private bool OpenFlag { get; set; }
        /// <summary>
        /// 初始路徑
        /// </summary>
        public string MyWorkPath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 天氣使用
        /// </summary>
        public GateWaySenserID GateWaySenserID { get; set; }
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
        /// 圖片
        /// </summary>
        public PictureUserControl PictureUserControl { get; set; }
        /// <summary>
        /// 天氣畫面
        /// </summary>
        public WeatherUserControl WeatherUserControl { get; set; }
        /// <summary>
        /// 感測器畫面
        /// </summary>
        public GIAScreenUserControl GIAScreenUserControl { get; set; }
        /// <summary>
        /// 設定按鈕
        /// </summary>
        public SettingButtonUserControl SettingButtonUserControl { get; set; }
        #endregion
        public StraightSenserForm()
        {
            InitializeComponent();
            #region 禁止軟體重複開啟功能
            string ProcessName = Process.GetCurrentProcess().ProcessName;
            Process[] p = Process.GetProcessesByName(ProcessName);
            if (p.Length > 1)
            {
                FlyoutAction action = new FlyoutAction();
                action.Caption = "軟體錯誤";
                action.Description = "重複開啟!";
                action.Commands.Add(FlyoutCommand.OK);
                FlyoutDialog.Show(FindForm().FindForm(), action);
                OpenFlag = true;
                Environment.Exit(1);
            }
            #endregion
            if (!OpenFlag)
            {
                #region serialog
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.File($"{AppDomain.CurrentDomain.BaseDirectory}\\log\\log-.txt",
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                    .CreateLogger();        //宣告Serilog初始化
                #endregion
                #region Configuration
                GateWaySetting = InitialMethod.GateWayLoad();
                MarqueeSetting = InitialMethod.MarqueeLoad();
                Taiwan_DistricsSetting = InitialMethod.Taiwan_DistricsLoad();
                SqlDBSetting = InitialMethod.SqlDBLoad();
                UploadSetting = InitialMethod.UploadLoad();
                ScreenMediaSetting = InitialMethod.ScreenMediaLoad();
                GroupSetting = InitialMethod.GroupLoad();
                MediaPlaySetting = InitialMethod.MediaPlayLoad();
                #endregion
                #region Component
                if (GateWaySetting.ControlFlag)//使用通訊
                {
                    foreach (var Gateitem in GateWaySetting.GateWays)
                    {
                        GatewayEnumType = (GatewayEnumType)Gateitem.GatewayEnumType;
                        switch (GatewayEnumType)
                        {
                            case GatewayEnumType.ModbusRTU:
                                {
                                    SerialportComponent component = new SerialportComponent(GateWaySetting, Gateitem, SqlMethod, Taiwan_DistricsSetting);
                                    component.MyWorkState = GateWaySetting.ControlFlag;
                                    Field4Components.Add(component);
                                    AbsProtocols.AddRange(component.AbsProtocols);
                                }
                                break;
                            case GatewayEnumType.ModbusTCP:
                                {
                                    TCPComponent component = new TCPComponent(GateWaySetting, Gateitem, SqlMethod, Taiwan_DistricsSetting);
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
                #endregion
                #region 資料庫
                if (GateWaySetting.RecordFlag & GateWaySetting.ControlFlag)//使用紀錄
                {
                    SqlMethod = new SqlMethod() { setting = SqlDBSetting };
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
                #endregion
                LogopictureEdit.Image = imageCollection1.Images[0];
                #region Views
                MarqueeUserControl = new MarqueeUserControl(MarqueeSetting, ScreenMediaSetting, new Point(1081, 17)) { Dock = DockStyle.Fill, Parent = MarqueepanelControl };
                VideoUserControl = new VideoUserControl(MediaPlaySetting) { Dock = DockStyle.Fill, Parent = VediopanelControl1 };
                //WeatherpanelControl.Parent = pictureEdit1;
                foreach (var GateWay in GateWaySetting.GateWays)
                {
                    foreach (var item in GateWay.GateWaySenserIDs)
                    {
                        SenserEnumType senserEnumType = (SenserEnumType)item.SenserEnumType;
                        switch (senserEnumType)
                        {
                            case SenserEnumType.WeatherAPI:
                                {
                                    WeatherUserControl = new WeatherUserControl(GateWay, Taiwan_DistricsSetting, item, AbsProtocols) { Dock = DockStyle.Fill, Parent = WeatherpanelControl };
                                    GateWaySenserID = item;
                                }
                                break;
                        }
                    }
                    foreach (var item in GateWay.GateWaySenserIDs)
                    {
                        SenserEnumType senserEnumType = (SenserEnumType)item.SenserEnumType;
                        switch (senserEnumType)
                        {
                            case SenserEnumType.GIAAPI:
                            case SenserEnumType.GIA:
                                {
                                    GIAScreenUserControl = new GIAScreenUserControl(GateWay, Taiwan_DistricsSetting, GateWaySenserID, ScreenMediaSetting, AbsProtocols) { Dock = DockStyle.Fill, Parent = SenserpanelControl };
                                }
                                break;
                        }
                    }
                }
                SettingButtonUserControl = new SettingButtonUserControl(null, null, this);
                #endregion
                timer1.Interval = 1000;
                timer1.Enabled = true;
            }
        }
        #region 通訊錯誤泡泡視窗
        /// <summary>
        /// 通訊錯誤泡泡視窗
        /// </summary>
        public void ComponentFail()
        {
            foreach (var Componentitem in Field4Components)
            {
                foreach (var item in Componentitem.AbsProtocols)
                {
                    if (!item.ConnectFlag)
                    {
                        if (ErrorflyoutPanel == null)
                        {
                            ErrorflyoutPanel = new FlyoutPanel()
                            {
                                OwnerControl = this,
                                Size = new Size(1080, 20)
                            };
                            LabelControl label = new LabelControl() { Size = new Size(1080, 20) };
                            label.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                            label.Appearance.Font = new Font("微軟正黑體", 12, FontStyle.Bold);
                            label.Appearance.ForeColor = Color.White;
                            label.Appearance.BackColor = Color.Red;
                            label.AutoSizeMode = LabelAutoSizeMode.None;
                            label.Text = "設備通訊失敗";
                            ErrorflyoutPanel.Controls.Add(label);
                            ErrorflyoutPanel.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.Bottom;
                            ErrorflyoutPanel.ShowPopup();
                        }
                        return;
                    }
                }

            }
            if (ErrorflyoutPanel != null)
            {
                ErrorflyoutPanel.HidePopup();
                ErrorflyoutPanel = null;
            }
        }
        #endregion
        #region 視窗建置後初始位址
        /// <summary>
        /// 視窗建置後初始位址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StraightSenserForm_Load(object sender, EventArgs e)
        {
            Location = new Point(0, 0);
            Size = new Size(1080, 1920);
        }
        #endregion
        #region 關閉視窗
        /// <summary>
        /// 關閉視窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SenserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var Componentitem in Field4Components)
            {
                Componentitem.MyWorkState = false;
            }
            //GUIDAuthorizationMethod.Close_System();
            foreach (var item in RecordComponents)
            {
                item.MyWorkState = false;
            }
            timer1.Enabled = false;
            MarqueeUserControl.timer1.Enabled = false;
            this.Dispose();
        }
        #endregion
        #region 設定畫面顯示
        /// <summary>
        /// 設定畫面顯示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingpanelControl_MouseHover(object sender, EventArgs e)
        {
            SettingflyoutPanel = new FlyoutPanel()
            {
                OwnerControl = this,
                Size = new Size(1080, 62)
            };
            SettingflyoutPanel.Controls.Add(SettingButtonUserControl);
            SettingflyoutPanel.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.Top;
            SettingflyoutPanel.Options.CloseOnOuterClick = true;
            SettingflyoutPanel.OptionsButtonPanel.ShowButtonPanel = true;
            SettingflyoutPanel.ShowPopup();
        }
        #endregion
        #region 重新啟動
        public void Restart()
        {
            foreach (var Componentitem in Field4Components)
            {
                Componentitem.MyWorkState = false;
            }
            //GUIDAuthorizationMethod.Close_System();
            foreach (var item in RecordComponents)
            {
                item.MyWorkState = false;
            }
            timer1.Enabled = false;
            MarqueeUserControl.timer1.Enabled = false;
            this.Dispose();
        }
        #endregion
        private void timer1_Tick(object sender, EventArgs e)
        {
            WeatherUserControl.TextChange();
            VideoUserControl.TextChange();
            GIAScreenUserControl.TextChange();
            ComponentFail();
        }
    }
}