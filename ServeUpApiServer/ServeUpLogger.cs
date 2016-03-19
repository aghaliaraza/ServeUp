//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Owin.Logging;

//namespace ServeUpApiServer
//{
//    public class ServeUpLogger : ILogger
//    {
//        public bool WriteCore(TraceEventType eventType, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
//        {
//            ILog logger = LogManager.GetLogger("myowinlogger");
//            switch (eventType)
//            {
//                case (TraceEventType.Critical):
//                    {
//                        logger.Fatal(...);
//                    }     
//            }
//        }
//    }
//}
