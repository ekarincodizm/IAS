using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using IAS.Common.Logging;
using System.ServiceModel.Activation;


namespace IAS.DataServices.Logging
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceLogger" in code, svc and config file together.
     [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServiceLogger : IAS.Common.Logging.Services.IServiceLogger
    {

        public void NewLog(Common.Logging.Services.LogServiceMessage message)
        {
            //LoggerFactory.CreateLog().LogInfo(message.Detail);
            //System.IO.File.AppendAllText(@"C:\logs\WriteText.txt", message.Detail + Environment.NewLine);

            if (message.LogLevel == log4net.Core.Level.Info.Value) 
            {
                LoggerFactory.CreateLog().LogInfo(message.Detail, message);
            }
            else if (message.LogLevel == log4net.Core.Level.Error.Value) {
                LoggerFactory.CreateLog().LogError(message.Detail, message);
            }
            else if (message.LogLevel == log4net.Core.Level.Fatal.Value)
            {
                LoggerFactory.CreateLog().Fatal(message.Detail, message);
            }
            else if (message.LogLevel == log4net.Core.Level.Warn.Value)
            {
                LoggerFactory.CreateLog().LogWarning(message.Detail, message);
            }
            else if (message.LogLevel == log4net.Core.Level.Debug.Value)
            {
                LoggerFactory.CreateLog().Debug(message.Detail, message);
            }
            else 
            {
                LoggerFactory.CreateLog().LogError("[Missing LogLevel]" + message.Detail, message);
            }
        }
    }
}
