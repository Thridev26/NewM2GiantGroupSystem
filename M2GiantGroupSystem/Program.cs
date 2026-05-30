using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M2GiantGroupSystem
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //  Application.Run(new Form1());
            //  Application.Run(new View_Clients_A());
            //Application.Run(new CaptureJobRequest_A());
            // Application.Run(new EditJobRequest_A());
            //  Application.Run(new Add_Details_to_R_Items());
            //Application.Run(new ViewJobRequests());
            Application.Run(new Add_Client_A());
        }
    }
}
