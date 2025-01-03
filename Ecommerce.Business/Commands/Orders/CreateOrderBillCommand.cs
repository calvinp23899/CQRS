using Ecommerce.Business.Models;
using Ecommerce.Core.DTOs.Orders;
using Ecommerce.Core.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Commands.Orders
{
    public class CreateOrderBillCommand : CommandRequest, IRequest<bool>
    {
        public List<OrderRequest> Data { get; set; }

    }
}
