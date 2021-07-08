
namespace GIAMultimediaSystemV2.Views
{
    partial class OtherUserControl
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
            this.PricenavigationFrame = new DevExpress.XtraBars.Navigation.NavigationFrame();
            this.KwhnavigationFrame = new DevExpress.XtraBars.Navigation.NavigationFrame();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PricenavigationFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KwhnavigationFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.PricenavigationFrame);
            this.panelControl1.Controls.Add(this.KwhnavigationFrame);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(500, 734);
            this.panelControl1.TabIndex = 0;
            // 
            // PricenavigationFrame
            // 
            this.PricenavigationFrame.Location = new System.Drawing.Point(250, 556);
            this.PricenavigationFrame.Name = "PricenavigationFrame";
            this.PricenavigationFrame.SelectedPage = null;
            this.PricenavigationFrame.Size = new System.Drawing.Size(242, 170);
            this.PricenavigationFrame.TabIndex = 1;
            this.PricenavigationFrame.Text = "vv";
            // 
            // KwhnavigationFrame
            // 
            this.KwhnavigationFrame.Location = new System.Drawing.Point(3, 556);
            this.KwhnavigationFrame.Name = "KwhnavigationFrame";
            this.KwhnavigationFrame.SelectedPage = null;
            this.KwhnavigationFrame.Size = new System.Drawing.Size(242, 170);
            this.KwhnavigationFrame.TabIndex = 0;
            this.KwhnavigationFrame.Text = "navigationFrame1";
            // 
            // OtherUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "OtherUserControl";
            this.Size = new System.Drawing.Size(500, 734);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PricenavigationFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KwhnavigationFrame)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraBars.Navigation.NavigationFrame PricenavigationFrame;
        private DevExpress.XtraBars.Navigation.NavigationFrame KwhnavigationFrame;
    }
}
