
namespace GIAMultimediaSystemV2.Views.Setting
{
    partial class SettingButtonUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingButtonUserControl));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.UnitsimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.ChartDaysimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.LocksimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.CloseFormsimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.ViewSettingsimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.MarqueeSettingsimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.SystemSettingsimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.Gray;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.UnitsimpleButton);
            this.panelControl1.Controls.Add(this.ChartDaysimpleButton);
            this.panelControl1.Controls.Add(this.LocksimpleButton);
            this.panelControl1.Controls.Add(this.CloseFormsimpleButton);
            this.panelControl1.Controls.Add(this.ViewSettingsimpleButton);
            this.panelControl1.Controls.Add(this.MarqueeSettingsimpleButton);
            this.panelControl1.Controls.Add(this.SystemSettingsimpleButton);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1920, 62);
            this.panelControl1.TabIndex = 0;
            // 
            // UnitsimpleButton
            // 
            this.UnitsimpleButton.AllowFocus = false;
            this.UnitsimpleButton.Appearance.Font = new System.Drawing.Font("微軟正黑體", 24.25F, System.Drawing.FontStyle.Bold);
            this.UnitsimpleButton.Appearance.Options.UseFont = true;
            this.UnitsimpleButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.UnitsimpleButton.Location = new System.Drawing.Point(668, 0);
            this.UnitsimpleButton.Name = "UnitsimpleButton";
            this.UnitsimpleButton.Size = new System.Drawing.Size(71, 62);
            this.UnitsimpleButton.TabIndex = 9;
            this.UnitsimpleButton.Click += new System.EventHandler(this.UnitsimpleButton_Click);
            // 
            // ChartDaysimpleButton
            // 
            this.ChartDaysimpleButton.AllowFocus = false;
            this.ChartDaysimpleButton.Appearance.Font = new System.Drawing.Font("微軟正黑體", 24.25F, System.Drawing.FontStyle.Bold);
            this.ChartDaysimpleButton.Appearance.Options.UseFont = true;
            this.ChartDaysimpleButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.ChartDaysimpleButton.Location = new System.Drawing.Point(597, 0);
            this.ChartDaysimpleButton.Name = "ChartDaysimpleButton";
            this.ChartDaysimpleButton.Size = new System.Drawing.Size(71, 62);
            this.ChartDaysimpleButton.TabIndex = 8;
            this.ChartDaysimpleButton.Text = "日";
            this.ChartDaysimpleButton.Click += new System.EventHandler(this.ChartDaysimpleButton_Click);
            // 
            // LocksimpleButton
            // 
            this.LocksimpleButton.AllowFocus = false;
            this.LocksimpleButton.Appearance.Font = new System.Drawing.Font("微軟正黑體", 24.25F, System.Drawing.FontStyle.Bold);
            this.LocksimpleButton.Appearance.Options.UseFont = true;
            this.LocksimpleButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.LocksimpleButton.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.LocksimpleButton.Location = new System.Drawing.Point(1730, 0);
            this.LocksimpleButton.Name = "LocksimpleButton";
            this.LocksimpleButton.Size = new System.Drawing.Size(62, 62);
            this.LocksimpleButton.TabIndex = 7;
            this.LocksimpleButton.Click += new System.EventHandler(this.LocksimpleButton_Click);
            // 
            // CloseFormsimpleButton
            // 
            this.CloseFormsimpleButton.AllowFocus = false;
            this.CloseFormsimpleButton.Appearance.Font = new System.Drawing.Font("微軟正黑體", 24.25F, System.Drawing.FontStyle.Bold);
            this.CloseFormsimpleButton.Appearance.Options.UseFont = true;
            this.CloseFormsimpleButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.CloseFormsimpleButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("CloseFormsimpleButton.ImageOptions.Image")));
            this.CloseFormsimpleButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.CloseFormsimpleButton.Location = new System.Drawing.Point(1792, 0);
            this.CloseFormsimpleButton.Name = "CloseFormsimpleButton";
            this.CloseFormsimpleButton.Size = new System.Drawing.Size(128, 62);
            this.CloseFormsimpleButton.TabIndex = 6;
            this.CloseFormsimpleButton.Text = "關閉";
            this.CloseFormsimpleButton.Click += new System.EventHandler(this.CloseFormsimpleButton_Click);
            // 
            // ViewSettingsimpleButton
            // 
            this.ViewSettingsimpleButton.AllowFocus = false;
            this.ViewSettingsimpleButton.Appearance.Font = new System.Drawing.Font("微軟正黑體", 24.25F, System.Drawing.FontStyle.Bold);
            this.ViewSettingsimpleButton.Appearance.Options.UseFont = true;
            this.ViewSettingsimpleButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.ViewSettingsimpleButton.Location = new System.Drawing.Point(398, 0);
            this.ViewSettingsimpleButton.Name = "ViewSettingsimpleButton";
            this.ViewSettingsimpleButton.Size = new System.Drawing.Size(199, 62);
            this.ViewSettingsimpleButton.TabIndex = 5;
            this.ViewSettingsimpleButton.Text = "外觀設定";
            this.ViewSettingsimpleButton.Click += new System.EventHandler(this.ViewSettingsimpleButton_Click);
            // 
            // MarqueeSettingsimpleButton
            // 
            this.MarqueeSettingsimpleButton.AllowFocus = false;
            this.MarqueeSettingsimpleButton.Appearance.Font = new System.Drawing.Font("微軟正黑體", 24.25F, System.Drawing.FontStyle.Bold);
            this.MarqueeSettingsimpleButton.Appearance.Options.UseFont = true;
            this.MarqueeSettingsimpleButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.MarqueeSettingsimpleButton.Location = new System.Drawing.Point(199, 0);
            this.MarqueeSettingsimpleButton.Name = "MarqueeSettingsimpleButton";
            this.MarqueeSettingsimpleButton.Size = new System.Drawing.Size(199, 62);
            this.MarqueeSettingsimpleButton.TabIndex = 4;
            this.MarqueeSettingsimpleButton.Text = "跑馬燈設定";
            this.MarqueeSettingsimpleButton.Click += new System.EventHandler(this.MarqueeSettingsimpleButton_Click);
            // 
            // SystemSettingsimpleButton
            // 
            this.SystemSettingsimpleButton.AllowFocus = false;
            this.SystemSettingsimpleButton.Appearance.Font = new System.Drawing.Font("微軟正黑體", 24.25F, System.Drawing.FontStyle.Bold);
            this.SystemSettingsimpleButton.Appearance.Options.UseFont = true;
            this.SystemSettingsimpleButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.SystemSettingsimpleButton.Location = new System.Drawing.Point(0, 0);
            this.SystemSettingsimpleButton.Name = "SystemSettingsimpleButton";
            this.SystemSettingsimpleButton.Size = new System.Drawing.Size(199, 62);
            this.SystemSettingsimpleButton.TabIndex = 3;
            this.SystemSettingsimpleButton.Text = "系統設定";
            this.SystemSettingsimpleButton.Click += new System.EventHandler(this.SystemSettingsimpleButton_Click);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageSize = new System.Drawing.Size(60, 60);
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "play_32x32.png");
            this.imageCollection1.Images.SetKeyName(1, "stop_32x32.png");
            this.imageCollection1.Images.SetKeyName(2, "Dollar.png");
            this.imageCollection1.Images.SetKeyName(3, "Lightning.png");
            // 
            // SettingButtonUserControl
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "SettingButtonUserControl";
            this.Size = new System.Drawing.Size(1920, 62);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton LocksimpleButton;
        private DevExpress.XtraEditors.SimpleButton CloseFormsimpleButton;
        private DevExpress.XtraEditors.SimpleButton ViewSettingsimpleButton;
        private DevExpress.XtraEditors.SimpleButton MarqueeSettingsimpleButton;
        private DevExpress.XtraEditors.SimpleButton SystemSettingsimpleButton;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.SimpleButton ChartDaysimpleButton;
        private DevExpress.XtraEditors.SimpleButton UnitsimpleButton;
    }
}
