// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.API.Utils;

    [Implementor]
    public static class RelationActions
    {
        [Invocation]
        public static void ToString(Relation obj, MethodReturnEventArgs<string> e)
        {
            if (obj.A == null ||
                obj.B == null ||
                obj.A.Type == null ||
                obj.B.Type == null)
            {
                e.Result = "incomplete relation:";
                if (obj.A == null)
                {
                    e.Result += " A missing";
                }
                else
                {
                    e.Result += " A.Type missing";
                }

                if (obj.B == null)
                {
                    e.Result += " B missing";
                }
                else
                {
                    e.Result += " B.Type missing";
                }
            }
            else
            {
                string aDesc = (obj.A.RoleName ?? String.Empty).Equals(obj.A.Type.Name)
                    ? obj.A.RoleName
                    : String.Format("{0}({1})", obj.A.RoleName, obj.A.Type.Name);

                string bDesc = (obj.B.RoleName ?? String.Empty).Equals(obj.B.Type.Name)
                    ? obj.B.RoleName
                    : String.Format("{0}({1})", obj.B.RoleName, obj.B.Type.Name);

                e.Result = String.Format("Relation: {0} {1} {2}",
                    aDesc,
                    obj.Verb,
                    bDesc);
            }

            ToStringHelper.FixupFloatingObjectsToString(obj, e);
        }
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
        public static void SwapRelationEnds(Zetbox.App.Base.Relation obj)
        {
            var tmp = obj.A;
            obj.A = obj.B;
            obj.B = tmp;

            switch (obj.Containment)
            {
                case ContainmentSpecification.AContainsB:
                    obj.Containment = ContainmentSpecification.BContainsA;
                    break;
                case ContainmentSpecification.BContainsA:
                    obj.Containment = ContainmentSpecification.AContainsB;
                    break;
            }

            switch (obj.Storage)
            {
                case StorageType.MergeIntoA:
                    obj.Storage = StorageType.MergeIntoB;
                    break;
                case StorageType.MergeIntoB:
                    obj.Storage = StorageType.MergeIntoA;
                    break;
            }
        }

        [Invocation]
        public static void isValid_Containment(Relation obj, PropertyIsValidEventArgs e)
        {
            var rel = obj;
            if (rel.A != null && rel.B != null)
            {
                if (rel.A.Multiplicity == 0 || rel.B.Multiplicity == 0)
                {
                    e.IsValid = false;
                    e.Error = "Incomplete Relation (A.Multiplicity or B.Multiplicity missing)";
                    return;
                }

                var relType = rel.GetRelationType();

                switch (rel.Containment)
                {
                    case ContainmentSpecification.AContainsB:
                        if (relType == RelationType.n_m)
                        {
                            e.IsValid = false;
                            e.Error = "N:M relations cannot be containment relations";
                        }
                        else if (relType == RelationType.one_n)
                        {
                            e.IsValid = rel.B.Multiplicity.UpperBound() > 1;
                            if (!e.IsValid) e.Error = "Can only contain the N-side of a 1:N relationship (which is A).";
                        }
                        else if (relType == RelationType.one_one)
                        {
                            e.IsValid = true;
                        }
                        else
                        {
                            e.IsValid = false;
                            e.Error = String.Format("RelationType.{0} not implemented.", relType);
                        }
                        break;
                    case ContainmentSpecification.BContainsA:
                        if (relType == RelationType.n_m)
                        {
                            e.IsValid = false;
                            e.Error = "N:M relations cannot be containment relations";
                        }
                        else if (relType == RelationType.one_n)
                        {
                            e.IsValid = rel.A.Multiplicity.UpperBound() > 1;
                            if (!e.IsValid) e.Error = "Can only contain the N-side of a 1:N relationship (which is B).";
                        }
                        else if (relType == RelationType.one_one)
                        {
                            e.IsValid = true;
                        }
                        else
                        {
                            e.IsValid = false;
                            e.Error = String.Format("RelationType.{0} not implemented.", relType);
                        }
                        break;
                    case ContainmentSpecification.Independent:
                        e.IsValid = true;
                        break;
                    default:
                        {
                            e.IsValid = false;
                            e.Error = String.Format("ContainmentType.{0} not implemented.", rel.Containment);
                            break;
                        }
                }
            }
            else
            {
                e.IsValid = false;
                e.Error = "Incomplete Relation (A or B missing)";
            }
        }

        [Invocation]
        public static void isValid_Storage(Relation obj, PropertyIsValidEventArgs e)
        {
            var rel = obj;
            if (rel.A != null && rel.B != null)
            {
                if (rel.A.Multiplicity == 0 || rel.B.Multiplicity == 0)
                {
                    e.IsValid = false;
                    e.Error = "Incomplete Relation (Multiplicity is missing)";
                    return;
                }
                switch (rel.Storage)
                {
                    case StorageType.MergeIntoA:
                        e.IsValid = rel.B.Multiplicity.UpperBound() <= 1;
                        if(!e.IsValid) e.Error = "B side could be more than one. Not able to merge foreign key into A";
                        break;
                    case StorageType.MergeIntoB:
                        e.IsValid = rel.A.Multiplicity.UpperBound() <= 1;
                        if(!e.IsValid) e.Error = "A side could be more than one. Not able to merge foreign key into B";
                        break;
                    case StorageType.Separate:
                        e.IsValid = rel.A.Multiplicity.UpperBound() > 1 && rel.B.Multiplicity.UpperBound() > 1;
                        if(!e.IsValid) 
                        {
                            if (rel.A.Multiplicity.UpperBound() <= 1 && rel.B.Multiplicity.UpperBound() <= 1)
                            {
                                e.Error = "A side is only one-ary. Please use MergeIntoA or MergeIntoB";
                            }
                            else if (rel.A.Multiplicity.UpperBound() <= 1)
                            {
                                e.Error ="A side is only one-ary. Please use MergeIntoB";
                            }
                            else if (rel.B.Multiplicity.UpperBound() <= 1)
                            {
                                e.Error ="B side is only one-ary. Please use MergeIntoA";
                            }
                        }
                        break;
                    case StorageType.Replicate:
                    default:
                        e.IsValid = false;
                        e.Error = String.Format("StorageType.{0} not implemented.", rel.Storage);
                        break;
                }
            }
            else
            {
                e.IsValid = false;
                e.Error = "Incomplete Relation (A or B missing)";
            }
        }
    }
}
