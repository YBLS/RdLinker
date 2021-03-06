using RDLinker.Logging.Provider;
using System;
using System.Collections.Generic;
using System.Text;

namespace RDLinker.Logging.Logger
{
    public class LoggerManager
    {
        public static ILogger GetLogger()
        {
            return new Log4NetLogger();
        }

        public static ILogger GetLogger(string name)
        {
            return new Log4NetLogger(name);
        }
    }
}
