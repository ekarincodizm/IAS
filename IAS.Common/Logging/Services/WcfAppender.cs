using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Appender;
using IAS.Common.Logging.Services;
using System.ServiceModel;
using log4net.Core;
using System.Web;

namespace IAS.Common.Logging
{
    public class WcfAppender : AppenderSkeleton
    {
        // instanse of IServiceLogger that will 
        // hold channel to wcf service
        static IServiceLogger Service;
        private LoggingEvent _loggingEvent;

        public WcfAppender()
        {
            // the logger initialize this class automatically
            // the first thing to do is to get channel to service
            // in order ot send messages to service.
            CreateChannelToWcfService();
        }

        private static void CreateChannelToWcfService()
        {
            // address for service
            EndpointAddress address = new EndpointAddress(new Uri(System.Configuration.ConfigurationManager.AppSettings["SERVICELOG_URL"]));
            // binding for service
            BasicHttpBinding binding = new BasicHttpBinding();

            // get channel to wcf service from channelFactory class
            Service = ChannelFactory<IServiceLogger>.CreateChannel(binding, address);
        }

        #region Properties

        public DateTime TransDate { get { return DateTime.Now; } }
        public String LogingName {
            get { return log4net.LogicalThreadContext.Properties["login_name"] == null ? "" : log4net.LogicalThreadContext.Properties["login_name"].ToString(); }
        }
        public String OICUserId
        {
            get { return log4net.LogicalThreadContext.Properties["oic_userid"] == null ? "" : log4net.LogicalThreadContext.Properties["oic_userid"].ToString(); }
        }
        public String DeptCode
        {
            get { return log4net.LogicalThreadContext.Properties["dept_code"] == null ? "" : log4net.LogicalThreadContext.Properties["dept_code"].ToString(); }
        }
        public String CompanyCode
        {
            get { return log4net.LogicalThreadContext.Properties["company_code"] == null ? "" : log4net.LogicalThreadContext.Properties["company_code"].ToString(); }
        }
        public String SystemCode
        {
            get { return log4net.LogicalThreadContext.Properties["system_code"] == null ? "" : log4net.LogicalThreadContext.Properties["system_code"].ToString(); }
        }
        public String SubSystemCode
        {
            get { return log4net.LogicalThreadContext.Properties["subsystem_code"] == null ? "" : log4net.LogicalThreadContext.Properties["subsystem_code"].ToString(); }
        }
        public String PrgId
        {
            get { return log4net.LogicalThreadContext.Properties["prg_id"] == null ? "" : log4net.LogicalThreadContext.Properties["prg_id"].ToString(); }
        }
        public String IPAddress
        {
            get { return log4net.LogicalThreadContext.Properties["ip_address"] == null ? "" : log4net.LogicalThreadContext.Properties["ip_address"].ToString(); }
        }
        public String LogHeader
        {
            get { return _loggingEvent.Level.DisplayName; }
        }
        public String Detail
        {
            get { return _loggingEvent.RenderedMessage; }
        }
        public DateTime CreatedDate
        {
            get { return DateTime.Now; }
        }
        public String CreatedBy
        {
            get { return log4net.LogicalThreadContext.Properties["created_by"] == null ? "" : log4net.LogicalThreadContext.Properties["created_by"].ToString(); }
        }
        public Int32 LogLevel
        {
            get { return _loggingEvent.Level.Value; }
        }                                                                
        #endregion       

        #region AppenderSkeleton Members

        protected override void Append(LoggingEvent loggingEvent)
        {
            _loggingEvent = loggingEvent;
            // append the level message and the content to one string
            //string message = string.Format("{0}, {1}",
            //loggingEvent.Level.DisplayName,
            //loggingEvent.RenderedMessage);
            LogServiceMessage message = new LogServiceMessage() { 
               TransDate = DateTime.Now,
               LogLevel =  LogLevel,
               LogingName = LogingName,
               OICUserId = OICUserId,
               DeptCode = DeptCode,
               CompanyCode = CompanyCode,
               SystemCode = SystemCode,
               SubSystemCode = SubSystemCode,
               PrgId = PrgId,
               IpAddress = IPAddress,
               LogHeader = LogHeader,
               Detail = Detail,
               CreateDate = CreatedDate,
               CreateBy = CreatedBy 
              
            };
            if (loggingEvent.ExceptionObject != null)
                message.LogException = loggingEvent.ExceptionObject.Message;

            // send this string message to wcf service
            Service.NewLog(message);
        }

        #endregion
    }
}
