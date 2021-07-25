using RDLinker.Log.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RDLinker.Log.Logger
{
    public interface ILogger
    {
        string Name { get; set; }

        void Log(LoggingEvent loggingEvent);

        bool IsEnabledFor(LogLevel level);
    }
}
