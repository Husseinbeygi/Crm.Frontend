using Framework.HttpServices;

namespace Leads.Services;

public class LeadsService : ServiceBase
{
	public LeadsService(HttpClient http) : base(http)
	{
		BaseUrl = "https://localhost:1403";
	}
}
