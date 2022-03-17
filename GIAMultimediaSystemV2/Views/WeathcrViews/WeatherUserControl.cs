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
    public partial class WeatherUserControl : Field4UserControl
    {
        /// <summary>
        /// senser通訊類型與設備編號
        /// </summary>
        private GateWaySenserID GateWaySenserID { get; set; }
        public WeatherUserControl(GateWay gateWay, List<Taiwan_DistricsSetting> taiwan_DistricsSetting, GateWaySenserID gateWaySenserID, List<AbsProtocol> absProtocols)
        {
            InitializeComponent();
            ImagePictureEdit.Image = imageCollection1.Images[2];
            ImagePictureEdit1.Tag = "0";
            UnitlabelControl.Text = "\xb0" + "C";
            GateWay = gateWay;
            Taiwan_DistricsSetting = taiwan_DistricsSetting;
            GateWaySenserID = gateWaySenserID;
            AbsProtocols = absProtocols;
            var ListArea = taiwan_DistricsSetting.Where(g => g.CityName == gateWay.LocationName).Select(v => v.AreaList).Single();
            var AreaENGName = ListArea.Where(g => g.AreaName == gateWay.DistrictName).Select(v => v.AreaName).Single();
            CitylabelControl.Text = $"{AreaENGName}";
            DaypictureEdit.Image = imageCollection1.Images[0];
            ImagePictureEdit2.Image = imageCollection1.Images[1];
            TImelabelControl.Text = $"{DateTime.Now:HH:mm}";
            DaylabelControl.Text = $"{DateTime.Now:yyyy年MM月dd日},{DateTime.Now:ddd}";
        }
        public override void TextChange()
        {
            TImelabelControl.Text = $"{DateTime.Now:HH:mm}";
            DaylabelControl.Text = $"{DateTime.Now:yyyy年MM月dd日},{DateTime.Now:ddd}";
            var WeatherAbsProtocol = AbsProtocols.SingleOrDefault(g => g.GatewayIndex == GateWay.GatewayIndex & g.DeviceIndex == GateWaySenserID.DeviceIndex);
            if (WeatherAbsProtocol != null)
            {
                SenserData data = (SenserData)WeatherAbsProtocol;
                TemperaturelabelControl.Text = $"{data.T}";
                HumiditylabelControl.Text = $"{data.RH}";
                if (DateTime.Now.Hour >= 18)
                {
                    if (data.WxIndex != null)
                    {
                        if (ImagePictureEdit1.Tag.ToString() != data.WxIndex.ToString())
                        {
                            if (File.Exists($"{MyWorkPath}\\Images\\night\\{data.WxIndex}.png"))
                            {
                                ImagePictureEdit1.Image = Image.FromFile($"{MyWorkPath}\\Images\\night\\{data.WxIndex}.png");
                            }
                        }
                    }
                }
                else
                {
                    if (data.WxIndex != null)
                    {
                        if (ImagePictureEdit1.Tag.ToString() != data.WxIndex.ToString())
                        {
                            if (File.Exists($"{MyWorkPath}\\Images\\day\\{data.WxIndex}.png"))
                            {
                                ImagePictureEdit1.Image = Image.FromFile($"{MyWorkPath}\\Images\\day\\{data.WxIndex}.png");
                            }
                        }
                    }
                }
            }
        }
    }
}
