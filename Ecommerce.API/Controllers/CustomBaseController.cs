using Ecommerce.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.API.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        internal IActionResult OkResult<T>(T data, string? ErrorMessage = null)
        {
            var result = new Response<T>
            {
                Data = data,
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = ErrorMessage,
            };
            return Ok(result);
        }
    }
}
