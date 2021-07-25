using System;
using System.Collections.Generic;
using System.Text;

namespace RDLinker.Log.Logger
{
    public class LoggerFactory
    {
        public static ILogger GetDefaultLogger()
        {
            return LoggerManager.GetLogger();
        }
        public static ILogger GetDefaultLogger(string name)
        {
            return LoggerManager.GetLogger(name);
        }
    }
}
