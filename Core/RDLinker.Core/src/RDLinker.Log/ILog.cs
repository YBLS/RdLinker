using System;
using System.Collections.Generic;
using System.Text;

namespace RDLinker.Log
{
    public interface ILog
    {
        string Name { get; set; }

        void Debug(string message);

        void Info(string message);

        void Warn(string message);

        void Error(string message);

        void Error(string message, Exception exception);

        void Fatal(string message);

        void Fatal(string message, Exception exception);
    }
}
