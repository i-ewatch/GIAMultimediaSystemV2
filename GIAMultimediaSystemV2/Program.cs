using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using GIAMultimediaSystemV2.Configuration;
using GIAMultimediaSystemV2.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GIAMultimediaSystemV2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GateWaySetting GateWaySetting = InitialMethod.GateWayLoad();
            switch (GateWaySetting.ModeIndex)
            {
                case 0://感測器含影片
                    {
                        Application.Run(new SenserForm());
                    }
                    break;
                case 1://感測器含電表
                    {
                        Application.Run(new ElectricForm());
                    }
                    break;
            }
        }
    }
}
