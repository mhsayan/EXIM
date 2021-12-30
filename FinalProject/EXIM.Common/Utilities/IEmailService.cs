using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.Common.Utilities
{
    public interface IEmailService
    {
        void SendEmail(string receiver, string subject, string body);
    }
}
