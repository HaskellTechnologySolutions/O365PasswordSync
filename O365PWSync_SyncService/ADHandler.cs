using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices.AccountManagement;
using O365PWSync_ConfigHandler;

namespace O365PWSync_SyncService
{
	static class ADHandler
	{
		private static PrincipalContext context;
		public static string DomainName;
		public static string GroupName;
		private static GroupPrincipal _group;
		private static string _username;
		private static string _password;

		public static bool Initialize()
		{
			DomainName = ConfigHandler.ADDomain;
			GroupName = ConfigHandler.ADGroupName;
			_username = ConfigHandler.ADServiceUsername;
			_password = ConfigHandler.ADServicePassword;

			try
			{
				ContextType type = ContextType.Domain;

				if (Environment.MachineName == DomainName)
				{
					type = ContextType.Machine;
				}

				context = new PrincipalContext(type, DomainName);
			
				try
				{
					_group = GroupPrincipal.FindByIdentity(context, GroupName);
					if (_group == null)
					{
						Logger.Send("Bad Configuration, could not find the group: " + GroupName + " in the directory!", Logger.LogLevel.ERROR, 1000);
						return false;
					}
				}
				catch (MultipleMatchesException)
				{
					Logger.Send("Bad Configuration, multiple groups match the group: " + GroupName + " in the directory!", Logger.LogLevel.ERROR, 1000);
					return false;
				}

				return true;
			}
			catch (Exception ex)
			{
				Logger.Send("Exception thrown while initializing the AD Handler: " + ex.Message, Logger.LogLevel.ERROR, 1000);
				return false;
			}
		}

		public static bool ReloadGroup()
		{
			try
			{
				_group = GroupPrincipal.FindByIdentity(context, GroupName);
				if (_group == null)
				{
					Logger.Send("Bad Configuration, could not find the group: " + GroupName + " in the directory!", Logger.LogLevel.ERROR, 1000);
					return false;
				}
			}
			catch (MultipleMatchesException)
			{
				Logger.Send("Bad Configuration, multiple groups match the group: " + GroupName + " in the directory!", Logger.LogLevel.ERROR, 1000);
				return false;
			}

			return true;
		}

		public static bool IsUserInGroup(string username, string caller)
		{
			try
			{
				System.Diagnostics.Stopwatch usercheckwatch = new System.Diagnostics.Stopwatch();
				usercheckwatch.Start();
				UserPrincipal user = new UserPrincipal(context);
				user.SamAccountName = username;
				PrincipalSearcher searcher = new PrincipalSearcher(user);
				user = searcher.FindOne() as UserPrincipal;
				usercheckwatch.Stop();

				if (user == null)
				{
					Logger.Send("User in Group check requested for user: " + username + ", but no user can be found by the AD Handler!", Logger.LogLevel.ERROR, 1000);
					return false;
				}

				if (_group == null)
				{
					Logger.Send("AD Handler checking IsUserInGroup but _group hasnt been set yet. Coding error?", Logger.LogLevel.ERROR, 1000);
					return false;
				}

				System.Diagnostics.Stopwatch groupcheckwatch = new System.Diagnostics.Stopwatch();
				groupcheckwatch.Start();
				bool ret = user.IsMemberOf(_group);
				groupcheckwatch.Stop();
				
				Logger.Send("[" + caller + "] User search took: " + usercheckwatch.ElapsedMilliseconds + " milliseconds. Group search took: " + groupcheckwatch.ElapsedMilliseconds + " milliseconds.", Logger.LogLevel.INFO, 1000);

				return ret;
			}
			catch (Exception ex)
			{
				Logger.Send("Exception thrown while checking IsUserInGroup in AD Handler: " + ex.Message, Logger.LogLevel.ERROR, 1000);
				return false;
			}
		}


	}
}
