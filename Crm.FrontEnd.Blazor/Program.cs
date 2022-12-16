using Crm.FrontEnd.Blazor;
using Crm.FrontEnd.Blazor.Infrastructure;
using Framework.HttpServices;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var services = builder.Services;

services.AddOidcAuthentication(options =>
{
	builder.Configuration.Bind("LocalSSO", options.ProviderOptions);
});

Leads.Configurations.ServiceBootstrapper.Register(services);

services.AddSingleton
	(current => new System.Net.Http.HttpClient
	{
		BaseAddress =
			new System.Uri(builder.HostEnvironment.BaseAddress),
	});

services.AddScoped(x => new TokenProvider
(x.GetRequiredService<IJSRuntime>(),
builder.Configuration.GetRequiredSection("LocalSSO:Authority")?.Value,
builder.Configuration.GetRequiredSection("LocalSSO:ClientId")?.Value));

services.AddAntDesign();

services.AddLocalization();

var host = builder.Build();

await User.SetUserCulture(host);

await host.RunAsync();

