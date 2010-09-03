using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Kistl.API.Migration
{
    public interface ITypeConverter
    {
        object Convert(object v);
    }

    public class DateTimeConverter : ITypeConverter
    {
        private static readonly Regex RegExYear = new Regex("^(\\d{4})$");
        private static readonly Regex RegExYearMonth = new Regex("^(\\d{4})/(\\d{2})$");

        public virtual object Convert(object v)
        {
            if (v == null || v == DBNull.Value) return DBNull.Value;

            try
            {
                return System.Convert.ToDateTime(v, CultureInfo.GetCultureInfo("de-AT"));
            }
            catch
            {
                // ignore, continue
            }

            try
            {
                // Try some regex
                if (v is string)
                {
                    var s = (string)v;

                    var m = RegExYearMonth.Match(s);
                    if (m != null && m.Groups.Count == 3)
                    {
                        return new DateTime(
                            int.Parse(m.Groups[1].Value),
                            int.Parse(m.Groups[2].Value),
                            1);
                    }
                    m = RegExYear.Match(s);
                    if (m != null && m.Groups.Count == 2)
                    {
                        return new DateTime(
                            int.Parse(m.Groups[1].Value),
                            1,
                            1);
                    }
                }
            }
            catch
            {
                // ignore, continue
            }

            return DBNull.Value;
        }
    }

    public class SqlServerDateTimeConverter : DateTimeConverter
    {
        private static readonly DateTime SqlMinValue = new DateTime(1753, 1, 1);
        private static readonly DateTime SqlMaxValue = new DateTime(9999, 12, 31);

        public override object Convert(object v)
        {
            var result = base.Convert(v);
            if (result is DateTime)
            {
                var d = (DateTime)result;
                if (d < SqlMinValue || d > SqlMaxValue) return DBNull.Value;
            }
            return result;
        }
    }
}
