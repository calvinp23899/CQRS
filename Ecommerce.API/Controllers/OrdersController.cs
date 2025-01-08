using Ecommerce.Business.Commands.Orders;
using Ecommerce.Business.Commands.Users;
using Ecommerce.Business.Models.Orders;
using Ecommerce.Business.Queries.Orders;
using Ecommerce.Business.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrdersController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _mediator.Send(new GetAllOrdersQuery());
            return OkResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderBill(CreateOrderBillCommand request)
        {
            var result = await _mediator.Send(request);
            return OkResult(result);
        }

        [HttpGet("{id:int}/detail")]
        public async Task<IActionResult> OrderBillDetail([Required] int id)
        {
            var result = await _mediator.Send(new GetOrderDetailQuery { Id = id});
            return OkResult(result);
        }
    }
}
