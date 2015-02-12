﻿
using System;
namespace IAS.Common.Logging
{
    /// <summary>
    /// A Trace Source base, log factory
    /// </summary>
    public class TraceSourceLogFactory
        : ILoggerFactory
    {
        /// <summary>
        /// Create the trace source log
        /// </summary>
        /// <returns>New ILog based on Trace Source infrastructure</returns>
        public ILogger Create(Type callingType, String methodName)
        {
            return new TraceSourceLog(callingType, methodName);  
        }
    }
}