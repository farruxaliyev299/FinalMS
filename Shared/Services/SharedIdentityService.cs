using Microsoft.AspNetCore.Http;

namespace FinalMS.Shared.Services;

public class SharedIdentityService : ISharedIdentityService
{
    private IHttpContextAccessor _context { get; set; }

    public SharedIdentityService(IHttpContextAccessor context)
    {
        _context = context;
    }

    public string GetUserId => _context.HttpContext.User.FindFirst("sub").Value;
}
