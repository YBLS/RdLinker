using RDLinker.Log.Core;
using RDLinker.Log.Logger;
using System;

namespace RDLinker.Log
{
    public class LogHelper
    {
        private static readonly ILogger _logger;

        static LogHelper()
        {
            _logger = LoggerFactory.GetDefaultLogger();
        }

        public static void Debug(object msg)
        {
            _logger.Log(ConvertLogginEvent(LogLevel.Debug, msg, null));
        }

        public static void Info(object msg)
        {
            _logger.Log(ConvertLogginEvent(LogLevel.Information, msg, null));
        }

        public static void Warn(object msg)
        {
            _logger.Log(ConvertLogginEvent(LogLevel.Warning, msg, null));
        }

        public static void Error(object msg)
        {
            _logger.Log(ConvertLogginEvent(LogLevel.Error, msg, null));
        }

        public static void Error(Exception ex)
        {
            _logger.Log(ConvertLogginEvent(LogLevel.Error, ex.Message, ex));
        }

        public static void Critical(object msg)
        {
            _logger.Log(ConvertLogginEvent(LogLevel.Critical, msg, null));
        }

        public static void Critical(Exception ex)
        {
            _logger.Log(ConvertLogginEvent(LogLevel.Critical, ex.Message, ex));
        }

        private static LoggingEvent ConvertLogginEvent(LogLevel logLevel, object message, Exception? ex)
        {
            return new LoggingEvent(_logger.Name, logLevel, message, ex);
        }
    }
}
