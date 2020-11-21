using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Bluegrams.Application;

namespace RobotSpace
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            PortableSettingsProvider.ApplyProvider(Properties.Settings.Default);
            Application.Run(new MainForm());
        }
    }
}
