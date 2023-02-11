using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHSachWebAppData.Models
{
    public class PaymentModel
    {
        public string Token { get; set; }

        public long Amount { get; set; }

        public string Description { get; set; }
    }
}