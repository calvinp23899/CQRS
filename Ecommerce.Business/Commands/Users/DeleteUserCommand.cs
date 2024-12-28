using Ecommerce.Business.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Commands.Users
{
    public class DeleteUserCommand : CommandRequest, IRequest<bool>
    {
        public int Id { get; set; }
    }
}
