using Ecommerce.Business.Commands.Products;
using Ecommerce.Business.Commands.Users;
using Ecommerce.Business.Models.Products;
using Ecommerce.Business.Queries.Products;
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
    public class ProductsController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand request)
        {
            var result = await _mediator.Send(request);
            return OkResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var result = await _mediator.Send(new GetAllProductQuery());
            return OkResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([Required] int id, ProductUpdateRequest req)
        {
            var request = new UpdateProductCommand
            {
                ProductId = id,
                ProductName = req.ProductName,
                Price = req.Price
            };
            var result = await _mediator.Send(request);
            return OkResult(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([Required] int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand { ProductId = id});
            return OkResult(result);
        }
    }
}
