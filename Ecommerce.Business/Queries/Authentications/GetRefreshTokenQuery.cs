using Ecommerce.Core.DTOs;
using Ecommerce.Core.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Queries.Authentications
{
    public class GetRefreshTokenQuery : IRequest<TokenDTO>
    {
        public string RefreshToken { get; set; }
        public int Id { get; set; }
    }
}
