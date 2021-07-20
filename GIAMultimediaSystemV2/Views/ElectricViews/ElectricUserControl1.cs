using DevExpress.XtraEditors;
using GIAMultimediaSystemV2.Configuration;
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
    public partial class ElectricUserControl1 : Field4UserControl
    {
        /// <summary>
        /// 數值
        /// </summary>
        public decimal Value { get; set; }
        public int DataIndex { get; set; }
        public int CircelIndex { get; set; }
        /// <summary>
        /// 顏色改變
        /// </summary>
        private Color NewColor { get; set; } = Color.WhiteSmoke;
        /// <summary>
        /// 電量/金額數值顯示
        /// </summary>
        /// <param name="value">數值</param>
        /// <param name="dataIndex">顯示單位
        /// <para> 0 = 度，1 = 元</para></param>
        public ElectricUserControl1(int dataIndex,GroupSetting groupSetting)
        {
            InitializeComponent();
            GroupSetting = groupSetting;
            if (dataIndex == 0)
            {
                pictureEdit.Image = LogoimageCollection.Images[dataIndex];
                TitlelabelControl.Text = "總用電度";
                UnitlabelControl.Text = "度";
            }
            else
            {
                pictureEdit.Image = LogoimageCollection.Images[dataIndex];
                TitlelabelControl.Text = "總金額";
                UnitlabelControl.Text = "元";
            }
            DataIndex = dataIndex;
        }

        public override void TextChange()
        {
            if (CircelIndex == 0)
            {
                if (DataIndex == 0)
                {
                    TitlelabelControl.Text = "總用電度";
                }
                else
                {
                    TitlelabelControl.Text = "總金額";
                }
            }
            else
            {
                if (DataIndex == 0)
                {
                    TitlelabelControl.Text = $"{GroupSetting.Groups[CircelIndex-1].GroupName}用電度";
                }
                else
                {
                    TitlelabelControl.Text = $"{GroupSetting.Groups[CircelIndex - 1].GroupName}總金額";
                }
            }
            ValuelabelControl.Text = Value.ToString("0.##");
        }
        #region 圖片顏色變更
        private void LeftpictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            using (Bitmap bmp = new Bitmap($"{MyWorkPath}\\Images\\Electric_left.png"))
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
            using (Bitmap bmp = new Bitmap($"{MyWorkPath}\\Images\\Electric_right.png"))
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
