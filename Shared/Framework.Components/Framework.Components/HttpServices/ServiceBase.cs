using System.Net.Http.Headers;
using System.Net.Http.Json;
namespace Framework.HttpServices;


public abstract class ServiceBase : object
{
	public ServiceBase(System.Net.Http.HttpClient http)
	{
		Http = http;
	}

	protected string BaseUrl { get; set; }

	protected HttpClient Http { get; }

	public virtual
		async
		System.Threading.Tasks.Task<TResponse>
		GetAsync<TResponse>(string url, string query = null)
	{
		HttpResponseMessage response = null;

		try
		{
			var LanguageHeader = Http.DefaultRequestHeaders.AcceptLanguage;
			if (!LanguageHeader.Any(x => x.Value == Thread.CurrentThread.CurrentUICulture.Name))
			{
				Http.DefaultRequestHeaders.Remove("Accept-Language");
				Http.DefaultRequestHeaders.Add("Accept-Language",
					Thread.CurrentThread.CurrentUICulture.Name);

			}


			string requestUri =
				$"{BaseUrl}/{url}";

			if (string.IsNullOrWhiteSpace(query) == false)
			{
				requestUri =
					$"{requestUri}?{query}";
			}

			response =
				await
				Http.GetAsync
				(requestUri: requestUri)
				;

			response.EnsureSuccessStatusCode();

			if (response.IsSuccessStatusCode)
			{
				try
				{
					TResponse result =
						await
						response?.Content?.ReadFromJsonAsync<TResponse>();

					return result;
				}
				catch (System.NotSupportedException ex)
				{
					string errorMessage =
						$"Exception: {ex.Message} - The content type is not supported.";
				}
				catch (System.Text.Json.JsonException ex)
				{
					string errorMessage =
						$"Exception: {ex.Message} - Invalid JSON.";

				}
			}
		}
		catch (System.Net.Http.HttpRequestException ex)
		{
			string errorMessage =
				$"Exception: {ex.Message}";
		}
		finally
		{
			response?.Dispose();
		}

		return default;
	}
}
