using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using GIAMultimediaSystemV2.Configuration;
using GIAMultimediaSystemV2.Methods;
using GIAMultimediaSystemV2.Modules;
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

namespace GIAMultimediaSystemV2.Views.ElectricViews
{
    public partial class ChartUserControl : Field4UserControl
    {
        /// <summary>
        /// 圖表顏色
        /// </summary>
        PaletteEntry[] paletteEntries;
        /// <summary>
        /// 天累積量(單位 小時)
        /// </summary>
        private ChartControl DayChartControl { get; set; }
        /// <summary>
        /// 月累積量(單位 天)
        /// </summary>
        private ChartControl MonthChartControl { get; set; }
        /// <summary>
        /// 年累積量(單位 月)
        /// </summary>
        private ChartControl YearChartControl { get; set; }
        /// <summary>
        /// 圓餅圖
        /// </summary>
        private Series PieData { get; set; }
        /// <summary>
        /// 天Bar
        /// </summary>
        private List<Series> DayData { get; set; } = new List<Series>();
        /// <summary>
        /// 月Bar
        /// </summary>
        private List<Series> MonthData { get; set; } = new List<Series>();
        /// <summary>
        /// 年Bar
        /// </summary>
        private List<Series> YearData { get; set; } = new List<Series>();
        /// <summary>
        /// 第一次啟動
        /// </summary>
        private bool FirstFlag { get; set; } = false;
        /// <summary>
        /// 延遲時間
        /// </summary>
        private DateTime DelayTime { get; set; }
        /// <summary>
        /// Bar圖切換
        /// </summary>
        private int AfterSelectIndex = 0;
        public ChartUserControl(GroupSetting groupSetting, GateWaySetting gateWaySetting, SqlMethod sqlMethod)
        {
            InitializeComponent();
            GroupSetting = groupSetting;
            GateWaySetting = gateWaySetting;
            SqlMethod = sqlMethod;
            paletteEntries = PiechartControl.GetPaletteEntries(GroupSetting.Groups.Count);//圓餅圖有幾個Series
            Create_Electric_Pie();
            Create_Electric_Bar();
        }

        #region 建立Pie圖
        public void Create_Electric_Pie()
        {
            PieData = new Series($"各電表用電度累積", viewType: ViewType.Pie);
            PieData.ArgumentDataMember = "Argument";
            PieData.ValueDataMembers.AddRange(new string[] { "Value" });
            PieData.Label.TextPattern = "{A}: {VP:p0}";
            PieData.LegendTextPattern = "{A}";
            PiechartControl.Series.Add(PieData);
            Change_PieChartDatasoure();

            PieSeriesView seriesView = (PieSeriesView)PieData.View;//動畫特效
            if (seriesView != null)
            {
                seriesView.Animation = new PieBurstAnimation();
            }
            ((PieSeriesLabel)PieData.Label).Position = PieSeriesLabelPosition.TwoColumns;
            ((PieSeriesLabel)PieData.Label).ColumnIndent = 20;
            PiechartControl.Enabled = false;
            PiechartControl.SelectionMode = ElementSelectionMode.Multiple;
            PiechartControl.SeriesSelectionMode = SeriesSelectionMode.Point;
            if (PieData.DataSource != null)
            {
                XYDiagram diagram = PiechartControl.Diagram as XYDiagram;
                if (diagram != null)
                {
                    diagram.SelectionOptions.RectangleSelectionMouseAction.MouseButton = DevExpress.Portable.Input.PortableMouseButtons.Left;
                    diagram.SelectionOptions.RectangleSelectionMouseAction.ModifierKeys = ChartModifierKeys.None;
                }
            }
            PiechartControl.SelectedItemsChanged += (s, e) =>
            {
                if (PiechartControl.SelectedItems.Count > 0)
                {
                    List<string> DeviceName = new List<string>();
                    foreach (PieModule piePoint in PiechartControl.SelectedItems)
                    {
                        DeviceName.Add(piePoint.Argument);
                    }
                    UPdate_BarChart(DeviceName);
                }
                else
                {
                    UPdate_BarChart(null);
                }
            };
        }
        #endregion

        #region 建立Bar圖
        public void Create_Electric_Bar()
        {
            #region 天累積量(單位 小時)
            DayChartControl = new ChartControl() { Dock = DockStyle.Fill };
            //DayChartControl.Enabled = false;
            DayChartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            DayChartControl.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            Change_BarChartDatasoure(0);
            foreach (var DayDataitem in DayData)
            {
                DayChartControl.Series.Add(DayDataitem);
            }
            DayChartControl.CrosshairOptions.CrosshairLabelMode = CrosshairLabelMode.ShowCommonForAllSeries; //顯示全部線條內容
            DayChartControl.CrosshairOptions.LinesMode = CrosshairLinesMode.Auto;//自動獲取點上面的數值
            DayChartControl.CrosshairOptions.GroupHeaderTextOptions.Font = new System.Drawing.Font("微軟正黑體", 12);
            DayChartControl.SideBySideEqualBarWidth = false;//線條是否需要相等寬度
            if (DayChartControl.Series.Count > 0)
            {
                XYDiagram diagram = (XYDiagram)DayChartControl.Diagram;
                if (diagram != null)
                {
                    diagram.AxisX.DateTimeScaleOptions.AggregateFunction = AggregateFunction.Sum;//變更時間時為加總
                    diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Hour; // 顯示設定
                    diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Hour; // 刻度設定 
                    //diagram.AxisX.Label.Angle = 90;
                    diagram.AxisX.Label.TextPattern = "{A:HH:00}";//X軸顯示
                    diagram.AxisX.WholeRange.SideMarginsValue = 1;//不需要邊寬
                }
                DayChartControl.CrosshairOptions.ShowArgumentLabels = true;//是否顯示Y軸垂直線
                DayChartControl.CrosshairOptions.ShowArgumentLine = false;//是否顯示Y軸垂直線
                DayChartControl.CrosshairOptions.ShowCrosshairLabels = true;//是否顯示Y軸垂直線
                DayChartControl.CrosshairOptions.ShowValueLine = false;
                DayChartControl.CrosshairOptions.ShowValueLabels = false;

            }
            DayChartControl.CustomDrawSeriesPoint += (s, e) =>
             {
                 BarDrawOptions barOptions = e.SeriesDrawOptions as BarDrawOptions;
                 foreach (var Groupsitem in GroupSetting.Groups)
                 {
                     if (Groupsitem.GroupName == e.Series.Name)
                     {
                         if (Groupsitem.GroupIndex != 0)
                         {
                             barOptions.Color = paletteEntries[Groupsitem.GroupIndex - 1].Color;
                             e.LegendDrawOptions.Color = paletteEntries[Groupsitem.GroupIndex - 1].Color;
                         }
                     }
                 }
             };
            ElectricnavigationFrame.AddPage(DayChartControl);
            #endregion

            #region 月累積量(單位 天)
            MonthChartControl = new ChartControl() { Dock = DockStyle.Fill };
            //MonthChartControl.Enabled = false;
            MonthChartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            MonthChartControl.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            Change_BarChartDatasoure(1);
            foreach (var MonthDataitem in MonthData)
            {
                MonthChartControl.Series.Add(MonthDataitem);
            }
            MonthChartControl.CrosshairOptions.CrosshairLabelMode = CrosshairLabelMode.ShowCommonForAllSeries; //顯示全部線條內容
            MonthChartControl.CrosshairOptions.LinesMode = CrosshairLinesMode.Auto;//自動獲取點上面的數值
            MonthChartControl.CrosshairOptions.GroupHeaderTextOptions.Font = new System.Drawing.Font("微軟正黑體", 12);
            MonthChartControl.SideBySideEqualBarWidth = false;//線條是否需要相等寬度
            if (MonthChartControl.Series.Count > 0)
            {
                XYDiagram diagram = (XYDiagram)MonthChartControl.Diagram;
                if (diagram != null)
                {
                    diagram.AxisX.DateTimeScaleOptions.AggregateFunction = AggregateFunction.Sum;//變更時間時為加總
                    diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Day; // 顯示設定
                    diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Day; // 刻度設定 
                    //diagram.AxisX.Label.Angle = 90;
                    diagram.AxisX.Label.TextPattern = "{A:MM-dd}";//X軸顯示
                    diagram.AxisX.WholeRange.SideMarginsValue = 1;//不需要邊寬
                }
                MonthChartControl.CrosshairOptions.ShowArgumentLabels = true;//是否顯示Y軸垂直線
                MonthChartControl.CrosshairOptions.ShowArgumentLine = false;//是否顯示Y軸垂直線
                MonthChartControl.CrosshairOptions.ShowCrosshairLabels = true;//是否顯示Y軸垂直線
                MonthChartControl.CrosshairOptions.ShowValueLine = false;
                MonthChartControl.CrosshairOptions.ShowValueLabels = false;

            }
            MonthChartControl.CustomDrawSeriesPoint += (s, e) =>
            {
                BarDrawOptions barOptions = e.SeriesDrawOptions as BarDrawOptions;
                foreach (var Groupsitem in GroupSetting.Groups)
                {
                    if (Groupsitem.GroupName == e.Series.Name)
                    {
                        if (Groupsitem.GroupIndex != 0)
                        {
                            barOptions.Color = paletteEntries[Groupsitem.GroupIndex - 1].Color;
                            e.LegendDrawOptions.Color = paletteEntries[Groupsitem.GroupIndex - 1].Color;
                        }
                    }
                }
            };
            ElectricnavigationFrame.AddPage(MonthChartControl);
            #endregion

            #region 年累積量(單位 月)
            YearChartControl = new ChartControl() { Dock = DockStyle.Fill };
            //YearChartControl.Enabled = false;
            YearChartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            YearChartControl.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            Change_BarChartDatasoure(2);
            foreach (var YearDataitem in YearData)
            {
                YearChartControl.Series.Add(YearDataitem);
            }
            YearChartControl.CrosshairOptions.CrosshairLabelMode = CrosshairLabelMode.ShowCommonForAllSeries; //顯示全部線條內容
            YearChartControl.CrosshairOptions.LinesMode = CrosshairLinesMode.Auto;//自動獲取點上面的數值
            YearChartControl.CrosshairOptions.GroupHeaderTextOptions.Font = new System.Drawing.Font("微軟正黑體", 12);
            YearChartControl.SideBySideEqualBarWidth = false;//線條是否需要相等寬度
            if (YearChartControl.Series.Count > 0)
            {
                XYDiagram diagram = (XYDiagram)YearChartControl.Diagram;
                if (diagram != null)
                {
                    diagram.AxisX.DateTimeScaleOptions.AggregateFunction = AggregateFunction.Sum;//變更時間時為加總
                    diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Month; // 顯示設定
                    diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Month; // 刻度設定
                    //diagram.AxisX.Label.Angle = 90;
                    diagram.AxisX.Label.TextPattern = "{A:yyyy-MM}";//X軸顯示
                    diagram.AxisX.WholeRange.SideMarginsValue = 1;//不需要邊寬
                }
                YearChartControl.CrosshairOptions.ShowArgumentLabels = true;
                YearChartControl.CrosshairOptions.ShowArgumentLine = false;//是否顯示Y軸垂直線
                YearChartControl.CrosshairOptions.ShowCrosshairLabels = true;//是否顯示Y軸垂直線
                YearChartControl.CrosshairOptions.ShowValueLabels = false;
                YearChartControl.CrosshairOptions.ShowValueLine = false;
            }
            YearChartControl.CustomDrawSeriesPoint += (s, e) =>
            {
                BarDrawOptions barOptions = e.SeriesDrawOptions as BarDrawOptions;
                foreach (var Groupsitem in GroupSetting.Groups)
                {
                    if (Groupsitem.GroupIndex != 0)
                    {
                        if (Groupsitem.GroupName == e.Series.Name)
                        {
                            barOptions.Color = paletteEntries[Groupsitem.GroupIndex - 1].Color;
                            e.LegendDrawOptions.Color = paletteEntries[Groupsitem.GroupIndex - 1].Color;
                        }
                    }
                }
            };
            ElectricnavigationFrame.AddPage(YearChartControl);
            #endregion
        }
        #endregion

        #region 建立Pie圖資料
        private void Change_PieChartDatasoure()
        {
            List<PieModule> modules = new List<PieModule>();
            foreach (var Groupitem in GroupSetting.Groups)
            {
                var data = SqlMethod.Serch_TotalMeter_Pie(GateWaySetting, Groupitem.GroupIndex, Groupitem.GroupName);
                modules.Add(data);
            }
            PieData.DataSource = modules;
        }
        #endregion

        #region 建立Bar圖資料
        private void Change_BarChartDatasoure(int ChartIndex)
        {
            switch (ChartIndex)
            {
                case 0://天
                    {
                        int Index = 0;
                        foreach (var Groupitem in GroupSetting.Groups)
                        {
                            List<BarModule> data = new List<BarModule>();
                            for (int i = 0; i < 24; i++)
                            {
                                BarModule barModule = new BarModule()
                                {
                                    Argument = Convert.ToDateTime($"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day} {i}:00:00"),
                                    Value = 0
                                };
                                data.Add(barModule);
                            }
                            List<BarModule> bar = new List<BarModule>();
                            bar.AddRange(data);
                            var SQLbar = SqlMethod.Serch_TotalMeter_Bar(GateWaySetting, Groupitem.GroupIndex, ChartIndex);
                            for (int i = 0; i < SQLbar.Count; i++)
                            {
                                foreach (var item in bar)
                                {
                                    if (item.Argument == SQLbar[i].Argument)
                                    {
                                        item.Value = SQLbar[i].Value;
                                        break;
                                    }
                                }
                            }
                            if (FirstFlag)
                            {
                                DayData[Index].DataSource = bar;
                                Index++;
                            }
                            else
                            {
                                Series series = new Series($"{Groupitem.GroupName}", viewType: ViewType.Bar);
                                series.DataSource = bar;
                                series.ArgumentDataMember = "Argument";
                                series.ValueDataMembers.AddRange(new string[] { "Value" });
                                series.CrosshairLabelPattern = "{S} {V:0.##} kWh";
                                series.Label.TextPattern = "{V:0.##}";
                                series.LabelsVisibility = DefaultBoolean.True;
                                (series.Label as BarSeriesLabel).ShowForZeroValues = false;
                                (series.Label as BarSeriesLabel).Position = BarSeriesLabelPosition.Top;
                                series.LabelsVisibility =  DefaultBoolean.False;
                                DayData.Add(series);
                            }
                        }
                    }
                    break;
                case 1://月
                    {
                        int Index = 0;
                        foreach (var Groupitem in GroupSetting.Groups)
                        {
                            List<BarModule> data = new List<BarModule>();
                            for (int i = 0; i < DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++)
                            {
                                BarModule barModule = new BarModule()
                                {
                                    Argument = Convert.ToDateTime($"{DateTime.Now.Year}-{DateTime.Now.Month}-{i + 1}"),
                                    Value = 0
                                };
                                data.Add(barModule);
                            }
                            List<BarModule> bar = new List<BarModule>();
                            bar.AddRange(data);
                            var SQLbar = SqlMethod.Serch_TotalMeter_Bar(GateWaySetting, Groupitem.GroupIndex, ChartIndex);
                            for (int i = 0; i < SQLbar.Count; i++)
                            {
                                foreach (var item in bar)
                                {
                                    if (item.Argument.ToString("yyyyMMdd") == SQLbar[i].Argument.ToString("yyyyMMdd"))
                                    {
                                        item.Value += SQLbar[i].Value;
                                        break;
                                    }
                                }
                            }
                            if (FirstFlag)
                            {
                                MonthData[Index].DataSource = bar;
                                Index++;
                            }
                            else
                            {
                                Series series = new Series($"{Groupitem.GroupName}", viewType: ViewType.Bar);
                                series.DataSource = bar;
                                series.ArgumentDataMember = "Argument";
                                series.ValueDataMembers.AddRange(new string[] { "Value" });
                                series.CrosshairLabelPattern = "{S} {V:0.##} kWh";
                                series.Label.TextPattern = "{V:0.##}";
                                series.LabelsVisibility = DefaultBoolean.True;
                                (series.Label as BarSeriesLabel).ShowForZeroValues = false;
                                (series.Label as BarSeriesLabel).Position = BarSeriesLabelPosition.Top;
                                series.LabelsVisibility = DefaultBoolean.False;
                                MonthData.Add(series);
                            }
                        }
                    }
                    break;
                case 2://年
                    {
                        int Index = 0;
                        foreach (var Groupitem in GroupSetting.Groups)
                        {
                            List<BarModule> data = new List<BarModule>();
                            for (int i = 0; i < 12; i++)
                            {
                                BarModule barModule = new BarModule()
                                {
                                    Argument = Convert.ToDateTime($"{DateTime.Now.Year}-{i + 1}"),
                                    Value = 0
                                };
                                data.Add(barModule);
                            }
                            List<BarModule> bar = new List<BarModule>();
                            bar.AddRange(data);
                            var SQLbar = SqlMethod.Serch_TotalMeter_Bar(GateWaySetting, Groupitem.GroupIndex, ChartIndex);
                            for (int i = 0; i < SQLbar.Count; i++)
                            {
                                foreach (var item in bar)
                                {
                                    if (item.Argument.ToString("yyyyMM") == SQLbar[i].Argument.ToString("yyyyMM"))
                                    {
                                        item.Value += SQLbar[i].Value;
                                        break;
                                    }
                                }
                            }
                            if (FirstFlag)
                            {
                                YearData[Index].DataSource = bar;
                                Index++;
                            }
                            else
                            {
                                Series series = new Series($"{Groupitem.GroupName}", viewType: ViewType.Bar);
                                series.DataSource = bar;
                                series.ArgumentDataMember = "Argument";
                                series.ValueDataMembers.AddRange(new string[] { "Value" });
                                series.CrosshairLabelPattern = "{S} {V:0.##} kWh";
                                series.Label.TextPattern = "{V:0.##}";
                                series.LabelsVisibility = DefaultBoolean.True;
                                (series.Label as BarSeriesLabel).ShowForZeroValues = false;
                                (series.Label as BarSeriesLabel).Position = BarSeriesLabelPosition.Top;
                                series.LabelsVisibility = DefaultBoolean.False;
                                YearData.Add(series);
                            }
                        }
                        FirstFlag = true;
                    }
                    break;
            }
        }
        #endregion

        #region 更新長條圖
        /// <summary>
        /// 更新長條圖
        /// </summary>
        /// <param name="DeviceName"></param>
        private void UPdate_BarChart(List<string> DeviceName)
        {
            if (DeviceName != null)
            {
                foreach (Series item in DayChartControl.Series)
                {
                    item.Visible = false;
                }
                foreach (Series item in MonthChartControl.Series)
                {
                    item.Visible = false;
                }
                foreach (Series item in YearChartControl.Series)
                {
                    item.Visible = false;
                }
                foreach (var item in DeviceName)
                {
                    foreach (Series Dayitem in DayChartControl.Series)
                    {
                        if (Dayitem.Name == item)
                        {
                            Dayitem.Visible = true;
                        }
                    }
                    foreach (Series Monthitem in MonthChartControl.Series)
                    {
                        if (Monthitem.Name == item)
                        {
                            Monthitem.Visible = true;
                        }
                    }
                    foreach (Series Yearitem in YearChartControl.Series)
                    {
                        if (Yearitem.Name == item)
                        {
                            Yearitem.Visible = true;
                        }
                    }
                }
            }
            else
            {
                foreach (Series item in DayChartControl.Series)
                {
                    item.Visible = true;
                }
                foreach (Series item in MonthChartControl.Series)
                {
                    item.Visible = true;
                }
                foreach (Series item in YearChartControl.Series)
                {
                    item.Visible = true;
                }
            }
        }
        #endregion

        public override void TextChange()
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(DelayTime);
            if (timeSpan.TotalMilliseconds >= 30000 || AfterSelectIndex != ElectricnavigationFrame.SelectedPageIndex)
            {
                switch (ElectricnavigationFrame.SelectedPageIndex)
                {
                    case 0:
                        {
                            Change_BarChartDatasoure(0);
                            DayChartControl.Refresh();
                        }
                        break;
                    case 1:
                        {
                            Change_BarChartDatasoure(1);
                            MonthChartControl.Refresh();
                        }
                        break;
                    case 2:
                        {
                            Change_BarChartDatasoure(2);
                            YearChartControl.Refresh();
                        }
                        break;
                }
                Change_PieChartDatasoure();
                AfterSelectIndex = ElectricnavigationFrame.SelectedPageIndex;
                DelayTime = DateTime.Now;
            }
            else
            {
                Thread.Sleep(80);
            }
        }
    }
}
