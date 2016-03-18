using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServeUpApiServer.Models.Telco
{
    public class ServiceCard
    {
        string Service { get; set; }
        string Price { get; set; }
        //ChargingPeriod can be set Daily/Weekly/Monthly
        string ChargingPeriod { get; set; }
    }
}
