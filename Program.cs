using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TraktRater
{
    static class Program
    {
        public static TraktRater MainWindow;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			//#*# move this to a button and allow the user to select the file location -aLzyEE
			bool asdfx= Sites.Listal.ListalTest("C:\\Users\\it\\Documents\\Listal Test\\Test.xml");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainWindow = new TraktRater();
            Application.Run(MainWindow);
        }
    }
}
