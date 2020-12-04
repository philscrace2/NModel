using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NModel.Visualization;

namespace MPVIDE
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
            ModelProgramGraphViewForm mv = new ModelProgramGraphViewForm("NModel MPV IDE");
            
            mv.WindowState = FormWindowState.Maximized;
            Application.Run(mv);
        }
    }
}
