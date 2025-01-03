using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTOs.Orders
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public decimal TotalOrder { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public List<OrderDetailDTO> orderDetails { get; set; }
    }

    public class OrderRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderDetailDTO
    {
        public int ProductId { get; set;}
        public string ProductName { get; set;}
        public int Quantity { get; set;}
        public decimal UnitPrice { get; set;}
        public decimal Total { get; set;}
    }
}
