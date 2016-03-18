using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServeUpApiServer.Models.Telco
{
    public class CustomerProfile
    {
        public string Operator { get; set; }

        //ProfileType can be set as Personal/Business etc.
        public string ProfileType { get; set; }
        public string MSISDN { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string ServeupName { get; set; }                
        public string Location { get; set;}
        public string Email { get; set;}
        public string BirthDay { get; set;}
        public string PersonalURL { get; set; }
        public string TagLine { get; set; }
        public string AboutMe { get; set; }
        //Rating can be GOLD/Platinum etc.
        public string Rating { get; set; }
        public byte[] Snap { get; set; }

        public List<string> Interests { get; set; }
        public List<SocialProfile> SocialProfiles;

        //public string FaceBook { get; set; }
        //public string Twitter { get; set; }
        //public string LinkedIn { get; set; }

        //business fields
        public string Company { get; set; }
        public string JobTitle { get; set; }
        public string ReportsTo { get; set; }
        public string Role { get; set; }
    }
}
