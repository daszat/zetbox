using System;
using System.Collections.Generic;
using System.Linq;

using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;

namespace Kistl.Server
{
    /// <summary>
    /// Temp. Kist Objects Extensions
    /// </summary>
    public static class TheseMethodsShouldBeImplementedOnKistlObjects
    {
        public static ICollection<ObjectClass> GetObjectHierarchie(this ObjectClass objClass)
        {
            List<ObjectClass> result = new List<ObjectClass>();
            while (objClass != null)
            {
                result.Add(objClass);
                objClass = objClass.BaseObjectClass;
            }

            result.Reverse();
            return result;
        }

        public static ObjectClass GetRootClass(this ObjectClass objClass)
        {
            while (objClass.BaseObjectClass != null)
            {
                objClass = objClass.BaseObjectClass;
            }
            return objClass;
        }

        public static ObjectClass GetObjectClass(this IDataObject obj, Kistl.API.IKistlContext ctx)
        {
            Type type = obj.GetInterfaceType();
            return ctx.GetQuery<ObjectClass>().First(o => o.Module.Namespace == type.Namespace && o.ClassName == type.Name);
        }

        public static BaseProperty GetProperty(this ObjectClass c, string property)
        {
            ObjectClass objClass = c;
            while (objClass != null)
            {
                BaseProperty prop = objClass.Properties.SingleOrDefault(p => p.PropertyName == property);
                if (prop != null)
                {
                    return prop;
                }
                objClass = objClass.BaseObjectClass;
            }

            return null;
        }

        public static Relation GetRelation(this ObjectReferenceProperty p)
        {
            return p.LeftOf ?? p.RightOf;
        }

        public static ObjectReferenceProperty GetOpposite(this ObjectReferenceProperty p)
        {
            Relation rel = GetRelation(p);
            if (rel == null) return null;
            if (rel.LeftPart == p) return rel.RightPart;
            if (rel.RightPart == p) return rel.LeftPart;

            throw new InvalidOperationException("Unable to find Opposite Property");
        }

        public static RelationType GetRelationType(this Relation rel)
        {
            if (rel.LeftPart.IsList == false && rel.RightPart.IsList == true) return RelationType.one_n;
            if (rel.LeftPart.IsList == true && rel.RightPart.IsList == false) return RelationType.one_n;
            if (rel.LeftPart.IsList == true && rel.RightPart.IsList == true) return RelationType.n_m;
            if (rel.LeftPart.IsList == false && rel.RightPart.IsList == false) return RelationType.one_one;

            throw new InvalidOperationException("Unable to find out RelationType");
        }

        public static RelationType GetRelationType(this ObjectReferenceProperty p)
        {
            Relation rel = p.GetRelation();
            if (rel == null) return p.IsList ? RelationType.n_m : RelationType.one_n;
            return rel.GetRelationType();
        }

        public static bool HasStorage(this BaseProperty bp)
        {
            if (bp is ObjectReferenceProperty)
            {
                return HasStorage((ObjectReferenceProperty)bp);
            }
            else
            {
                return true;
            }
        }

        private static bool HasStorage(this ObjectReferenceProperty p)
        {
            Relation rel = GetRelation(p);
            if (rel == null) return true;

            if (rel.Storage == StorageType.Replicate)
                throw new NotImplementedException();

            RelationType type = rel.GetRelationType();
            return
                (type == RelationType.one_n && p.IsList == false) ||
                (type == RelationType.one_one && rel.Storage == StorageType.Replicate) ||
                (type == RelationType.one_one && rel.Storage == StorageType.Left && rel.LeftPart == p) ||
                (type == RelationType.one_one && rel.Storage == StorageType.Right && rel.RightPart == p) ||
                // TODO: n:m darf nicht an eine Seite gebunden sein
                (type == RelationType.n_m && rel.LeftPart == p);
        }
        public static bool IsFrozen(this ObjectClass cls)
        {
            while (cls != null)
            {
                if (cls.IsFrozenObject)
                    return true;
                cls = cls.BaseObjectClass;
            }
            return false;
        }
    }
}
