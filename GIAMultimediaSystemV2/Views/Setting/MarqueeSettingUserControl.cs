using DevExpress.XtraEditors;
using GIAMultimediaSystemV2.Methods;
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
    public partial class MarqueeSettingUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 設定按鈕視窗
        /// </summary>
        private SettingButtonUserControl SettingButtonUserControl { get; set; }
        public MarqueeSettingUserControl(SettingButtonUserControl settingButtonUserControl)
        {
            InitializeComponent();
            SettingButtonUserControl = settingButtonUserControl;
            if (SettingButtonUserControl.SenserForm != null)
            {
                textEdit1.Text = SettingButtonUserControl.SenserForm.MarqueeSetting.MarqueeStr;
            }
            else if (SettingButtonUserControl.ElectricForm != null)
            {
                textEdit1.Text = SettingButtonUserControl.ElectricForm.MarqueeSetting.MarqueeStr;
            }
        }
        /// <summary>
        /// 取消按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (SettingButtonUserControl.SenserForm != null)
            {
                SettingButtonUserControl.SenserForm.GIAScreenUserControl1.LockFlag = SettingButtonUserControl.AfterLockFlag;
                SettingButtonUserControl.FlyoutFlag = false;
                SettingButtonUserControl.flyout.Close();
            }
            else if (SettingButtonUserControl.ElectricForm != null)
            {
                SettingButtonUserControl.ElectricForm.GIAScreenUserControl1.LockFlag = SettingButtonUserControl.AfterLockFlag;
                SettingButtonUserControl.FlyoutFlag = false;
                SettingButtonUserControl.flyout.Close();
            }
        }
        /// <summary>
        /// 確定按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKsimpleButton_Click(object sender, EventArgs e)
        {
            if (SettingButtonUserControl.SenserForm != null)
            {
                SettingButtonUserControl.SenserForm.MarqueeSetting.MarqueeStr = textEdit1.Text;
                InitialMethod.Save_Marquee(SettingButtonUserControl.SenserForm.MarqueeSetting);
                SettingButtonUserControl.SenserForm.MarqueeUserControl.Change_MarqueeText();
                SettingButtonUserControl.SenserForm.GIAScreenUserControl1.LockFlag = SettingButtonUserControl.AfterLockFlag;
            }
            else if (SettingButtonUserControl.ElectricForm != null)
            {
                SettingButtonUserControl.ElectricForm.MarqueeSetting.MarqueeStr = textEdit1.Text;
                InitialMethod.Save_Marquee(SettingButtonUserControl.ElectricForm.MarqueeSetting);
                SettingButtonUserControl.ElectricForm.MarqueeUserControl.Change_MarqueeText();
                SettingButtonUserControl.ElectricForm.GIAScreenUserControl1.LockFlag = SettingButtonUserControl.AfterLockFlag;
            }
            SettingButtonUserControl.FlyoutFlag = false;
            SettingButtonUserControl.flyout.Close();
        }
    }
}
