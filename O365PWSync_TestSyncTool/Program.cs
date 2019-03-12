using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using O365PWSync_O365Client;

namespace O365PWSync_TestSyncTool
{
	class Program
	{
		private static string ClientID = "IDHERE";
		private static string ClientSecret = "SECRETHERE";
		private static string userName = "USERNAMEHERE";
		private static string userPassword = "PASSWORDHERE";


		static void Main(string[] args)
		{
			MainAsync().Wait();
		}

		static async Task MainAsync()
		{
			O365Client client = new O365Client();

			try
			{
				await client.GetAccessToken("TenantName", ClientID, ClientSecret, userName, userPassword);
				await client.ChangePassword("testUser", "TestPassword4423");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}


			Console.WriteLine();
		}
	}
}
