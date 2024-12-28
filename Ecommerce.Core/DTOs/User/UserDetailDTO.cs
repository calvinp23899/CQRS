using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTOs.User
{
    public class UserDetailDTO
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
