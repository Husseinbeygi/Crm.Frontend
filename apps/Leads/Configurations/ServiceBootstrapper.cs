using Microsoft.Extensions.DependencyInjection;

namespace Leads.Configurations;

public class ServiceBootstrapper
{
	public static void Register(IServiceCollection service)
	{
		service.AddScoped<Leads.Services.LeadsService>();
	}
}
