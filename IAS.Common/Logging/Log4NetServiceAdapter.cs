using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using IAS.Common.Configuration;
using log4net;
using log4net.Config;
using System.Globalization;
using StructureMap;
using log4net.Repository;
using System.Diagnostics;
using IAS.Common.Logging.Services;
using System.Web;
using System.Net;
namespace IAS.Common.Logging
{
    public class Log4NetServiceAdapter : ILogger  
    {
        private readonly log4net.ILog _log;
        private Type _callingType;
        private String _methodName;
        private String _ipAddress = "";   

        public String MethodName { get { return _methodName; } }
        #region Properties

        public DateTime TransDate { get { return DateTime.Now; } }
        public String LogingName
        {
            get { return (HttpContext.Current.Session == null)? "" : ( HttpContext.Current.Session["LogingName"] == null) ? "" : HttpContext.Current.Session["LogingName"].ToString(); }
        }
        public String OICUserId
        {
            get { return (HttpContext.Current.Session == null) ? "" : (HttpContext.Current.Session["OICUserId"] == null) ? "" : HttpContext.Current.Session["OICUserId"].ToString(); }
        }
        public String DeptCode
        {
            get { return (HttpContext.Current.Session == null) ? "" : (HttpContext.Current.Session["DeptCode"] == null) ? "" : HttpContext.Current.Session["DeptCode"].ToString(); }
        }
        public String CompanyCode
        {
            get { return (HttpContext.Current.Session == null) ? "" : (HttpContext.Current.Session["CompanyCode"] == null) ? "" : HttpContext.Current.Session["CompanyCode"].ToString(); }
        }
        public DateTime CreatedDate
        {
            get { return DateTime.Now; }
        }
        public String CreatedBy
        {
            get { return (HttpContext.Current.Session == null) ? "" : (HttpContext.Current.Session["CreatedBy"] == null) ? "" : HttpContext.Current.Session["CreatedBy"].ToString(); }
        }

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
                if (String.IsNullOrEmpty(_ipAddress)) {
                    string strHostName = System.Net.Dns.GetHostName();
                    IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                    IPAddress ipAddress = ipHostInfo.AddressList[0];
                    _ipAddress = ipAddress.ToString();
                }


                return _ipAddress;
            }
        }

        private void SettingProperties()
        {
                log4net.LogicalThreadContext.Properties["login_name"] = LogingName;
                log4net.LogicalThreadContext.Properties["oic_userid"] = OICUserId;
                log4net.LogicalThreadContext.Properties["dept_code"] = DeptCode;
                log4net.LogicalThreadContext.Properties["company_code"] = CompanyCode;
                log4net.LogicalThreadContext.Properties["system_code"] = SystemCode;
                log4net.LogicalThreadContext.Properties["subsystem_code"] = SubSystemCode;
                log4net.LogicalThreadContext.Properties["prg_id"] = PrgId;
                log4net.LogicalThreadContext.Properties["ip_address"] = IPAddress;
            

                log4net.LogicalThreadContext.Properties["created_by"] = CreatedBy;

        }

        #endregion
        private String TraceMessage(String message) {
            return  message;
        }

        public Log4NetServiceAdapter(Type callingType, String methodName)
        {
            _callingType = callingType;
            _methodName = methodName;
            XmlConfigurator.Configure();

            _log = LogManager.GetLogger("WcfLogger");
            //_log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


            //log4net.LogicalThreadContext.Properties["LogingName"] = _methodName; 
            
        }



        public void Debug(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);
                SettingProperties();
                _log.Debug(TraceMessage(messageToTrace));
            }
        }

        public void Debug(string message, Exception exception, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);
                SettingProperties();
                _log.Debug(TraceMessage(messageToTrace), exception);

            }
        }

        public void Debug(object item)
        {
            if (item != null)
            {
                SettingProperties();
                _log.Info(item.ToString());

            }
        }

        public void Fatal(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);
                SettingProperties();
                _log.Fatal(TraceMessage(messageToTrace));

            }
        }

        public void Fatal(string message, Exception exception, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);
                SettingProperties();
                _log.Fatal(TraceMessage(messageToTrace), exception);
            }
        }

        public void LogInfo(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, new { module = "Test" });
                SettingProperties();
                _log.Info(TraceMessage(messageToTrace));
             
            }
        }

        public void LogWarning(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);
                SettingProperties();
                _log.Warn(TraceMessage(messageToTrace));

            }
        }

        public void LogError(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);
                SettingProperties();
                _log.Error(TraceMessage(messageToTrace));

            }
        }

        public void LogError(string message, Exception exception, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);
                SettingProperties();
                _log.Error(TraceMessage(messageToTrace), exception);

            }
        }
    }

}
