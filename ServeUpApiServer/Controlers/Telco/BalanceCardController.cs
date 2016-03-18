using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ServeUpApiServer.Models.Telco;

namespace ServeUpApiServer.Controlers.Telco
{
    public class BalanceCardController : ApiController
    {
        private static List<BalanceCard> _DbBalanceCards = new List<BalanceCard>
            {
                new BalanceCard
                {
                    CardType ="Prepaid", AvailableBalance="73", LastActivityAmount="5", UsageSince="1/03/2016",UsageSinceAmount="13",
                    LastXTopUPs = new List<TopUpUsage>() { new TopUpUsage { UsageDate="1/03/2016",UsageAmount="2" }, new TopUpUsage { UsageDate = "2/03/2016", UsageAmount = "1" }, new TopUpUsage { UsageDate = "5/03/2016", UsageAmount = "5" }, new TopUpUsage { UsageDate = "12/03/2016", UsageAmount = "2" }, new TopUpUsage { UsageDate = "15/03/2016", UsageAmount = "3" } }
                },
                new BalanceCard
                {
                    CardType ="Postpaid", AvailableBalance="73", LastActivityAmount="5", UsageSince="1/03/2016",UsageSinceAmount="13",
                    LastXTopUPs = new List<TopUpUsage>() { new TopUpUsage { UsageDate="1/03/2016",UsageAmount="2" }, new TopUpUsage { UsageDate = "2/03/2016", UsageAmount = "1" }, new TopUpUsage { UsageDate = "5/03/2016", UsageAmount = "5" }, new TopUpUsage { UsageDate = "12/03/2016", UsageAmount = "2" }, new TopUpUsage { UsageDate = "15/03/2016", UsageAmount = "3" } },
                    DueDate="30/3/2016"
                },
                new BalanceCard
                {
                    CardType ="Wallet", AvailableBalance="73", LastActivityAmount="5", UsageSince="1/03/2016",UsageSinceAmount="13",
                    LastXTopUPs = new List<TopUpUsage>() { new TopUpUsage { UsageDate="1/03/2016",UsageAmount="2" }, new TopUpUsage { UsageDate = "2/03/2016", UsageAmount = "1" }, new TopUpUsage { UsageDate = "5/03/2016", UsageAmount = "5" }, new TopUpUsage { UsageDate = "12/03/2016", UsageAmount = "2" }, new TopUpUsage { UsageDate = "15/03/2016", UsageAmount = "3" } }
                }
            };



        [Route("ServeUpApi/v1/Telco/Cards/Balance/GetByMSISDN")]
        [ResponseType(typeof(List<BalanceCard>))]
        [HttpGet]
        public async Task<IHttpActionResult> Get(string MSISDN)
        {
            var balances = _DbBalanceCards;
            if (balances == null)
            {
                throw new HttpResponseException(
                    System.Net.HttpStatusCode.NotFound);
            }
            return Ok(balances);
        }

        //[Route("ServeUpApi/v1/Telco/Cards/Balance/Post")]
        //[HttpPost]
        //public async Task<IHttpActionResult> Post(BalanceCard balance)
        //{
        //    _DbBalanceCards.Add(balance);
        //    return Ok();
        //}

    }
}
