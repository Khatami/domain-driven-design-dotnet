using Microsoft.AspNetCore.WebUtilities;

namespace Marketplace.Infrastructure
{
	// <summary>
	/// PurgoMalum is a simple, free, RESTful web service for filtering and removing content of profanity, obscenity and other unwanted text.
	/// Check http://www.purgomalum.com
	/// </summary>
	public class PurgomalumClient
	{
		private readonly HttpClient _httpClient;
		public PurgomalumClient() : this(new HttpClient()) { }
		public PurgomalumClient(HttpClient httpClient) => _httpClient = httpClient;
		public async Task<bool> CheckForProfanity(string text)
		{
			try
			{
				var url = QueryHelpers.AddQueryString("https://www.purgomalum.com/service/containsprofanity", "text", text);
				_httpClient.Timeout = TimeSpan.FromSeconds(2);
				var result = await _httpClient.GetAsync(url);

				var value = await result.Content.ReadAsStringAsync();
				return bool.Parse(value);
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}