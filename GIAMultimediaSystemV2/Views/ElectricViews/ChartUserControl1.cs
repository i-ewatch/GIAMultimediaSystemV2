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
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GIAMultimediaSystemV2.Views.ElectricViews
{
    public partial class ChartUserControl1 : Field4UserControl
    {
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
        /// 第一次啟動
        /// </summary>
        private bool FirstFlag { get; set; } = false;
        /// <summary>
        /// 延遲時間
        /// </summary>
        private DateTime DelayTime { get; set; }
        /// <summary>
        /// 顯示Bar內容
        ///  <para>0 = 天</para>
        ///  <para>1 = 月</para>
        ///  <para>2 = 年</para>
        /// </summary>
        private int barIndex = 2;
        /// <summary>
        /// 顯示Bar內容
        ///  <para>0 = 天</para>
        ///  <para>1 = 月</para>
        ///  <para>2 = 年</para>
        /// </summary>
        public int BarIndex
        {
            get { return barIndex; }
            set
            {
                if (value != barIndex)
                {
                    barIndex = value;
                    switch (value)
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
                }
            }
        }
        /// <summary>
        /// Bar數值顯示內容
        /// <para>0 = kW</para>
        /// <para>1 = 錢</para>
        /// </summary>
        private int dataIndex = 0;
        /// <summary>
        /// Bar數值顯示內容
        /// <para>0 = kW</para>
        /// <para>1 = 錢</para>
        /// </summary>
        public int DataIndex
        {
            get { return dataIndex; }
            set
            {
                if (value != dataIndex)
                {
                    dataIndex = value;
                    switch (BarIndex)
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
                }
            }
        }
        /// <summary>
        /// 顏色改變
        /// </summary>
        private Color NewColor { get; set; }
        public ChartUserControl1(GroupSetting groupSetting, GateWaySetting gateWaySetting, SqlMethod sqlMethod)
        {
            InitializeComponent();
            GroupSetting = groupSetting;
            GateWaySetting = gateWaySetting;
            SqlMethod = sqlMethod;
            NewColor = Color.FromArgb(255, 255, 255);
            DayUnitlabelControl.Appearance.BackColor = NewColor;
            MonthUnitlabelControl.Appearance.BackColor = NewColor;
            YearUnitlabelControl.Appearance.BackColor = NewColor;
            Create_Electric_Bar(0);
            Create_Electric_Bar(1);
            Create_Electric_Bar(2);
            BarIndex = 0;
        }
        #region 建立Bar圖
        public void Create_Electric_Bar(int BarIndex)
        {
            switch (BarIndex)
            {
                case 0:
                    {
                        #region 天累積量(單位 小時)
                        DayChartControl = new ChartControl() { Dock = DockStyle.Fill, Parent = ChartpanelControl };
                        DayChartControl.Enabled = false;
                        //DayChartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                        DayChartControl.Legend.Border.Visibility = DefaultBoolean.False;
                        DayChartControl.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
                        DayChartControl.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
                        DayChartControl.Legend.Direction = LegendDirection.LeftToRight;
                        DayChartControl.BorderOptions.Visibility = DefaultBoolean.False;
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
                                diagram.DefaultPane.BorderVisible = false;
                                diagram.DefaultPane.BackColor = NewColor;
                                diagram.AxisX.DateTimeScaleOptions.AggregateFunction = AggregateFunction.Sum;//變更時間時為加總
                                diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Hour; // 顯示設定
                                diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Hour; // 刻度設定 
                                diagram.AxisY.GridLines.Visible = false;
                                diagram.AxisX.Label.TextPattern = "{A:HH:00}";//X軸顯示
                                diagram.AxisX.WholeRange.SideMarginsValue = 1;//不需要邊寬
                            }
                            Legend legend = (Legend)DayChartControl.Legend;
                            if (legend != null)
                            {
                                legend.BackColor = NewColor;
                            }
                            DayChartControl.BackColor = NewColor;
                            DayChartControl.CrosshairOptions.ShowArgumentLabels = true;//是否顯示Y軸垂直線
                            DayChartControl.CrosshairOptions.ShowArgumentLine = false;//是否顯示Y軸垂直線
                            DayChartControl.CrosshairOptions.ShowCrosshairLabels = true;//是否顯示Y軸垂直線
                            DayChartControl.CrosshairOptions.ShowValueLine = false;
                            DayChartControl.CrosshairOptions.ShowValueLabels = false;

                        }
                        #endregion
                    }
                    break;
                case 1:
                    {
                        #region 月累積量(單位 天)
                        MonthChartControl = new ChartControl() { Dock = DockStyle.Fill, Parent = ChartpanelControl };
                        //MonthChartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                        MonthChartControl.Legend.Border.Visibility = DefaultBoolean.False;
                        MonthChartControl.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
                        MonthChartControl.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
                        MonthChartControl.Legend.Direction = LegendDirection.LeftToRight;
                        MonthChartControl.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
                        MonthChartControl.Enabled = false;
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
                                diagram.DefaultPane.BorderVisible = false;
                                diagram.DefaultPane.BackColor = NewColor;
                                diagram.AxisX.DateTimeScaleOptions.AggregateFunction = AggregateFunction.Sum;//變更時間時為加總
                                diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Day; // 顯示設定
                                diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Day; // 刻度設定 
                                diagram.AxisY.GridLines.Visible = false;
                                diagram.AxisX.Label.TextPattern = "{A:MM-dd}";//X軸顯示
                                diagram.AxisX.WholeRange.SideMarginsValue = 1;//不需要邊寬
                            }
                            Legend legend = (Legend)MonthChartControl.Legend;
                            if (legend != null)
                            {
                                legend.BackColor = NewColor;
                            }
                            MonthChartControl.BackColor = NewColor;
                            MonthChartControl.CrosshairOptions.ShowArgumentLabels = true;//是否顯示Y軸垂直線
                            MonthChartControl.CrosshairOptions.ShowArgumentLine = false;//是否顯示Y軸垂直線
                            MonthChartControl.CrosshairOptions.ShowCrosshairLabels = true;//是否顯示Y軸垂直線
                            MonthChartControl.CrosshairOptions.ShowValueLine = false;
                            MonthChartControl.CrosshairOptions.ShowValueLabels = false;

                        }
                        #endregion
                    }
                    break;
                case 2:
                    {
                        #region 年累積量(單位 月)
                        YearChartControl = new ChartControl() { Dock = DockStyle.Fill, Parent = ChartpanelControl };
                        YearChartControl.Enabled = false;
                        //YearChartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                        YearChartControl.Legend.Border.Visibility = DefaultBoolean.False;
                        YearChartControl.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
                        YearChartControl.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
                        YearChartControl.Legend.Direction = LegendDirection.LeftToRight;
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
                                diagram.DefaultPane.BorderVisible = false;
                                diagram.DefaultPane.BackColor = NewColor;
                                diagram.AxisX.DateTimeScaleOptions.AggregateFunction = AggregateFunction.Sum;//變更時間時為加總
                                diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Month; // 顯示設定
                                diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Month; // 刻度設定
                                diagram.AxisY.GridLines.Visible = false;
                                diagram.AxisX.Label.TextPattern = "{A:yyyy-MM}";//X軸顯示
                                diagram.AxisX.WholeRange.SideMarginsValue = 1;//不需要邊寬
                            }
                            Legend legend = (Legend)YearChartControl.Legend;
                            if (legend != null)
                            {
                                legend.BackColor = NewColor;
                            }
                            YearChartControl.BackColor = NewColor;
                            YearChartControl.CrosshairOptions.ShowArgumentLabels = true;
                            YearChartControl.CrosshairOptions.ShowArgumentLine = false;//是否顯示Y軸垂直線
                            YearChartControl.CrosshairOptions.ShowCrosshairLabels = true;//是否顯示Y軸垂直線
                            YearChartControl.CrosshairOptions.ShowValueLabels = false;
                            YearChartControl.CrosshairOptions.ShowValueLine = false;
                        }
                        #endregion
                    }
                    break;
            }
        }
        #endregion

        #region 建立Bar圖資料
        private void Change_BarChartDatasoure(int ChartIndex)
        {
            switch (ChartIndex)
            {
                case 0://天
                    {
                        DayChartControl.BringToFront();
                        DayUnitlabelControl.BringToFront();
                        int Index = 0;
                        foreach (var Groupitem in GroupSetting.Groups)
                        {
                            if (Index < 4)
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
                                var SQLbar = SqlMethod.Serch_TotalMeter_Bar(GateWaySetting, Groupitem.GroupIndex, ChartIndex, DataIndex);
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
                                    if (DayData.Count > 0)
                                    {
                                        DayData[Index].DataSource = bar;
                                        Index++;
                                    }
                                }
                                else
                                {

                                    Series series = new Series($"{Groupitem.GroupName}", viewType: ViewType.StackedBar);
                                    series.DataSource = bar;
                                    series.ArgumentDataMember = "Argument";
                                    series.ValueDataMembers.AddRange(new string[] { "Value" });
                                    series.CrosshairLabelPattern = "{S} {V:0.##} kWh";
                                    series.Label.TextPattern = "{V:0.##}";
                                    series.LabelsVisibility = DefaultBoolean.True;
                                    (series.Label as BarSeriesLabel).ShowForZeroValues = false;
                                    //(series.Label as BarSeriesLabel).Position = BarSeriesLabelPosition.Top;
                                    series.LabelsVisibility = DefaultBoolean.False;
                                    switch (DayData.Count)
                                    {
                                        case 0:
                                            {
                                                series.View.Color = Color.FromArgb(33, 174, 141);

                                            }
                                            break;
                                        case 1:
                                            {
                                                series.View.Color = Color.FromArgb(103, 187, 223);
                                            }
                                            break;
                                        case 2:
                                            {
                                                series.View.Color = Color.FromArgb(240, 93, 125);
                                            }
                                            break;
                                        case 3:
                                            {
                                                series.View.Color = Color.FromArgb(254, 151, 10);
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                    DayData.Add(series);
                                    Index++;
                                }
                            }
                        }
                    }
                    break;
                case 1://月
                    {
                        MonthChartControl.BringToFront();
                        MonthChartControl.BorderOptions.Visibility = DefaultBoolean.False;
                        MonthUnitlabelControl.BringToFront();
                        int Index = 0;
                        foreach (var Groupitem in GroupSetting.Groups)
                        {
                            if (Index < 4)
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
                                var SQLbar = SqlMethod.Serch_TotalMeter_Bar(GateWaySetting, Groupitem.GroupIndex, ChartIndex, DataIndex);
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
                                    if (MonthData.Count > 0)
                                    {
                                        MonthData[Index].DataSource = bar;
                                        Index++;
                                    }
                                }
                                else
                                {
                                    Series series = new Series($"{Groupitem.GroupName}", viewType: ViewType.StackedBar);
                                    series.DataSource = bar;
                                    series.ArgumentDataMember = "Argument";
                                    series.ValueDataMembers.AddRange(new string[] { "Value" });
                                    series.CrosshairLabelPattern = "{S} {V:0.##} kWh";
                                    series.Label.TextPattern = "{V:0.##}";
                                    series.LabelsVisibility = DefaultBoolean.True;
                                    (series.Label as BarSeriesLabel).ShowForZeroValues = false;
                                    //(series.Label as BarSeriesLabel).Position = BarSeriesLabelPosition.Top;
                                    series.LabelsVisibility = DefaultBoolean.False;
                                    switch (MonthData.Count)
                                    {
                                        case 0:
                                            {
                                                series.View.Color = Color.FromArgb(33, 174, 141);

                                            }
                                            break;
                                        case 1:
                                            {
                                                series.View.Color = Color.FromArgb(103, 187, 223);
                                            }
                                            break;
                                        case 2:
                                            {
                                                series.View.Color = Color.FromArgb(240, 93, 125);
                                            }
                                            break;
                                        case 3:
                                            {
                                                series.View.Color = Color.FromArgb(254, 151, 10);
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                    MonthData.Add(series);
                                    Index++;
                                }
                            }
                        }
                    }
                    break;
                case 2://年
                    {
                        YearChartControl.BringToFront();
                        YearUnitlabelControl.BringToFront();
                        int Index = 0;
                        foreach (var Groupitem in GroupSetting.Groups)
                        {
                            if (Index < 4)
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
                                var SQLbar = SqlMethod.Serch_TotalMeter_Bar(GateWaySetting, Groupitem.GroupIndex, ChartIndex, DataIndex);
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
                                    if (YearData.Count > 0)
                                    {
                                        YearData[Index].DataSource = bar;
                                        Index++;
                                    }
                                }
                                else
                                {
                                    Series series = new Series($"{Groupitem.GroupName}", viewType: ViewType.StackedBar);
                                    series.DataSource = bar;
                                    series.ArgumentDataMember = "Argument";
                                    series.ValueDataMembers.AddRange(new string[] { "Value" });
                                    series.CrosshairLabelPattern = "{S} {V:0.##} kWh";
                                    series.Label.TextPattern = "{V:0.##}";
                                    series.LabelsVisibility = DefaultBoolean.True;
                                    (series.Label as BarSeriesLabel).ShowForZeroValues = false;
                                    //(series.Label as BarSeriesLabel).Position = BarSeriesLabelPosition.Top;
                                    series.LabelsVisibility = DefaultBoolean.False;
                                    switch (YearData.Count)
                                    {
                                        case 0:
                                            {
                                                series.View.Color = Color.FromArgb(33, 174, 141);

                                            }
                                            break;
                                        case 1:
                                            {
                                                series.View.Color = Color.FromArgb(103, 187, 223);
                                            }
                                            break;
                                        case 2:
                                            {
                                                series.View.Color = Color.FromArgb(240, 93, 125);
                                            }
                                            break;
                                        case 3:
                                            {
                                                series.View.Color = Color.FromArgb(254, 151, 10);
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                    YearData.Add(series);
                                    Index++;
                                }
                            }
                        }
                        FirstFlag = true;
                    }
                    break;
            }
            if (DataIndex == 0)
            {
                DayUnitlabelControl.Text = "kW";
                MonthUnitlabelControl.Text = "kW";
                YearUnitlabelControl.Text = "kW";
            }
            else
            {
                DayUnitlabelControl.Text = "元";
                MonthUnitlabelControl.Text = "元";
                YearUnitlabelControl.Text = "元";
            }
        }
        #endregion

        #region 顯示變更
        public override void TextChange()
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(DelayTime);
            if (timeSpan.TotalMilliseconds >= 30000)
            {
                switch (BarIndex)
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
                DelayTime = DateTime.Now;
            }
            else
            {
                Thread.Sleep(80);
            }
        }
        #endregion

        #region 圖片顏色變更
        private void LeftpictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            using (Bitmap bmp = new Bitmap($"{MyWorkPath}\\Images\\Chart_left.png"))
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
            using (Bitmap bmp = new Bitmap($"{MyWorkPath}\\Images\\Chart_right.png"))
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
