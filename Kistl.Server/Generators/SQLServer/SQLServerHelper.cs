using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.SQLServer
{
    internal class SQLServerHelper
    {
        #region GetDBType
        /// <summary>
        /// Returns the coresponding database type of the given CLR Type
        /// </summary>
        /// <param name="clrType">CLR Type as string</param>
        /// <returns>Databasetype</returns>
        public static string GetDBType(string clrType)
        {
            // Try to get the CLRType
            Type t = Type.GetType(clrType, false, false);

            if (t == null) throw new DBTypeNotFoundException(clrType);

            // Resolve...
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

            throw new DBTypeNotFoundException(clrType);
        }
        #endregion
    }
}
