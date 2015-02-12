
using System;
using System.Diagnostics;
namespace IAS.Common.Logging
{
    /// <summary>
    /// Log Factory
    /// </summary>
    public static class LoggerFactory
    {
        #region Members

        static ILoggerFactory _currentLogFactory = null;
        static Type callingType;
        #endregion

        #region Public Methods

        /// <summary>
        /// Set the  log factory to use
        /// </summary>
        /// <param name="logFactory">Log factory to use</param>
        public static void SetCurrent(ILoggerFactory logFactory)
        {
            _currentLogFactory = logFactory;
        }

        /// <summary>
        /// Createt a new Log
        /// </summary>
        /// <returns>Created ILog</returns>
        public static ILogger CreateLog()
        {
            var st = new StackTrace();
            // this is what you are asking for
            callingType = st.GetFrame(1).GetMethod().DeclaringType;

            return (_currentLogFactory != null) ? _currentLogFactory.Create(callingType, st.GetFrame(1).GetMethod().Name) : null;
        }

        #endregion
    }
}
