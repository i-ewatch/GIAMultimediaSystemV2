
namespace GIAMultimediaSystemV2.Views
{
    partial class MarqueeUserControl
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
            this.MarqueepanelControl = new DevExpress.XtraEditors.PanelControl();
            this.MarqueelabelControl = new DevExpress.XtraEditors.LabelControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.MarqueepanelControl)).BeginInit();
            this.MarqueepanelControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // MarqueepanelControl
            // 
            this.MarqueepanelControl.Appearance.BackColor = System.Drawing.Color.White;
            this.MarqueepanelControl.Appearance.BorderColor = System.Drawing.Color.Black;
            this.MarqueepanelControl.Appearance.Options.UseBackColor = true;
            this.MarqueepanelControl.Appearance.Options.UseBorderColor = true;
            this.MarqueepanelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.MarqueepanelControl.Controls.Add(this.MarqueelabelControl);
            this.MarqueepanelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MarqueepanelControl.Location = new System.Drawing.Point(0, 0);
            this.MarqueepanelControl.Name = "MarqueepanelControl";
            this.MarqueepanelControl.Size = new System.Drawing.Size(1896, 37);
            this.MarqueepanelControl.TabIndex = 2;
            // 
            // MarqueelabelControl
            // 
            this.MarqueelabelControl.Appearance.BackColor = System.Drawing.Color.White;
            this.MarqueelabelControl.Appearance.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MarqueelabelControl.Appearance.Options.UseBackColor = true;
            this.MarqueelabelControl.Appearance.Options.UseFont = true;
            this.MarqueelabelControl.Appearance.Options.UseTextOptions = true;
            this.MarqueelabelControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MarqueelabelControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.MarqueelabelControl.Location = new System.Drawing.Point(0, 38);
            this.MarqueelabelControl.Name = "MarqueelabelControl";
            this.MarqueelabelControl.Size = new System.Drawing.Size(1896, 37);
            this.MarqueelabelControl.TabIndex = 0;
            this.MarqueelabelControl.Text = "N/A";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MarqueeUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MarqueepanelControl);
            this.Name = "MarqueeUserControl";
            this.Size = new System.Drawing.Size(1896, 37);
            ((System.ComponentModel.ISupportInitialize)(this.MarqueepanelControl)).EndInit();
            this.MarqueepanelControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl MarqueepanelControl;
        private DevExpress.XtraEditors.LabelControl MarqueelabelControl;
        public System.Windows.Forms.Timer timer1;
    }
}
