namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class EnumerationActions
    {
        [Invocation]
        public static void GetEntryByName(Kistl.App.Base.Enumeration obj, MethodReturnEventArgs<Kistl.App.Base.EnumerationEntry> e, System.String name)
        {
            e.Result = obj.EnumerationEntries.SingleOrDefault(i => i.Name == name);
        }
        [Invocation]
        public static void GetEntryByValue(Kistl.App.Base.Enumeration obj, MethodReturnEventArgs<Kistl.App.Base.EnumerationEntry> e, System.Int32 val)
        {
            e.Result = obj.EnumerationEntries.SingleOrDefault(i => i.Value == val);
        }
        [Invocation]
        public static void GetLabelByName(Kistl.App.Base.Enumeration obj, MethodReturnEventArgs<string> e, System.String name)
        {
            var entry = obj.GetEntryByName(name);
            e.Result = entry != null ? entry.GetLabel() : string.Empty;
        }
        [Invocation]
        public static void GetLabelByValue(Kistl.App.Base.Enumeration obj, MethodReturnEventArgs<string> e, System.Int32 val)
        {
            var entry = obj.GetEntryByValue(val);
            e.Result = entry != null ? entry.GetLabel() : string.Empty;
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        [Invocation]
        public static void ToString(Enumeration obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;

            ToStringHelper.FixupFloatingObjectsToString(obj, e);
        }
    }
}
