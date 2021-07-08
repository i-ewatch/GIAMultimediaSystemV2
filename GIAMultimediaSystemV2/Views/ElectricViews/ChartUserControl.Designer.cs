
namespace GIAMultimediaSystemV2.Views.ElectricViews
{
    partial class ChartUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.ElectricnavigationFrame = new DevExpress.XtraBars.Navigation.NavigationFrame();
            this.ElectricradioGroup = new DevExpress.XtraEditors.RadioGroup();
            this.PiechartControl = new DevExpress.XtraCharts.ChartControl();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ElectricnavigationFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ElectricradioGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiechartControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.ElectricnavigationFrame);
            this.panelControl1.Controls.Add(this.ElectricradioGroup);
            this.panelControl1.Controls.Add(this.PiechartControl);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1896, 277);
            this.panelControl1.TabIndex = 0;
            // 
            // ElectricnavigationFrame
            // 
            this.behaviorManager1.SetBehaviors(this.ElectricnavigationFrame, new DevExpress.Utils.Behaviors.Behavior[] {
            ((DevExpress.Utils.Behaviors.Behavior)(((DevExpress.Utils.Behaviors.Common.PagerBehavior)(DevExpress.Utils.Behaviors.Behavior.Create(typeof(DevExpress.Utils.Behaviors.Common.PagerBehavior), typeof(DevExpress.XtraBars.Behaviors.PagerBehaviorSourceForNavigationFrame), new object[] {
                        ((object)(this.ElectricradioGroup))})))))});
            this.ElectricnavigationFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ElectricnavigationFrame.Location = new System.Drawing.Point(0, 0);
            this.ElectricnavigationFrame.Name = "ElectricnavigationFrame";
            this.ElectricnavigationFrame.SelectedPage = null;
            this.ElectricnavigationFrame.Size = new System.Drawing.Size(1529, 252);
            this.ElectricnavigationFrame.TabIndex = 5;
            this.ElectricnavigationFrame.Text = "navigationFrame1";
            // 
            // ElectricradioGroup
            // 
            this.ElectricradioGroup.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ElectricradioGroup.Location = new System.Drawing.Point(0, 252);
            this.ElectricradioGroup.Name = "ElectricradioGroup";
            this.ElectricradioGroup.Properties.AllowFocused = false;
            this.ElectricradioGroup.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ElectricradioGroup.Properties.Appearance.Options.UseBackColor = true;
            this.ElectricradioGroup.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.ElectricradioGroup.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.ElectricradioGroup.Properties.ItemHorzAlignment = DevExpress.XtraEditors.RadioItemHorzAlignment.Center;
            this.ElectricradioGroup.Properties.ItemsLayout = DevExpress.XtraEditors.RadioGroupItemsLayout.Flow;
            this.ElectricradioGroup.Properties.ItemVertAlignment = DevExpress.XtraEditors.RadioItemVertAlignment.Top;
            this.ElectricradioGroup.Size = new System.Drawing.Size(1529, 25);
            this.ElectricradioGroup.TabIndex = 4;
            // 
            // PiechartControl
            // 
            this.PiechartControl.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.PiechartControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.PiechartControl.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
            this.PiechartControl.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.BottomOutside;
            this.PiechartControl.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.PiechartControl.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
            this.PiechartControl.Legend.Name = "Default Legend";
            this.PiechartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.PiechartControl.Location = new System.Drawing.Point(1529, 0);
            this.PiechartControl.Name = "PiechartControl";
            this.PiechartControl.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.PiechartControl.Size = new System.Drawing.Size(367, 277);
            this.PiechartControl.TabIndex = 1;
            // 
            // ChartUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "ChartUserControl";
            this.Size = new System.Drawing.Size(1896, 277);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ElectricnavigationFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ElectricradioGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PiechartControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraCharts.ChartControl PiechartControl;
        private DevExpress.XtraBars.Navigation.NavigationFrame ElectricnavigationFrame;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private DevExpress.XtraEditors.RadioGroup ElectricradioGroup;
    }
}
