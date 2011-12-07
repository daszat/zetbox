namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class CompoundObjectPropertyActions
    {
        [Invocation]
        public static void GetPropertyType(CompoundObjectProperty obj, MethodReturnEventArgs<Type> e)
        {
            var def = obj.CompoundObjectDefinition;
            e.Result = Type.GetType(def.Module.Namespace + "." + def.Name, true);
            PropertyActions.DecorateParameterType(obj, e, false, obj.IsList, obj.HasPersistentOrder);
        }

        [Invocation]
        public static void GetElementTypeString(CompoundObjectProperty obj, MethodReturnEventArgs<string> e)
        {
            var def = obj.CompoundObjectDefinition;
            if (def == null)
            {
                e.Result = "<no type>";
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
        public static void GetPropertyTypeString(CompoundObjectProperty obj, MethodReturnEventArgs<string> e)
        {
            GetElementTypeString(obj, e);
            PropertyActions.DecorateParameterType(obj, e, false, obj.IsList, obj.HasPersistentOrder);
        }
    }
}
