using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EXIM.Web.Models.Account
{
    public class RegisterConfirmationModel
    {
        public string Email { get; set; }
        public string ReturnUrl { get; set; }
    }
}
