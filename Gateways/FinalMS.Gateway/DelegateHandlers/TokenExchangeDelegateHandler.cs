using IdentityModel;
using IdentityModel.Client;

namespace FinalMS.Gateway.DelegateHandlers;

public class TokenExchangeDelegateHandler: DelegatingHandler
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    private string _accessToken;

    public TokenExchangeDelegateHandler(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<string> GetTokenAsync(string requestToken)
    {
        if (!string.IsNullOrEmpty(_accessToken))
        {
            return _accessToken;
        }

        var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = _config.GetSection("IdentityServerURL").Value,
            Policy = new DiscoveryPolicy { RequireHttps = false }
        });

        if (discovery.IsError)
        {
            throw discovery.Exception;
        }

        var tokenExchangeTokenRequest = new TokenExchangeTokenRequest
        {
            Address = discovery.TokenEndpoint,
            ClientId = _config.GetSection("ClientId").Value,
            ClientSecret = _config.GetSection("ClientSecret").Value,
            GrantType = OidcConstants.GrantTypes.TokenExchange,
            SubjectToken = requestToken,
            SubjectTokenType = OidcConstants.TokenTypeIdentifiers.AccessToken,
            Scope = "openid discount_fullpermission payment_fullpermission",
        };

        var tokenResponse = await _httpClient.RequestTokenExchangeTokenAsync(tokenExchangeTokenRequest);

        if(tokenResponse.IsError)
        {
            throw tokenResponse.Exception;
        }

        _accessToken = tokenResponse.AccessToken;

        return _accessToken;

    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var requestToken = request.Headers.Authorization.Parameter;

        var newToken = await GetTokenAsync(requestToken);

        request.SetBearerToken(newToken);

        return await base.SendAsync(request, cancellationToken);
    }
}
