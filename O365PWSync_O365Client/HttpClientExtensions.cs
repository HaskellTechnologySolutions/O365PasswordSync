using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace O365PWSync_O365Client
{
	public static class HttpClientExtensions
	{
		public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, Uri requestUri, HttpContent iContent)
		{
			var method = new HttpMethod("PATCH");
			var request = new HttpRequestMessage(method, requestUri)
			{
				Content = iContent
			};

			HttpResponseMessage response = new HttpResponseMessage();
			try
			{
				response = await client.SendAsync(request);
			}
			catch (TaskCanceledException e)
			{
				Console.WriteLine("ERROR: " + e.ToString());
			}

			return response;
		}
	}
}
