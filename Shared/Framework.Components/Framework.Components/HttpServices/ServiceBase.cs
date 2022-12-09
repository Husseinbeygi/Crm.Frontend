
using System.Net.Http.Json;
namespace Framework.HttpServices;


public abstract class ServiceBase : object
{
	public ServiceBase(System.Net.Http.HttpClient http)
	//,Client.Services.LogsService logsService) : base()
	{
		Http = http;
		//	LogsService = logsService;
	}

	protected string BaseUrl { get; set; }

	protected HttpClient Http { get; }

	//protected Client.Services.LogsService LogsService { get; }

	public virtual
		async
		System.Threading.Tasks.Task<TResponse>
		GetAsync<TResponse>(string url, string query = null)
	{
		HttpResponseMessage response = null;

		try
		{
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
					// ReadFromJsonAsync -> Extension Method -> using System.Net.Http.Json;
					TResponse result =
						await
						response?.Content?.ReadFromJsonAsync<TResponse>();

					return result;
				}

				// When content type is not valid
				catch (System.NotSupportedException ex)
				{
					string errorMessage =
						$"Exception: {ex.Message} - The content type is not supported.";

					// Static داخل تابع غیر
					//LogsService.AddLog(type: GetType(), message: errorMessage);

					// Static داخل تابع
					//LogsService.AddLog(type: typeof(ServiceBase), message: errorMessage);
				}

				// Invalid JSON
				catch (System.Text.Json.JsonException ex)
				{
					string errorMessage =
						$"Exception: {ex.Message} - Invalid JSON.";

					//	LogsService.AddLog(type: GetType(), message: errorMessage);
				}
			}
		}
		catch (System.Net.Http.HttpRequestException ex)
		{
			string errorMessage =
				$"Exception: {ex.Message}";

			//	LogsService.AddLog(type: GetType(), message: errorMessage);
		}
		finally
		{
			response?.Dispose();
		}

		return default;
	}
}
