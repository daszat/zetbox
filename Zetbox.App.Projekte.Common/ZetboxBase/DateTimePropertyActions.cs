namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class DateTimePropertyActions
    {
        [Invocation]
        public static void GetPropertyType(DateTimeProperty obj, MethodReturnEventArgs<Type> e)
        {
            e.Result = typeof(DateTime);
            PropertyActions.DecorateParameterType(obj, e, true, obj.IsList, obj.HasPersistentOrder);
        }

        [Invocation]
        public static void GetElementTypeString(DateTimeProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "DateTime";
            PropertyActions.DecorateElementType(obj, e, true);
        }

        [Invocation]
        public static void GetPropertyTypeString(DateTimeProperty obj, MethodReturnEventArgs<string> e)
        {
            GetElementTypeString(obj, e);
            PropertyActions.DecorateParameterType(obj, e, true, obj.IsList, obj.HasPersistentOrder);
        }
    }
}
