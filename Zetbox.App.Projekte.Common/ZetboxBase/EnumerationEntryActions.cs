namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class EnumerationEntryActions
    {
        [Invocation]
        public static void GetLabel(Zetbox.App.Base.EnumerationEntry obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = !string.IsNullOrEmpty(obj.Label) ? obj.Label : obj.Name;
        }

        
        [Invocation]
        public static void ToString(EnumerationEntry obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Enumeration + "." + obj.Name;

            ToStringHelper.FixupFloatingObjectsToString(obj, e);
        }
    }
}
