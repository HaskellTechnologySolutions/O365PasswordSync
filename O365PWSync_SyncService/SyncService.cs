using O365PWSync_ConfigHandler;
using O365PWSync_O365Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace O365PWSync_SyncService
{
	class SyncService
	{
		Timer _timerSync;
		const long SYNC_WAIT_TIME = 1000 * 60 * 1;

		public void Main()
		{
			_timerSync = new Timer(TimerRunSync, null, SYNC_WAIT_TIME, Timeout.Infinite);
		}

		private void TimerRunSync(Object state)
		{
			List<DBUser> users = DatabaseHandler.GetSyncs();

			O365Client client = new O365Client();

			try
			{
				client.GetAccessToken(ConfigHandler.O365TenantName, ConfigHandler.ClientID, ConfigHandler.ClientSecret, ConfigHandler.O365ServiceUsername, ConfigHandler.O365ServicePassword).Wait();
			}
			catch (AggregateException ae)
			{
				foreach (Exception e in ae.Flatten().InnerExceptions)
				{
					Logger.Send("Exception thrown when requesting an Access Token from O365: " + e.Message, Logger.LogLevel.ERROR, 17);
				}
			}
			catch (Exception ex)
			{
				Logger.Send("Exception thrown when requesting an Access Token from O365: " + ex.Message, Logger.LogLevel.ERROR, 17);
			}

			foreach (DBUser user in users)
			{
				Logger.Send("Syncing password for user: " + user.Username + " originally changed at " + user.TimestampDatetime.ToLocalTime().ToString(), Logger.LogLevel.INFO, 17);

				try
				{
					client.ChangePassword(user.Username, user.Password).Wait();
					user.Processed = SyncProcessedStatus.COMPLETE;
				}
				catch (AggregateException ae)
				{
					foreach (Exception e in ae.Flatten().InnerExceptions)
					{
						Logger.Send("Exception thrown when changing users password: " + e.Message, Logger.LogLevel.ERROR, 17);
					}
					user.Processed = SyncProcessedStatus.FAILED;
				}
				catch (Exception ex)
				{
					Logger.Send("Exception thrown when changing users password: " + ex.Message, Logger.LogLevel.ERROR, 17);
					user.Processed = SyncProcessedStatus.FAILED;
				}
			}

			DatabaseHandler.UpdateSyncStatus(users);
			
			_timerSync.Change(SYNC_WAIT_TIME, Timeout.Infinite);
		}
	}

}
