using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Extensions.Compression.Core.Interfaces;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using log4net;
using log4net.Repository.Hierarchy;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Owin;

namespace ServeUpApiServer.Infrastructure.Middlewares
{
    public class LoggingMiddleware : OwinMiddleware
    {
        private static ILog log = LogManager.GetLogger("WebApi");

        public LoggingMiddleware(OwinMiddleware next, IAppBuilder app)
            : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            var sw = new Stopwatch();
            sw.Start();

            string Body = new StreamReader(context.Request.Body).ReadToEndAsync().Result;
            var Headers = JsonConvert.SerializeObject(context.Request.Headers);
            string IPTo = context.Request.LocalIpAddress;
            string IpFrom = context.Request.RemoteIpAddress;
            string Method = context.Request.Method;
            string Service = "Api";
            string Uri = context.Request.Uri.ToString();
            //string UserName = context.Request.User.Identity.Name;

            //log.Logger

            context.Request.Body.Position = 0;

            //keeping the pointer of response body
            var stream = context.Response.Body;
            var buffer = new MemoryStream();

            //providing the buffer to response body for loading response into the buffer
            context.Response.Body = buffer;

            await Next.Invoke(context);

           
            if (Headers.Contains("accept-enconding"))
            {

            }

              buffer.Seek(0, SeekOrigin.Begin);
              //GZipStream gs = new GZipStream(buffer, CompressionMode.Decompress);
            //DeflateStream gs = new DeflateStream(buffer, CompressionMode.Decompress);
            var sr = new StreamReader(buffer).ReadToEndAsync().Result;
            //string responseBody = string.Empty;
            //responseBody = sr.ReadToEnd();

            //var sr  = new StreamReader(gs);


            ////var sr = new StreamReader(buffer).ReadToEndAsync().Result;

            //string responseBody = string.Empty;

            ////if (!sr.EndOfStream)
            //try
            //{
            //    responseBody = sr.ReadToEnd();
            //}
            //catch (Exception ex)
            //{

            //}


            buffer.Seek(0, SeekOrigin.Begin);
            await buffer.CopyToAsync(stream);




            

            
            //var json = JObject.Parse(responseBody);

            var RespHeaders = JsonConvert.SerializeObject(context.Response.Headers);            
            var ResultCode = context.Response.StatusCode;
            sw.Stop();
            var ProcessingTime = sw.Elapsed;


            //db.log_Response.Add(logResponse);

            //await db.SaveChangesAsync();
        }

        //private static Encoding GetEncoding(string contentType)
        //{
        //    var charset = "utf-8";
        //    var regex = new Regex(@";\s*charset=(?<charset>[^\s;]+)");
        //    var match = regex.Match(contentType);
        //    if (match.Success)
        //        charset = match.Groups["charset"].Value;

        //    try
        //    {
        //        return Encoding.GetEncoding(charset);
        //    }
        //    catch (ArgumentException e)
        //    {
        //        return Encoding.UTF8;
        //    }
        //}

        //static byte[] GetBytes(string str)
        //{
        //    byte[] bytes = new byte[str.Length * sizeof(char)];
        //    System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
        //    return bytes;
        //}


        //private static async Task<HttpContent> DecompressContent(HttpContent compressedContent, ICompressor compressor)
        //{
        //    using (compressedContent)
        //    {
        //        MemoryStream decompressed = new MemoryStream();
        //        await compressor.Decompress(await compressedContent.ReadAsStreamAsync(), decompressed);
        //        var newContent = new StreamContent(decompressed);
        //        copy content type so we know how to load correct formatter
        //        newContent.Headers.ContentType = compressedContent.Headers.ContentType;

        //        return newContent;
        //    }
        //}

        //private string ToString(byte[] bytes)
        //{
        //    string response = string.Empty;

        //    foreach (byte b in bytes)
        //        response += (Char)b;

        //    return response;
        //}


        //static byte[] Decompress(byte[] gzip)
        //{
        //    using (GZipStream stream = new GZipStream(new MemoryStream(gzip),
        //                          CompressionMode.Decompress))
        //    {
        //        int size = gzip.Count();
        //        byte[] buffer = new byte[size];
        //        using (MemoryStream memory = new MemoryStream())
        //        {
        //            int count = 0;
        //            do
        //            {
        //                try
        //                {
        //                    count = stream.Read(buffer, 0, size);
        //                }
        //                catch (Exception ex)
        //                {
        //                    string exa = ex.Message;
        //                }

        //                if (count > 0)
        //                {
        //                    memory.Write(buffer, 0, count);
        //                }
        //            }
        //            while (count > 0);
        //            return memory.ToArray();
        //        }
        //    }
        //}

    }
}
