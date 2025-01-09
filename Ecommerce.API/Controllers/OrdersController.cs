using Ecommerce.Business.Commands.Orders;
using Ecommerce.Business.Queries.Orders;
using Ecommerce.Core.DTOs.Excels;
using Ecommerce.Core.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Drawing;
using Syncfusion.XlsIO;
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
        public async Task<IActionResult> OrderBillDetail([Required] int id, bool isDownload = false)
        {
            var result = await _mediator.Send(new GetOrderDetailQuery { Id = id});
            if (isDownload)
            {
                var fileDownloadName = $"Output.xlsx";
                var data = new ReportExcelDTO
                {
                    InvoiceId = result.Id.ToString(),
                    CreatedDate = result.CreatedDate.ToString("dd/MM/yyyy"),
                    CustomerName = result.UserName,
                    CustomerEmail = result.Email,
                    CustomerAddress = "8386 LA",
                    productDetails = result.orderDetails,
                };
                var stream = ExportExcelDetail.ExportOrderDetail(data);
                const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File(stream, contentType, fileDownloadName);
            }
            return OkResult(result);
        }    
    }
}
