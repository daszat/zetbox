namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class CalculatedObjectReferencePropertyActions
    {
        [Invocation]
        public static void GetPropertyType(CalculatedObjectReferenceProperty obj, MethodReturnEventArgs<Type> e)
        {
            var def = obj.ReferencedClass;
            e.Result = Type.GetType(def.Module.Namespace + "." + def.Name, true);
            PropertyActions.DecorateParameterType(obj, e, false, false, false);
        }

        [Invocation]
        public static void GetElementTypeString(CalculatedObjectReferenceProperty obj, MethodReturnEventArgs<string> e)
        {
            var def = obj.ReferencedClass;
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
        public static void GetPropertyTypeString(CalculatedObjectReferenceProperty obj, MethodReturnEventArgs<string> e)
        {
            GetElementTypeString(obj, e);
            PropertyActions.DecorateParameterType(obj, e, false, false, false);
        }
    }
}
