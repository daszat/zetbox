using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;

namespace Kistl.Server
{
    /// <summary>
    /// Server Helper
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Handles an Error
        /// </summary>
        /// <param name="ex">Expeption to handle</param>
        public static void HandleError(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            System.Diagnostics.Trace.TraceError(ex.ToString());
        }

        /// <summary>
        /// Path to temp. location for Code Generation.
        /// TODO: Hardcoded yet
        /// </summary>
        public static string CodeGenPath
        {
            get
            {
                return @"c:\temp\KistlCodeGen";
            }
        }
    }
}
