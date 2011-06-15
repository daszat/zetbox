namespace Kistl.App.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    [Implementor]
    public static class EasterCalendarRuleActions
    {
        [Invocation]
        public static void ToString(EasterCalendarRule obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format(e.Result + "; Easter, offset {0} days", obj.Offset);
        }

        [Invocation]
        public static void AppliesTo(EasterCalendarRule obj, MethodReturnEventArgs<System.Boolean> e, System.DateTime date)
        {
            var ostern = GetOstersonntag(date.Year);
            e.Result = ostern.AddDays(obj.Offset ?? 0) == date.Date;
        }

        private static DateTime GetOstersonntag(int jahr)
        {
            int c;
            int i;
            int j;
            int k;
            int l;
            int n;
            int osterTag;
            int osterMonat;

            c = jahr / 100;
            n = jahr % 19;
            k = (c - 17) / 25;
            i = c - c / 4 - ((c - k) / 3) + 19 * n + 15;
            i = i % 30;
            i = i - (i / 28) * ((1 - (i / 28)) * ((29 / (i + 1))) * ((21 - n) / 11));
            j = jahr + (jahr / 4) + i + 2 - c + (c / 4);
            j = j % 7;
            l = i - j;

            osterMonat = 3 + ((l + 40) / 44);
            osterTag = l + 28 - 31 * (osterMonat / 4);

            return new DateTime(jahr, osterMonat, osterTag);
        }
    }
}
