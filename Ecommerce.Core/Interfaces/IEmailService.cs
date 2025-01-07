using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(List<string> toEmails, string subject, string body, List<string> ccEmails );
    }
}
