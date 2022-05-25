using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIAMultimediaSystemV2.Configuration
{
    /// <summary>
    /// 畫面媒體設定
    /// </summary>
    public class ScreenMediaSetting
    {
        /// <summary>
        /// 天氣畫面顏色
        /// </summary>
        public string WeatherPanelRGB { get; set; }
        /// <summary>
        /// 天氣字體顏色
        /// </summary>
        public string WeatherForeRGB { get; set; }
        /// <summary>
        /// 感測器畫面顏色(大)
        /// </summary>
        public string BigSenserPanelRGB { get; set; }
        /// <summary>
        /// 感測器字體顏色(大)
        /// </summary>
        public string BigSenserForeRGB { get; set; }
        /// <summary>
        /// 感測器畫面顏色(小)
        /// </summary>
        public string SmallSenserPanelRGB { get; set; }
        /// <summary>
        /// 感測器字體顏色(小)
        /// </summary>
        public string SmallSenserForeRGB { get; set; }
        /// <summary>
        /// 跑馬燈畫面顏色
        /// </summary>
        public string MarqueePanelRGB { get; set; }
        /// <summary>
        /// 跑馬燈字體顏色
        /// </summary>
        public string MarqueeForeRGB { get; set; }
        /// <summary>
        /// GIA畫面顏色
        /// </summary>
        public string PanelRGB { get; set; }
        /// <summary>
        /// GIA切換畫面秒數
        /// </summary>
        public int ChangePageSec { get; set; }
        /// <summary>
        /// GIA字體顏色
        /// </summary>
        public string ForeRGB { get; set; }
        /// <summary>
        /// Logo位址
        /// </summary>
        public string LogoPath { get; set; }
        /// <summary>
        /// 超標功能旗標
        /// </summary>
        public bool AlarmFlag { get; set; }
        /// <summary>
        /// 超標數值
        /// </summary>
        public double[] AlarmValue { get; set; } = new double[12];
        /// <summary>
        /// 超標顯示顏色
        /// </summary>
        public string[] AlarmForeRGB { get; set; } = new string[12];
        public List<ScreenSwitch> ScreenSwitches { get; set; } = new List<ScreenSwitch>();
    }
    /// <summary>
    /// 畫面資訊
    /// </summary>
    public class ScreenSwitch
    {
        /// <summary>
        /// 畫面編號
        /// </summary>
        public int ScreenIndex { get; set; }
        /// <summary>
        /// 第一組顯示旗標
        /// </summary>
        public bool VisibleFlag1 { get; set; }
        /// <summary>
        /// 第一組感測器類型
        /// </summary>
        public int SenserTypeEnum1 { get; set; }
        /// <summary>
        /// 第二組顯示旗標
        /// </summary>
        public bool VisibleFlag2 { get; set; }
        /// <summary>
        /// 第二組感測器類型
        /// </summary>
        public int SenserTypeEnum2 { get; set; }
    }
}
