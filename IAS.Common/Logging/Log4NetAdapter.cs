using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.Common.Configuration;
using log4net;
using log4net.Config;
using System.Globalization;
using StructureMap;
using log4net.Repository;
using System.Diagnostics;
namespace IAS.Common.Logging
{
    public class Log4NetAdapter : ILogger
    {
        private readonly log4net.ILog _log;
        private Type _callingType;
        private String _methodName;

        public String MethodName { get { return _methodName; } }

        private String TraceMessage(String message) {
            return String.Format("[{0}.{1}] : {2}", (_callingType!=null)?_callingType.Name:"", _methodName, message);
        }

        public Log4NetAdapter(Type callingType, String methodName)
        {
            _callingType = callingType;
            _methodName = methodName;
            XmlConfigurator.Configure();
            
            _log = LogManager.GetLogger("IASLogger");
            //log4net.LogicalThreadContext.Properties["functionName"] = _methodName; 
            //_log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }


        public void Debug(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                _log.Debug(TraceMessage(messageToTrace));
            }
        }

        public void Debug(string message, Exception exception, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                _log.Debug(TraceMessage(messageToTrace), exception);

            }
        }

        public void Debug(object item)
        {
            if (item != null)
            {
                _log.Info(item.ToString());

            }
        }

        public void Fatal(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                _log.Fatal(TraceMessage(messageToTrace));
            }
        }

        public void Fatal(string message, Exception exception, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                _log.Fatal(TraceMessage(messageToTrace), exception);
            }
        }

        public void LogInfo(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, new { module = "Test" });   

                _log.Info(TraceMessage(messageToTrace));
             
            }
        }

        public void LogWarning(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                _log.Warn(TraceMessage(messageToTrace));

            }
        }

        public void LogError(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                _log.Error(TraceMessage(messageToTrace));

            }
        }

        public void LogError(string message, Exception exception, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                _log.Error(TraceMessage(messageToTrace), exception);

            }
        }
    }

}
