
namespace GIAMultimediaSystemV2.Views.GIAViews
{
    partial class GIABigUserControl
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
            this.RightpictureBox = new System.Windows.Forms.PictureBox();
            this.LeftpictureBox = new System.Windows.Forms.PictureBox();
            this.ScreenpanelControl = new DevExpress.XtraEditors.PanelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.UnitlabelControl = new DevExpress.XtraEditors.LabelControl();
            this.SenserNamelabelControl = new DevExpress.XtraEditors.LabelControl();
            this.ValuelabelControl = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.RightpictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeftpictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ScreenpanelControl)).BeginInit();
            this.ScreenpanelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // RightpictureBox
            // 
            this.RightpictureBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightpictureBox.Location = new System.Drawing.Point(311, 0);
            this.RightpictureBox.Name = "RightpictureBox";
            this.RightpictureBox.Size = new System.Drawing.Size(26, 342);
            this.RightpictureBox.TabIndex = 0;
            this.RightpictureBox.TabStop = false;
            this.RightpictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.RightpictureBox_Paint);
            // 
            // LeftpictureBox
            // 
            this.LeftpictureBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftpictureBox.Location = new System.Drawing.Point(0, 0);
            this.LeftpictureBox.Name = "LeftpictureBox";
            this.LeftpictureBox.Size = new System.Drawing.Size(26, 342);
            this.LeftpictureBox.TabIndex = 1;
            this.LeftpictureBox.TabStop = false;
            this.LeftpictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.LeftpictureBox_Paint);
            // 
            // ScreenpanelControl
            // 
            this.ScreenpanelControl.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ScreenpanelControl.Appearance.Options.UseBackColor = true;
            this.ScreenpanelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.ScreenpanelControl.Controls.Add(this.pictureEdit1);
            this.ScreenpanelControl.Controls.Add(this.UnitlabelControl);
            this.ScreenpanelControl.Controls.Add(this.SenserNamelabelControl);
            this.ScreenpanelControl.Controls.Add(this.ValuelabelControl);
            this.ScreenpanelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScreenpanelControl.Location = new System.Drawing.Point(26, 0);
            this.ScreenpanelControl.Name = "ScreenpanelControl";
            this.ScreenpanelControl.Size = new System.Drawing.Size(285, 342);
            this.ScreenpanelControl.TabIndex = 12;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Location = new System.Drawing.Point(32, 18);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.AllowFocused = false;
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.NullText = " ";
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.pictureEdit1.Size = new System.Drawing.Size(85, 85);
            this.pictureEdit1.TabIndex = 4;
            // 
            // UnitlabelControl
            // 
            this.UnitlabelControl.Appearance.Font = new System.Drawing.Font("微軟正黑體", 24F);
            this.UnitlabelControl.Appearance.Options.UseFont = true;
            this.UnitlabelControl.Appearance.Options.UseTextOptions = true;
            this.UnitlabelControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.UnitlabelControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.UnitlabelControl.Location = new System.Drawing.Point(136, 292);
            this.UnitlabelControl.Name = "UnitlabelControl";
            this.UnitlabelControl.Size = new System.Drawing.Size(149, 46);
            this.UnitlabelControl.TabIndex = 7;
            this.UnitlabelControl.Text = "N/A";
            // 
            // SenserNamelabelControl
            // 
            this.SenserNamelabelControl.Appearance.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.SenserNamelabelControl.Appearance.Options.UseFont = true;
            this.SenserNamelabelControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.SenserNamelabelControl.Location = new System.Drawing.Point(136, 31);
            this.SenserNamelabelControl.Name = "SenserNamelabelControl";
            this.SenserNamelabelControl.Size = new System.Drawing.Size(149, 58);
            this.SenserNamelabelControl.TabIndex = 5;
            this.SenserNamelabelControl.Text = "N/A";
            // 
            // ValuelabelControl
            // 
            this.ValuelabelControl.Appearance.Font = new System.Drawing.Font("微軟正黑體", 85F);
            this.ValuelabelControl.Appearance.Options.UseFont = true;
            this.ValuelabelControl.Appearance.Options.UseTextOptions = true;
            this.ValuelabelControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ValuelabelControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.ValuelabelControl.Location = new System.Drawing.Point(3, 95);
            this.ValuelabelControl.Name = "ValuelabelControl";
            this.ValuelabelControl.Size = new System.Drawing.Size(282, 189);
            this.ValuelabelControl.TabIndex = 6;
            this.ValuelabelControl.Text = "N/A";
            // 
            // GIABigUserControl
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ScreenpanelControl);
            this.Controls.Add(this.LeftpictureBox);
            this.Controls.Add(this.RightpictureBox);
            this.Name = "GIABigUserControl";
            this.Size = new System.Drawing.Size(337, 342);
            ((System.ComponentModel.ISupportInitialize)(this.RightpictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeftpictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ScreenpanelControl)).EndInit();
            this.ScreenpanelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox RightpictureBox;
        private System.Windows.Forms.PictureBox LeftpictureBox;
        private DevExpress.XtraEditors.PanelControl ScreenpanelControl;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.LabelControl UnitlabelControl;
        private DevExpress.XtraEditors.LabelControl SenserNamelabelControl;
        private DevExpress.XtraEditors.LabelControl ValuelabelControl;
    }
}
