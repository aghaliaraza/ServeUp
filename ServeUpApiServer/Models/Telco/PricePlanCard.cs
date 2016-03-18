using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServeUpApiServer.Models.Telco
{
    public class PricePlanCard
    {
        public string PricePlan { get; set; }
        //Plan type can be Postpaid/Prepaid/PayAsYouGo etc
        public string PlanType { get; set; }
        public string Voice { get; set; }
        public string Data { get; set; }
        public string SMS { get; set; }
        public string Price { get; set; }
    }
}
