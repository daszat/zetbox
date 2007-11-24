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
        /// TODO: Das ist nicht die beste implementierung...
        /// </summary>
        /// <param name="ex"></param>
        public static void HandleError(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        public static string CodeGenPath
        {
            get
            {
                return @"c:\temp\KistlCodeGen";
            }
        }

        #region GetDBType
        public static string GetDBType(string clrType)
        {
            Type t = Type.GetType(clrType, false, false);

            // TODO: Lang lebe der Pfusch!
            if (t == null) return "unknown";

            // TODO: Get from Metadata
            if (t == typeof(int))
                return "int";
            if (t == typeof(string))
                return "nvarchar";
            if (t == typeof(double))
                return "float";
            if (t == typeof(bool))
                return "bit";
            if (t == typeof(DateTime))
                return "datetime";
            return "unknown";
        }
        #endregion
    }
}
