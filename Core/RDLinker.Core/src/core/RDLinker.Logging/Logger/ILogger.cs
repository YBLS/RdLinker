using RDLinker.Logging.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RDLinker.Logging.Logger
{
    public interface ILogger
    {
        string Name { get; set; }

        void Log(LoggingEvent loggingEvent);

        bool IsEnabledFor(LogLevel level);
    }
}
