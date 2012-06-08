namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class IntPropertyActions
    {
        [Invocation]
        public static void GetPropertyType(IntProperty obj, MethodReturnEventArgs<Type> e)
        {
            e.Result = typeof(int);
            PropertyActions.DecorateParameterType(obj, e, true, obj.IsList, obj.HasPersistentOrder);
        }

        [Invocation]
        public static void GetElementTypeString(IntProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "int";
            PropertyActions.DecorateElementType(obj, e, true);
        }

        [Invocation]
        public static void GetPropertyTypeString(IntProperty obj, MethodReturnEventArgs<string> e)
        {
            GetElementTypeString(obj, e);
            PropertyActions.DecorateParameterType(obj, e, true, obj.IsList, obj.HasPersistentOrder);
        }
    }
}
