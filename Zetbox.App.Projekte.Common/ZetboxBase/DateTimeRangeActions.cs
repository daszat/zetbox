namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class DateTimeRangeActions
    {
        [Invocation]
        public static void ToString(DateTimeRange obj, MethodReturnEventArgs<string> e)
        {
            e.Result = string.Format("{0:d} - {1:d}", obj.From, obj.Thru);
        }

        [Invocation]
        public static void get_TotalDays(DateTimeRange obj, PropertyGetterEventArgs<int?> e)
        {
            e.Result = obj.From.HasValue && obj.Thru.HasValue ? (int?)((obj.Thru.Value - obj.From.Value).TotalDays) : null;
        }
    }
}
