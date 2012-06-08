namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class ServiceDescriptorActions
    {
        [Invocation]
        public static void ToString(Kistl.App.Base.ServiceDescriptor obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = obj.Description;

            ToStringHelper.FixupFloatingObjectsToString(obj, e);
        }

    }
}
