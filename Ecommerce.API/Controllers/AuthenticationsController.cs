using Ecommerce.Business.Commands.Authentications;
using Ecommerce.Business.Queries.Authentications;
using Ecommerce.Business.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public AuthenticationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetToken(LoginCommand request)
        {
            var result = await _mediator.Send(request);
            return OkResult(result);
        }

        [HttpGet("refresh-token")]
        public async Task<IActionResult> GetRefreshToken([Required] int userId, [Required] string refreshToken)
        {
            var result = await _mediator.Send(new GetRefreshTokenQuery { Id = userId, RefreshToken = refreshToken});
            return OkResult(result);
        }
    }
}
