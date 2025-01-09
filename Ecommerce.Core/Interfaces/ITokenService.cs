using Ecommerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateAccessToken(User user);
        public Task<string> GenerateRefreshToken(User user);

    }
}
