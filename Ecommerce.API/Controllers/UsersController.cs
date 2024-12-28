using Ecommerce.Business.Commands.User;
using Ecommerce.Business.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser() 
        {
            var result = await _mediator.Send(new GetUserQuery());
            return OkResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserById()
        {
            var result = await _mediator.Send(new UpdateUserCommand());
            return OkResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand request)
        {
            var result = await _mediator.Send(request);
            return OkResult(result);
        }
    }
}
