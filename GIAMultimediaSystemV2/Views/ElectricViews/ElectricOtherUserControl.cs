using DevExpress.XtraEditors;
using GIAMultimediaSystemV2.Configuration;
using GIAMultimediaSystemV2.Methods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GIAMultimediaSystemV2.Views.ElectricViews
{
    public partial class ElectricOtherUserControl : Field4UserControl
    {
        /// <summary>
        /// 顏色改變
        /// </summary>
        private Color NewColor { get; set; } = Color.WhiteSmoke;

        private List<ElectricCircleUserControl> ElectricCircleUserControls = new List<ElectricCircleUserControl>();
        private List<ElectricUserControl1> ElectricUserControl1s = new List<ElectricUserControl1>();
        private int circelIndex { get; set; } = 0;
        public DateTime CircelTime { get; set; }
        public int CircelIndex
        {
            get { return circelIndex; }
            set
            {
                if (value != circelIndex)
                {
                    circelIndex = value;
                    CircelTime = DateTime.Now;
                }
            }
        }
        private List<decimal> value { get; set; } = new List<decimal>();
        private List<decimal> pricevalue { get; set; } = new List<decimal>();

        private decimal Money { get; set; }
        private decimal kwh { get; set; }
        public ElectricOtherUserControl(SqlMethod sqlMethod, GroupSetting groupSetting, GateWaySetting gateWaySetting)
        {
            InitializeComponent();
            GroupSetting = groupSetting;
            GateWaySetting = gateWaySetting;
            SqlMethod = sqlMethod;
            value.Clear();
            pricevalue.Clear();
            foreach (var item in groupSetting.Groups)
            {
                if (value.Count < 4)
                {
                    var data = sqlMethod.Serch_TotalMeter_Circel(GateWaySetting, item.GroupIndex, 0);
                    var Pricedata = sqlMethod.Serch_TotalMeter_Circel(GateWaySetting, item.GroupIndex, 1);
                    value.Add(data);
                    pricevalue.Add(Pricedata);
                }
            }
            for (int i = 0; i < value.Count; i++)
            {
                kwh = kwh + value[i];
                Money = Money + pricevalue[i];
            }
            ElectricUserControl1 electric1 = new ElectricUserControl1(0, GroupSetting);
            ElectricUserControl1 electric2 = new ElectricUserControl1(1, GroupSetting);
            ElectricUserControl1s.Add(electric1);
            panelControl3.Controls.Add(electric1);
            ElectricUserControl1s.Add(electric2);
            panelControl4.Controls.Add(electric2);
            foreach (var item in groupSetting.Groups)
            {
                if (ElectricCircleUserControls.Count < 4)
                {
                    switch (ElectricCircleUserControls.Count)
                    {
                        case 0:
                            {
                                ElectricCircleUserControl control = new ElectricCircleUserControl(this, Color.FromArgb(33, 174, 141), item.GroupName, ElectricCircleUserControls.Count + 1) { Location = new Point(80, 10) };
                                ElectricCircleUserControls.Add(control);
                                ElectricCircelpanelControl.Controls.Add(control);
                            }
                            break;
                        case 1:
                            {
                                ElectricCircleUserControl control = new ElectricCircleUserControl(this, Color.FromArgb(103, 187, 223), item.GroupName, ElectricCircleUserControls.Count + 1) { Location = new Point(170 + 120, 10) };
                                ElectricCircleUserControls.Add(control);
                                ElectricCircelpanelControl.Controls.Add(control);
                            }
                            break;
                        case 2:
                            {
                                ElectricCircleUserControl control = new ElectricCircleUserControl(this, Color.FromArgb(240, 93, 125), item.GroupName, ElectricCircleUserControls.Count + 1) { Location = new Point(80 + 0, 140) };
                                ElectricCircleUserControls.Add(control);
                                ElectricCircelpanelControl.Controls.Add(control);
                            }
                            break;
                        case 3:
                            {
                                ElectricCircleUserControl control = new ElectricCircleUserControl(this, Color.FromArgb(254, 151, 10), item.GroupName, ElectricCircleUserControls.Count + 1) { Location = new Point(170 + 120, 140) };
                                ElectricCircleUserControls.Add(control);
                                ElectricCircelpanelControl.Controls.Add(control);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        public override void TextChange()
        {
            int Index = 0;
            kwh = 0;
            Money = 0;
            value.Clear();
            pricevalue.Clear();
            foreach (var item in GroupSetting.Groups)
            {
                if (value.Count < 4)
                {
                    var data = SqlMethod.Serch_TotalMeter_Circel(GateWaySetting, item.GroupIndex, 0);
                    var Pricedata = SqlMethod.Serch_TotalMeter_Circel(GateWaySetting, item.GroupIndex, 1);
                    value.Add(data);
                    pricevalue.Add(Pricedata);
                }
            }
            for (int i = 0; i < value.Count; i++)
            {
                kwh = kwh + value[i];
                Money = Money + pricevalue[i];
            }
            foreach (var item in ElectricCircleUserControls)
            {
                if (kwh == 0)
                {
                    item.TotalValue = 100;
                }
                else
                {
                    item.TotalValue = kwh;
                }
                item.Value = value[Index];
                item.TextChange();
                Index++;
            }
            TimeSpan timeSpan = DateTime.Now.Subtract(CircelTime);
            if (circelIndex != 0)
            {
                foreach (var item in ElectricUserControl1s)
                {
                    item.CircelIndex = CircelIndex;
                    if (item.DataIndex == 0)
                    {
                        item.Value = value[circelIndex - 1];
                    }
                    else
                    {
                        item.Value = pricevalue[circelIndex - 1];
                    }
                    item.TextChange();
                }
                if (timeSpan.TotalSeconds > 30)
                {
                    circelIndex = 0;
                    foreach (var item in ElectricUserControl1s)
                    {
                        item.CircelIndex = CircelIndex;
                        if (item.DataIndex == 0)
                        {
                            item.Value = kwh;
                        }
                        else
                        {
                            item.Value = Money;
                        }
                        item.TextChange();
                    }
                }
            }
            else if (circelIndex == 0)
            {
                foreach (var item in ElectricUserControl1s)
                {
                    item.CircelIndex = CircelIndex;
                    if (item.DataIndex == 0)
                    {
                        item.Value = kwh;
                    }
                    else
                    {
                        item.Value = Money;
                    }
                    item.TextChange();
                }
            }
        }
        #region 圖片顏色變更
        private void LeftpictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            using (Bitmap bmp = new Bitmap($"{MyWorkPath}\\Images\\Circel_left.png"))
            {
                ColorMap[] colorMaps = new ColorMap[1];
                colorMaps[0] = new ColorMap();
                colorMaps[0].OldColor = Color.FromArgb(255, 255, 255);
                colorMaps[0].NewColor = NewColor;
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetRemapTable(colorMaps);
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                g.DrawImage(bmp, rect, 0, 0, rect.Width, rect.Height, GraphicsUnit.Pixel, attributes);
            }
        }

        private void RightpictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            using (Bitmap bmp = new Bitmap($"{MyWorkPath}\\Images\\Circel_right.png"))
            {
                ColorMap[] colorMaps = new ColorMap[1];
                colorMaps[0] = new ColorMap();
                colorMaps[0].OldColor = Color.FromArgb(255, 255, 255);
                colorMaps[0].NewColor = NewColor;
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetRemapTable(colorMaps);
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                g.DrawImage(bmp, rect, 0, 0, rect.Width, rect.Height, GraphicsUnit.Pixel, attributes);
            }
        }
        #endregion
    }
}
