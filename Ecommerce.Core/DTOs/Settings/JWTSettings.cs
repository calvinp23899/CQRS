using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTOs.Settings
{
    public class JWTSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenExpireTime { get; set; }
        public int RefreshTokenExpireTime { get; set; }
        public string SecretKey { get; set; }
    }
}
