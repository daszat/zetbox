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
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    [Implementor]
    public static class ObjectReferencePropertyActions
    {
        [Invocation]
        public static async System.Threading.Tasks.Task GetPropertyType(ObjectReferenceProperty obj, MethodReturnEventArgs<Type> e)
        {
            var def = await obj.GetReferencedObjectClass();
            e.Result = Type.GetType(def.Module.Namespace + "." + def.Name + ", " + Zetbox.API.Helper.InterfaceAssembly, true);
            PropertyActions.DecorateParameterType(obj, e, false, await obj.GetIsList(), (await obj.RelationEnd.Parent.GetOtherEnd(obj.RelationEnd)).HasPersistentOrder);
        }

        [Invocation]
        public static async System.Threading.Tasks.Task GetElementTypeString(ObjectReferenceProperty obj, MethodReturnEventArgs<string> e)
        {
            var def = await obj.GetReferencedObjectClass();
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
            await PropertyActions.DecorateElementType(obj, e, false);
        }

        [Invocation]
        public static async System.Threading.Tasks.Task GetPropertyTypeString(ObjectReferenceProperty obj, MethodReturnEventArgs<string> e)
        {
            await GetElementTypeString(obj, e);
            if (obj.RelationEnd != null && obj.RelationEnd.Parent != null)
            {
                PropertyActions.DecorateParameterType(obj, e, false, await obj.GetIsList(), (await obj.RelationEnd.Parent.GetOtherEnd(obj.RelationEnd)).HasPersistentOrder);
            }
        }

        [Invocation]
        public static async System.Threading.Tasks.Task GetIsList(ObjectReferenceProperty prop, MethodReturnEventArgs<bool> e)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            RelationEnd relEnd = prop.RelationEnd;
            Relation rel = relEnd.GetParent();
            if (rel != null)
            {
                RelationEnd otherEnd = await rel.GetOtherEnd(relEnd);
                e.Result = otherEnd.Multiplicity.UpperBound() > 1;
            }
            else
            {
                e.Result = false;
            }
        }

        [Invocation]
        public static System.Threading.Tasks.Task ToString(ObjectReferenceProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "-> " + e.Result;

            // already handled by base OnToString_Property()
            // ToStringHelper.FixupFloatingObjects(obj, e);

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
