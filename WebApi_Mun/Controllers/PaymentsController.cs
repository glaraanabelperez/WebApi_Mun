using System.Web.Http;
using System.Web.Http.Cors;
using WebApi_Mun.Data;
using MercadoPago;
using MercadoPago.Config;
using MercadoPago.Client.Preference;
using MercadoPago.Resource.Preference;
using System.Collections.Generic;
using MercadoPago.Client.Payment;
using MercadoPago.Client.Common;
using System.Net;
using System.IO;
using System;
using MercadoPago.Client.PaymentMethod;
using MercadoPago.Resource.PaymentMethod;
using MercadoPago.Resource;
using System.Threading.Tasks;
using MercadoPago.Resource.Common;

namespace WebApi_Mun.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class PaymentsController : ApiController 
    {

        [Route("api/payments/post/")]
        [HttpPost]
        public async Task<Preference> PostAsync([FromBody] List<ItemBuy> item)
        {
            MercadoPagoConfig.AccessToken = "TEST-986919186102180-121614-6b51be8f45d87142d9fcb56564d0d925-1265763490";

            var Items = new List<PreferenceItemRequest>();
            foreach (ItemBuy it in item)
            {
                Items.Add(new PreferenceItemRequest
                {
                    Title = it.Title,
                    Quantity = it.Quantity,
                    CurrencyId = "ARS",
                    UnitPrice = (it.Price * it.Quantity)
                });
            }

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
                    Name="Testa",
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

            // Crea la preferencia usando el client
            var client = new PreferenceClient();
            Preference preference = await client.CreateAsync(request);
            return preference;



        }

        [Route("api/paymentsMethods/")]
        [HttpGet]
        public async Task<string> PostPaymentsMethodsAsync()
        {
            MercadoPagoConfig.AccessToken = "TEST-986919186102180-121614-6b51be8f45d87142d9fcb56564d0d925-1265763490";



                var client = new PaymentMethodClient();
                ResourcesList<PaymentMethod> paymentMethods = await client.ListAsync();
            return null;


        }

    }
}
