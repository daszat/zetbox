namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class StringPropertyActions
    {
        [Invocation]
        public static void GetPropertyType(StringProperty obj, MethodReturnEventArgs<Type> e)
        {
            e.Result = typeof(string);
            PropertyActions.DecorateParameterType(obj, e, false, obj.IsList, obj.HasPersistentOrder);
        }

        [Invocation]
        public static void GetElementTypeString(StringProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "string";
            PropertyActions.DecorateElementType(obj, e, false);
        }

        [Invocation]
        public static void GetPropertyTypeString(StringProperty obj, MethodReturnEventArgs<string> e)
        {
            GetElementTypeString(obj, e);
            PropertyActions.DecorateParameterType(obj, e, false, obj.IsList, obj.HasPersistentOrder);
        }
    }
}
