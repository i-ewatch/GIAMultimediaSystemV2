using DevExpress.XtraEditors;
using GIAMultimediaSystemV2.Configuration;
using GIAMultimediaSystemV2.Protocols;
using GIAMultimediaSystemV2.Protocols.Senser;
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

namespace GIAMultimediaSystemV2.Views.WeathcrViews
{
    public partial class WeatherUserControl1 : Field4UserControl
    {
        /// <summary>
        /// 0 = 新茂天氣資訊
        /// 1 = GIA天氣資訊
        /// </summary>
        private int WeatherIndex = 1;
        /// <summary>
        /// senser通訊類型與設備編號
        /// </summary>
        private GateWaySenserID GateWaySenserID { get; set; }
        public WeatherUserControl1(GateWay gateWay, List<Taiwan_DistricsSetting> taiwan_DistricsSetting, GateWaySenserID gateWaySenserID, List<AbsProtocol> absProtocols, GIA_DistricsSetting gIA_DistricsSetting)
        {
            InitializeComponent();
            ImagePictureEdit.Tag = "0";
            UnitlabelControl.Text = "\xb0" + "C";
            GateWay = gateWay;
            Taiwan_DistricsSetting = taiwan_DistricsSetting;
            GateWaySenserID = gateWaySenserID;
            AbsProtocols = absProtocols;
            GIA_DistricsSetting = gIA_DistricsSetting;
            CitylabelControl.Text = $"{gateWay.DistrictName}";
            switch (WeatherIndex)
            {
                case 0:
                    {
                        #region 新茂天氣資訊
                        var ListArea = taiwan_DistricsSetting.Where(g => g.CityName == gateWay.LocationName).Select(v => v.AreaList).Single();
                        var AreaENGName = ListArea.SingleOrDefault(g => g.AreaName == gateWay.DistrictName);
                        if (AreaENGName != null)
                        {
                            DistlabelControl.Text = $"{AreaENGName.AreaEngName}";
                        }
                        #endregion
                    }
                    break;
                case 1:
                    {
                        #region GIA天氣資訊
                        var AreaENGName = GIA_DistricsSetting.data.SingleOrDefault(g => g.alias == gateWay.DistrictName);
                        if (AreaENGName != null)
                        {
                            DistlabelControl.Text = $"{AreaENGName.uuid}";
                        }
                        #endregion
                    }
                    break;
            }   
            pictureEdit1.Image = imageCollection1.Images[0];                //日期
            pictureEdit2.Image = imageCollection1.Images[1];                //日期
            TImelabelControl.Text = $"{DateTime.Now:HH:mm}";
            DaylabelControl.Text = $"{DateTime.Now:yyyy/MM/dd}";
            WeeklabelControl.Text = $"{DateTime.Now:ddd}";
        }
        public override void TextChange()
        {
            TImelabelControl.Text = $"{DateTime.Now:HH:mm}";
            DaylabelControl.Text = $"{DateTime.Now:yyyy/MM/dd}";
            WeeklabelControl.Text = $"{DateTime.Now:ddd}";
            try
            {
                var WeatherAbsProtocol = AbsProtocols.SingleOrDefault(g => g.GatewayIndex == GateWay.GatewayIndex & g.DeviceIndex == GateWaySenserID.DeviceIndex);
                if (WeatherAbsProtocol != null)
                {
                    SenserData data = (SenserData)WeatherAbsProtocol;
                    if (data != null)
                    {
                        switch (WeatherIndex)
                        {
                            case 0:
                                {
                                    #region 新茂天氣資訊
                                    if (data.EwatchWeather != null && data.ConnectFlag)
                                    {
                                        TemperaturelabelControl.Text = $"{data.EwatchWeather.t}";
                                        HumiditylabelControl.Text = $"{data.EwatchWeather.rh}";
                                        if (DateTime.Now.Hour >= 18)
                                        {
                                            if (data.EwatchWeather.wx != null)
                                            {
                                                if (ImagePictureEdit.Tag.ToString() != data.EwatchWeather.wx_Code.ToString())
                                                {
                                                    if (File.Exists($"{MyWorkPath}\\Images\\night\\{data.EwatchWeather.wx_Code}.png"))
                                                    {
                                                        ImagePictureEdit.Image = Image.FromFile($"{MyWorkPath}\\Images\\night\\{data.EwatchWeather.wx_Code}.png");
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (data.EwatchWeather.wx != null)
                                            {
                                                if (ImagePictureEdit.Tag.ToString() != data.EwatchWeather.wx_Code.ToString())
                                                {
                                                    if (File.Exists($"{MyWorkPath}\\Images\\day\\{data.EwatchWeather.wx_Code}.png"))
                                                    {
                                                        ImagePictureEdit.Image = Image.FromFile($"{MyWorkPath}\\Images\\day\\{data.EwatchWeather.wx_Code}.png");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                }
                                break;
                            case 1:
                                {
                                    #region GIA天氣資訊
                                    if (data.GIAWeatherData != null && data.GIAWeatherData.data != null)
                                    {
                                        TemperaturelabelControl.Text = $"{data.GIAWeatherData.data.temperature}";
                                        HumiditylabelControl.Text = $"{data.GIAWeatherData.data.humidity}";
                                    }
                                    #endregion
                                }
                                break;
                        }            
                    }
                    #region 中央氣象天氣資訊
                    //TemperaturelabelControl.Text = $"{data.T}";
                    //HumiditylabelControl.Text = $"{data.RH}";
                    //if (DateTime.Now.Hour >= 18)
                    //{
                    //    if (data.WxIndex != null)
                    //    {
                    //        if (ImagePictureEdit.Tag.ToString() != data.WxIndex.ToString())
                    //        {
                    //            if (File.Exists($"{MyWorkPath}\\Images\\night\\{data.WxIndex}.png"))
                    //            {
                    //                ImagePictureEdit.Image = Image.FromFile($"{MyWorkPath}\\Images\\night\\{data.WxIndex}.png");
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    if (data.WxIndex != null)
                    //    {
                    //        if (ImagePictureEdit.Tag.ToString() != data.WxIndex.ToString())
                    //        {
                    //            if (File.Exists($"{MyWorkPath}\\Images\\day\\{data.WxIndex}.png"))
                    //            {
                    //                ImagePictureEdit.Image = Image.FromFile($"{MyWorkPath}\\Images\\day\\{data.WxIndex}.png");
                    //            }
                    //        }
                    //    }
                    //}
                    #endregion
                }
            }
            catch (Exception) { }
        }
    }
}
