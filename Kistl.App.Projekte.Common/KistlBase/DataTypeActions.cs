namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class DataTypeActions
    {
        [Invocation]
        public static void ToString(DataType obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0}.{1}",
                obj.Module == null
                    ? "[no module]"
                    : obj.Module.Namespace,
                obj.Name);

            ToStringHelper.FixupFloatingObjectsToString(obj, e);
        }

    }
}
