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

namespace Zetbox.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator.Extensions;

    public partial class ObjectListProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IZetboxContext ctx,
            Serialization.SerializationMembersList serializationList,
            ObjectReferenceProperty prop)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (prop == null) { throw new ArgumentNullException("prop"); }
            if (!prop.IsList()) { throw new ArgumentOutOfRangeException("prop", "prop must be a List-valued property"); }

            string name = prop.Name;
            string wrapperClass = "OneNRelationList";
            var rel = RelationExtensions.Lookup(ctx, prop);
            var relEnd = rel.GetEnd(prop);
            var otherEnd = rel.GetOtherEnd(relEnd);
            var exposedListType = rel.NeedsPositionStorage(otherEnd.GetRole()) ? "IList" : "ICollection";
            // the name of the position property
            var positionPropertyName = rel.NeedsPositionStorage(otherEnd.GetRole()) ? Construct.ListPositionPropertyName(otherEnd) : String.Empty;

            Call(host, ctx, serializationList, name, wrapperClass, exposedListType, rel, relEnd.GetRole(), positionPropertyName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="ctx"></param>
        /// <param name="serializationList"></param>
        /// <param name="name">the name of the property to create</param>
        /// <param name="wrapperClass">the name of the wrapper class for wrapping the EntityCollection</param>
        /// <param name="exposedListType">which generic interface to use for the collection (IList or ICollection)</param>
        /// <param name="rel"></param>
        /// <param name="endRole"></param>
        /// <param name="positionPropertyName">the name of the position property for this list</param>
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IZetboxContext ctx,
            Serialization.SerializationMembersList serializationList,
            string name,
            string wrapperClass,
            string exposedListType,
            Relation rel,
            RelationEndRole endRole,
            string positionPropertyName)
        {
            if (rel == null) { throw new ArgumentNullException("rel"); }

            RelationEnd relEnd = rel.GetEndFromRole(endRole);
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

            string wrapperName = "_" + name;

            var otherName = otherEnd.Navigator == null ? relEnd.RoleName : otherEnd.Navigator.Name;

            string referencedInterface = otherEnd.Type.GetDataTypeString();

            Call(host, ctx, serializationList, name, wrapperName, wrapperClass, exposedListType, rel, endRole, positionPropertyName, otherName, referencedInterface);
        }

        public ObjectListProperty(Arebis.CodeGeneration.IGenerationHost host,
            IZetboxContext ctx,
            Serialization.SerializationMembersList serializationList,
            Relation rel,
            RelationEndRole endRole)
            : this(host, ctx, serializationList, "name", "wrapperName", "wrapperClass", "exposedListType",
                rel, endRole, "positionPropertyName", "otherName", "referencedInterface")
        {
        }


        protected virtual void AddSerialization(Serialization.SerializationMembersList list, string memberName, bool eagerLoading)
        {
            if (list != null && eagerLoading)
            {
                list.Add("Serialization.EagerLoadingSerialization", Serialization.SerializerType.Binary, null, null, memberName, true, rel.GetRelationType() == RelationType.n_m);
            }
        }
    }
}
