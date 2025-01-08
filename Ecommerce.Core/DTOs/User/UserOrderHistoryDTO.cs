using Ecommerce.Core.DTOs.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTOs.User
{
    public class UserOrderHistoryDTO 
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public List<HistoryDTO> HistoryOrders { get; set; }
    }

    public class HistoryDTO
    {
        public int Id { get; set; }
        public decimal TotalOrder { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public List<OrderDetailDTO> orderDetails { get; set; }
    }
}
