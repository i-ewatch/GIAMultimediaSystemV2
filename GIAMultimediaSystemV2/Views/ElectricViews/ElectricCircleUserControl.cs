using DevExpress.XtraEditors;
using DevExpress.XtraGauges.Core.Drawing;
using DevExpress.XtraGauges.Core.Model;
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

namespace GIAMultimediaSystemV2.Views.ElectricViews
{
    public partial class ElectricCircleUserControl : Field4UserControl
    {
        private ElectricOtherUserControl ElectricOtherUserControl { get; set; }
        private Color NewColor;
        public decimal TotalValue { get; set; } = 100;
        public decimal Value { get; set; }
        public int CircelIndex { get; set; }
        public ElectricCircleUserControl(ElectricOtherUserControl electricOtherUserControl, Color newColor, string name,int circelIndex)
        {
            InitializeComponent();
            arcScaleComponent1.EnableAnimation = true;
            arcScaleComponent1.EasingMode = EasingMode.EaseIn;
            arcScaleComponent1.EasingFunction = new CubicEase();
            ElectricOtherUserControl = electricOtherUserControl;
            CircelIndex = circelIndex;
            NewColor = newColor;
            gaugeControl1.ColorScheme.Color = NewColor;
            TitallabelControl.Text = name;
            TitallabelControl.Appearance.ForeColor = NewColor;
        }
        public override void TextChange()
        {
            var data = Value / TotalValue;
            labelComponent1.Text = $"{Convert.ToInt32(data * 100)}";
            arcScaleRangeBarComponent1.Value = Convert.ToInt32(data * 100);
        }

        private void TitallabelControl_Click(object sender, EventArgs e)
        {
            ElectricOtherUserControl.CircelIndex = CircelIndex;
        }
    }
}
