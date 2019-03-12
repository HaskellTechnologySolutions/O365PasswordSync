using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.IO;

namespace O365PWSync_SyncService
{
	static class DatabaseHandler
	{
		private static string DBFilename;
		private static string ConnectionString;

		public static bool Initialize(string filename)
		{
			DBFilename = filename;

			SQLiteConnectionStringBuilder csb = new SQLiteConnectionStringBuilder();
			csb.DataSource = DBFilename;
			csb.Version = 3;
			ConnectionString = csb.ToString();

			if (!File.Exists(DBFilename))
			{
				if (!CreateDB())
				{
					return false;
				}
			}

			if (!TestDB())
			{
				Logger.Send("File specified in configuration is not a valid SQLite database: " + DBFilename, Logger.LogLevel.ERROR, 7);
				return false;
			}

			return true;
		}

		private static bool TestDB()
		{
			byte[] bytes = new byte[17];
			using (FileStream fs = new FileStream(DBFilename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				fs.Read(bytes, 0, 16);
			}
			string chkStr = System.Text.ASCIIEncoding.ASCII.GetString(bytes);
			return chkStr.Contains("SQLite format");
		}

		private static bool CreateDB()
		{
			try
			{
				SQLiteConnection.CreateFile(DBFilename);

				using (SQLiteConnection dbc = new SQLiteConnection(ConnectionString))
				{
					dbc.Open();

					using (SQLiteTransaction trans = dbc.BeginTransaction())
					{
						SQLiteCommand com = new SQLiteCommand();
						com.Transaction = trans;
						com.CommandText = "CREATE TABLE PendingSyncs (ID INTEGER PRIMARY KEY, Username TEXT, Password TEXT, Timestamp INTEGER, Processed INTEGER DEFAULT 0);";
						com.ExecuteNonQuery();

						trans.Commit();
					}

					dbc.Close();
				}

				Logger.Send("Created new SQLite Database: " + DBFilename, Logger.LogLevel.INFO, 6);
			}
			catch (Exception ex)
			{
				Logger.Send("Exception thrown while creating SQLite Database: " + ex.Message, Logger.LogLevel.ERROR, 5);
				return false;
			}

			return true;
		}

		public static List<DBUser> GetSyncs()
		{
			try
			{
				List<DBUser> users = new List<DBUser>();

				using (SQLiteConnection dbc = new SQLiteConnection(ConnectionString))
				{
					dbc.Open();

					SQLiteCommand com = new SQLiteCommand();
					com.Connection = dbc;
					com.CommandText = "UPDATE PendingSyncs SET Processed=1 WHERE Processed=0;";
					com.ExecuteNonQuery();


					com.CommandText = "SELECT ID, Username, Password, Timestamp, Processed FROM PendingSyncs WHERE Processed=1 ORDER BY Timestamp ASC;";
					SQLiteDataReader sdr = com.ExecuteReader();

					while (sdr.Read())
					{
						long id, timestamp;
						int processed;
						string un, pwencb64, pw;
						byte[] pwenc, pwdec;

						try { id = (long)(int)sdr["ID"]; } catch (InvalidCastException) { id = (long)sdr["ID"]; }

						un = sdr["Username"].ToString();

						pwencb64 = sdr["Password"].ToString();

						try { timestamp = (long)(int)sdr["Timestamp"]; } catch (InvalidCastException) { timestamp = (long)sdr["Timestamp"]; }

						try { processed = (int)sdr["Processed"]; } catch (InvalidCastException) { processed = (int)(long)sdr["Processed"]; }


						pwenc = Convert.FromBase64String(pwencb64);
						pwdec = ProtectedData.Unprotect(pwenc, null, DataProtectionScope.LocalMachine);
						pw = Encoding.Unicode.GetString(pwdec);

						DBUser user = new DBUser();
						user.ID = id;
						user.Username = un;
						user.Password = pw;
						user.Timestamp = timestamp;
						user.Processed = (SyncProcessedStatus)processed;

						users.Add(user);
					}

					dbc.Close();
				}

				return users;
			}
			catch (Exception ex)
			{
				Logger.Send("Exception thrown while retrieving pending sync items from database: " + ex.ToString(), Logger.LogLevel.ERROR, 8);
				return null;
			}
		}

		public static void UpdateSyncStatus(List<DBUser> users)
		{
			try
			{
				using (SQLiteConnection dbc = new SQLiteConnection(ConnectionString))
				{
					dbc.Open();

					foreach (DBUser user in users)
					{
						SQLiteCommand com = new SQLiteCommand();
						com.Connection = dbc;
						com.CommandText = "UPDATE PendingSyncs SET Processed=@PROCESSED WHERE ID=@ID;";
						com.Parameters.AddWithValue("PROCESSED", (int)user.Processed);
						com.Parameters.AddWithValue("ID", user.ID);

						com.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Send("Exception thrown while updating sync process flags in database: " + ex.Message, Logger.LogLevel.ERROR, 9);
			}
		}

		public static void AddSync(string username, string password)
		{
			try
			{
				using (SQLiteConnection dbc = new SQLiteConnection(ConnectionString))
				{
					dbc.Open();
					SQLiteCommand com = new SQLiteCommand();
					com.Connection = dbc;
					com.CommandText = "INSERT INTO PendingSyncs (Username, Password, Timestamp) VALUES (@USERNAME, @PASSWORD, @TIMESTAMP)";

					byte[] pwenc, pwdec;
					string pwencb64;

					pwdec = Encoding.Unicode.GetBytes(password);
					pwenc = ProtectedData.Protect(pwdec, null, DataProtectionScope.LocalMachine);
					pwencb64 = Convert.ToBase64String(pwenc);

					long timestamp = DateTime.UtcNow.Ticks;

					com.Parameters.AddWithValue("USERNAME", username);
					com.Parameters.AddWithValue("PASSWORD", pwencb64);
					com.Parameters.AddWithValue("TIMESTAMP", timestamp);

					com.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				Logger.Send("Exception thrown while adding sync items to database: " + ex.Message, Logger.LogLevel.ERROR, 10);
			}
		}

		public static void ClearStaleSyncs()
		{
			try
			{
				using (SQLiteConnection dbc = new SQLiteConnection(ConnectionString))
				{
					dbc.Open();
					SQLiteCommand com = new SQLiteCommand();
					com.Connection = dbc;
					com.CommandText = "DELETE FROM PendingSyncs WHERE Processed > 1;";
					
					com.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				Logger.Send("Exception thrown while clearing stale sync items: " + ex.Message, Logger.LogLevel.ERROR, 10);
			}
		}
	}
}
