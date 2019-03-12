using System;
using System.Collections.Generic;
using System.Threading;
using System.ServiceProcess;
using System.IO;
using O365PWSync_ConfigHandler;

namespace O365PWSync_SyncService
{
	class O365PWSyncService : ServiceBase
	{
		Thread netWorker;
		Thread syncWorker;

		public O365PWSyncService()
		{
		}

		static void Main()
		{
			ServiceBase.Run(new O365PWSyncService());
		}

		protected override void OnStart(string[] args)
		{
			try
			{
				if (!OnStartChecks()) { this.Stop(); return; }

				Logger.Send("Cleaning up stale Sync items", Logger.LogLevel.INFO, 11);
				DatabaseHandler.ClearStaleSyncs();

				Logger.Send("Service Starting, launching threads", Logger.LogLevel.INFO, 11);

				NetworkService netSvc = new NetworkService();
				netWorker = new Thread(() => netSvc.Main());
				netWorker.IsBackground = true;
				netWorker.Start();

				SyncService syncSvc = new SyncService();
				syncWorker = new Thread(() => syncSvc.Main());
				syncWorker.IsBackground = true;
				syncWorker.Start();


				base.OnStart(args);
			}
			catch (Exception)
			{
				this.Stop();
			}
		}

		protected override void OnShutdown()
		{
			Logger.Send("Service Stopping, aborting threads", Logger.LogLevel.INFO, 11);

			netWorker.Abort();
			syncWorker.Abort();
			
			base.OnShutdown();
		}

		private void InitializeComponent()
		{
			this.ServiceName = "O365PWSync";
		}

		private bool OnStartChecks()
		{
			if (!Logger.Initialize()) { return false; }

			string configFilePath = System.Configuration.ConfigurationManager.AppSettings["secureConfigFile"];

			if (configFilePath == null || configFilePath == String.Empty)
			{
				Logger.Send("App.Config file is missing the secureConfigFile setting. Cannot start the Sync service.", Logger.LogLevel.ERROR, 1);
				return false;
			}

			if (!ConfigHandler.Initialize(configFilePath))
			{
				Logger.Send("Configuration Handler failed to initialize. The secure configuration file likely does not exist. Has the Configuration tool been run?", Logger.LogLevel.ERROR, 2);
				return false;
			}

			if (!ConfigHandler.LoadConfig())
			{
				Logger.Send("Configuration handler failed to load settings from the secure configuration file. Likely the file is corrupt or invalid. Recreate the configuration withe the config tool.", Logger.LogLevel.ERROR, 3);
				return false;
			}

			if (!DatabaseHandler.Initialize(ConfigHandler.DatabaseFilename))
			{
				Logger.Send("Database handler failed to initialize.", Logger.LogLevel.ERROR, 4);
				return false;
			}

			if (!ADHandler.Initialize())
			{
				Logger.Send("AD Handler failed to initialize.", Logger.LogLevel.ERROR, 1004);
				return false;
			}

			return true;
		}
	}
}
