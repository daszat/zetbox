namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class GuidPropertyActions
    {
        [Invocation]
        public static void GetPropertyType(GuidProperty obj, MethodReturnEventArgs<Type> e)
        {
            e.Result = typeof(Guid);
            PropertyActions.DecorateParameterType(obj, e, true, obj.IsList, obj.HasPersistentOrder);
        }

        [Invocation]
        public static void GetElementTypeString(GuidProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "Guid";
            PropertyActions.DecorateElementType(obj, e, true);
        }

        [Invocation]
        public static void GetPropertyTypeString(GuidProperty obj, MethodReturnEventArgs<string> e)
        {
            GetElementTypeString(obj, e);
            PropertyActions.DecorateParameterType(obj, e, true, obj.IsList, obj.HasPersistentOrder);
        }
    }
}
