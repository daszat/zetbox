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
    using Zetbox.App.GUI;

    [Implementor]
    public static class RelationEndActions
    {
        [Invocation]
        public static System.Threading.Tasks.Task ToString(RelationEnd obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("RelationEnd {0}({1})",
                obj.RoleName,
                obj.Type == null
                    ? "no type"
                    : obj.Type.Name);

            ToStringHelper.FixupFloatingObjectsToString(obj, e);

            return System.Threading.Tasks.Task.CompletedTask;
        }
        [Invocation]
        public static System.Threading.Tasks.Task get_Parent(RelationEnd relEnd, PropertyGetterEventArgs<Relation> e)
        {
            e.Result = relEnd.AParent ?? relEnd.BParent;

            return System.Threading.Tasks.Task.CompletedTask;
        }

        // TODO: Replace this when NamedInstances are introduced 
        public static readonly Guid ViewModelDescriptor_ObjectReferenceModel = new Guid("83aae6fd-0fae-4348-b313-737a6e751027");
        public static readonly Guid ViewModelDescriptor_ObjectListModel = new Guid("9fce01fe-fd6d-4e21-8b55-08d5e38aea36");
        public static readonly Guid ViewModelDescriptor_ObjectCollectionModel = new Guid("67A49C49-B890-4D35-A8DB-1F8E43BFC7DF");

        [Invocation]
        public static System.Threading.Tasks.Task CreateNavigator(RelationEnd obj, MethodReturnEventArgs<ObjectReferenceProperty> e)
        {
            Relation rel = obj.AParent ?? obj.BParent;
            RelationEnd other = rel != null ? rel.GetOtherEnd(obj) : null;

            var nav = obj.Context.Create<ObjectReferenceProperty>();
            nav.CategoryTags = String.Empty;
            nav.ObjectClass = obj.Type;
            nav.RelationEnd = obj;
            nav.Module = rel != null ? rel.Module : null;

            if (other != null)
            {
                if (nav.GetIsList())
                {
                    if (nav.RelationEnd.Parent.GetOtherEnd(nav.RelationEnd).HasPersistentOrder)
                    {
                        nav.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(ViewModelDescriptor_ObjectListModel);
                    }
                    else
                    {
                        nav.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(ViewModelDescriptor_ObjectCollectionModel);
                    }
                }
                else
                {
                    nav.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(ViewModelDescriptor_ObjectReferenceModel);
                }

                nav.Name = other.RoleName;
            }

            e.Result = nav;

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task isValid_Navigator(RelationEnd obj, PropertyIsValidEventArgs e)
        {
            var relEnd = obj;
            var rel = relEnd.GetParent();
            if (rel == null)
            {
                e.IsValid = false;
                e.Error = "No Relation assigned to Relation end";
                return System.Threading.Tasks.Task.CompletedTask;
            }
            var otherEnd = rel.GetOtherEnd(relEnd);
            var orp = obj.Navigator;

            e.IsValid = true;

            if (orp != null)
            {
                if (orp.ObjectClass == null)
                {
                    e.IsValid = false;
                    e.Error = String.Format("Navigator should be attached to {0}", relEnd.Type);
                }
                if (orp.ObjectClass != relEnd.Type)
                {
                    e.IsValid = false;
                    e.Error = String.Format("Navigator is attached to {0} but should be attached to {1}",
                                orp.ObjectClass,
                                relEnd.Type);
                }

                switch (otherEnd.Multiplicity)
                {
                    case Multiplicity.One:
                        if(orp.Constraints.OfType<NotNullableConstraint>().Count() == 0)
                        {
                            e.IsValid = false;
                            e.Error = "Navigator should have NotNullableConstraint because Multiplicity of opposite RelationEnd is One";
                        }
                        break;
                    case Multiplicity.ZeroOrMore:
                        if(orp.Constraints.OfType<NotNullableConstraint>().Count() > 0)
                        {
                            e.IsValid = false;
                            e.Error = "Navigator should not have NotNullableConstraint because Multiplicity of opposite RelationEnd is ZeroOrMore";
                        }
                        break;
                    case Multiplicity.ZeroOrOne:
                        if(orp.Constraints.OfType<NotNullableConstraint>().Count() > 0)
                        {
                            e.IsValid = false;
                            e.Error = "Navigator should not have NotNullableConstraint because Multiplicity of opposite RelationEnd is ZeroOrOne";
                        }
                        break;
                }
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task isValid_HasPersistentOrder(RelationEnd obj, PropertyIsValidEventArgs e)
        {
            if (obj.HasPersistentOrder && obj.Multiplicity != Multiplicity.ZeroOrMore)
            {
                e.IsValid = false;
                e.Error = String.Format("Can only require persistent order when multiplicity is ZeroOrMore, but multiplicity is {0}", obj.Multiplicity);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
