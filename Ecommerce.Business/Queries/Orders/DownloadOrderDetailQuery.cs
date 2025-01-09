using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Queries.Orders
{
    public class DownloadOrderDetailQuery : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
