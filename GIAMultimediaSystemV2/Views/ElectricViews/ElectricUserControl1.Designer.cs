
namespace GIAMultimediaSystemV2.Views.ElectricViews
{
    partial class ElectricUserControl1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ElectricUserControl1));
            this.LeftpictureBox = new System.Windows.Forms.PictureBox();
            this.RightpictureBox = new System.Windows.Forms.PictureBox();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.TitlelabelControl = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.ValuelabelControl = new DevExpress.XtraEditors.LabelControl();
            this.LogoimageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.UnitlabelControl = new DevExpress.XtraEditors.LabelControl();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            ((System.ComponentModel.ISupportInitialize)(this.LeftpictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightpictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoimageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // LeftpictureBox
            // 
            this.LeftpictureBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftpictureBox.Location = new System.Drawing.Point(0, 0);
            this.LeftpictureBox.Name = "LeftpictureBox";
            this.LeftpictureBox.Size = new System.Drawing.Size(7, 124);
            this.LeftpictureBox.TabIndex = 4;
            this.LeftpictureBox.TabStop = false;
            this.LeftpictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.LeftpictureBox_Paint);
            // 
            // RightpictureBox
            // 
            this.RightpictureBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightpictureBox.Location = new System.Drawing.Point(235, 0);
            this.RightpictureBox.Name = "RightpictureBox";
            this.RightpictureBox.Size = new System.Drawing.Size(7, 124);
            this.RightpictureBox.TabIndex = 5;
            this.RightpictureBox.TabStop = false;
            this.RightpictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.RightpictureBox_Paint);
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.TitlelabelControl);
            this.panelControl1.Controls.Add(this.pictureEdit);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(7, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(228, 52);
            this.panelControl1.TabIndex = 6;
            // 
            // TitlelabelControl
            // 
            this.TitlelabelControl.Appearance.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitlelabelControl.Appearance.Options.UseFont = true;
            this.TitlelabelControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.TitlelabelControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.TitlelabelControl.Location = new System.Drawing.Point(71, 0);
            this.TitlelabelControl.Name = "TitlelabelControl";
            this.TitlelabelControl.Size = new System.Drawing.Size(157, 52);
            this.TitlelabelControl.TabIndex = 2;
            this.TitlelabelControl.Text = "Title";
            // 
            // pictureEdit
            // 
            this.pictureEdit.Location = new System.Drawing.Point(16, 2);
            this.pictureEdit.Name = "pictureEdit";
            this.pictureEdit.Properties.AllowFocused = false;
            this.pictureEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit.Properties.NullText = " ";
            this.pictureEdit.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit.Size = new System.Drawing.Size(48, 48);
            this.pictureEdit.TabIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.separatorControl1);
            this.panelControl2.Controls.Add(this.UnitlabelControl);
            this.panelControl2.Controls.Add(this.ValuelabelControl);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(7, 52);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(228, 72);
            this.panelControl2.TabIndex = 7;
            // 
            // ValuelabelControl
            // 
            this.ValuelabelControl.Appearance.Font = new System.Drawing.Font("微軟正黑體", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ValuelabelControl.Appearance.Options.UseFont = true;
            this.ValuelabelControl.Appearance.Options.UseTextOptions = true;
            this.ValuelabelControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ValuelabelControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.ValuelabelControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.ValuelabelControl.Location = new System.Drawing.Point(0, 0);
            this.ValuelabelControl.Name = "ValuelabelControl";
            this.ValuelabelControl.Size = new System.Drawing.Size(164, 72);
            this.ValuelabelControl.TabIndex = 3;
            this.ValuelabelControl.Text = "-";
            // 
            // LogoimageCollection
            // 
            this.LogoimageCollection.ImageSize = new System.Drawing.Size(44, 46);
            this.LogoimageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("LogoimageCollection.ImageStream")));
            this.LogoimageCollection.Images.SetKeyName(0, "icon-01.png");
            this.LogoimageCollection.Images.SetKeyName(1, "icon-02.png");
            // 
            // UnitlabelControl
            // 
            this.UnitlabelControl.Appearance.Font = new System.Drawing.Font("微軟正黑體", 16F);
            this.UnitlabelControl.Appearance.Options.UseFont = true;
            this.UnitlabelControl.Appearance.Options.UseTextOptions = true;
            this.UnitlabelControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.UnitlabelControl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Bottom;
            this.UnitlabelControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.UnitlabelControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.UnitlabelControl.Location = new System.Drawing.Point(164, 0);
            this.UnitlabelControl.Name = "UnitlabelControl";
            this.UnitlabelControl.Size = new System.Drawing.Size(40, 72);
            this.UnitlabelControl.TabIndex = 6;
            this.UnitlabelControl.Text = "Unit";
            // 
            // separatorControl1
            // 
            this.separatorControl1.LineColor = System.Drawing.Color.Black;
            this.separatorControl1.LineOrientation = System.Windows.Forms.Orientation.Vertical;
            this.separatorControl1.LineThickness = 1;
            this.separatorControl1.Location = new System.Drawing.Point(209, 16);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(19, 60);
            this.separatorControl1.TabIndex = 7;
            // 
            // ElectricUserControl1
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.RightpictureBox);
            this.Controls.Add(this.LeftpictureBox);
            this.Name = "ElectricUserControl1";
            this.Size = new System.Drawing.Size(242, 124);
            ((System.ComponentModel.ISupportInitialize)(this.LeftpictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightpictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LogoimageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox LeftpictureBox;
        private System.Windows.Forms.PictureBox RightpictureBox;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl ValuelabelControl;
        private DevExpress.XtraEditors.PictureEdit pictureEdit;
        private DevExpress.XtraEditors.LabelControl TitlelabelControl;
        private DevExpress.Utils.ImageCollection LogoimageCollection;
        private DevExpress.XtraEditors.LabelControl UnitlabelControl;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
    }
}
