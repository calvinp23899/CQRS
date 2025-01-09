using Ecommerce.Core.DTOs.Orders;
using Ecommerce.Core.DTOs.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Queries.Orders
{
    public class GetAllOrdersQuery : IRequest<List<OrderDTO>>
    {
    }
}
