
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.API.Utils;

    [Implementor]
    public static class RelationActions
    {
        [Invocation]
        public static void GetOtherEnd(Relation rel, MethodReturnEventArgs<RelationEnd> e, RelationEnd relEnd)
        {
            if (rel.A == relEnd)
                e.Result = rel.B;
            else if (rel.B == relEnd)
                e.Result = rel.A;
            else
                e.Result = null;
        }

        [Invocation]
        public static void GetEndFromRole(Relation rel, MethodReturnEventArgs<RelationEnd> e, RelationEndRole role)
        {
            switch (role)
            {
                case RelationEndRole.A:
                    e.Result = rel.A;
                    break;
                case RelationEndRole.B:
                    e.Result = rel.B;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("role");
            }
        }

        [Invocation]
        public static void GetEnd(Relation rel, MethodReturnEventArgs<RelationEnd> e, ObjectReferenceProperty prop)
        {
            if (rel.A != null && rel.A.Navigator == prop)
                e.Result = rel.A;
            else if (rel.B != null && rel.B.Navigator == prop)
                e.Result = rel.B;
            else
                e.Result = null;
        }

        [Invocation]
        public static void GetRelationType(Relation rel, MethodReturnEventArgs<RelationType> e)
        {
            if (rel == null)
            {
                throw new ArgumentNullException("rel");
            }
            if (rel.A == null)
            {
                throw new ArgumentNullException("rel", "rel.A is null");
            }
            if (rel.B == null)
            {
                throw new ArgumentNullException("rel", "rel.B is null");
            }

            if ((rel.A.Multiplicity.UpperBound() == 1 && rel.B.Multiplicity.UpperBound() > 1)
                || (rel.A.Multiplicity.UpperBound() > 1 && rel.B.Multiplicity.UpperBound() == 1))
            {
                e.Result = RelationType.one_n;
            }
            else if (rel.A.Multiplicity.UpperBound() > 1 && rel.B.Multiplicity.UpperBound() > 1)
            {
                e.Result = RelationType.n_m;
            }
            else if (rel.A.Multiplicity.UpperBound() == 1 && rel.B.Multiplicity.UpperBound() == 1)
            {
                e.Result = RelationType.one_one;
            }
            else
            {
                throw new InvalidOperationException(String.Format("Unable to find out RelationType: {0}:{1}", rel.A.Multiplicity, rel.B.Multiplicity));
            }
        }

        [Invocation]
        public static void NeedsPositionStorage(Relation rel, MethodReturnEventArgs<bool> e, RelationEndRole endRole)
        {
            if (rel == null)
            {
                throw new ArgumentNullException("rel");
            }
            if (rel.A == null)
            {
                throw new ArgumentNullException("rel", "rel.A is null");
            }
            if (rel.B == null)
            {
                throw new ArgumentNullException("rel", "rel.B is null");
            }

            e.Result = ((rel.Storage == StorageType.MergeIntoA && RelationEndRole.A == endRole && rel.A.HasPersistentOrder)
                || (rel.Storage == StorageType.MergeIntoB && RelationEndRole.B == endRole && rel.B.HasPersistentOrder)
                || (rel.Storage == StorageType.Replicate
                    && (
                        (rel.A.HasPersistentOrder && RelationEndRole.A == endRole)
                        || (rel.B.HasPersistentOrder && RelationEndRole.B == endRole))
                    )
                || (rel.Storage == StorageType.Separate && (rel.A.HasPersistentOrder || rel.B.HasPersistentOrder))
                );
        }

        [Invocation]
        public static void GetEntryInterfaceType(Relation rel, MethodReturnEventArgs<InterfaceType> e)
        {
            e.Result = rel.Context.GetInterfaceType(String.Format("{0}.{1}_{2}_{3}_RelationEntry", rel.Module.Namespace, rel.A.Type.Name, rel.Verb, rel.B.Type.Name));
        }

        [Invocation]
        public static void SwapRelationEnds(Kistl.App.Base.Relation obj)
        {
            var tmp = obj.A;
            obj.A = obj.B;
            obj.B = tmp;

            switch(obj.Containment)
            {
                case ContainmentSpecification.AContainsB:
                    obj.Containment = ContainmentSpecification.BContainsA;
                    break;
                case ContainmentSpecification.BContainsA:
                    obj.Containment = ContainmentSpecification.AContainsB;
                    break;
            }

            switch(obj.Storage)
            {
                case StorageType.MergeIntoA:
                    obj.Storage = StorageType.MergeIntoB;
                    break;
                case StorageType.MergeIntoB:
                    obj.Storage = StorageType.MergeIntoA;
                    break;
            }
        }
    }
}
