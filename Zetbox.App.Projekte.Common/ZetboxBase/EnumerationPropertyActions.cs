namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class EnumerationPropertyActions
    {
        [Invocation]
        public static void GetPropertyType(EnumerationProperty obj, MethodReturnEventArgs<Type> e)
        {
            var cls = obj.Enumeration;
            e.Result = Type.GetType(cls.Module.Namespace + "." + cls.Name + ", " + Kistl.API.Helper.InterfaceAssembly, true);
            PropertyActions.DecorateParameterType(obj, e, true, obj.IsList, obj.HasPersistentOrder);
        }

        [Invocation]
        public static void GetElementTypeString(EnumerationProperty obj, MethodReturnEventArgs<string> e)
        {
            var cls = obj.Enumeration;
            if (cls == null)
            {
                e.Result = "<no enum>";
            }
            else if (cls.Module == null)
            {
                e.Result = "<no namespace>." + cls.Name;
            }
            else
            {
                e.Result = cls.Module.Namespace + "." + cls.Name;
            }
            PropertyActions.DecorateElementType(obj, e, true);
        }

        [Invocation]
        public static void GetPropertyTypeString(EnumerationProperty obj, MethodReturnEventArgs<string> e)
        {
            GetElementTypeString(obj, e);
            PropertyActions.DecorateParameterType(obj, e, true, obj.IsList, obj.HasPersistentOrder);
        }
    }
}
