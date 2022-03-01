using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors;
using GIAMultimediaSystemV2.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GIAMultimediaSystemV2.Views.Setting
{
    public partial class SettingButtonUserControl : Field4UserControl
    {
        /// <summary>
        /// 感測器含影片
        /// </summary>
        public SenserForm SenserForm { get; set; }
        /// <summary>
        /// 感測器含電表
        /// </summary>
        public ElectricForm ElectricForm { get; set; }
        /// <summary>
        /// 直式 感測器含影片、圖片
        /// </summary>
        public StraightSenserForm StraightSenserForm { get; set; }
        /// <summary>
        /// 浮動視窗
        /// </summary>
        public FlyoutDialog flyout { get; set; }
        /// <summary>
        /// 浮動視窗旗標
        /// </summary>
        public bool FlyoutFlag { get; set; }
        /// <summary>
        /// 之前畫面鎖定旗標
        /// </summary>
        public bool AfterLockFlag { get; set; }
        public SettingButtonUserControl(SenserForm senserForm = null, ElectricForm electricForm = null, StraightSenserForm straightSenserForm = null)
        {
            InitializeComponent();
            LocksimpleButton.ImageOptions.Image = imageCollection1.Images[0];
            UnitsimpleButton.ImageOptions.Image = imageCollection1.Images["Lightning.png"];
            SenserForm = senserForm;
            ElectricForm = electricForm;
            StraightSenserForm = straightSenserForm;
            if (senserForm != null)
            {
                ChartDaysimpleButton.Visible = false;
                UnitsimpleButton.Visible = false;
            }
            else if (electricForm != null)
            {

            }
            else if (straightSenserForm != null)
            {
                Size = new Size(1080, 62);
                ChartDaysimpleButton.Visible = false;
                UnitsimpleButton.Visible = false;
            }
        }

        #region 關閉視窗
        /// <summary>
        /// 關閉視窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseFormsimpleButton_Click(object sender, EventArgs e)
        {
            if (SenserForm != null)
            {
                SenserForm.SenserForm_FormClosing(null, null);
            }
            else if (ElectricForm != null)
            {
                ElectricForm.ElectricForm_FormClosing(null, null);
            }
            else if (StraightSenserForm != null)
            {
                StraightSenserForm.SenserForm_FormClosing(null, null);
            }
        }
        #endregion

        #region GIA畫面切換鎖定按鈕
        /// <summary>
        /// GIA畫面切換鎖定按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LocksimpleButton_Click(object sender, EventArgs e)
        {
            if (SenserForm != null)
            {
                if (SenserForm.GIAScreenUserControl1.LockFlag)
                {
                    LocksimpleButton.ImageOptions.Image = imageCollection1.Images[1];
                    SenserForm.GIAScreenUserControl1.LockFlag = false;
                    AfterLockFlag = false;
                }
                else
                {
                    LocksimpleButton.ImageOptions.Image = imageCollection1.Images[0];
                    SenserForm.GIAScreenUserControl1.LockFlag = true;
                    AfterLockFlag = true;
                }
            }
            else if (ElectricForm != null)
            {
                if (ElectricForm.GIAScreenUserControl1.LockFlag)
                {
                    LocksimpleButton.ImageOptions.Image = imageCollection1.Images[1];
                    ElectricForm.GIAScreenUserControl1.LockFlag = false;
                    AfterLockFlag = false;
                }
                else
                {
                    LocksimpleButton.ImageOptions.Image = imageCollection1.Images[0];
                    ElectricForm.GIAScreenUserControl1.LockFlag = true;
                    AfterLockFlag = true;
                }
            }
            else if (StraightSenserForm != null)
            {
                if (StraightSenserForm.GIAScreenUserControl.LockFlag)
                {
                    LocksimpleButton.ImageOptions.Image = imageCollection1.Images[1];
                    StraightSenserForm.GIAScreenUserControl.LockFlag = false;
                    AfterLockFlag = false;
                }
                else
                {
                    LocksimpleButton.ImageOptions.Image = imageCollection1.Images[0];
                    StraightSenserForm.GIAScreenUserControl.LockFlag = true;
                    AfterLockFlag = true;
                }
            }
        }
        #endregion

        #region 跑馬燈設定按鈕
        /// <summary>
        /// 跑馬燈設定按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MarqueeSettingsimpleButton_Click(object sender, EventArgs e)
        {
            if (SenserForm != null)
            {
                AfterLockFlag = SenserForm.GIAScreenUserControl1.LockFlag;
                SenserForm.GIAScreenUserControl1.LockFlag = false;
                if (!FlyoutFlag)
                {
                    FlyoutFlag = true;
                    PanelControl panelControl = new PanelControl()
                    {
                        Size = new Size(609, 283)
                    };
                    flyout = new FlyoutDialog(SenserForm, panelControl);
                    flyout.Properties.Style = FlyoutStyle.Popup;
                    MarqueeSettingUserControl marqueeSetting = new MarqueeSettingUserControl(this) { Dock = DockStyle.Fill };
                    marqueeSetting.Parent = panelControl;
                    flyout.Show();
                }
                else
                {
                    FlyoutFlag = false;
                    flyout.Close();
                }
            }
            else if (ElectricForm != null)
            {
                AfterLockFlag = ElectricForm.GIAScreenUserControl1.LockFlag;
                ElectricForm.GIAScreenUserControl1.LockFlag = false;
                if (!FlyoutFlag)
                {
                    FlyoutFlag = true;
                    PanelControl panelControl = new PanelControl()
                    {
                        Size = new Size(609, 283)
                    };
                    flyout = new FlyoutDialog(ElectricForm, panelControl);
                    flyout.Properties.Style = FlyoutStyle.Popup;
                    MarqueeSettingUserControl marqueeSetting = new MarqueeSettingUserControl(this) { Dock = DockStyle.Fill };
                    marqueeSetting.Parent = panelControl;
                    flyout.Show();
                }
                else
                {
                    FlyoutFlag = false;
                    flyout.Close();
                }
            }
            else if (StraightSenserForm != null)
            {
                AfterLockFlag = StraightSenserForm.GIAScreenUserControl.LockFlag;
                StraightSenserForm.GIAScreenUserControl.LockFlag = false;
                if (!FlyoutFlag)
                {
                    FlyoutFlag = true;
                    PanelControl panelControl = new PanelControl()
                    {
                        Size = new Size(609, 283)
                    };
                    flyout = new FlyoutDialog(StraightSenserForm, panelControl);
                    flyout.Properties.Style = FlyoutStyle.Popup;
                    MarqueeSettingUserControl marqueeSetting = new MarqueeSettingUserControl(this) { Dock = DockStyle.Fill };
                    marqueeSetting.Parent = panelControl;
                    flyout.Show();
                }
                else
                {
                    FlyoutFlag = false;
                    flyout.Close();
                }
            }
        }
        #endregion

        #region 外觀設定
        private void ViewSettingsimpleButton_Click(object sender, EventArgs e)
        {
            if (SenserForm != null)
            {
                AfterLockFlag = SenserForm.GIAScreenUserControl1.LockFlag;
                SenserForm.GIAScreenUserControl1.LockFlag = false;
                if (!FlyoutFlag)
                {
                    FlyoutFlag = true;
                    PanelControl panelControl = new PanelControl()
                    {
                        Size = new Size(463, 348)
                    };
                    flyout = new FlyoutDialog(SenserForm, panelControl);
                    flyout.Properties.Style = FlyoutStyle.Popup;
                    ViewSettingUserControl viewSetting = new ViewSettingUserControl(this);
                    viewSetting.Parent = panelControl;
                    flyout.Show();
                }
                else
                {
                    FlyoutFlag = false;
                    flyout.Close();
                }
            }
            else if (ElectricForm != null)
            {
                AfterLockFlag = ElectricForm.GIAScreenUserControl1.LockFlag;
                ElectricForm.GIAScreenUserControl1.LockFlag = false;
                if (!FlyoutFlag)
                {
                    FlyoutFlag = true;
                    PanelControl panelControl = new PanelControl()
                    {
                        Size = new Size(463, 348)
                    };
                    flyout = new FlyoutDialog(ElectricForm, panelControl);
                    flyout.Properties.Style = FlyoutStyle.Popup;
                    ViewSettingUserControl viewSetting = new ViewSettingUserControl(this);
                    viewSetting.Parent = panelControl;
                    flyout.Show();
                }
                else
                {
                    FlyoutFlag = false;
                    flyout.Close();
                }
            }
            else if (StraightSenserForm != null)
            {
                AfterLockFlag = StraightSenserForm.GIAScreenUserControl.LockFlag;
                StraightSenserForm.GIAScreenUserControl.LockFlag = false;
                if (!FlyoutFlag)
                {
                    FlyoutFlag = true;
                    PanelControl panelControl = new PanelControl()
                    {
                        Size = new Size(463, 348)
                    };
                    flyout = new FlyoutDialog(StraightSenserForm, panelControl);
                    flyout.Properties.Style = FlyoutStyle.Popup;
                    ViewSettingUserControl viewSetting = new ViewSettingUserControl(this);
                    viewSetting.Parent = panelControl;
                    flyout.Show();
                }
                else
                {
                    FlyoutFlag = false;
                    flyout.Close();
                }
            }
        }
        #endregion

        #region 系統設定
        /// <summary>
        /// 系統設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemSettingsimpleButton_Click(object sender, EventArgs e)
        {
            if (SenserForm != null)
            {
                AfterLockFlag = SenserForm.GIAScreenUserControl1.LockFlag;
                SenserForm.GIAScreenUserControl1.LockFlag = false;
                if (!FlyoutFlag)
                {
                    FlyoutFlag = true;
                    PanelControl panelControl = new PanelControl()
                    {
                        Size = new Size(1299, 702)
                    };
                    flyout = new FlyoutDialog(SenserForm, panelControl);
                    flyout.Properties.Style = FlyoutStyle.Popup;
                    ScreenSettingUserControl screenSetting = new ScreenSettingUserControl(this) { Dock = DockStyle.Fill };
                    screenSetting.Parent = panelControl;
                    flyout.Show();
                }
                else
                {
                    FlyoutFlag = false;
                    flyout.Close();
                }
            }
            else if (ElectricForm != null)
            {
                AfterLockFlag = ElectricForm.GIAScreenUserControl1.LockFlag;
                ElectricForm.GIAScreenUserControl1.LockFlag = false;
                if (!FlyoutFlag)
                {
                    FlyoutFlag = true;
                    PanelControl panelControl = new PanelControl()
                    {
                        Size = new Size(1299, 702)
                    };
                    flyout = new FlyoutDialog(ElectricForm, panelControl);
                    flyout.Properties.Style = FlyoutStyle.Popup;
                    ScreenSettingUserControl screenSetting = new ScreenSettingUserControl(this) { Dock = DockStyle.Fill };
                    screenSetting.Parent = panelControl;
                    flyout.Show();
                }
                else
                {
                    FlyoutFlag = false;
                    flyout.Close();
                }
            }
            else if (StraightSenserForm != null)
            {
                AfterLockFlag = StraightSenserForm.GIAScreenUserControl.LockFlag;
                StraightSenserForm.GIAScreenUserControl.LockFlag = false;
                if (!FlyoutFlag)
                {
                    FlyoutFlag = true;
                    PanelControl panelControl = new PanelControl()
                    {
                        Size = new Size(1080, 702)
                    };
                    flyout = new FlyoutDialog(StraightSenserForm, panelControl);
                    flyout.Properties.Style = FlyoutStyle.Popup;
                    ScreenSettingUserControl screenSetting = new ScreenSettingUserControl(this) { Dock = DockStyle.Fill };
                    screenSetting.Parent = panelControl;
                    flyout.Show();
                }
                else
                {
                    FlyoutFlag = false;
                    flyout.Close();
                }
            }
        }
        #endregion

        #region 報表日月年切換
        /// <summary>
        /// 報表日月年切換
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChartDaysimpleButton_Click(object sender, EventArgs e)
        {
            if (ElectricForm.ChartUserControl1.BarIndex == 2)
            {
                ElectricForm.ChartUserControl1.BarIndex = 0;
                ChartDaysimpleButton.Text = "日";
            }
            else
            {
                ElectricForm.ChartUserControl1.BarIndex++;
                if (ElectricForm.ChartUserControl1.BarIndex == 1)
                {
                    ChartDaysimpleButton.Text = "月";
                }
                else
                {
                    ChartDaysimpleButton.Text = "年";
                }
            }
        }
        #endregion

        #region 報表單位切換
        /// <summary>
        /// 報表單位切換
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnitsimpleButton_Click(object sender, EventArgs e)
        {
            if (ElectricForm.ChartUserControl1.DataIndex == 1)
            {
                ElectricForm.ChartUserControl1.DataIndex = 0;
                UnitsimpleButton.ImageOptions.Image = imageCollection1.Images["Lightning.png"];
            }
            else
            {
                ElectricForm.ChartUserControl1.DataIndex++;
                UnitsimpleButton.ImageOptions.Image = imageCollection1.Images["Dollar.png"];
            }
        }
        #endregion

        #region GIA通訊/天氣設定
        /// <summary>
        /// GIA通訊/天氣設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (SenserForm != null)
            {
                AfterLockFlag = SenserForm.GIAScreenUserControl1.LockFlag;
                SenserForm.GIAScreenUserControl1.LockFlag = false;
                if (!FlyoutFlag)
                {
                    FlyoutFlag = true;
                    PanelControl panelControl = new PanelControl()
                    {
                        Size = new Size(650, 261)
                    };
                    flyout = new FlyoutDialog(SenserForm, panelControl);
                    flyout.Properties.Style = FlyoutStyle.Popup;
                    ProtocolSettingUserControl viewSetting = new ProtocolSettingUserControl(this, SenserForm.GateWaySetting, SenserForm.Taiwan_DistricsSetting);
                    viewSetting.Parent = panelControl;
                    flyout.Show();
                }
            }
            else if (ElectricForm != null)
            {
                AfterLockFlag = ElectricForm.GIAScreenUserControl1.LockFlag;
                ElectricForm.GIAScreenUserControl1.LockFlag = false;
                if (!FlyoutFlag)
                {
                    FlyoutFlag = true;
                    PanelControl panelControl = new PanelControl()
                    {
                        Size = new Size(650, 261)
                    };
                    flyout = new FlyoutDialog(ElectricForm, panelControl);
                    flyout.Properties.Style = FlyoutStyle.Popup;
                    ProtocolSettingUserControl viewSetting = new ProtocolSettingUserControl(this, ElectricForm.GateWaySetting, ElectricForm.Taiwan_DistricsSetting);
                    viewSetting.Parent = panelControl;
                    flyout.Show();
                }
            }
            else if (StraightSenserForm != null)
            {
                AfterLockFlag = StraightSenserForm.GIAScreenUserControl.LockFlag;
                StraightSenserForm.GIAScreenUserControl.LockFlag = false;
                if (!FlyoutFlag)
                {
                    FlyoutFlag = true;
                    PanelControl panelControl = new PanelControl()
                    {
                        Size = new Size(650, 261)
                    };
                    flyout = new FlyoutDialog(StraightSenserForm, panelControl);
                    flyout.Properties.Style = FlyoutStyle.Popup;
                    ProtocolSettingUserControl viewSetting = new ProtocolSettingUserControl(this, StraightSenserForm.GateWaySetting, StraightSenserForm.Taiwan_DistricsSetting);
                    viewSetting.Parent = panelControl;
                    flyout.Show();
                }
            }
        }
        #endregion
        #region 重新啟動
        public void Restart()
        {
            if (SenserForm != null)
            {
                SenserForm.Restart();
            }
            else if (ElectricForm != null)
            {
                ElectricForm.Restart();
            }
            else if (StraightSenserForm!= null)
            {
                StraightSenserForm.Restart();
            }
        }
        #endregion
    }
}
