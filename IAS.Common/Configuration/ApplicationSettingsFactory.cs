using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Common.Configuration
{
    public class ApplicationSettingsFactory
    {
        #region Private 
        //private static T _webServiceSettings;
        private static IApplicationSettings _applicationSettings;
        #endregion
                                                         

        #region Web ApplicationSetting                                       
        //public static void InitializeApplicationSettingsFactory(
        //                              T webServiceSettings)   
        //{
        //    _webServiceSettings = webServiceSettings;
        //}
        ///// <summary>
        ///// เรียกใช้ ข้อมูล Application Setting WebConfig ของ IAS.DataServices
        ///// </summary>
        ///// <returns></returns>
        //public static T GetApplicationSettings()
        //{
        //    return _webServiceSettings;
        //}
        #endregion

        #region WebService ApplicationSetting

        public static void InitializeApplicationSettingsFactory(
                                        IApplicationSettings webApplicationSettings)
        {
            _applicationSettings = webApplicationSettings;
        }
        /// <summary>
        /// เรียกใช้ ข้อมูล Application Setting WebConfig ของ IAS
        /// </summary>
        /// <returns></returns>
        public static IApplicationSettings GetApplicationSettings()
        {
            return _applicationSettings;
        }
        #endregion

     
        
    }
}
