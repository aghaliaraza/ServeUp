using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Owin;
using Swashbuckle.Application;

namespace ServeUpApiServer.Infrastructure.Middlewares
{
    public class WebApiMiddlewareConfigurations
    {
        public static void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {

            var issuer = ConfigurationManager.AppSettings["AuthorizationServerURL"];
            string audienceId = ConfigurationManager.AppSettings["as:AudienceId"];
            byte[] audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["as:AudienceSecret"]);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audienceId },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, audienceSecret)
                    }
                });
        }
        public static void ConfigureWebApi(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
            config.MapHttpAttributeRoutes();
        }


        public static void ConfigureWebApiDocumentation(HttpConfiguration config)
        {
            config.EnableSwagger(c =>
            {
                c.IncludeXmlComments(GetXmlCommentsPath());
                c.SingleApiVersion("v1", "ServeUpApi");
            }).EnableSwaggerUi();


            //var defaultSettings = new JsonSerializerSettings
            //{
            //    Formatting = Formatting.Indented,
            //    ContractResolver = new CamelCasePropertyNamesContractResolver(),
            //    Converters = new List<JsonConverter> { new StringEnumConverter { CamelCaseText = true }, }
            //};
            //JsonConvert.DefaultSettings = () => { return defaultSettings; };
            //config.Formatters.Clear();
            //config.Formatters.Add(new JsonMediaTypeFormatter());
            //config.Formatters.JsonFormatter.SerializerSettings = defaultSettings;

            //config.MapHttpAttributeRoutes();
            //config.EnableSwagger(c =>
            //{
            //    c.IncludeXmlComments(GetXmlCommentsPath());
            //    c.SingleApiVersion("1.0", "Owin Swashbuckle Demo");
            //}).EnableSwaggerUi();
        }   

        protected static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}ServeUpApiServer.XML", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
