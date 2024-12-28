using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Models
{
    public class Order : BaseEntity
    {
        public decimal TotalPrice { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
