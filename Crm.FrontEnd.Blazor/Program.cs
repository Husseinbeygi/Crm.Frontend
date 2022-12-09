using Crm.FrontEnd.Blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient(sp => new HttpClient 
{
	BaseAddress = 
		new Uri(builder.HostEnvironment.BaseAddress) 
});

builder.Services.AddOidcAuthentication(options =>
{
	builder.Configuration.Bind("LocalSSO", options.ProviderOptions);
});

builder.Services.AddSingleton<Leads.Services.LeadsService>();

builder.Services.AddSingleton
	(current => new System.Net.Http.HttpClient
	{
		BaseAddress =
			new System.Uri(builder.HostEnvironment.BaseAddress),
	});


//await builder.Build().RunAsync();

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

