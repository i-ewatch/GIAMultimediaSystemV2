using DevExpress.XtraEditors;
using GIAMultimediaSystemV2.Configuration;
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

namespace GIAMultimediaSystemV2.Views.ElectricViews
{
    public partial class ElectricUserControl : Field4UserControl
    {
        public ElectricUserControl(SqlDBSetting setting, bool kwh_Price_Flag, GateWaySetting gateWaySetting, GroupSetting groupSetting, int groupIndex, SqlMethod sqlMethod)
        {
            InitializeComponent();
            Kwh_Price_Flag = kwh_Price_Flag;
            ElectricMeterPriceFlag = setting.ElectricMeterPriceFlag;
            GateWaySetting = gateWaySetting;
            GroupIndex = groupIndex;
            SqlMethod = sqlMethod;
            if (Kwh_Price_Flag)
            {
                TitlelabelControl.Text = groupSetting.Groups[GroupIndex-1].GroupName + " 用電度";
                UnitlabelControl.Text = "Kwh";
            }
            else
            {
                TitlelabelControl.Text = groupSetting.Groups[GroupIndex-1].GroupName + " 金額";
                UnitlabelControl.Text = "元";
            }
        }
        private bool Kwh_Price_Flag { get; set; }
        private bool ElectricMeterPriceFlag { get; set; }
        private int GroupIndex { get; set; }
        public override void TextChange()
        {
            if (ElectricMeterPriceFlag)
            {
                var data = SqlMethod.Serch_TotalMeter_ElectricDailykwh(GateWaySetting,GroupIndex);
                if (Kwh_Price_Flag)
                {
                    ValuelabelControl.Text = data.Total.ToString("0.##");
                }
                else
                {
                    ValuelabelControl.Text = data.MoneyTotal.ToString("0.##");
                }
            }
            else
            {
                var data = SqlMethod.Serch_TotalMeter_ElectricTotalPrice(GateWaySetting, GroupIndex);
                if (Kwh_Price_Flag)
                {
                    ValuelabelControl.Text = data.KwhTotal.ToString("0.##");
                }
                else
                {
                    ValuelabelControl.Text = data.Price.ToString("0.##");
                }
            }                   
        }
    }
}
