namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class DoublePropertyActions
    {
        [Invocation]
        public static void GetPropertyType(DoubleProperty obj, MethodReturnEventArgs<Type> e)
        {
            e.Result = typeof(double);
            PropertyActions.DecorateParameterType(obj, e, true, obj.IsList, obj.HasPersistentOrder);
        }

        [Invocation]
        public static void GetElementTypeString(DoubleProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "double";
            PropertyActions.DecorateElementType(obj, e, true);
        }

        [Invocation]
        public static void GetPropertyTypeString(DoubleProperty obj, MethodReturnEventArgs<string> e)
        {
            GetElementTypeString(obj, e);
            PropertyActions.DecorateParameterType(obj, e, true, obj.IsList, obj.HasPersistentOrder);
        }
    }
}
