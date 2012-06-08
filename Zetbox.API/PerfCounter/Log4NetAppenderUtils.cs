
namespace Kistl.API.PerfCounter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using log4net;

    public class Log4NetAppenderUtils
    {
        private readonly static ILog _mainLogger = LogManager.GetLogger("Kistl.PerfCounter.Main");
        private readonly static ILog _objectsLogger = LogManager.GetLogger("Kistl.PerfCounter.Objects");

        private static bool firstObjectWrite = true;
        private static object firstObjectWriteLock = new object();

        private static bool firstMainWrite = true;
        private static object firstMainWriteLock = new object();

        public static void Dump(Dictionary<string, ObjectMemoryCounters> objects, Dictionary<string, string> totals)
        {
            try
            {
                if (objects != null && _objectsLogger != null)
                {
                    foreach (var omc in objects.Values)
                    {
                        var collector = new Dictionary<string, string>();
                        omc.FormatTo(collector);
                        var data = collector.OrderBy(kvp => kvp.Key);
                        CheckFirstObjectWrite(data);
                        _objectsLogger.InfoFormat("{0};{1}", omc.Name, string.Join(";", data.Select(kvp => kvp.Value).ToArray()));
                    }
                }

                if (totals != null && _mainLogger != null)
                {
                    var data = totals.OrderBy(kvp => kvp.Key);
                    CheckFirstMainWrite(data);
                    _mainLogger.Info(string.Join(";", data.Select(kvp => kvp.Value).ToArray()));
                }
            }
            catch
            {
                // don't care
            }
        }

        private static void CheckFirstObjectWrite(IOrderedEnumerable<KeyValuePair<string, string>> data)
        {
            if (firstObjectWrite)
            {
                lock (firstObjectWriteLock)
                {
                    if (firstObjectWrite)
                    {
                        _objectsLogger.Info("name;" + string.Join(";", data.Select(kvp => kvp.Key).ToArray()));
                        firstObjectWrite = false;
                    }
                }
            }
        }

        private static void CheckFirstMainWrite(IOrderedEnumerable<KeyValuePair<string, string>> data)
        {
            if (firstMainWrite)
            {
                lock (firstMainWriteLock)
                {
                    if (firstMainWrite)
                    {
                        _mainLogger.Info(string.Join(";", data.Select(kvp => kvp.Key).ToArray()));
                        firstMainWrite = false;
                    }
                }
            }
        }
    }
}

