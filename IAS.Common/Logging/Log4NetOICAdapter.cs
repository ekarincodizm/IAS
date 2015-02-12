using System;
using System.Collections.Generic;
using System.Linq;

using log4net;
using log4net.Config;
using System.Globalization;
using IAS.Common.Logging.Services;
using System.Configuration;
using System.Net;

namespace IAS.Common.Logging
{
    public class Log4NetOICAdapter : ILogger  
    {
        private readonly log4net.ILog _log;
        private Type _callingType;
        private String _methodName;
        private String _ipAddress = "";

        public String MethodName { get { return _methodName; } }
        public String SystemCode
        {
            get { return ConfigurationManager.AppSettings["SystemCode"] == null ? "" : ConfigurationManager.AppSettings["SystemCode"].ToString(); }
        }
        public String SubSystemCode
        {
            get { return ConfigurationManager.AppSettings["SubSystemCode"] == null ? "" : ConfigurationManager.AppSettings["SubSystemCode"].ToString(); }
        }
        public String PrgId
        {
            get { return String.Format("[{0}.{1}]", (_callingType != null) ? _callingType.Name : "", _methodName); }
        }
        public String IPAddress
        {
            get
            {
                if (String.IsNullOrEmpty(_ipAddress))
                {
                    string strHostName = System.Net.Dns.GetHostName();
                    IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                    IPAddress ipAddress = ipHostInfo.AddressList[0];
                    _ipAddress = ipAddress.ToString();
                }


                return _ipAddress;
            }
        }
        private String TraceMessage(String message) {
            return  message;
        }

        public Log4NetOICAdapter(Type callingType, String methodName)
        {

            XmlConfigurator.Configure();

            _log = LogManager.GetLogger("WcfLogger");
            //log4net.LogicalThreadContext.Properties["functionName"] = _methodName; 
            //_log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        private void SettingProperties(object[] args, String header, String exception = "")
        {
            if ((args != null && args.Length > 0) && (args[0]).GetType() == typeof(LogServiceMessage))
            {
                LogServiceMessage logMessage = (LogServiceMessage)args[0];
                log4net.LogicalThreadContext.Properties["login_name"] = logMessage.LogingName;
                log4net.LogicalThreadContext.Properties["oic_userid"] = logMessage.OICUserId;
                log4net.LogicalThreadContext.Properties["dept_code"] = logMessage.DeptCode;
                log4net.LogicalThreadContext.Properties["company_code"] = logMessage.CompanyCode;
                log4net.LogicalThreadContext.Properties["system_code"] = logMessage.SystemCode;
                log4net.LogicalThreadContext.Properties["subsystem_code"] = logMessage.SubSystemCode;
                log4net.LogicalThreadContext.Properties["prg_id"] = logMessage.PrgId;
                log4net.LogicalThreadContext.Properties["ip_address"] = logMessage.IpAddress;
                log4net.LogicalThreadContext.Properties["log_header"] = logMessage.LogHeader;
                log4net.LogicalThreadContext.Properties["log_exception"] = logMessage.LogException;
                log4net.LogicalThreadContext.Properties["created_by"] = logMessage.CreateBy;
            }
            else {
                log4net.LogicalThreadContext.Properties["login_name"] = "";
                log4net.LogicalThreadContext.Properties["oic_userid"] = "";
                log4net.LogicalThreadContext.Properties["dept_code"] = "";
                log4net.LogicalThreadContext.Properties["company_code"] = "";
                log4net.LogicalThreadContext.Properties["system_code"] = SystemCode;
                log4net.LogicalThreadContext.Properties["subsystem_code"] = SubSystemCode;
                log4net.LogicalThreadContext.Properties["prg_id"] = PrgId;
                log4net.LogicalThreadContext.Properties["ip_address"] = IPAddress;
                log4net.LogicalThreadContext.Properties["log_header"] = header;
                log4net.LogicalThreadContext.Properties["log_exception"] = exception;
                log4net.LogicalThreadContext.Properties["created_by"] = "";
            }

            
        }

        public void Debug(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);
                SettingProperties(args, log4net.Core.Level.Debug.DisplayName);
                _log.Debug(TraceMessage(messageToTrace));
            }
        }

        public void Debug(string message, Exception exception, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);
                SettingProperties(args, log4net.Core.Level.Debug.DisplayName, exception.Message);
                _log.Debug(TraceMessage(messageToTrace), exception);

            }
        }

        public void Debug(object item)
        {
            if (item != null)
            {
                SettingProperties(null, log4net.Core.Level.Info.DisplayName);
                _log.Info(item.ToString());

            }
        }

        public void Fatal(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);
                SettingProperties(args, log4net.Core.Level.Fatal.DisplayName);
                _log.Fatal(TraceMessage(messageToTrace));

            }
        }

        public void Fatal(string message, Exception exception, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);
                SettingProperties(args, log4net.Core.Level.Fatal.DisplayName, exception.Message);
                _log.Fatal(TraceMessage(messageToTrace), exception);
            }
        }

        public void LogInfo(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, new { module = "Test" });
      
                   SettingProperties(args, log4net.Core.Level.Info.DisplayName);
                _log.Info(TraceMessage(messageToTrace));
             
            }
        }

        public void LogWarning(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);
                SettingProperties(args, log4net.Core.Level.Warn.DisplayName);
                _log.Warn(TraceMessage(messageToTrace));

            }
        }

        public void LogError(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);
                SettingProperties(args, log4net.Core.Level.Error.DisplayName);
                _log.Error(TraceMessage(messageToTrace));

            }
        }

        public void LogError(string message, Exception exception, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);
                SettingProperties(args, log4net.Core.Level.Error.DisplayName, exception.Message);
                _log.Error(TraceMessage(messageToTrace), exception);

            }
        }
    }

}
