using Ecommerce.Business.Commands.Users;
using Ecommerce.Business.Models.Users;
using Ecommerce.Business.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllUser() 
        {
            var result = await _mediator.Send(new GetUserQuery());
            return OkResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserById([Required] int id, UserUpdateRequest requestUpdate)
        {
            var request = new UpdateUserCommand
            {
                Id = id,
                FirstName = requestUpdate.FirstName,
                LastName = requestUpdate.LastName,
                PhoneNumber = requestUpdate.PhoneNumber,
                Password = requestUpdate.Password,
            };
            var result = await _mediator.Send(request);
            return OkResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand request)
        {
            var result = await _mediator.Send(request);
            return OkResult(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser([Required] int id)
        {
            var result = await _mediator.Send(new DeleteUserCommand { Id = id});
            return OkResult(result);
        }
    }
}
