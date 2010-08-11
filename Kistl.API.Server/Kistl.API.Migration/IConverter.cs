using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Kistl.API.Migration
{
    public interface IConverter
    {
        object Convert(object v);
    }

    public class SqlServerDateTimeConverter : IConverter
    {
        private static readonly DateTime SqlMinValue = new DateTime(1753, 1, 1);
        private static readonly DateTime SqlMaxValue = new DateTime(9999, 12, 31);

        public object Convert(object v)
        {
            if (v == null || v == DBNull.Value) return DBNull.Value;

            try
            {
                var result = System.Convert.ToDateTime(v, CultureInfo.GetCultureInfo("de-AT"));
                if (result < SqlMinValue || result > SqlMaxValue) return DBNull.Value;
                return result;
            }
            catch
            {
                // ignore, return DBNull
                return DBNull.Value;
            }
        }
    }
}
