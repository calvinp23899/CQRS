using Ecommerce.Business.Models;
using Ecommerce.Core.DTOs;
using Ecommerce.Core.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Commands.Authentications
{
    public class LoginCommand : CommandRequest, IRequest<TokenDTO>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
