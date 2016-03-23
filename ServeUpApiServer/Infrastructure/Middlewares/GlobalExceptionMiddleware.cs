using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace ServeUpApiServer.Infrastructure.Middlewares
{
    public class GlobalExceptionMiddleware : OwinMiddleware
    {
        public GlobalExceptionMiddleware(OwinMiddleware next) : base(next)
        { }

        public override async Task Invoke(IOwinContext context)
        {
            try
            {
                await Next.Invoke(context);
            }
            catch (Exception ex)
            {
                // your handling logic
            }
        }
    }
}
