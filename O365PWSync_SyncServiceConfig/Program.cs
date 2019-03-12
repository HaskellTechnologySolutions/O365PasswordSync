using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace O365PWSync_SyncServiceConfig
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
			Application.Run(new Form1());

			Application.ApplicationExit += Application_ApplicationExit;
		}

		private static void Application_ApplicationExit(object sender, EventArgs e)
		{
			MessageBox.Show("If this is the first time configuration for the O365 PW Sync tool, you will need to restart the machine to begin synchronization. If this is a configuration update, you simply need to restart the O365PWSync Service.");
		}
	}
}
