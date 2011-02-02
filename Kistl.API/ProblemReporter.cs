namespace Kistl.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API.Utils;
    using System.IO;

    public interface IProblemReporter
    {
        void Report(/*string message, string description, byte[] screenshot, Exception ex*/); // would be nice, but doen't work today with FogBugz
    }

    public class LoggingProblemReporter : IProblemReporter
    {
        public void Report()
        {
        }
    }

    public class FogBugzProblemReporter : IProblemReporter
    {
        public void Report()
        {
            try
            {
                System.Diagnostics.ProcessStartInfo si = new System.Diagnostics.ProcessStartInfo();
                var filename = Path.Combine(ProgramFilesx86(), "FogBugz" + Path.DirectorySeparatorChar + "Screenshot" + Path.DirectorySeparatorChar + "screenshot.exe");
                if (!File.Exists(filename)) throw new InvalidOperationException("FogBugz screenshot tool not found. Maybe it's not installed");
                si.FileName = filename;
                si.Arguments = "/picture";
                System.Diagnostics.Process.Start(si);
            }
            catch (Exception ex)
            {
                Logging.Log.Error(ex.ToString());
                throw;
            }
        }

        static string ProgramFilesx86()
        {
            if (8 == IntPtr.Size
                || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }
    }

}
