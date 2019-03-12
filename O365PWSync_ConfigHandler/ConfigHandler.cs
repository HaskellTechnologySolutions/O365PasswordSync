using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;

namespace O365PWSync_ConfigHandler
{
	public static class ConfigHandler
	{
		private static string ConfigFile = null;

		public static string DatabaseFilename;
		public static string ClientID;
		public static string ClientSecret;
		public static string O365ServiceUsername;
		public static string O365ServicePassword;
		public static string ADServiceUsername;
		public static string ADServicePassword;
		public static string ADGroupName;
		public static string O365TenantName;
		public static string ADDomain;

		public static bool Initialize(string configFile)
		{
			ConfigFile = configFile;

			if (!SanityCheck()) { return false; }

			return true;
		}

		public static bool SanityCheck()
		{
			if (ConfigFile == null) { return false; }
			if (!File.Exists(ConfigFile)) { return false; }
			return true;
		}

		public static bool LoadConfig()
		{
			if (!SanityCheck()) { return false; }

			try
			{
				byte[] encrypted = File.ReadAllBytes(ConfigFile);
				byte[] decrypted = ProtectedData.Unprotect(encrypted, null, DataProtectionScope.LocalMachine);
				string jsonString = Encoding.Unicode.GetString(decrypted);

				JObject json = JObject.Parse(jsonString);
				DatabaseFilename = json["DatabaseFilename"].ToString();
				ClientID = json["ClientID"].ToString();
				ClientSecret = json["ClientSecret"].ToString();
				O365ServiceUsername = json["O365ServiceUsername"].ToString();
				O365ServicePassword = json["O365ServicePassword"].ToString();
				ADServiceUsername = json["ADServiceUsername"].ToString();
				ADServicePassword = json["ADServicePassword"].ToString();
				O365TenantName = json["O365TenantName"].ToString();
				ADGroupName = json["ADGroupName"].ToString();
				ADDomain = json["ADDomain"].ToString();

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public static bool SaveConfig()
		{
			if (!SanityCheck()) { return false; }

			try
			{
				JObject json = new JObject();
				json.Add("DatabaseFilename", DatabaseFilename);
				json.Add("ClientID", ClientID);
				json.Add("ClientSecret", ClientSecret);
				json.Add("O365ServiceUsername", O365ServiceUsername);
				json.Add("O365ServicePassword", O365ServicePassword);
				json.Add("ADServiceUsername", ADServiceUsername);
				json.Add("ADServicePassword", ADServicePassword);
				json.Add("O365TenantName", O365TenantName);
				json.Add("ADGroupName", ADGroupName);
				json.Add("ADDomain", ADDomain);

				string jsonString = json.ToString();
				byte[] decrypted = Encoding.Unicode.GetBytes(jsonString);
				byte[] encrypted = ProtectedData.Protect(decrypted, null, DataProtectionScope.LocalMachine);
				File.WriteAllBytes(ConfigFile, encrypted);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
