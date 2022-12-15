using Framework.HttpServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;

namespace Leads.Services;

public class LeadsService : ServiceBase
{
	public LeadsService(HttpClient http,
		AuthenticationStateProvider authenticationStateProvider, TokenProvider tokenProvider)
		: base(http,authenticationStateProvider, tokenProvider)
	{
		BaseUrl = "https://localhost:1403";
	}
}
