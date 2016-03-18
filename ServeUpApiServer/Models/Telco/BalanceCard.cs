using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServeUpApiServer.Models.Telco
{
    public class BalanceCard
    {
        // CardType can be set as Prepaid/Postpaid/Wallet etc
        public string CardType { get; set; }

        //In case of Prepaid or Wallet it will be available balance, in case of postpaid it will be Payable Amount
        public string AvailableBalance { get; set; }
        public string LastActivityAmount { get; set; }
        
        //Viewing the Usage since Date
        public string UsageSince { get; set; }
        public string UsageSinceAmount { get; set; }
        public List<TopUpUsage> LastXTopUPs { get; set; }

        // in case of Postpaid cardType
        public string DueDate { get; set; }
    }
}
