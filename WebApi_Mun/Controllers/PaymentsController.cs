using System.Web.Http;
using System.Web.Http.Cors;
using MercadoPago.Config;
using MercadoPago.Client.Preference;
using MercadoPago.Resource.Preference;
using System.Collections.Generic;
using MercadoPago.Client.Payment;
using MercadoPago.Client.Common;
using MercadoPago.Client.PaymentMethod;
using MercadoPago.Resource.PaymentMethod;
using MercadoPago.Resource;
using System.Threading.Tasks;
using MercadoPago.Resource.Common;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using MercadoPago.Client;
using MercadoPago.Resource.Payment;

namespace WebApi_Mun.Controllers
{
    //[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [EnableCors(origins: "https://pañaleracolores.com.ar/", headers: "*", methods: "*")]
    public class PaymentsController : ApiController 
    {
        string token = "APP_USR-7912324733117961-031615-8c01725371458674feaeb2d8ecad41ba-1311490712";
    
        [Route("api/payments/post/")]
        [HttpPost]
        public async Task<Preference> PostAsync([FromBody] List<ItemBuy> item)
        {
  
            MercadoPagoConfig.AccessToken =token;

            var Items = new List<PreferenceItemRequest>();
            foreach (ItemBuy it in item)
            {
                Items.Add(new PreferenceItemRequest
                {
                    Id = it.Id.ToString(),
                    Title = it.Title,
                    Quantity = it.Quantity,
                    CurrencyId = "ARS",
                    UnitPrice = (it.Price)
                }) ;
            }
            //PaymentCreateRequest
            var request = new PreferenceRequest
            {
                Items = Items,
                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = "http://localhost:4200/home",
                    Failure = "http://localhost:4200/home",
                    Pending = "http://localhost:4200/home"
                }

            };

            // Crea la preferencia usando el client insertar cambios
            var client = new PreferenceClient();
            Preference preference = await client.CreateAsync(request);
            return preference;



        }

        [Route("api/paymentsMethods/")]
        [HttpGet]
        public async Task<string> PostPaymentsMethodsAsync()
        {
            MercadoPagoConfig.AccessToken = token;



            var client = new PaymentMethodClient();
                ResourcesList<PaymentMethod> paymentMethods = await client.ListAsync();
            return null;


        }

        [Route("api/payment/{id}")]
        [HttpGet]
        public async Task<long> GetPayment(long id)
        {
            MercadoPagoConfig.AccessToken = token;
            //Payment payment = new PaymentClient().Capture(id);

            var client = new PaymentClient();
            Payment payment = await client.CaptureAsync(id);
            //var order = payment.AdditionalInfo.Items;
            //this.pay.Save(payment);
            return (long)payment.Id;

        }

    }
}
