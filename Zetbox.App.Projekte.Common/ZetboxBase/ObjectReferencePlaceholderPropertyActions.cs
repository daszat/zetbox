namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class ObjectReferencePlaceholderPropertyActions
    {
        [Invocation]
        public static void GetPropertyType(ObjectReferencePlaceholderProperty obj, MethodReturnEventArgs<Type> e)
        {
            var def = obj.ReferencedObjectClass;
            e.Result = Type.GetType(def.Module.Namespace + "." + def.Name + ", " + Kistl.API.Helper.InterfaceAssembly, true);
            PropertyActions.DecorateParameterType(obj, e, false, obj.IsList, obj.HasPersistentOrder);
        }

        [Invocation]
        public static void GetElementTypeString(ObjectReferencePlaceholderProperty obj, MethodReturnEventArgs<string> e)
        {
            var def = obj.ReferencedObjectClass;
            if (def == null)
            {
                e.Result = "<no class>";
            }
            else if (def.Module == null)
            {
                e.Result = "<no namespace>." + def.Name;
            }
            else
            {
                e.Result = def.Module.Namespace + "." + def.Name;
            }
            PropertyActions.DecorateElementType(obj, e, false);
        }

        [Invocation]
        public static void GetPropertyTypeString(ObjectReferencePlaceholderProperty obj, MethodReturnEventArgs<string> e)
        {
            GetElementTypeString(obj, e);
            PropertyActions.DecorateParameterType(obj, e, false, obj.IsList, obj.HasPersistentOrder);
        }
    }
}
