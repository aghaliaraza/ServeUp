using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using ServeUpAuthorizationServer.OAuthServer;

namespace ServeUpAuthorizationServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuthentication(app);
            var webApiConfiguration = new HttpConfiguration();
            ConfigureWebApi(webApiConfiguration);
            app.UseWebApi(webApiConfiguration);
        }
        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
        }

        private void ConfigureAuthentication(IAppBuilder app)
        {
            var OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/OAuth2/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30), //TimeSpan.FromDays(14),
                Provider = new OAuthServerProvider(),
                AccessTokenFormat = new CustomJwtFormat(ConfigurationManager.AppSettings["AuthorizationServerURL"]),

                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true
            };

            app.UseOAuthAuthorizationServer(OAuthOptions);
            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
