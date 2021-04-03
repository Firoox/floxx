using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Floxx.Core.DTO.Mail;

namespace Floxx.Core.Interfaces
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}
