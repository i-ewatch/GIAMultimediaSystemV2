using DevExpress.XtraEditors;
using GIAMultimediaSystemV2.Configuration;
using GIAMultimediaSystemV2.Methods;
using GIAMultimediaSystemV2.Views.ElectricViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GIAMultimediaSystemV2.Views
{
    public partial class OtherUserControl : Field4UserControl
    {
        public OtherUserControl(GateWaySetting gateWaySetting, SqlDBSetting setting, GroupSetting groupSetting, SqlMethod sqlMethod)
        {
            InitializeComponent();
            GateWaySetting = gateWaySetting;
            SqlDBSetting = setting;
            GroupSetting = groupSetting;
            SqlMethod = sqlMethod;
            foreach (var item in groupSetting.Groups)
            {
                ElectricUserControl kwh = new ElectricUserControl(SqlDBSetting, true, GateWaySetting, GroupSetting, item.GroupIndex, SqlMethod) { Dock = DockStyle.Fill };
                KwhnavigationFrame.AddPage(kwh);
                KwhControl.Add(kwh);

                ElectricUserControl pric = new ElectricUserControl(SqlDBSetting, false, GateWaySetting, GroupSetting, item.GroupIndex, SqlMethod) { Dock = DockStyle.Fill };
                PricenavigationFrame.AddPage(pric);
                PriceControl.Add(pric);
            }
        }
        /// <summary>
        /// 切換畫面頁數
        /// </summary>
        private int PageIndex { get; set; } = 0;
        /// <summary>
        /// 切換畫面最後時間
        /// </summary>
        private DateTime PageTime { get; set; }
        /// <summary>
        /// 畫面切換鎖定 True = 不鎖定 ,False = 鎖定
        /// </summary>
        public bool LockFlag { get; set; } = true;
        List<Field4UserControl> KwhControl { get; set; } = new List<Field4UserControl>();
        List<Field4UserControl> PriceControl { get; set; } = new List<Field4UserControl>();
        public override void TextChange()
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(PageTime);
            if (timeSpan.TotalSeconds > 5)
            {
                if (LockFlag)
                {
                    if (PageIndex == GroupSetting.Groups.Count-1)
                    {
                        PageIndex = 0;
                    }
                    else
                    {
                        PageIndex++;
                    }
                    KwhnavigationFrame.SelectedPageIndex = PageIndex;
                    PricenavigationFrame.SelectedPageIndex = PageIndex;
                    PageTime = DateTime.Now;
                }
            }

            KwhControl[PageIndex].TextChange();
            PriceControl[PageIndex].TextChange();
        }
    }
}
