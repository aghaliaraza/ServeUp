using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;

namespace ServeUpApiServer.Infrastructure.Middlewares
{
    public static class TrackingBuilderExtension
    {
        public static IAppBuilder UseTracking(this IAppBuilder builder, TrackingOptions options)
        {
            return builder.Use<TrackingMiddleware>(options);
        }
    }
}
