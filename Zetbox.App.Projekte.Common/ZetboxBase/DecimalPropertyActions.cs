namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class DecimalPropertyActions
    {
        [Invocation]
        public static void GetPropertyType(DecimalProperty obj, MethodReturnEventArgs<Type> e)
        {
            e.Result = typeof(decimal);
            PropertyActions.DecorateParameterType(obj, e, true, obj.IsList, obj.HasPersistentOrder);
        }

        [Invocation]
        public static void GetElementTypeString(DecimalProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "decimal";
            PropertyActions.DecorateElementType(obj, e, true);
        }

        [Invocation]
        public static void GetPropertyTypeString(DecimalProperty obj, MethodReturnEventArgs<string> e)
        {
            GetElementTypeString(obj, e);
            PropertyActions.DecorateParameterType(obj, e, true, obj.IsList, obj.HasPersistentOrder);
        }
    }
}
