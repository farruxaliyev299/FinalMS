using Microsoft.AspNetCore.Mvc;
using FinalMS.Shared.DTOs;

namespace FinalMS.Shared.ControllerBases;

public class CustomControllerBase: ControllerBase
{
    public IActionResult CreateActionResultInstance<T>(Response<T> response)
    {
        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };
    }
}
