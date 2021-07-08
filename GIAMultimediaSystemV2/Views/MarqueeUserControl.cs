using DevExpress.XtraEditors;
using GIAMultimediaSystemV2.Configuration;
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

namespace GIAMultimediaSystemV2.Views
{
    public partial class MarqueeUserControl : Field4UserControl
    {
        public int Index = 0;
        public MarqueeUserControl(MarqueeSetting marqueeSetting)
        {
            InitializeComponent();

            MarqueelabelControl.Text = marqueeSetting.MarqueeStr;
            //MarqueelabelControl.Location = new Point(MarqueepanelControl.Size.Width + 1,2);
            MarqueelabelControl.Location = new Point(1921, 13);
            timer1.Interval = 20;
            timer1.Enabled = true;
        }
        private async void timer1_Tick(object sender, EventArgs e)
        {

            //if (MarqueelabelControl.Location.Y > -33)
            //{

            //    if (MarqueelabelControl.Location.Y == 13)
            //    {
            //        timer1.Interval = 5000;
            //        if (Index == 1)
            //        {
            //            Index = 0;
            //            MarqueelabelControl.Location = new Point(0, MarqueelabelControl.Location.Y - 1);
            //            timer1.Interval = 50;
            //        }
            //        else
            //        {
            //            Index++;
            //        }
            //    }
            //    else
            //    {
            //        MarqueelabelControl.Location = new Point(0, MarqueelabelControl.Location.Y - 1);
            //    }

            //}
            //else
            //{
            //    MarqueelabelControl.Location = new Point(0, 71);
            //}
            //await Task.Delay(1000);
            Point x101 = MarqueelabelControl.Location;
            Size x102 = MarqueelabelControl.Size;
            Size x103 = MarqueepanelControl.Size;
            if (x102.Width + x101.X > 0)
            {
                MarqueelabelControl.Location = new Point(x101.X - 2, x101.Y);
            }
            else
            {
                MarqueelabelControl.Location = new Point(x103.Width, x101.Y);
            }
        }
        /// <summary>
        /// 改變跑馬燈字串
        /// </summary>
        public void Change_MarqueeText()
        {
            MarqueelabelControl.Location = new Point(MarqueepanelControl.Size.Width + 1, 2);
            MarqueelabelControl.Text = MarqueeSetting.MarqueeStr;
        }
    }
}
