using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace O365PWSync_SyncService
{
	class NetworkService
	{
		public void Main()
		{
			IPAddress ip = IPAddress.Parse("127.0.0.1");
			IPEndPoint local = new IPEndPoint(ip, 5999);
			Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			try
			{
				listener.Bind(local);
				listener.Listen(64);
				while (true)
				{
					Socket client = listener.Accept();
					new Thread(() => Handle(client)).Start();
				}
			}
			catch (Exception ex)
			{
				Logger.Send("Exception thrown when launching Network Service: " + ex.Message, Logger.LogLevel.ERROR, 12);
			}
		}

		public void Handle(Socket client)
		{
			try
			{
				NetworkStream netStream = new NetworkStream(client);
				StreamReader istream = new StreamReader(netStream);
				StreamWriter ostream = new StreamWriter(netStream);
				string command = istream.ReadLine();
				if (command == "notify")
				{
					string username = istream.ReadLine();
					string password = istream.ReadLine();

					Logger.Send("[Notify] Received a Password Changed Notification for: " + username, Logger.LogLevel.INFO, 13);
					if (ADHandler.IsUserInGroup(username, "Notify"))
					{
						Logger.Send("[Notify]" + username + " is in the target group", Logger.LogLevel.INFO, 13);
						DatabaseHandler.AddSync(username, password);
					}
					else
					{
						Logger.Send("[Notify]" + username + " is not in the target group", Logger.LogLevel.INFO, 13);
					}
				}
				else if (command == "test")
				{
					string username = istream.ReadLine();
					string password = istream.ReadLine();

					Logger.Send("[Filter] Received a Password Filter request for: " + username, Logger.LogLevel.INFO, 14);
					
					// LSA Blocks the ability to look up user groups. Suck I know.
					/*if (ADHandler.IsUserInGroup(username, "Filter"))
					{
						Logger.Send(username + " is in the target group", Logger.LogLevel.INFO, 14);*/

						bool validPassword = false;

						int score = 0;
						const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
						const string lowercase = "abcdefghijklmnopqrstuvwxyz";
						const string digit = "1234567890";
						const string symbol = "~`!@#$%^&*()+=_-{}[]\\|:;\"\'?/<>,.";

						if (password.IndexOfAny(uppercase.ToCharArray()) != -1) { score++; }
						if (password.IndexOfAny(lowercase.ToCharArray()) != -1) { score++; }
						if (password.IndexOfAny(digit.ToCharArray()) != -1) { score++; }
						if (password.IndexOfAny(symbol.ToCharArray()) != -1) { score++; }

						if (score >= 3 && password.Length >= 8 && password.Length <= 16) { validPassword = true; }

						ostream.WriteLine(validPassword ? "t" : "f");
						ostream.Flush();

						Logger.Send("[Filter] Password Filter results. Length: " + password.Length.ToString() + ". Score: " + score.ToString() + ". Verdict: " + (validPassword ? "PASSED" : "DENIED"), Logger.LogLevel.INFO, 15);
					/*}
					else
					{
						Logger.Send("[Filter]" + username + " is not in the target group", Logger.LogLevel.INFO, 14);

						ostream.WriteLine("t");
						ostream.Flush();
					}*/
				}
				else
				{
					ostream.WriteLine("ERROR");
					ostream.Flush();

					Logger.Send("Received bad data on network port. Possible localhost port scan?", Logger.LogLevel.ERROR, 16);
				}
			}
			catch (Exception ex)
			{
				Logger.Send("Exception thrown when handling network socket data: " + ex.Message, Logger.LogLevel.ERROR, 13);
			}
			client.Close();
		}
	}
}
