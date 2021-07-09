using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using GIAMultimediaSystemV2.Methods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GIAMultimediaSystemV2.Views.Setting
{
    public partial class ViewSettingUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 設定按鈕視窗
        /// </summary>
        private SettingButtonUserControl SettingButtonUserControl { get; set; }
        /// <summary>
        /// Loading物件繼承
        /// </summary>
        private IOverlaySplashScreenHandle handle = null;
        /// 關閉Loading視窗
        /// </summary>
        /// <param name="handle"></param>
        private void CloseProgressPanel(IOverlaySplashScreenHandle handle)
        {
            if (handle != null)
                SplashScreenManager.CloseOverlayForm(handle);
        }
        public ViewSettingUserControl(SettingButtonUserControl settingButtonUserControl)
        {
            InitializeComponent();
            SettingButtonUserControl = settingButtonUserControl;
            if (SettingButtonUserControl.SenserForm != null)
            {
                /*天氣資訊底板顏色*/
                WeatherPanelcolorEdit.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.WeatherPanelRGB;
                /*天氣資訊字體顏色*/
                WeatherForecolorEdit.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.WeatherForeRGB;
                /*GIA底板顏色*/
                GIAPanelcolorEdit.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.PanelRGB;
                /*感測器底板顏色*/
                BigSenserPanelcolorEdit.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.BigSenserPanelRGB;
                /*感測器字體顏色*/
                BigSenserForecolorEdit.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.BigSenserForeRGB;
                /*感測器底板顏色*/
                SmallSenserPanelcolorEdit.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.SmallSenserPanelRGB;
                /*感測器字體顏色*/
                SmallSenserForecolorEdit.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.SmallSenserForeRGB;
                /*跑馬燈底板顏色*/
                MarqueePanelcolorEdit.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.MarqueePanelRGB;
                /*跑馬燈字體顏色*/
                MarqueeForecolorEdit.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.MarqueeForeRGB;
            }
            else if (SettingButtonUserControl.ElectricForm != null)
            {

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
            }
            else if (SettingButtonUserControl.ElectricForm != null)
            {
            }
            SettingButtonUserControl.FlyoutFlag = false;
            SettingButtonUserControl.flyout.Close();
        }
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
                /*天氣資訊底板顏色*/
                Color weatherPanel = ColorTranslator.FromHtml(WeatherPanelcolorEdit.Text);
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.WeatherPanelRGB = $"{weatherPanel.R},{weatherPanel.G},{weatherPanel.B}";
                /*天氣資訊字體顏色*/
                Color weatherFore = ColorTranslator.FromHtml(WeatherForecolorEdit.Text);
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.WeatherForeRGB = $"{weatherFore.R},{weatherFore.G},{weatherFore.B}";
                /*GIA底板顏色*/
                Color GIAPanel = ColorTranslator.FromHtml(GIAPanelcolorEdit.Text);
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.PanelRGB = $"{GIAPanel.R},{GIAPanel.G},{GIAPanel.B}";
                /*感測器底板顏色(大)*/
                Color BigsenserPanel = ColorTranslator.FromHtml(BigSenserPanelcolorEdit.Text);
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.BigSenserPanelRGB = $"{BigsenserPanel.R},{BigsenserPanel.G},{BigsenserPanel.B}";
                /*感測器字體顏色(大)*/
                Color BigsenserFore = ColorTranslator.FromHtml(BigSenserForecolorEdit.Text);
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.BigSenserForeRGB = $"{BigsenserFore.R},{BigsenserFore.G},{BigsenserFore.B}";
                /*感測器底板顏色(大)*/
                Color SmallsenserPanel = ColorTranslator.FromHtml(SmallSenserPanelcolorEdit.Text);
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.SmallSenserPanelRGB = $"{SmallsenserPanel.R},{SmallsenserPanel.G},{SmallsenserPanel.B}";
                /*感測器字體顏色(大)*/
                Color SmallsenserFore = ColorTranslator.FromHtml(SmallSenserForecolorEdit.Text);
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.SmallSenserForeRGB = $"{SmallsenserFore.R},{SmallsenserFore.G},{SmallsenserFore.B}";
                /*跑馬燈底板顏色*/
                Color marqueePanel = ColorTranslator.FromHtml(MarqueePanelcolorEdit.Text);
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.MarqueePanelRGB = $"{marqueePanel.R},{marqueePanel.G},{marqueePanel.B}";
                /*跑馬燈字體顏色*/
                Color marqueeFore = ColorTranslator.FromHtml(MarqueeForecolorEdit.Text);
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.MarqueeForeRGB = $"{marqueeFore.R},{marqueeFore.G},{marqueeFore.B}";
                /*(整合過)顏色變更*/
                SettingButtonUserControl.SenserForm.GIAScreenUserControl1.Change_ScreenMedia(SettingButtonUserControl.SenserForm.ScreenMediaSetting);
                SettingButtonUserControl.SenserForm.MarqueeUserControl.Change_MarqueeColor();
                /*復原GIA切換畫面旗標*/
                SettingButtonUserControl.SenserForm.GIAScreenUserControl1.LockFlag = SettingButtonUserControl.AfterLockFlag;
                /*存取修改後的畫面JSON*/
                InitialMethod.Save_ScreenMedia(SettingButtonUserControl.SenserForm.ScreenMediaSetting);
            }
            else if (SettingButtonUserControl.ElectricForm != null)
            {
            }           
            SettingButtonUserControl.FlyoutFlag = false;
            SettingButtonUserControl.flyout.Close();
            /*結束等待畫面*/
            CloseProgressPanel(handle);
        }
    }
}
