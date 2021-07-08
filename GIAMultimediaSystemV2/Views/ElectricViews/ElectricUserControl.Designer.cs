
namespace GIAMultimediaSystemV2.Views.ElectricViews
{
    partial class ElectricUserControl
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.ValuelabelControl = new DevExpress.XtraEditors.LabelControl();
            this.UnitlabelControl = new DevExpress.XtraEditors.LabelControl();
            this.TitlelabelControl = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.ValuelabelControl);
            this.panelControl1.Controls.Add(this.UnitlabelControl);
            this.panelControl1.Controls.Add(this.TitlelabelControl);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(242, 170);
            this.panelControl1.TabIndex = 0;
            // 
            // ValuelabelControl
            // 
            this.ValuelabelControl.Appearance.Font = new System.Drawing.Font("Tahoma", 28F);
            this.ValuelabelControl.Appearance.Options.UseFont = true;
            this.ValuelabelControl.Appearance.Options.UseTextOptions = true;
            this.ValuelabelControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ValuelabelControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.ValuelabelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ValuelabelControl.Location = new System.Drawing.Point(2, 35);
            this.ValuelabelControl.Name = "ValuelabelControl";
            this.ValuelabelControl.Size = new System.Drawing.Size(238, 100);
            this.ValuelabelControl.TabIndex = 2;
            this.ValuelabelControl.Text = "-";
            // 
            // UnitlabelControl
            // 
            this.UnitlabelControl.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.UnitlabelControl.Appearance.Options.UseFont = true;
            this.UnitlabelControl.Appearance.Options.UseTextOptions = true;
            this.UnitlabelControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.UnitlabelControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.UnitlabelControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.UnitlabelControl.Location = new System.Drawing.Point(2, 135);
            this.UnitlabelControl.Name = "UnitlabelControl";
            this.UnitlabelControl.Size = new System.Drawing.Size(238, 33);
            this.UnitlabelControl.TabIndex = 1;
            this.UnitlabelControl.Text = "Unit";
            // 
            // TitlelabelControl
            // 
            this.TitlelabelControl.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.TitlelabelControl.Appearance.Options.UseFont = true;
            this.TitlelabelControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.TitlelabelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.TitlelabelControl.Location = new System.Drawing.Point(2, 2);
            this.TitlelabelControl.Name = "TitlelabelControl";
            this.TitlelabelControl.Size = new System.Drawing.Size(238, 33);
            this.TitlelabelControl.TabIndex = 0;
            this.TitlelabelControl.Text = "Title";
            // 
            // ElectricUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "ElectricUserControl";
            this.Size = new System.Drawing.Size(242, 170);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl ValuelabelControl;
        private DevExpress.XtraEditors.LabelControl UnitlabelControl;
        private DevExpress.XtraEditors.LabelControl TitlelabelControl;
    }
}
