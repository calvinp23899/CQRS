using Ecommerce.Core.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Queries.User
{
    public class GetUserOrderHistoryQuery : IRequest<UserOrderHistoryDTO>
    {
        public int Id { get; set; }
    }
}
