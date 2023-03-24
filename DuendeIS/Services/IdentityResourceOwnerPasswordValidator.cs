using Duende.IdentityServer.Validation;
using FinalMS.DuendeIS.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace FinalMS.DuendeIS.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var existUser = await _userManager.FindByEmailAsync(context.UserName);

            if (existUser == null)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("errors", "Email or Password is incorrect");
                context.Result.CustomResponse = errors;

                return;
            }

            var existPassword = await _userManager.CheckPasswordAsync(existUser, context.Password);

            if (existPassword == false)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("errors", "Email or Password is incorrect");
                context.Result.CustomResponse = errors;

                return;
            }

            context.Result = new GrantValidationResult(existUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }
    }
}
