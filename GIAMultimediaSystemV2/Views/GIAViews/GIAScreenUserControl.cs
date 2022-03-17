using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using GIAMultimediaSystemV2.Configuration;
using GIAMultimediaSystemV2.Enums;
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

namespace GIAMultimediaSystemV2.Views.GIAViews
{
    public partial class GIAScreenUserControl : Field4UserControl
    {
        /// <summary>
        /// senser通訊類型與設備編號
        /// </summary>
        private GateWaySenserID GateWaySenserID { get; set; }
        /// <summary>
        /// 天氣通訊類型與設備編號
        /// </summary>
        private GateWaySenserID WGateWaySenserID { get; set; }
        /// <summary>
        /// 切換畫面頁數
        /// </summary>
        private int PageIndex { get; set; } = 0;
        /// <summary>
        /// 切換畫面最後時間
        /// </summary>
        private DateTime PageTime { get; set; }
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
        public GIAScreenUserControl(GateWay gateWay, List<Taiwan_DistricsSetting> taiwan_DistricsSetting, GateWaySenserID gateWaySenserID, ScreenMediaSetting screenMediaSetting, List<AbsProtocol> absProtocols)
        {
            InitializeComponent();
            GateWay = gateWay;
            AbsProtocols = absProtocols;
            #region 感測器
            radioGroup1.Visible = false;
            radioGroup2.Visible = false;
            radioGroup3.Visible = false;
            radioGroup4.Visible = false;
            radioGroup5.Visible = false;
            radioGroup6.Visible = false;
            radioGroup7.Visible = false;
            ScreenMediaSetting = screenMediaSetting;
            foreach (var item in GateWay.GateWaySenserIDs)
            {
                SenserEnumType senserEnumType = (SenserEnumType)item.SenserEnumType;
                switch (senserEnumType)
                {
                    case SenserEnumType.BlackSenser:
                    case SenserEnumType.WhiteSenser:
                    case SenserEnumType.WeatherAPI:
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
            #endregion
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
                navigation.Visible = false;
            }
            else
            {
                navigation.Visible = true;
                if (screen.ScreenSwitches[0].VisibleFlag1)
                {
                    GIABigUserControl1 bigSenser = new GIABigUserControl1(screen.ScreenSwitches[0].SenserTypeEnum1, GateWay, GateWaySenserID, screen, true) { Dock = DockStyle.Fill };
                    BigUserControls.Add(bigSenser);
                    navigation.AddPage(bigSenser);
                }
                if (screen.ScreenSwitches[0].VisibleFlag2)
                {
                    GIABigUserControl1 bigSenser = new GIABigUserControl1(screen.ScreenSwitches[0].SenserTypeEnum2, GateWay, GateWaySenserID, screen, true) { Dock = DockStyle.Fill };
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
                navigation.Visible = false;
            }
            else
            {
                navigation.Visible = true;
                if (screen.ScreenSwitches[Index].VisibleFlag1)
                {
                    GIASmallUserControl1 smallSenser = new GIASmallUserControl1(screen.ScreenSwitches[Index].SenserTypeEnum1, GateWay, GateWaySenserID, screen, true) { Dock = DockStyle.Fill };
                    SmallUserControls.Add(smallSenser);
                    navigation.AddPage(smallSenser);
                }
                if (screen.ScreenSwitches[Index].VisibleFlag2)
                {
                    GIASmallUserControl1 smallSenser = new GIASmallUserControl1(screen.ScreenSwitches[Index].SenserTypeEnum2, GateWay, GateWaySenserID, screen, true) { Dock = DockStyle.Fill };
                    SmallUserControls.Add(smallSenser);
                    navigation.AddPage(smallSenser);
                }
                navigation.SelectedPageIndex = 0;
                PageIndex = 0;
            }
        }
        #endregion
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
                    }
                    else
                    {
                        PageIndex++;
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
                if (Bignavigation.SelectedPageIndex > -1)
                {
                    var bigsenser = (GIABigUserControl1)Bignavigation.Pages[Bignavigation.SelectedPageIndex].Controls[0];
                    bigsenser.AbsProtocols = AbsProtocols;
                    bigsenser.TextChange();
                }
                if (Smallnavigation1.SelectedPageIndex > -1)
                {
                    var smallsenser1 = (GIASmallUserControl1)Smallnavigation1.Pages[Smallnavigation1.SelectedPageIndex].Controls[0];
                    smallsenser1.AbsProtocols = AbsProtocols;
                    smallsenser1.TextChange();
                }
                if (Smallnavigation2.SelectedPageIndex > -1)
                {
                    var smallsenser2 = (GIASmallUserControl1)Smallnavigation2.Pages[Smallnavigation2.SelectedPageIndex].Controls[0];
                    smallsenser2.AbsProtocols = AbsProtocols;
                    smallsenser2.TextChange();
                }
                if (Smallnavigation3.SelectedPageIndex > -1)
                {
                    var smallsenser3 = (GIASmallUserControl1)Smallnavigation3.Pages[Smallnavigation3.SelectedPageIndex].Controls[0];
                    smallsenser3.AbsProtocols = AbsProtocols;
                    smallsenser3.TextChange();
                }
                if (Smallnavigation4.SelectedPageIndex > -1)
                {
                    var smallsenser4 = (GIASmallUserControl1)Smallnavigation4.Pages[Smallnavigation4.SelectedPageIndex].Controls[0];
                    smallsenser4.AbsProtocols = AbsProtocols;
                    smallsenser4.TextChange();
                }
                if (Smallnavigation5.SelectedPageIndex > -1)
                {
                    var smallsenser5 = (GIASmallUserControl1)Smallnavigation5.Pages[Smallnavigation5.SelectedPageIndex].Controls[0];
                    smallsenser5.AbsProtocols = AbsProtocols;
                    smallsenser5.TextChange();
                }
                if (Smallnavigation6.SelectedPageIndex > -1)
                {
                    var smallsenser6 = (GIASmallUserControl1)Smallnavigation6.Pages[Smallnavigation6.SelectedPageIndex].Controls[0];
                    smallsenser6.AbsProtocols = AbsProtocols;
                    smallsenser6.TextChange();
                }
            }
        }
    }
}
