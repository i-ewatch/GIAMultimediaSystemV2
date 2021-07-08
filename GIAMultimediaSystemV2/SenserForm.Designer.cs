
namespace GIAMultimediaSystemV2
{
    partial class SenserForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MarqueepanelControl = new DevExpress.XtraEditors.PanelControl();
            this.SenserpanelControl = new DevExpress.XtraEditors.PanelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.WeatherpanelControl = new DevExpress.XtraEditors.PanelControl();
            this.VediopanelControl = new DevExpress.XtraEditors.PanelControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.MarqueepanelControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SenserpanelControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WeatherpanelControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VediopanelControl)).BeginInit();
            this.SuspendLayout();
            // 
            // MarqueepanelControl
            // 
            this.MarqueepanelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.MarqueepanelControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.MarqueepanelControl.Location = new System.Drawing.Point(0, 1010);
            this.MarqueepanelControl.Name = "MarqueepanelControl";
            this.MarqueepanelControl.Size = new System.Drawing.Size(1920, 70);
            this.MarqueepanelControl.TabIndex = 0;
            // 
            // SenserpanelControl
            // 
            this.SenserpanelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.SenserpanelControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.SenserpanelControl.Location = new System.Drawing.Point(0, 0);
            this.SenserpanelControl.Name = "SenserpanelControl";
            this.SenserpanelControl.Size = new System.Drawing.Size(441, 1010);
            this.SenserpanelControl.TabIndex = 1;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureEdit1.Location = new System.Drawing.Point(441, 0);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.AllowFocused = false;
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.NullText = " ";
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.Size = new System.Drawing.Size(1479, 1010);
            this.pictureEdit1.TabIndex = 2;
            // 
            // WeatherpanelControl
            // 
            this.WeatherpanelControl.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.WeatherpanelControl.Appearance.Options.UseBackColor = true;
            this.WeatherpanelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.WeatherpanelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.WeatherpanelControl.Location = new System.Drawing.Point(441, 0);
            this.WeatherpanelControl.Name = "WeatherpanelControl";
            this.WeatherpanelControl.Size = new System.Drawing.Size(1479, 205);
            this.WeatherpanelControl.TabIndex = 3;
            // 
            // VediopanelControl
            // 
            this.VediopanelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.VediopanelControl.Location = new System.Drawing.Point(551, 223);
            this.VediopanelControl.Name = "VediopanelControl";
            this.VediopanelControl.Size = new System.Drawing.Size(1280, 720);
            this.VediopanelControl.TabIndex = 4;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // SenserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.VediopanelControl);
            this.Controls.Add(this.WeatherpanelControl);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.SenserpanelControl);
            this.Controls.Add(this.MarqueepanelControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SenserForm";
            this.Text = "SenserForm";
            this.Load += new System.EventHandler(this.SenserForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MarqueepanelControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SenserpanelControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WeatherpanelControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VediopanelControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl MarqueepanelControl;
        private DevExpress.XtraEditors.PanelControl SenserpanelControl;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.PanelControl WeatherpanelControl;
        private DevExpress.XtraEditors.PanelControl VediopanelControl;
        private System.Windows.Forms.Timer timer1;
    }
}