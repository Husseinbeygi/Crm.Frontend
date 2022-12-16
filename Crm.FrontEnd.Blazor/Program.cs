using Crm.FrontEnd.Blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Http;
using Framework.HttpServices;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOidcAuthentication(options =>
{
	builder.Configuration.Bind("LocalSSO", options.ProviderOptions);
});

Leads.Configurations.ServiceBootstrapper.Register(builder.Services);

builder.Services.AddSingleton
	(current => new System.Net.Http.HttpClient
	{
		BaseAddress =
			new System.Uri(builder.HostEnvironment.BaseAddress),
	});

builder.Services.AddScoped<TokenProvider>();

builder.Services.AddAntDesign();

builder.Services.AddLocalization();

var host = builder.Build();

CultureInfo culture;
var js = host.Services.GetRequiredService<IJSRuntime>();
var result = await js.InvokeAsync<string>("blazorCulture.get");

if (result != null)
{
	culture = new CultureInfo(result);
}
else
{
	culture = new CultureInfo("en-US");
	await js.InvokeVoidAsync("blazorCulture.set", "en-US");
}

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await host.RunAsync();

