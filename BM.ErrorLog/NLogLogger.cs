using NLog;
using System;
using nlog = NLog;

namespace BM.ErrorLog
{
    public class NLogLogger
    {
        private Logger _logger;

        public NLogLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception x)
        {
            Error(LogUtility.BuildExceptionMessage(x));
        }

        public void Error(string message, Exception x)
        {
            _logger.ErrorException(message, x);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(Exception x)
        {
            Fatal(LogUtility.BuildExceptionMessage(x));
        }

        //public static nlog.Logger Instance { get; private set; }

        //static NLogLogger()
        //{
        //    nlog.LogManager.ReconfigExistingLoggers();

        //    Instance = nlog.LogManager.GetCurrentClassLogger();
        //}

        //public static void LogError(Exception exception, string message = "")
        //{
        //    Instance.ErrorException(message, exception);
        //}

        //public enum LogType
        //{
        //    Error = "ERROR",
        //    Information = "INFORMATION",
        //    Warning = "WARNING"
        //}

        //public enum LogMessaage
        //{
        //    ExceptionOccured="An error occured.\n",
        //    ExceptionOccuredLogDetail = "An error occured. See event log for detailed information or Contact your system administrator."

        //}

    }
}

