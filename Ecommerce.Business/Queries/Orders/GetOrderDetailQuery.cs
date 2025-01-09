using Ecommerce.Core.DTOs.Orders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Queries.Orders
{
    public class GetOrderDetailQuery : IRequest<OrderDTO>
    {
        public int Id { get; set; }
    }
}
