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
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class PaymentsController : ApiController 
    {
        string token= "APP_USR-986919186102180-121614-36e62d04f5685e93d6f84d98dab69587-1265763490";

        [Route("api/payments/post/")]
        [HttpPost]
        public async Task<Preference> PostAsync([FromBody] List<ItemBuy> item)
        {
            //APP_USR-986919186102180-121614-36e62d04f5685e93d6f84d98dab69587-1265763490
            //TEST - 986919186102180 - 121614 - 6b51be8f45d87142d9fcb56564d0d925 - 1265763490
            MercadoPagoConfig.AccessToken = "TEST-986919186102180-121614-6b51be8f45d87142d9fcb56564d0d925-1265763490";

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
                },
                Payer= new PreferencePayerRequest
                {
                    Name="Testa222",
                    Surname="Teste Tes",
                    Email="gnanajsu@hdhd.com",
                    Identification = new IdentificationRequest
                    {
                        Type = "dni",
                        Number = "345676546"
                    },
                    Phone = new PhoneRequest
                    {
                        AreaCode = "011",
                        Number = "546474645"
                    },
                    Address=new AddressRequest
                    {
                        StreetName="Pointsenot",
                        StreetNumber="41526",
                        ZipCode="14521"
                    }

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
        public Task<string> GetPayment(long id)
        {
            MercadoPagoConfig.AccessToken = "TEST-986919186102180-121614-6b51be8f45d87142d9fcb56564d0d925-1265763490";

            Payment payment = new PaymentClient().Capture(id);
            var order=payment.AdditionalInfo.Items;
            return null;


        }

    }
}
