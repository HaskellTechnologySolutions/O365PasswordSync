using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graph;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace O365PWSync_O365Client
{
	public class O365Client
	{

		AccessToken Token;
			
		public async Task ChangePassword(string username, string password)
		{
			DelegateAuthenticationProvider authProvider = new DelegateAuthenticationProvider((requestMessage) =>
			{
				requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Token.Token);
				return Task.FromResult(0);
			});

			GraphServiceClient gcs = new GraphServiceClient(authProvider);
			HttpProvider http = new HttpProvider();


			List<User> users = (await gcs.Users.Request().GetAsync()).ToList();
			User targetUser;

			targetUser = users.Find(x => x.UserPrincipalName.Split('@')[0] == username);

			if (targetUser == null)
			{
				throw new KeyNotFoundException("The specified User: " + username + " could not be found");
			}

			// GRAPH LIBRARY WHY U NO WORK!!!!
			/*User resultUser = await gcs.Users[targetUser.Id].Request().UpdateAsync(new User()
			{
				PasswordProfile = new PasswordProfile() { ForceChangePasswordNextSignIn = false, Password = password }
			});*/

			HttpClient hc = new HttpClient();
			hc.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Token.Token);
			HttpContent con = new StringContent("{\"passwordProfile\":{\"forceChangePasswordNextSignIn\":false,\"password\": \"" + password + "\"}}", Encoding.UTF8, "application/json");
			HttpResponseMessage resp = await hc.PatchAsync(new Uri(@"https://graph.microsoft.com/v1.0/users/" + targetUser.Id), con);

			if (resp.StatusCode != System.Net.HttpStatusCode.NoContent)
			{
				throw new InvalidOperationException("Failed to set the password for User: " + username + ". This likely means the password is of incorrect complexity. Full response: " + Environment.NewLine + resp.Headers + Environment.NewLine + resp.Content);
			}
		}

		public async Task GetAccessToken(string TenantName, string ClientID, string ClientSecret, string userName, string userPassword)
		{
			HttpClient httpc = new HttpClient();
			HttpRequestMessage request = new HttpRequestMessage();
			request.Method = HttpMethod.Post;
			request.RequestUri = new Uri(@"https://login.microsoftonline.com/" + TenantName + @".onmicrosoft.com/oauth2/token");

			Dictionary<string, string> parameters = new Dictionary<string, string>();
			parameters.Add("grant_type", "password");
			parameters.Add("client_id", ClientID);
			parameters.Add("client_secret", ClientSecret);
			parameters.Add("resource", @"https://graph.microsoft.com");
			parameters.Add("username", userName);
			parameters.Add("password", userPassword);
			FormUrlEncodedContent encoded = new FormUrlEncodedContent(parameters);

			request.Content = encoded;

			HttpResponseMessage response = await httpc.SendAsync(request);
			if (!response.IsSuccessStatusCode)
			{
				throw new InvalidOperationException("Invalid response code received indicating non-success in getting access token");
			}

			JObject responseObj = (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

			AccessToken retToken = new AccessToken();
			retToken.TokenType = responseObj["token_type"].ToString();
			retToken.ExpiresOn = responseObj["expires_on"].ToString();
			retToken.Token = responseObj["access_token"].ToString();
			retToken.RefreshToken = responseObj["refresh_token"].ToString();

			Token = retToken;
		}
	}
}
