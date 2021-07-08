using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using GIAMultimediaSystemV2.Configuration;
using GIAMultimediaSystemV2.Enums;
using GIAMultimediaSystemV2.Protocols;
using GIAMultimediaSystemV2.Views.WeathcrViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GIAMultimediaSystemV2.Views.GIAViews
{
    public partial class GIAScreenUserControl : Field4UserControl
    {
        /// <summary>
        /// 切換畫面頁數
        /// </summary>
        private int PageIndex { get; set; } = 0;
        /// <summary>
        /// 天氣畫面
        /// </summary>
        public WeatherUserControl WeatherUserControl { get; set; }
        /// <summary>
        /// 切換畫面最後時間
        /// </summary>
        private DateTime PageTime { get; set; }
        private GateWaySenserID GateWaySenserID { get; set; }
        /// <summary>
        /// (大)總畫面物件
        /// </summary>
        private List<Field4UserControl> BigUserControls { get; set; } = new List<Field4UserControl>();
        /// <summary>
        /// (小)總畫面物件
        /// </summary>
        private List<Field4UserControl> SmallUserControls { get; set; } = new List<Field4UserControl>();
        /// <summary>
        /// 畫面切換鎖定 True = 不鎖定 ,False = 鎖定
        /// </summary>
        public bool LockFlag { get; set; } = true;
        /// <summary>
        public GIAScreenUserControl(GateWay gateWay, List<Taiwan_DistricsSetting> taiwan_DistricsSetting, ScreenMediaSetting screenMediaSetting, List<AbsProtocol> absProtocols)
        {
            InitializeComponent();
            pictureBox1.Image = Image.FromFile($"{MyWorkPath}\\Images\\欣寶-空氣品質看板UI底圖.png");
            Bignavigation.Parent = pictureBox1;
            Smallnavigation1.Parent = pictureBox1;
            Smallnavigation2.Parent = pictureBox1;
            Smallnavigation3.Parent = pictureBox1;
            Smallnavigation4.Parent = pictureBox1;
            Smallnavigation5.Parent = pictureBox1;
            Smallnavigation6.Parent = pictureBox1;

            radioGroup1.Visible = false;
            radioGroup2.Visible = false;
            radioGroup3.Visible = false;
            radioGroup4.Visible = false;
            radioGroup5.Visible = false;
            radioGroup6.Visible = false;
            radioGroup7.Visible = false;

            ScreenMediaSetting = screenMediaSetting;
            GateWay = gateWay;
            AbsProtocols = absProtocols;
            foreach (var item in GateWay.GateWaySenserIDs)
            {
                SenserEnumType senserEnumType = (SenserEnumType)item.SenserEnumType;
                switch (senserEnumType)
                {
                    case SenserEnumType.BlackSenser:
                    case SenserEnumType.WhiteSenser:
                        break;
                    case SenserEnumType.WeatherAPI:
                        {
                            WeatherUserControl = new WeatherUserControl(GateWay, taiwan_DistricsSetting, item, AbsProtocols) { Dock = DockStyle.Fill };
                            WeatherpanelControl.Controls.Add(WeatherUserControl);
                        }
                        break;
                    case SenserEnumType.GIAAPI:
                        {
                            GateWaySenserID = item;
                        }
                        break;
                    case SenserEnumType.GIA:
                        {
                            GateWaySenserID = item;
                        }
                        break;
                }
            }
            Change_ScreenMedia(screenMediaSetting);
        }
        #region 變更感測器顯示
        /// <summary>
        /// 變更感測器顯示
        /// </summary>
        /// <param name="screen"></param>
        public void Change_ScreenMedia(ScreenMediaSetting screen)
        {
            if (BigUserControls.Count > 0)
            {
                BigUserControls.Clear();
            }
            if (SmallUserControls.Count > 0)
            {
                SmallUserControls.Clear();
            }
            Change_BigScreenMedia(screen, Bignavigation);
            Change_SmallScreenMedia(screen, Smallnavigation1, 1);
            Change_SmallScreenMedia(screen, Smallnavigation2, 2);
            Change_SmallScreenMedia(screen, Smallnavigation3, 3);
            Change_SmallScreenMedia(screen, Smallnavigation4, 4);
            Change_SmallScreenMedia(screen, Smallnavigation5, 5);
            Change_SmallScreenMedia(screen, Smallnavigation6, 6);
            PageTime = DateTime.Now;
            #region 切換畫面
            PageIndex = 0;
            Bignavigation.SelectedPageIndex = PageIndex;
            Smallnavigation1.SelectedPageIndex = PageIndex;
            Smallnavigation2.SelectedPageIndex = PageIndex;
            Smallnavigation3.SelectedPageIndex = PageIndex;
            Smallnavigation4.SelectedPageIndex = PageIndex;
            Smallnavigation5.SelectedPageIndex = PageIndex;
            Smallnavigation6.SelectedPageIndex = PageIndex;
            #endregion
        }
        /// <summary>
        /// 大畫面感測器
        /// </summary>
        /// <param name="screen">畫面資訊</param>
        /// <param name="navigation">切換畫面物件</param>
        private void Change_BigScreenMedia(ScreenMediaSetting screen, NavigationFrame navigation)
        {
            if (navigation.Pages.Count > 0)
            {
                navigation.Pages.Clear();
            }
            if (!screen.ScreenSwitches[0].VisibleFlag1 && !screen.ScreenSwitches[0].VisibleFlag2)
            {
                navigation.Parent.Visible = false;
            }
            else
            {
                navigation.Parent.Visible = true;
                if (screen.ScreenSwitches[0].VisibleFlag1)
                {
                    GIABigUserControl bigSenser = new GIABigUserControl(screen.ScreenSwitches[0].SenserTypeEnum1, GateWay, GateWaySenserID, screen) { Dock = DockStyle.Fill };
                    BigUserControls.Add(bigSenser);
                    navigation.AddPage(bigSenser);
                }
                if (screen.ScreenSwitches[0].VisibleFlag2)
                {
                    GIABigUserControl bigSenser = new GIABigUserControl(screen.ScreenSwitches[0].SenserTypeEnum2, GateWay, GateWaySenserID, screen) { Dock = DockStyle.Fill };
                    BigUserControls.Add(bigSenser);
                    navigation.AddPage(bigSenser);
                }
                navigation.SelectedPageIndex = 0;
            }
        }
        /// <summary>
        /// 小畫面感測器
        /// </summary>
        /// <param name="screen">畫面資訊</param>
        /// <param name="navigation">切換畫面物件</param>
        /// <param name="Index">小畫面編號</param>
        private void Change_SmallScreenMedia(ScreenMediaSetting screen, NavigationFrame navigation, int Index)
        {
            if (navigation.Pages.Count > 0)
            {
                navigation.Pages.Clear();
            }
            if (!screen.ScreenSwitches[Index].VisibleFlag1 && !screen.ScreenSwitches[Index].VisibleFlag2)
            {
                navigation.Parent.Visible = false;
            }
            else
            {
                navigation.Parent.Visible = true;
                if (screen.ScreenSwitches[Index].VisibleFlag1)
                {
                    GIASmallUserControl smallSenser = new GIASmallUserControl(screen.ScreenSwitches[Index].SenserTypeEnum1, GateWay, GateWaySenserID, screen) { Dock = DockStyle.Fill };
                    SmallUserControls.Add(smallSenser);
                    navigation.AddPage(smallSenser);
                }
                if (screen.ScreenSwitches[Index].VisibleFlag2)
                {
                    GIASmallUserControl smallSenser = new GIASmallUserControl(screen.ScreenSwitches[Index].SenserTypeEnum2, GateWay, GateWaySenserID, screen) { Dock = DockStyle.Fill };
                    SmallUserControls.Add(smallSenser);
                    navigation.AddPage(smallSenser);
                }
                navigation.SelectedPageIndex = 0;
                PageIndex = 0;
            }
        }
        #endregion
        #region 顯示變更
        public override void TextChange()
        {
            #region 切換畫面功能
            TimeSpan timeSpan = DateTime.Now.Subtract(PageTime);
            if (timeSpan.TotalSeconds > ScreenMediaSetting.ChangePageSec)
            {
                if (LockFlag)
                {
                    if (PageIndex == 1)
                    {
                        PageIndex = 0;
                        //Bignavigation.SelectedPageIndex = PageIndex;
                        //Smallnavigation1.SelectedPageIndex = PageIndex;
                        //Smallnavigation2.SelectedPageIndex = PageIndex;
                        //Smallnavigation3.SelectedPageIndex = PageIndex;
                        //Smallnavigation4.SelectedPageIndex = PageIndex;
                        //Smallnavigation5.SelectedPageIndex = PageIndex;
                        //Smallnavigation6.SelectedPageIndex = PageIndex;
                    }
                    else
                    {
                        PageIndex++;
                        //if (Bignavigation.Pages.Count == 2)
                        //{
                        //    Bignavigation.SelectedPageIndex = PageIndex;
                        //}
                        //if (Smallnavigation1.Pages.Count == 2)
                        //{
                        //    Smallnavigation1.SelectedPageIndex = PageIndex;
                        //}
                        //if (Smallnavigation2.Pages.Count == 2)
                        //{
                        //    Smallnavigation2.SelectedPageIndex = PageIndex;
                        //}
                        //if (Smallnavigation3.Pages.Count == 2)
                        //{
                        //    Smallnavigation3.SelectedPageIndex = PageIndex;
                        //}
                        //if (Smallnavigation4.Pages.Count == 2)
                        //{
                        //    Smallnavigation4.SelectedPageIndex = PageIndex;
                        //}
                        //if (Smallnavigation5.Pages.Count == 2)
                        //{
                        //    Smallnavigation5.SelectedPageIndex = PageIndex;
                        //}
                        //if (Smallnavigation6.Pages.Count == 2)
                        //{
                        //    Smallnavigation6.SelectedPageIndex = PageIndex;
                        //}
                    }
                    radioGroup1.SelectedIndex = PageIndex;
                    radioGroup2.SelectedIndex = PageIndex;
                    radioGroup3.SelectedIndex = PageIndex;
                    radioGroup4.SelectedIndex = PageIndex;
                    radioGroup5.SelectedIndex = PageIndex;
                    radioGroup6.SelectedIndex = PageIndex;
                    radioGroup7.SelectedIndex = PageIndex;
                }
                PageTime = DateTime.Now;
            }
            #endregion
            if (AbsProtocols != null)
            {
                var bigsenser = (GIABigUserControl)Bignavigation.Pages[Bignavigation.SelectedPageIndex].Controls[0];
                bigsenser.AbsProtocols = AbsProtocols;
                bigsenser.TextChange();
                var smallsenser1 = (GIASmallUserControl)Smallnavigation1.Pages[Smallnavigation1.SelectedPageIndex].Controls[0];
                smallsenser1.AbsProtocols = AbsProtocols;
                smallsenser1.TextChange();
                var smallsenser2 = (GIASmallUserControl)Smallnavigation2.Pages[Smallnavigation2.SelectedPageIndex].Controls[0];
                smallsenser2.AbsProtocols = AbsProtocols;
                smallsenser2.TextChange();
                var smallsenser3 = (GIASmallUserControl)Smallnavigation3.Pages[Smallnavigation3.SelectedPageIndex].Controls[0];
                smallsenser3.AbsProtocols = AbsProtocols;
                smallsenser3.TextChange();
                var smallsenser4 = (GIASmallUserControl)Smallnavigation4.Pages[Smallnavigation4.SelectedPageIndex].Controls[0];
                smallsenser4.AbsProtocols = AbsProtocols;
                smallsenser4.TextChange();
                var smallsenser5 = (GIASmallUserControl)Smallnavigation5.Pages[Smallnavigation5.SelectedPageIndex].Controls[0];
                smallsenser5.AbsProtocols = AbsProtocols;
                smallsenser5.TextChange();
                var smallsenser6 = (GIASmallUserControl)Smallnavigation6.Pages[Smallnavigation6.SelectedPageIndex].Controls[0];
                smallsenser6.AbsProtocols = AbsProtocols;
                smallsenser6.TextChange();
                WeatherUserControl.TextChange();
            }
        }
        #endregion
    }
}
