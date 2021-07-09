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
        public SettingButtonUserControl(SenserForm senserForm = null, ElectricForm electricForm = null)
        {
            InitializeComponent();
            LocksimpleButton.ImageOptions.Image = imageCollection1.Images[0];
            SenserForm = senserForm;
            ElectricForm = electricForm;
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
                        Size = new Size(1299, 676)
                    };
                    flyout = new FlyoutDialog(SenserForm, panelControl);
                    flyout.Properties.Style = FlyoutStyle.Popup;
                    ScreenSettingUserControl screenSetting = new ScreenSettingUserControl(this) { Dock= DockStyle.Fill };
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

            }       
        }
        #endregion
    }
}
