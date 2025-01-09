using Ecommerce.Core.DTOs.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTOs.Excels
{
    public class ReportExcelDTO
    {
        public string Address { get; set; } = "123 Street AA";
        public string City { get; set; } = "New York";
        public string Country { get; set; } = "USA";
        public string PhoneNumber { get; set; } = "+81 231-231-9999";
        public string InvoiceId { get; set; }
        public string CreatedDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhoneNumber { get; set; } = "+84 231-231-9999";
        public List<OrderDetailDTO> productDetails { get; set; }

    }
}
