using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Extensions.Compression.Core.Compressors;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.WebApi.Extensions.Compression.Server;
using Microsoft.AspNet.WebApi.Extensions.Compression.Server.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Owin;
using ServeUpApiServer.Infrastructure;
using ServeUpApiServer.Infrastructure.Middlewares;
using Swashbuckle.Application;

namespace ServeUpApiServer
{
    public class Startup
    {
        // This method is required by Katana:
        public void Configuration(IAppBuilder app)
        {
            app.UseTracking
                (
                    new TrackingOptions
                    {
                        TrackingStore = new TrackingStore(),
                        TrackingIdPropertyName = "x-tracking-id",
                        MaximumRecordedRequestLength = 64 * 1024,
                        MaximumRecordedResponseLength = 64 * 1024,
                    }
                );

            var webApiConfiguration = new HttpConfiguration();
            app.Use<GlobalExceptionMiddleware>();
            //app.Use<LoggingMiddleware>(app);

            WebApiMiddlewareConfigurations.ConfigureOAuthTokenConsumption(app);
            WebApiMiddlewareConfigurations.ConfigureWebApi(webApiConfiguration);
            WebApiMiddlewareConfigurations.ConfigureWebApiDocumentation(webApiConfiguration);            
            // Use the extension method provided by the WebApi.Owin library:                       
            app.UseWebApi(webApiConfiguration);

            //webApiConfiguration.MessageHandlers.Insert(0, new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));
            //webApiConfiguration.MessageHandlers.Insert(0, new OwinServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));
        }     
    }
}
