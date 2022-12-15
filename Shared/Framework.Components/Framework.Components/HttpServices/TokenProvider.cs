using Microsoft.JSInterop;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Framework.HttpServices;


public class TokenProvider
{
	private readonly IJSRuntime _jS;

	public TokenProvider(IJSRuntime JS)
	{
		_jS = JS;
	}

	public async Task<string> ReadIdToken()
	{
		const string clientId = "184cf96c-5e2a-457f-bb8c-9514fe4e085d";
		var userDataKey = $"oidc.user:https://localhost:7117/:{clientId}";
		var userData = await _jS.InvokeAsync<string>("sessionStorage.getItem", userDataKey);
		var userDataObject = JsonSerializer.Deserialize<Root>(userData);
		return userDataObject?.id_token;
	}

}


public class Root
{
	[JsonPropertyName("id_token")]
	public string id_token { get; set; }

	[JsonPropertyName("session_state")]
	public string session_state { get; set; }

	[JsonPropertyName("scope")]
	public string scope { get; set; }

	[JsonPropertyName("profile")]
	public Profile profile { get; set; }

}


public class Profile
{
	[JsonPropertyName("s_hash")]
	public string s_hash { get; set; }

	[JsonPropertyName("sid")]
	public string sid { get; set; }

	[JsonPropertyName("sub")]
	public string sub { get; set; }

	[JsonPropertyName("auth_time")]
	public int auth_time { get; set; }

	[JsonPropertyName("idp")]
	public string idp { get; set; }

	[JsonPropertyName("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")]
	public string httpschemasxmlsoaporgws200505identityclaimsnameidentifier { get; set; }

	[JsonPropertyName("http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor")]
	public string httpschemasxmlsoaporgws200909identityclaimsactor { get; set; }

	[JsonPropertyName("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")]
	public string httpschemasxmlsoaporgws200505identityclaimsgivenname { get; set; }

	[JsonPropertyName("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")]
	public string httpschemasxmlsoaporgws200505identityclaimssurname { get; set; }

	[JsonPropertyName("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")]
	public string httpschemasxmlsoaporgws200505identityclaimsname { get; set; }

	[JsonPropertyName("OrganizationId")]
	public string OrganizationId { get; set; }

	[JsonPropertyName("BusinessUnitId")]
	public string BusinessUnitId { get; set; }

	[JsonPropertyName("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality")]
	public string httpschemasxmlsoaporgws200505identityclaimslocality { get; set; }

	[JsonPropertyName("CurrentUICulture")]
	public string CurrentUICulture { get; set; }

	[JsonPropertyName("amr")]
	public List<string> amr { get; set; }
}

