
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    [Implementor]
    public static class ObjectReferencePropertyActions
    {
        [Invocation]
        public static void GetPropertyType(ObjectReferenceProperty obj, MethodReturnEventArgs<Type> e)
        {
            var def = obj.GetReferencedObjectClass();
            e.Result = Type.GetType(def.Module.Namespace + "." + def.Name + ", " + Kistl.API.Helper.InterfaceAssembly, true);
            PropertyActions.DecorateParameterType(obj, e, false, obj.GetIsList(), obj.RelationEnd.Parent.GetOtherEnd(obj.RelationEnd).HasPersistentOrder);
        }

        [Invocation]
        public static void GetElementTypeString(ObjectReferenceProperty obj, MethodReturnEventArgs<string> e)
        {
            var def = obj.GetReferencedObjectClass();
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
        public static void GetPropertyTypeString(ObjectReferenceProperty obj, MethodReturnEventArgs<string> e)
        {
            GetElementTypeString(obj, e);
            PropertyActions.DecorateParameterType(obj, e, false, obj.GetIsList(), obj.RelationEnd.Parent.GetOtherEnd(obj.RelationEnd).HasPersistentOrder);
        }

        [Invocation]
        public static void GetIsList(ObjectReferenceProperty prop, MethodReturnEventArgs<bool> e)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            RelationEnd relEnd = prop.RelationEnd;
            Relation rel = relEnd.GetParent();
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

            e.Result = otherEnd.Multiplicity.UpperBound() > 1;
        }

        [Invocation]
        public static void ToString(ObjectReferenceProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "-> " + e.Result;

            // already handled by base OnToString_Property()
            // ToStringHelper.FixupFloatingObjects(obj, e);
        }
    }
}
