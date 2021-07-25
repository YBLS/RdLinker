using log4net;
using RDLinker.Log.Core;
using RDLinker.Log.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace RDLinker.Log.Provider
{
    public class Log4NetLogger : ILogger
    {
        public string? Name { get; set; }
        public string? RepositoryName { get; set; }
        public Assembly? RepositoryAssembly { get; set; }

        private log4net.Core.ILogger _log4NetLogger;

        private const string _defaultLoggerName = "rdlinker-log-default";

        private const string _defaultRepositoryName = "rdlinker-log-defaultRepository";

        log4net.Repository.ILoggerRepository repository;


        public Log4NetLogger()
        {
            Name = _defaultLoggerName;
            RepositoryName = _defaultRepositoryName;
            //实例化log4net
            initializeLog4Net();
        }

        public Log4NetLogger(string name)
        {
            Name = name;
            RepositoryName = _defaultRepositoryName;
            //实例化log4net
            initializeLog4Net();
        }

        public Log4NetLogger(Assembly repositoryAssembly, string name)
        {
            Name = name;
            RepositoryAssembly = repositoryAssembly;
            //实例化log4net
            initializeLog4Net();
        }

        public Log4NetLogger(string repository, string name)
        {
            Name = name;
            RepositoryName = repository;
            //实例化log4net
            initializeLog4Net();
        }

        private void initializeLog4Net()
        {
            if (RepositoryAssembly != null)
            {
                repository = log4net.Core.LoggerManager.CreateRepository(RepositoryAssembly, GetType());
            }
            else
            {
                repository = log4net.Core.LoggerManager.CreateRepository(RepositoryName);
            }
            configLog4NetLogger(repository);
            _log4NetLogger = repository.GetLogger(Name);
        }

        private void configLog4NetLogger(log4net.Repository.ILoggerRepository repository)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4netconfig.xml");
            //初始化配置文件
            log4net.Config.XmlConfigurator.Configure(repository, new System.IO.FileInfo(path));
        }

        public bool IsEnabledFor(LogLevel level)
        {
            throw new NotImplementedException();
        }

        public void Log(LoggingEvent loggingEvent)
        {
            _log4NetLogger.Log(new log4net.Core.LoggingEvent(GetType(), repository, Name, ConvertLevel(loggingEvent._logLevel), loggingEvent._message, loggingEvent._exception));
        }

        private static log4net.Core.Level ConvertLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Trace:
                    return log4net.Core.Level.Trace;
                case LogLevel.Debug:
                    return log4net.Core.Level.Debug;
                case LogLevel.Information:
                    return log4net.Core.Level.Info;
                case LogLevel.Warning:
                    return log4net.Core.Level.Warn;
                case LogLevel.Error:
                    return log4net.Core.Level.Error;
                case LogLevel.Critical:
                    return log4net.Core.Level.Critical;
                case LogLevel.None:
                    return log4net.Core.Level.Off;
                default:
                    return log4net.Core.Level.Info;
            }
        }
    }
}
