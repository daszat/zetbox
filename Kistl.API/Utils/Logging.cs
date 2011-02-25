using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using log4net;

namespace Kistl.API.Utils
{
    public static class Logging
    {
        private readonly static ILog _logger = LogManager.GetLogger("Kistl");
        private readonly static ILog _facade = LogManager.GetLogger("Kistl.Facade");
        private readonly static ILog _linq = LogManager.GetLogger("Kistl.Linq");
        private readonly static ILog _linqquery = LogManager.GetLogger("Kistl.Linq.Query");
        private readonly static ILog _reflection = LogManager.GetLogger("Kistl.Reflection");
        private readonly static ILog _assemblyLoader = LogManager.GetLogger("Kistl.AssemblyLoader");

        public static ILog Log
        {
            get { return _logger; }
        }

        public static ILog Facade
        {
            get { return _facade; }
        }

        public static ILog Linq
        {
            get { return _linq; }
        }
        public static ILog LinqQuery
        {
            get { return _linqquery; }
        }
        public static ILog Reflection
        {
            get { return _reflection; }
        }
        public static ILog AssemblyLoader
        {
            get { return _assemblyLoader; }
        }

        public static void Configure()
        {
            log4net.Config.XmlConfigurator.Configure();

            ResetDefaultProperties();

            // push empty string onto NDC stack to avoid (null) messages in output
            ThreadContext.Stacks["NDC"].Push(String.Empty);
        }

        private static void ResetDefaultProperties()
        {
            // log4net sets the property to null when popping the last stack
            // this makes for ugly messages; thus we fix it here.
            SetEmptyIfNull(GlobalContext.Properties, "INDENT");
            SetEmptyIfNull(ThreadContext.Properties, "INDENT");
            SetEmptyIfNull(GlobalContext.Properties, "NDC");
        }

        private static void SetEmptyIfNull(log4net.Util.ContextPropertiesBase properties, string p)
        {
            if (properties[p] == null)
            {
                properties[p] = String.Empty;
            }
        }

        #region TraceMethodCallContext
        /// <summary>
        /// Method Call Context
        /// </summary>
        private abstract class TraceMethodCallContext
            : IDisposable
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

            public StackTrace StackTrace { get; private set; }

            /// <summary>Logger</summary>
            protected readonly ILog log;

            private IDisposable stack;

            protected abstract void LogFormat(string format, params object[] args);

            /// <summary>
            /// Constructs a new TraceMethodCallContext - internal
            /// </summary>
            /// <param name="log">Logger</param>
            /// <param name="method">Methodname</param>
            /// <param name="stackTrace">Stacktrace</param>
            /// <param name="msg">Message</param>
            internal TraceMethodCallContext(ILog log, string method, StackTrace stackTrace, string msg)
            {
                this.log = log;
                this.Method = method;
                this.Message = msg;
                this.StackTrace = stackTrace;

                if (String.IsNullOrEmpty(Message))
                    LogFormat(">> {0}", Method);
                else
                    LogFormat(">> {0}: {1}", Method, Message);

                //if (StackTrace != null)
                //{
                //    log.Debug(StackTrace.ToString());
                //}

                watch.Start();

                // only use the actual method name for NDC
                stack = log4net.NDC.Push(Method.Split('.').Last());
            }

            /// <summary>
            /// Rollback the TraceMethodCallContext
            /// </summary>
            public void Dispose()
            {
                if (stack != null)
                {
                    stack.Dispose();
                    stack = null;
                    ResetDefaultProperties();
                }

                if (watch != null)
                {
                    watch.Stop();
                    if (string.IsNullOrEmpty(Message))
                        LogFormat("<< {0:n0}ms {1}", watch.ElapsedMilliseconds, Method);
                    else
                        LogFormat("<< {0:n0}ms {1}: {2}", watch.ElapsedMilliseconds, Method, Message);
                    watch = null;
                }
                else
                {
                    Logging.Log.Error("TraceMethodCallContext: disposed a second time!");
                }
            }
        }

        private sealed class DebugTraceMethodCallContext
            : TraceMethodCallContext
        {
            internal DebugTraceMethodCallContext(ILog log, string method, StackTrace stackTrace, string msg) : base(log, method, stackTrace, msg) { }

            protected override void LogFormat(string format, params object[] args)
            {
                log.DebugFormat(format, args);
            }
        }

        private sealed class InfoTraceMethodCallContext
            : TraceMethodCallContext
        {
            internal InfoTraceMethodCallContext(ILog log, string method, StackTrace stackTrace, string msg) : base(log, method, stackTrace, msg) { }

            protected override void LogFormat(string format, params object[] args)
            {
                log.InfoFormat(format, args);
            }
        }
        #endregion

        /// <summary>
        /// Traces a method call context without a message if the DEBUG level is active.
        /// Usage: using(Log.DebugTraceMethodCall()) { ... }
        /// </summary>
        /// <param name="log">The logger to log to.</param>
        /// <param name="method">The calling method</param>
        /// <returns>An IDisposable helper that closes the context when it's disposed.</returns>
        public static IDisposable DebugTraceMethodCall(this ILog log, string method)
        {
            if (log == null || !log.IsDebugEnabled) { return null; }

            try
            {
                StackTrace s = new StackTrace();
                return new DebugTraceMethodCallContext(log, method, s, String.Empty);
            }
            catch
            {
                return new DebugTraceMethodCallContext(log, method, null, String.Empty);
            }
        }

        /// <summary>
        /// Traces a method call context with a message if the DEBUG level is active.
        /// Usage: using(Log.DebugTraceMethodCall("additional info")) { ... }
        /// </summary>
        /// <param name="log">The logger to log to.</param>
        /// <param name="method">The calling method</param>
        /// <param name="msg">The additional message to log.</param>
        /// <returns>An IDisposable helper that closes the context when it's disposed.</returns>
        public static IDisposable DebugTraceMethodCall(this ILog log, string method, string msg)
        {
            if (log == null || !log.IsDebugEnabled) { return null; }

            try
            {
                StackTrace s = new StackTrace();
                return new DebugTraceMethodCallContext(log, method, s, msg);
            }
            catch
            {
                return new DebugTraceMethodCallContext(log, method, null, msg);
            }
        }

        /// <summary>
        /// Traces a method call context with a message if the DEBUG level is active.
        /// Usage: using(Log.DebugTraceMethodCallFormat("Methodname", "foobar=[{0}]", foobar)) { ... }
        /// </summary>
        /// <param name="log">The logger to log to.</param>
        /// <param name="method">The calling method</param>
        /// <param name="format">The format string for the log message</param>
        /// <param name="p">the parameters for the log message format</param>
        /// <returns>An IDisposable helper that closes the context when it's disposed.</returns>
        public static IDisposable DebugTraceMethodCallFormat(this ILog log, string method, string format, params object[] p)
        {
            if (log == null || !log.IsDebugEnabled) { return null; }

            try
            {
                StackTrace s = new StackTrace();
                return new DebugTraceMethodCallContext(log, method, s, String.Format(format, p));
            }
            catch
            {
                return new DebugTraceMethodCallContext(log, method, null, String.Format(format, p));
            }
        }

        /// <summary>
        /// Traces a method call context without a message if the INFO level is active.
        /// Usage: using(Log.InfoTraceMethodCall()) { ... }
        /// </summary>
        /// <param name="log">The logger to log to.</param>
        /// <param name="method">The calling method</param>
        /// <returns>An IDisposable helper that closes the context when it's disposed.</returns>
        public static IDisposable InfoTraceMethodCall(this ILog log, string method)
        {
            if (log == null || !log.IsInfoEnabled) { return null; }

            try
            {
                StackTrace s = log.IsDebugEnabled ? new StackTrace() : null;
                return new InfoTraceMethodCallContext(log, method, s, String.Empty);
            }
            catch
            {
                return new InfoTraceMethodCallContext(log, method, null, String.Empty);
            }
        }

        /// <summary>
        /// Traces a method call context with a message if the INFO level is active.
        /// Usage: using(Log.InfoTraceMethodCall("additional info")) { ... }
        /// </summary>
        /// <param name="log">The logger to log to.</param>
        /// <param name="method">The calling method</param>
        /// <param name="msg">The additional message to log.</param>
        /// <returns>An IDisposable helper that closes the context when it's disposed.</returns>
        public static IDisposable InfoTraceMethodCall(this ILog log, string method, string msg)
        {
            if (log == null || !log.IsInfoEnabled) { return null; }

            try
            {
                StackTrace s = log.IsDebugEnabled ? new StackTrace() : null;
                return new InfoTraceMethodCallContext(log, method, s, msg);
            }
            catch
            {
                return new InfoTraceMethodCallContext(log, method, null, msg);
            }

        }

        /// <summary>
        /// Traces a method call context with a message if the INFO level is active.
        /// Usage: using(Log.InfoTraceMethodCallFormat("Method", "foobar=[{0}]", foobar)) { ... }
        /// </summary>
        /// <param name="log">The logger to log to.</param>
        /// <param name="method">The calling method</param>
        /// <param name="format">The format string for the log message</param>
        /// <param name="p">the parameters for the log message format</param>
        /// <returns>An IDisposable helper that closes the context when it's disposed.</returns>
        public static IDisposable InfoTraceMethodCallFormat(this ILog log, string method, string format, params object[] p)
        {
            if (log == null || !log.IsInfoEnabled) { return null; }

            try
            {
                StackTrace s = log.IsDebugEnabled ? new StackTrace() : null;
                return new InfoTraceMethodCallContext(log, method, s, String.Format(format, p));
            }
            catch
            {
                return new InfoTraceMethodCallContext(log, method, null, String.Format(format, p));
            }
        }

        public static void TraceTotalMemory(this ILog log, string msg)
        {
            if (log == null || !log.IsDebugEnabled) { return; }

            log.DebugFormat(msg + ": Consuming {0:0.00} kB Memory", (double)GC.GetTotalMemory(true) / 1024.0);
        }

        private static object _warnOnceLock = new object();

        private static Dictionary<string, string> _warnOnceSeen = new Dictionary<string, string>();

        /// <summary>
        /// Logs the specified warning message once. If it is called a second time with the same message and logger name, it won't produce a message.
        /// </summary>
        /// <param name="log">the logger to log to</param>
        /// <param name="msg">the message to log</param>
        /// <remarks>This method is thread-safe.</remarks>
        public static void WarnOnce(this ILog log, string msg)
        {
            if (log == null || !log.IsWarnEnabled) { return; }
            lock (_warnOnceLock)
            {
                var key = log.Logger.Name + msg;
                if (!_warnOnceSeen.ContainsKey(key))
                {
                    log.Warn(msg);
                    _warnOnceSeen[key] = key;
                }
            }
        }
    }
}
