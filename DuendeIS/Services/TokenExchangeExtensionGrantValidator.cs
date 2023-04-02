using Duende.IdentityServer.Validation;
using IdentityModel;

namespace FinalMS.DuendeIS.Services;

public class TokenExchangeExtensionGrantValidator : IExtensionGrantValidator
{
    public string GrantType => OidcConstants.GrantTypes.TokenExchange;

    private readonly ITokenValidator _tokenValidator;

    public TokenExchangeExtensionGrantValidator(ITokenValidator tokenValidator)
    {
        _tokenValidator = tokenValidator;
    }

    public async Task ValidateAsync(ExtensionGrantValidationContext context)
    {
        var request = context.Request.Raw.ToString();

        var token = context.Request.Raw.Get(OidcConstants.TokenRequest.SubjectToken);

        if (String.IsNullOrEmpty(token))
        {
            context.Result = new GrantValidationResult(Duende.IdentityServer.Models.TokenRequestErrors.InvalidRequest, "token is missing");
            return;
        }

        var tokenValidateResult = await _tokenValidator.ValidateAccessTokenAsync(token);

        if (tokenValidateResult.IsError)
        {
            context.Result = new GrantValidationResult(Duende.IdentityServer.Models.TokenRequestErrors.InvalidGrant, "token invalid");
            return;
        }

        var subClaim = tokenValidateResult.Claims.FirstOrDefault(c => c.Type == "sub");

        if (subClaim is null)
        {
            context.Result = new GrantValidationResult(Duende.IdentityServer.Models.TokenRequestErrors.InvalidGrant, "subject claim missing");
            return;
        }

        context.Result = new GrantValidationResult(subClaim.Value, OidcConstants.TokenTypes.AccessToken, tokenValidateResult.Claims);
        return;
    }
}
