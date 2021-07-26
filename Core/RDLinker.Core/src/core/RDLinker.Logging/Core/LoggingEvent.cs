using System;
using System.Collections.Generic;
using System.Text;

namespace RDLinker.Logging.Core
{
    public class LoggingEvent
    {
        public string _loggerName { get; set; }
        public object _message { get; set; }
        public LogLevel _logLevel { get; set; }
        public Exception _exception { get; set; }

        public LoggingEvent(string loggerName, LogLevel logLevel, object message, Exception exception)
        {
            _loggerName = loggerName;
            _logLevel = logLevel;
            _message = message;
            _exception = exception;
        }
    }
}
