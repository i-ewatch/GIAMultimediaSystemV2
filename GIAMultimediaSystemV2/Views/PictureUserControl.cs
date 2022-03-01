using DevExpress.XtraEditors;
using GIAMultimediaSystemV2.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GIAMultimediaSystemV2.Views
{
    public partial class PictureUserControl : Field4UserControl
    {
        FileStream[] Images = new FileStream[0];
        string MDirectory = string.Empty;
        DateTime PictureTime { get; set; }
        public PictureUserControl(MediaPlaySetting mediaPlaySetting)
        {
            InitializeComponent();
            MediaPlaySetting = mediaPlaySetting;
            Images_Count();
        }
        private void Images_Count()
        {
            string mDirectory = MediaPlaySetting.PicturePath;

            if (Directory.Exists(mDirectory) == true)
            {
                var file = Directory.GetFiles(mDirectory, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".jpg") || s.EndsWith(".png")).ToList();
                if (Images.Length != file.Count || MDirectory != mDirectory)
                {
                    Images = new FileStream[file.Count];
                    for (int i = 0; i < file.Count; i++)
                    {
                        Images[i] = new FileStream(file[i], FileMode.Open, FileAccess.Read);
                    }
                    MDirectory = mDirectory;
                    imageSlider1.Images.Clear() ;
                    foreach (var item in Images)
                    {
                        imageSlider1.Images.Add(Image.FromStream(item));
                    }
                    foreach (var item in Images)
                    {
                        item.Close();
                    }
                }
            }
        }
        public override void TextChange()
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(PictureTime);
            if (timeSpan.TotalSeconds >= 10)
            {
                Images_Count();
            }
        }
    }
}
