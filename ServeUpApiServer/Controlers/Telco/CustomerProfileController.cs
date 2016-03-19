using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.WebApi.Extensions.Compression.Server.Attributes;
using ServeUpApiServer.Models.Telco;

namespace ServeUpApiServer.Controlers.Telco
{
    public class CustomerProfileController : ApiController
    {
        private static List<CustomerProfile> _DbProfile = new List<CustomerProfile>
            {
                new CustomerProfile { ProfileType="Business", MSISDN="923008423238", Name = "Imran Shabbir", Title="Mr.",ServeupName = "Imran",  Location= "Lahore, Pakistan",
                                        Email="imran.shabbbir@gmail.com", BirthDay ="09/11/1977", TagLine ="The road will never be the same",PersonalURL="http://myurl.com",
                                        AboutMe = "Learning new technologies and applying them to solve real world problems is my passion.",
                                        Rating="GOLD", Company="Mobilink", JobTitle="Manager", ReportsTo = "Nabeel Zubair", Role="Software Development",
                                        SocialProfiles = new List<SocialProfile>()
                                        {
                                            new SocialProfile { SocialChannelName="FaceBook", SocialChannelURL = "https://www.facebook.com/ImranShabbbir" },
                                            new SocialProfile { SocialChannelName="Twitter", SocialChannelURL= "https://twitter.com/imranshabbbir" },
                                            new SocialProfile { SocialChannelName="LinkedIn", SocialChannelURL= "https://www.linkedin.com/in/imran-shabbir-9945423" }
                                        }, Interests = new List<string>() { "Cloths", "Movies", "Gadgets", "Technology", "IoT" }
                                    }
            };


        [Route("ServeUpApi/v1/Telco/CustomerProfile/GetByMSISDN")]
        [ResponseType(typeof(CustomerProfile))]
        [HttpGet]
        public async Task<IHttpActionResult> Get(string MSISDN)
        {
            var profile = _DbProfile.FirstOrDefault(c => c.MSISDN == MSISDN);
            if (profile == null)
            {
                throw new HttpResponseException(
                    System.Net.HttpStatusCode.NotFound);
            }
            return Ok(profile);
        }

        [Route("ServeUpApi/v1/Telco/CustomerProfile/Post")]
        [HttpPost]
        public async Task<IHttpActionResult> Post(CustomerProfile profile)
        {
            _DbProfile.Add(profile);
            return Ok();
        }


        private static List<Address> _DbAddress = new List<Address>
            {
                new Address { AddressType="Account", Name="Imran Shabbir", Address1="Allama Iqbal Town", Address2="Ravi Block",ZipCode="54000",City="Lahore", State="Punjab", Country="Pakistan" },
                new Address { AddressType="Billing", Name="Imran Shabbir", Address1="Town Ship", Address2="2nd Floor FRF Building",ZipCode="54000",City="Lahore", State="Punjab", Country="Pakistan" }
            };


        [Route("ServeUpApi/v1/Telco/CustomerProfile/Address/GetByMSISDN")]
        [ResponseType(typeof(List<Address>))]
        [HttpGet]
        public async Task<IHttpActionResult> GetAddress(string MSISDN)
        {
            var addresses = _DbAddress;
            if (addresses == null)
            {
                throw new HttpResponseException(
                    System.Net.HttpStatusCode.NotFound);
            }
            return Ok(addresses);
        }

        [Route("ServeUpApi/v1/Telco/CustomerProfile/Address/Post")]
        [HttpPost]
        public async Task<IHttpActionResult> Post(Address address)
        {
            _DbAddress.Add(address);
            return Ok();
        }



    }
}
