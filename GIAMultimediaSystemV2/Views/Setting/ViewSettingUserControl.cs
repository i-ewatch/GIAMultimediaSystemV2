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
            labelControl13.Text = "CO" + "\xb2";
            labelControl18.Text = "O" + "\xb3";
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

                AlarmtoggleSwitch.IsOn = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmFlag;

                AlarmtextEdit1.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmValue[0].ToString();
                AlarmtextEdit2.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmValue[1].ToString();
                AlarmtextEdit3.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmValue[2].ToString();
                AlarmtextEdit4.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmValue[3].ToString();
                AlarmtextEdit5.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmValue[4].ToString();
                AlarmtextEdit6.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmValue[5].ToString();
                AlarmtextEdit7.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmValue[6].ToString();
                AlarmtextEdit8.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmValue[7].ToString();
                AlarmtextEdit9.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmValue[8].ToString();
                AlarmtextEdit10.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmValue[9].ToString();
                AlarmtextEdit11.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmValue[10].ToString();
                AlarmtextEdit12.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmValue[11].ToString();

                AlarmcolorPickEdit1.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmForeRGB[0].ToString();
                AlarmcolorPickEdit2.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmForeRGB[1].ToString();
                AlarmcolorPickEdit3.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmForeRGB[2].ToString();
                AlarmcolorPickEdit4.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmForeRGB[3].ToString();
                AlarmcolorPickEdit5.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmForeRGB[4].ToString();
                AlarmcolorPickEdit6.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmForeRGB[5].ToString();
                AlarmcolorPickEdit7.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmForeRGB[6].ToString();
                AlarmcolorPickEdit8.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmForeRGB[7].ToString();
                AlarmcolorPickEdit9.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmForeRGB[8].ToString();
                AlarmcolorPickEdit10.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmForeRGB[9].ToString();
                AlarmcolorPickEdit11.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmForeRGB[10].ToString();
                AlarmcolorPickEdit12.Text = SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmForeRGB[11].ToString();
            }
            else if (SettingButtonUserControl.ElectricForm != null)
            {
                /*天氣資訊底板顏色*/
                WeatherPanelcolorEdit.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.WeatherPanelRGB;
                /*天氣資訊字體顏色*/
                WeatherForecolorEdit.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.WeatherForeRGB;
                /*GIA底板顏色*/
                GIAPanelcolorEdit.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.PanelRGB;
                /*感測器底板顏色*/
                BigSenserPanelcolorEdit.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.BigSenserPanelRGB;
                /*感測器字體顏色*/
                BigSenserForecolorEdit.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.BigSenserForeRGB;
                /*感測器底板顏色*/
                SmallSenserPanelcolorEdit.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.SmallSenserPanelRGB;
                /*感測器字體顏色*/
                SmallSenserForecolorEdit.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.SmallSenserForeRGB;
                /*跑馬燈底板顏色*/
                MarqueePanelcolorEdit.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.MarqueePanelRGB;
                /*跑馬燈字體顏色*/
                MarqueeForecolorEdit.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.MarqueeForeRGB;

                AlarmtoggleSwitch.IsOn = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmFlag;

                AlarmtextEdit1.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmValue[0].ToString();
                AlarmtextEdit2.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmValue[1].ToString();
                AlarmtextEdit3.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmValue[2].ToString();
                AlarmtextEdit4.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmValue[3].ToString();
                AlarmtextEdit5.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmValue[4].ToString();
                AlarmtextEdit6.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmValue[5].ToString();
                AlarmtextEdit7.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmValue[6].ToString();
                AlarmtextEdit8.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmValue[7].ToString();
                AlarmtextEdit9.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmValue[8].ToString();
                AlarmtextEdit10.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmValue[9].ToString();
                AlarmtextEdit11.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmValue[10].ToString();
                AlarmtextEdit12.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmValue[11].ToString();

                AlarmcolorPickEdit1.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmForeRGB[0].ToString();
                AlarmcolorPickEdit2.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmForeRGB[1].ToString();
                AlarmcolorPickEdit3.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmForeRGB[2].ToString();
                AlarmcolorPickEdit4.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmForeRGB[3].ToString();
                AlarmcolorPickEdit5.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmForeRGB[4].ToString();
                AlarmcolorPickEdit6.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmForeRGB[5].ToString();
                AlarmcolorPickEdit7.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmForeRGB[6].ToString();
                AlarmcolorPickEdit8.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmForeRGB[7].ToString();
                AlarmcolorPickEdit9.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmForeRGB[8].ToString();
                AlarmcolorPickEdit10.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmForeRGB[9].ToString();
                AlarmcolorPickEdit11.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmForeRGB[10].ToString();
                AlarmcolorPickEdit12.Text = SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmForeRGB[11].ToString();
            }
            else if (SettingButtonUserControl.StraightSenserForm != null)
            {
                /*天氣資訊底板顏色*/
                WeatherPanelcolorEdit.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.WeatherPanelRGB;
                /*天氣資訊字體顏色*/
                WeatherForecolorEdit.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.WeatherForeRGB;
                /*GIA底板顏色*/
                GIAPanelcolorEdit.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.PanelRGB;
                /*感測器底板顏色*/
                BigSenserPanelcolorEdit.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.BigSenserPanelRGB;
                /*感測器字體顏色*/
                BigSenserForecolorEdit.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.BigSenserForeRGB;
                /*感測器底板顏色*/
                SmallSenserPanelcolorEdit.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.SmallSenserPanelRGB;
                /*感測器字體顏色*/
                SmallSenserForecolorEdit.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.SmallSenserForeRGB;
                /*跑馬燈底板顏色*/
                MarqueePanelcolorEdit.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.MarqueePanelRGB;
                /*跑馬燈字體顏色*/
                MarqueeForecolorEdit.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.MarqueeForeRGB;

                AlarmtoggleSwitch.IsOn = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmFlag;

                AlarmtextEdit1.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmValue[0].ToString();
                AlarmtextEdit2.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmValue[1].ToString();
                AlarmtextEdit3.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmValue[2].ToString();
                AlarmtextEdit4.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmValue[3].ToString();
                AlarmtextEdit5.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmValue[4].ToString();
                AlarmtextEdit6.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmValue[5].ToString();
                AlarmtextEdit7.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmValue[6].ToString();
                AlarmtextEdit8.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmValue[7].ToString();
                AlarmtextEdit9.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmValue[8].ToString();
                AlarmtextEdit10.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmValue[9].ToString();
                AlarmtextEdit11.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmValue[10].ToString();
                AlarmtextEdit12.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmValue[11].ToString();

                AlarmcolorPickEdit1.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmForeRGB[0].ToString();
                AlarmcolorPickEdit2.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmForeRGB[1].ToString();
                AlarmcolorPickEdit3.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmForeRGB[2].ToString();
                AlarmcolorPickEdit4.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmForeRGB[3].ToString();
                AlarmcolorPickEdit5.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmForeRGB[4].ToString();
                AlarmcolorPickEdit6.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmForeRGB[5].ToString();
                AlarmcolorPickEdit7.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmForeRGB[6].ToString();
                AlarmcolorPickEdit8.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmForeRGB[7].ToString();
                AlarmcolorPickEdit9.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmForeRGB[8].ToString();
                AlarmcolorPickEdit10.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmForeRGB[9].ToString();
                AlarmcolorPickEdit11.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmForeRGB[10].ToString();
                AlarmcolorPickEdit12.Text = SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmForeRGB[11].ToString();
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
                SettingButtonUserControl.ElectricForm.GIAScreenUserControl1.LockFlag = SettingButtonUserControl.AfterLockFlag;
            }
            else if (SettingButtonUserControl.StraightSenserForm != null)
            {
                SettingButtonUserControl.StraightSenserForm.GIAScreenUserControl.LockFlag = SettingButtonUserControl.AfterLockFlag;
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

                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmFlag = AlarmtoggleSwitch.IsOn;

                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmValue[0] = Convert.ToDouble(AlarmtextEdit1.Text);
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmValue[1] = Convert.ToDouble(AlarmtextEdit2.Text);
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmValue[2] = Convert.ToDouble(AlarmtextEdit3.Text);
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmValue[3] = Convert.ToDouble(AlarmtextEdit4.Text);
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmValue[4] = Convert.ToDouble(AlarmtextEdit5.Text);
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmValue[5] = Convert.ToDouble(AlarmtextEdit6.Text);
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmValue[6] = Convert.ToDouble(AlarmtextEdit7.Text);
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmValue[7] = Convert.ToDouble(AlarmtextEdit8.Text);
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmValue[8] = Convert.ToDouble(AlarmtextEdit9.Text);
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmValue[9] = Convert.ToDouble(AlarmtextEdit10.Text);
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmValue[10] = Convert.ToDouble(AlarmtextEdit11.Text);
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmValue[11] = Convert.ToDouble(AlarmtextEdit12.Text);

                Color color1 = ColorTranslator.FromHtml(AlarmcolorPickEdit1.Text);
                Color color2 = ColorTranslator.FromHtml(AlarmcolorPickEdit2.Text);
                Color color3 = ColorTranslator.FromHtml(AlarmcolorPickEdit3.Text);
                Color color4 = ColorTranslator.FromHtml(AlarmcolorPickEdit4.Text);
                Color color5 = ColorTranslator.FromHtml(AlarmcolorPickEdit5.Text);
                Color color6 = ColorTranslator.FromHtml(AlarmcolorPickEdit6.Text);
                Color color7 = ColorTranslator.FromHtml(AlarmcolorPickEdit7.Text);
                Color color8 = ColorTranslator.FromHtml(AlarmcolorPickEdit8.Text);
                Color color9 = ColorTranslator.FromHtml(AlarmcolorPickEdit9.Text);
                Color color10 = ColorTranslator.FromHtml(AlarmcolorPickEdit10.Text);
                Color color11 = ColorTranslator.FromHtml(AlarmcolorPickEdit11.Text);
                Color color12 = ColorTranslator.FromHtml(AlarmcolorPickEdit12.Text);
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmForeRGB[0] = $"{color1.R},{color1.G},{color1.B}";
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmForeRGB[1] = $"{color2.R},{color2.G},{color2.B}";
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmForeRGB[2] = $"{color3.R},{color3.G},{color3.B}";
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmForeRGB[3] = $"{color4.R},{color4.G},{color4.B}";
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmForeRGB[4] = $"{color5.R},{color5.G},{color5.B}";
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmForeRGB[5] = $"{color6.R},{color6.G},{color6.B}";
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmForeRGB[6] = $"{color7.R},{color7.G},{color7.B}";
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmForeRGB[7] = $"{color8.R},{color8.G},{color8.B}";
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmForeRGB[8] = $"{color9.R},{color9.G},{color9.B}";
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmForeRGB[9] = $"{color10.R},{color10.G},{color10.B}";
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmForeRGB[10] = $"{color11.R},{color11.G},{color11.B}";
                SettingButtonUserControl.SenserForm.ScreenMediaSetting.AlarmForeRGB[11] = $"{color12.R},{color12.G},{color12.B}";

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
                /*天氣資訊底板顏色*/
                Color weatherPanel = ColorTranslator.FromHtml(WeatherPanelcolorEdit.Text);
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.WeatherPanelRGB = $"{weatherPanel.R},{weatherPanel.G},{weatherPanel.B}";
                /*天氣資訊字體顏色*/
                Color weatherFore = ColorTranslator.FromHtml(WeatherForecolorEdit.Text);
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.WeatherForeRGB = $"{weatherFore.R},{weatherFore.G},{weatherFore.B}";
                /*GIA底板顏色*/
                Color GIAPanel = ColorTranslator.FromHtml(GIAPanelcolorEdit.Text);
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.PanelRGB = $"{GIAPanel.R},{GIAPanel.G},{GIAPanel.B}";
                /*感測器底板顏色(大)*/
                Color BigsenserPanel = ColorTranslator.FromHtml(BigSenserPanelcolorEdit.Text);
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.BigSenserPanelRGB = $"{BigsenserPanel.R},{BigsenserPanel.G},{BigsenserPanel.B}";
                /*感測器字體顏色(大)*/
                Color BigsenserFore = ColorTranslator.FromHtml(BigSenserForecolorEdit.Text);
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.BigSenserForeRGB = $"{BigsenserFore.R},{BigsenserFore.G},{BigsenserFore.B}";
                /*感測器底板顏色(大)*/
                Color SmallsenserPanel = ColorTranslator.FromHtml(SmallSenserPanelcolorEdit.Text);
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.SmallSenserPanelRGB = $"{SmallsenserPanel.R},{SmallsenserPanel.G},{SmallsenserPanel.B}";
                /*感測器字體顏色(大)*/
                Color SmallsenserFore = ColorTranslator.FromHtml(SmallSenserForecolorEdit.Text);
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.SmallSenserForeRGB = $"{SmallsenserFore.R},{SmallsenserFore.G},{SmallsenserFore.B}";
                /*跑馬燈底板顏色*/
                Color marqueePanel = ColorTranslator.FromHtml(MarqueePanelcolorEdit.Text);
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.MarqueePanelRGB = $"{marqueePanel.R},{marqueePanel.G},{marqueePanel.B}";
                /*跑馬燈字體顏色*/
                Color marqueeFore = ColorTranslator.FromHtml(MarqueeForecolorEdit.Text);
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.MarqueeForeRGB = $"{marqueeFore.R},{marqueeFore.G},{marqueeFore.B}";

                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmFlag = AlarmtoggleSwitch.IsOn;

                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmValue[0] = Convert.ToDouble(AlarmtextEdit1.Text);
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmValue[1] = Convert.ToDouble(AlarmtextEdit2.Text);
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmValue[2] = Convert.ToDouble(AlarmtextEdit3.Text);
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmValue[3] = Convert.ToDouble(AlarmtextEdit4.Text);
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmValue[4] = Convert.ToDouble(AlarmtextEdit5.Text);
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmValue[5] = Convert.ToDouble(AlarmtextEdit6.Text);
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmValue[6] = Convert.ToDouble(AlarmtextEdit7.Text);
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmValue[7] = Convert.ToDouble(AlarmtextEdit8.Text);
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmValue[8] = Convert.ToDouble(AlarmtextEdit9.Text);
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmValue[9] = Convert.ToDouble(AlarmtextEdit10.Text);
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmValue[10] = Convert.ToDouble(AlarmtextEdit11.Text);
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmValue[11] = Convert.ToDouble(AlarmtextEdit12.Text);

                Color color1 = ColorTranslator.FromHtml(AlarmcolorPickEdit1.Text);
                Color color2 = ColorTranslator.FromHtml(AlarmcolorPickEdit2.Text);
                Color color3 = ColorTranslator.FromHtml(AlarmcolorPickEdit3.Text);
                Color color4 = ColorTranslator.FromHtml(AlarmcolorPickEdit4.Text);
                Color color5 = ColorTranslator.FromHtml(AlarmcolorPickEdit5.Text);
                Color color6 = ColorTranslator.FromHtml(AlarmcolorPickEdit6.Text);
                Color color7 = ColorTranslator.FromHtml(AlarmcolorPickEdit7.Text);
                Color color8 = ColorTranslator.FromHtml(AlarmcolorPickEdit8.Text);
                Color color9 = ColorTranslator.FromHtml(AlarmcolorPickEdit9.Text);
                Color color10 = ColorTranslator.FromHtml(AlarmcolorPickEdit10.Text);
                Color color11 = ColorTranslator.FromHtml(AlarmcolorPickEdit11.Text);
                Color color12 = ColorTranslator.FromHtml(AlarmcolorPickEdit12.Text);
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmForeRGB[0] = $"{color1.R},{color1.G},{color1.B}";
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmForeRGB[1] = $"{color2.R},{color2.G},{color2.B}";
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmForeRGB[2] = $"{color3.R},{color3.G},{color3.B}";
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmForeRGB[3] = $"{color4.R},{color4.G},{color4.B}";
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmForeRGB[4] = $"{color5.R},{color5.G},{color5.B}";
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmForeRGB[5] = $"{color6.R},{color6.G},{color6.B}";
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmForeRGB[6] = $"{color7.R},{color7.G},{color7.B}";
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmForeRGB[7] = $"{color8.R},{color8.G},{color8.B}";
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmForeRGB[8] = $"{color9.R},{color9.G},{color9.B}";
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmForeRGB[9] = $"{color10.R},{color10.G},{color10.B}";
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmForeRGB[10] = $"{color11.R},{color11.G},{color11.B}";
                SettingButtonUserControl.ElectricForm.ScreenMediaSetting.AlarmForeRGB[11] = $"{color12.R},{color12.G},{color12.B}";

                /*(整合過)顏色變更*/
                SettingButtonUserControl.ElectricForm.GIAScreenUserControl1.Change_ScreenMedia(SettingButtonUserControl.ElectricForm.ScreenMediaSetting);
                SettingButtonUserControl.ElectricForm.MarqueeUserControl.Change_MarqueeColor();
                /*復原GIA切換畫面旗標*/
                SettingButtonUserControl.ElectricForm.GIAScreenUserControl1.LockFlag = SettingButtonUserControl.AfterLockFlag;
                /*存取修改後的畫面JSON*/
                InitialMethod.Save_ScreenMedia(SettingButtonUserControl.ElectricForm.ScreenMediaSetting);
            }
            else if (SettingButtonUserControl.StraightSenserForm != null)
            {
                /*天氣資訊底板顏色*/
                Color weatherPanel = ColorTranslator.FromHtml(WeatherPanelcolorEdit.Text);
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.WeatherPanelRGB = $"{weatherPanel.R},{weatherPanel.G},{weatherPanel.B}";
                /*天氣資訊字體顏色*/
                Color weatherFore = ColorTranslator.FromHtml(WeatherForecolorEdit.Text);
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.WeatherForeRGB = $"{weatherFore.R},{weatherFore.G},{weatherFore.B}";
                /*GIA底板顏色*/
                Color GIAPanel = ColorTranslator.FromHtml(GIAPanelcolorEdit.Text);
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.PanelRGB = $"{GIAPanel.R},{GIAPanel.G},{GIAPanel.B}";
                /*感測器底板顏色(大)*/
                Color BigsenserPanel = ColorTranslator.FromHtml(BigSenserPanelcolorEdit.Text);
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.BigSenserPanelRGB = $"{BigsenserPanel.R},{BigsenserPanel.G},{BigsenserPanel.B}";
                /*感測器字體顏色(大)*/
                Color BigsenserFore = ColorTranslator.FromHtml(BigSenserForecolorEdit.Text);
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.BigSenserForeRGB = $"{BigsenserFore.R},{BigsenserFore.G},{BigsenserFore.B}";
                /*感測器底板顏色(大)*/
                Color SmallsenserPanel = ColorTranslator.FromHtml(SmallSenserPanelcolorEdit.Text);
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.SmallSenserPanelRGB = $"{SmallsenserPanel.R},{SmallsenserPanel.G},{SmallsenserPanel.B}";
                /*感測器字體顏色(大)*/
                Color SmallsenserFore = ColorTranslator.FromHtml(SmallSenserForecolorEdit.Text);
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.SmallSenserForeRGB = $"{SmallsenserFore.R},{SmallsenserFore.G},{SmallsenserFore.B}";
                /*跑馬燈底板顏色*/
                Color marqueePanel = ColorTranslator.FromHtml(MarqueePanelcolorEdit.Text);
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.MarqueePanelRGB = $"{marqueePanel.R},{marqueePanel.G},{marqueePanel.B}";
                /*跑馬燈字體顏色*/
                Color marqueeFore = ColorTranslator.FromHtml(MarqueeForecolorEdit.Text);
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.MarqueeForeRGB = $"{marqueeFore.R},{marqueeFore.G},{marqueeFore.B}";

                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmFlag = AlarmtoggleSwitch.IsOn;

                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmValue[0] = Convert.ToDouble(AlarmtextEdit1.Text);
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmValue[1] = Convert.ToDouble(AlarmtextEdit2.Text);
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmValue[2] = Convert.ToDouble(AlarmtextEdit3.Text);
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmValue[3] = Convert.ToDouble(AlarmtextEdit4.Text);
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmValue[4] = Convert.ToDouble(AlarmtextEdit5.Text);
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmValue[5] = Convert.ToDouble(AlarmtextEdit6.Text);
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmValue[6] = Convert.ToDouble(AlarmtextEdit7.Text);
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmValue[7] = Convert.ToDouble(AlarmtextEdit8.Text);
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmValue[8] = Convert.ToDouble(AlarmtextEdit9.Text);
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmValue[9] = Convert.ToDouble(AlarmtextEdit10.Text);
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmValue[10] = Convert.ToDouble(AlarmtextEdit11.Text);
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmValue[11] = Convert.ToDouble(AlarmtextEdit12.Text);

                Color color1 = ColorTranslator.FromHtml(AlarmcolorPickEdit1.Text);
                Color color2 = ColorTranslator.FromHtml(AlarmcolorPickEdit2.Text);
                Color color3 = ColorTranslator.FromHtml(AlarmcolorPickEdit3.Text);
                Color color4 = ColorTranslator.FromHtml(AlarmcolorPickEdit4.Text);
                Color color5 = ColorTranslator.FromHtml(AlarmcolorPickEdit5.Text);
                Color color6 = ColorTranslator.FromHtml(AlarmcolorPickEdit6.Text);
                Color color7 = ColorTranslator.FromHtml(AlarmcolorPickEdit7.Text);
                Color color8 = ColorTranslator.FromHtml(AlarmcolorPickEdit8.Text);
                Color color9 = ColorTranslator.FromHtml(AlarmcolorPickEdit9.Text);
                Color color10 = ColorTranslator.FromHtml(AlarmcolorPickEdit10.Text);
                Color color11 = ColorTranslator.FromHtml(AlarmcolorPickEdit11.Text);
                Color color12 = ColorTranslator.FromHtml(AlarmcolorPickEdit12.Text);
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmForeRGB[0] = $"{color1.R},{color1.G},{color1.B}";
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmForeRGB[1] = $"{color2.R},{color2.G},{color2.B}";
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmForeRGB[2] = $"{color3.R},{color3.G},{color3.B}";
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmForeRGB[3] = $"{color4.R},{color4.G},{color4.B}";
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmForeRGB[4] = $"{color5.R},{color5.G},{color5.B}";
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmForeRGB[5] = $"{color6.R},{color6.G},{color6.B}";
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmForeRGB[6] = $"{color7.R},{color7.G},{color7.B}";
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmForeRGB[7] = $"{color8.R},{color8.G},{color8.B}";
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmForeRGB[8] = $"{color9.R},{color9.G},{color9.B}";
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmForeRGB[9] = $"{color10.R},{color10.G},{color10.B}";
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmForeRGB[10] = $"{color11.R},{color11.G},{color11.B}";
                SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting.AlarmForeRGB[11] = $"{color12.R},{color12.G},{color12.B}";

                /*(整合過)顏色變更*/
                SettingButtonUserControl.StraightSenserForm.GIAScreenUserControl.Change_ScreenMedia(SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting);
                SettingButtonUserControl.StraightSenserForm.MarqueeUserControl.Change_MarqueeColor();
                /*復原GIA切換畫面旗標*/
                SettingButtonUserControl.StraightSenserForm.GIAScreenUserControl.LockFlag = SettingButtonUserControl.AfterLockFlag;
                /*存取修改後的畫面JSON*/
                InitialMethod.Save_ScreenMedia(SettingButtonUserControl.StraightSenserForm.ScreenMediaSetting);
            }
            SettingButtonUserControl.FlyoutFlag = false;
            SettingButtonUserControl.flyout.Close();
            /*結束等待畫面*/
            CloseProgressPanel(handle);
        }

        private void AlarmtoggleSwitch_Toggled(object sender, EventArgs e)
        {
            AlarmtextEdit1.Enabled = AlarmtoggleSwitch.IsOn;
            AlarmtextEdit2.Enabled = AlarmtoggleSwitch.IsOn;
            AlarmtextEdit3.Enabled = AlarmtoggleSwitch.IsOn;
            AlarmtextEdit4.Enabled = AlarmtoggleSwitch.IsOn;
            AlarmtextEdit5.Enabled = AlarmtoggleSwitch.IsOn;
            AlarmtextEdit6.Enabled = AlarmtoggleSwitch.IsOn;
            AlarmtextEdit7.Enabled = AlarmtoggleSwitch.IsOn;
            AlarmtextEdit8.Enabled = AlarmtoggleSwitch.IsOn;
            AlarmtextEdit9.Enabled = AlarmtoggleSwitch.IsOn;
            AlarmtextEdit10.Enabled = AlarmtoggleSwitch.IsOn;
            AlarmtextEdit11.Enabled = AlarmtoggleSwitch.IsOn;
            AlarmtextEdit12.Enabled = AlarmtoggleSwitch.IsOn;

            AlarmcolorPickEdit1.Enabled = AlarmtoggleSwitch.IsOn;
            AlarmcolorPickEdit2.Enabled = AlarmtoggleSwitch.IsOn;
            AlarmcolorPickEdit3.Enabled = AlarmtoggleSwitch.IsOn;
            AlarmcolorPickEdit4.Enabled = AlarmtoggleSwitch.IsOn;
            AlarmcolorPickEdit5.Enabled = AlarmtoggleSwitch.IsOn;
            AlarmcolorPickEdit6.Enabled = AlarmtoggleSwitch.IsOn;
            AlarmcolorPickEdit7.Enabled = AlarmtoggleSwitch.IsOn;
            AlarmcolorPickEdit8.Enabled = AlarmtoggleSwitch.IsOn;
            AlarmcolorPickEdit9.Enabled = AlarmtoggleSwitch.IsOn;
            AlarmcolorPickEdit10.Enabled = AlarmtoggleSwitch.IsOn;
            AlarmcolorPickEdit11.Enabled = AlarmtoggleSwitch.IsOn;
            AlarmcolorPickEdit12.Enabled = AlarmtoggleSwitch.IsOn;
        }
    }
}
