using CHSachWebAppData.Models;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CHSachWebAppData.Controllers
{
    public class PaymentService
    {
        public static bool PayWithCard(PaymentModel paymentModel)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["SecretKey"];
            var chargeOptions = new ChargeCreateOptions
            {
                Amount = paymentModel.Amount,
                Currency = "vnd",
                Source = paymentModel.Token,
                Description = paymentModel.Description
            };
            var service = new ChargeService();
            var response = service.Create(chargeOptions);

            if (response != null && response.Status.ToLower() == "succeeded")
            {
                return true;
            }
            return false;
        }
       
    }
}