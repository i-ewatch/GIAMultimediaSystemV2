
namespace GIAMultimediaSystemV2.Views.ElectricViews
{
    partial class ChartUserControl1
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
            this.ChartpanelControl = new DevExpress.XtraEditors.PanelControl();
            this.MonthUnitlabelControl = new DevExpress.XtraEditors.LabelControl();
            this.YearUnitlabelControl = new DevExpress.XtraEditors.LabelControl();
            this.DayUnitlabelControl = new DevExpress.XtraEditors.LabelControl();
            this.RightpictureBox = new System.Windows.Forms.PictureBox();
            this.LeftpictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ChartpanelControl)).BeginInit();
            this.ChartpanelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RightpictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeftpictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ChartpanelControl
            // 
            this.ChartpanelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.ChartpanelControl.Controls.Add(this.MonthUnitlabelControl);
            this.ChartpanelControl.Controls.Add(this.YearUnitlabelControl);
            this.ChartpanelControl.Controls.Add(this.DayUnitlabelControl);
            this.ChartpanelControl.Controls.Add(this.RightpictureBox);
            this.ChartpanelControl.Controls.Add(this.LeftpictureBox);
            this.ChartpanelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartpanelControl.Location = new System.Drawing.Point(0, 0);
            this.ChartpanelControl.Name = "ChartpanelControl";
            this.ChartpanelControl.Size = new System.Drawing.Size(1357, 310);
            this.ChartpanelControl.TabIndex = 0;
            // 
            // MonthUnitlabelControl
            // 
            this.MonthUnitlabelControl.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.MonthUnitlabelControl.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.MonthUnitlabelControl.Appearance.Options.UseBackColor = true;
            this.MonthUnitlabelControl.Appearance.Options.UseFont = true;
            this.MonthUnitlabelControl.Location = new System.Drawing.Point(22, 25);
            this.MonthUnitlabelControl.Name = "MonthUnitlabelControl";
            this.MonthUnitlabelControl.Size = new System.Drawing.Size(22, 19);
            this.MonthUnitlabelControl.TabIndex = 8;
            this.MonthUnitlabelControl.Text = "kW";
            // 
            // YearUnitlabelControl
            // 
            this.YearUnitlabelControl.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.YearUnitlabelControl.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.YearUnitlabelControl.Appearance.Options.UseBackColor = true;
            this.YearUnitlabelControl.Appearance.Options.UseFont = true;
            this.YearUnitlabelControl.Location = new System.Drawing.Point(22, 25);
            this.YearUnitlabelControl.Name = "YearUnitlabelControl";
            this.YearUnitlabelControl.Size = new System.Drawing.Size(22, 19);
            this.YearUnitlabelControl.TabIndex = 7;
            this.YearUnitlabelControl.Text = "kW";
            // 
            // DayUnitlabelControl
            // 
            this.DayUnitlabelControl.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.DayUnitlabelControl.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.DayUnitlabelControl.Appearance.Options.UseBackColor = true;
            this.DayUnitlabelControl.Appearance.Options.UseFont = true;
            this.DayUnitlabelControl.Location = new System.Drawing.Point(22, 25);
            this.DayUnitlabelControl.Name = "DayUnitlabelControl";
            this.DayUnitlabelControl.Size = new System.Drawing.Size(22, 19);
            this.DayUnitlabelControl.TabIndex = 6;
            this.DayUnitlabelControl.Text = "kW";
            // 
            // RightpictureBox
            // 
            this.RightpictureBox.BackColor = System.Drawing.Color.Transparent;
            this.RightpictureBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightpictureBox.Location = new System.Drawing.Point(1342, 0);
            this.RightpictureBox.Name = "RightpictureBox";
            this.RightpictureBox.Size = new System.Drawing.Size(15, 310);
            this.RightpictureBox.TabIndex = 4;
            this.RightpictureBox.TabStop = false;
            this.RightpictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.RightpictureBox_Paint);
            // 
            // LeftpictureBox
            // 
            this.LeftpictureBox.BackColor = System.Drawing.Color.Transparent;
            this.LeftpictureBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftpictureBox.Location = new System.Drawing.Point(0, 0);
            this.LeftpictureBox.Name = "LeftpictureBox";
            this.LeftpictureBox.Size = new System.Drawing.Size(15, 310);
            this.LeftpictureBox.TabIndex = 3;
            this.LeftpictureBox.TabStop = false;
            this.LeftpictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.LeftpictureBox_Paint);
            // 
            // ChartUserControl1
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ChartpanelControl);
            this.Name = "ChartUserControl1";
            this.Size = new System.Drawing.Size(1357, 310);
            ((System.ComponentModel.ISupportInitialize)(this.ChartpanelControl)).EndInit();
            this.ChartpanelControl.ResumeLayout(false);
            this.ChartpanelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RightpictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeftpictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl ChartpanelControl;
        private System.Windows.Forms.PictureBox RightpictureBox;
        private System.Windows.Forms.PictureBox LeftpictureBox;
        private DevExpress.XtraEditors.LabelControl DayUnitlabelControl;
        private DevExpress.XtraEditors.LabelControl MonthUnitlabelControl;
        private DevExpress.XtraEditors.LabelControl YearUnitlabelControl;
    }
}
