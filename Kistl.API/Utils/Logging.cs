using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using log4net;
using System.Reflection;

namespace Kistl.API.Utils
{
    public static class Logging
    {
        private static ILog _logger = null;

        public static ILog Log
        {
            get
            {
                if (_logger == null)
                {
                    _logger = LogManager.GetLogger("");
                }
                return _logger;
            }
        }

        public static void Configure()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        #region TraceMethodCallContext
        /// <summary>
        /// Method Call Context
        /// </summary>
        public class TraceMethodCallContext : IDisposable
        {
            /// <summary>
            /// Methodname
            /// </summary>
            public string Method { get; set; }
            /// <summary>
            /// Message (optional)
            /// </summary>
            public string Message { get; set; }
            /// <summary>
            /// Starttime of Methodcall
            /// </summary>
            private Stopwatch watch = new Stopwatch();

            /// <summary>
            /// Constructs a new TraceMethodCallContext - internal
            /// </summary>
            /// <param name="method">Methodname</param>
            /// <param name="msg">Message</param>
            internal TraceMethodCallContext(string method, string msg)
            {
                if (Log.IsDebugEnabled)
                {
                    this.Method = method;
                    this.Message = msg;
                    if (string.IsNullOrEmpty(Message))
                        Log.InfoFormat(">> {0}", Method);
                    else
                        Log.InfoFormat(">> {0}: {1}", Method, Message);
                    watch.Start();
                }
            }

            /// <summary>
            /// Rollback the TraceMethodCallContext
            /// </summary>
            public void Dispose()
            {
                if (Log.IsDebugEnabled)
                {
                    watch.Stop();
                    if (string.IsNullOrEmpty(Message))
                        Log.InfoFormat("<< {0:n0}ms {1}", watch.ElapsedMilliseconds, Method);
                    else
                        Log.InfoFormat("<< {0:n0}ms {1}: {2}", watch.ElapsedMilliseconds, Method, Message);
                }
            }
        }
        #endregion

        /// <summary>
        /// Internal Helper to get the calling Methodname
        /// </summary>
        /// <returns>Methodname from the current StackTrace</returns>
        private static string GetCallingMethodName(ILog log)
        {
            if (log.IsDebugEnabled)
            {
                StackTrace stackTrace = new StackTrace();
                MethodBase mi = stackTrace.GetFrame(2).GetMethod();
                return mi.DeclaringType.FullName + "." + mi.Name;
            }
            else
            {
                return "<Method Tracing is disabled, set TraceLevel to verbose>";
            }
        }

        /// <summary>
        /// Traces a Methodcall Context without a Message.
        /// using(TraceHelper.TraceMethodCall()) { ... }
        /// </summary>
        /// <returns>TraceMethodCallContext</returns>
        public static TraceMethodCallContext TraceMethodCall(this ILog log)
        {
            try
            {
                if (log.IsDebugEnabled)
                {
                    return new TraceMethodCallContext(GetCallingMethodName(log), "");
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return new TraceMethodCallContext("<unknown Method>", "");
            }
        }

        /// <summary>
        /// Traces a Methodcall Context with a Message.
        /// using(TraceHelper.TraceMethodCall("Message")) { ... }
        /// </summary>
        /// <param name="msg">Message</param>
        /// <returns>TraceMethodCallContext</returns>
        public static TraceMethodCallContext TraceMethodCall(this ILog log, string msg)
        {
            try
            {
                if (log.IsDebugEnabled)
                {
                    return new TraceMethodCallContext(GetCallingMethodName(log), msg);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return new TraceMethodCallContext("<unknown Method>", msg);
            }
        }

        /// <summary>
        /// Traces a Methodcall Context with a Formatstring and Parameter.
        /// using(TraceHelper.TraceMethodCall("Message {0}", i)) { ... }
        /// </summary>
        /// <param name="format">Formatstring</param>
        /// <param name="p">Formatstring Parameter</param>
        /// <returns>TraceMethodCallContext</returns>
        public static TraceMethodCallContext TraceMethodCall(this ILog log, string format, params object[] p)
        {
            try
            {
                if (log.IsDebugEnabled)
                {
                    return new TraceMethodCallContext(GetCallingMethodName(log), string.Format(format, p));
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return new TraceMethodCallContext("<unknown Method>", string.Format(format, p));
            }
        }
    }
}
