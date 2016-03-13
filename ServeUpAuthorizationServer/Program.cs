using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace ServeUpAuthorizationServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseUri = ConfigurationManager.AppSettings["ServerURL"];

            Console.WriteLine("Starting ServeUpAuthorizationServer...");
            using (WebApp.Start<Startup>(baseUri))
            {
                Console.WriteLine("ServeUpApiPlatform running at {0} - press Enter to quit. ", baseUri);

                //var fullUrl = baseUri.Replace("*", "localhost");
                //LaunchDocumentation(fullUrl);

                Console.ReadLine();
            }
        }
    }
}
