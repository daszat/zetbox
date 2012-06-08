namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class BoolPropertyActions
    {
        [Invocation]
        public static void GetPropertyType(BoolProperty obj, MethodReturnEventArgs<Type> e)
        {
            e.Result = typeof(bool);
            PropertyActions.DecorateParameterType(obj, e, true, obj.IsList, obj.HasPersistentOrder);
        }

        [Invocation]
        public static void GetElementTypeString(BoolProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "bool";
            PropertyActions.DecorateElementType(obj, e, true);
        }

        [Invocation]
        public static void GetPropertyTypeString(BoolProperty obj, MethodReturnEventArgs<string> e)
        {
            GetElementTypeString(obj, e);
            PropertyActions.DecorateParameterType(obj, e, true, obj.IsList, obj.HasPersistentOrder);
        }
    }
}
