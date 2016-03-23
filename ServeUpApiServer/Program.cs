using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace ServeUpApiServer
{
    class Program
    {
        static void Main(string[] args)
        {

            //ServeUp.Data.Class1 c = new ServeUp.Data.Class1();
            ServeUp.Data.Class2 c = new ServeUp.Data.Class2();


            string baseUri = ConfigurationManager.AppSettings["ApiServerURL"];

            Console.WriteLine("Starting ServeUpApiPlatform...");
            using (WebApp.Start<Startup>(baseUri))
            {
                Console.WriteLine("ServeUpApiPlatform running at {0} - press Enter to quit. ", baseUri);

                var fullUrl = baseUri.Replace("*", "localhost");
                LaunchDocumentation(fullUrl);

                Console.ReadLine();
            }
        }

        static void LaunchDocumentation(string url)
        {
            Process.Start("chrome.exe", string.Format("--incognito {0}", url + "/swagger/ui/index"));
        }
    }
}
