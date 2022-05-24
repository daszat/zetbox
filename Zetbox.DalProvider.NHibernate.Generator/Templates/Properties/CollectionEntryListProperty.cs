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

namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator.Extensions;
    using Templates = Zetbox.Generator.Templates;

    public partial class CollectionEntryListProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IZetboxContext ctx,
            Templates.Serialization.SerializationMembersList serializationList,
            Relation rel, RelationEndRole endRole)
        {
            if (rel == null) { throw new ArgumentNullException("rel"); }

            RelationEnd relEnd = rel.GetEndFromRole(endRole).Result;
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd).Result;

            string backingCollectionType = RelationToBackingCollectionType(rel, otherEnd);

            Call(host, ctx, serializationList, rel, endRole, backingCollectionType);
        }

        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IZetboxContext ctx,
            Templates.Serialization.SerializationMembersList serializationList,
            Relation rel, RelationEndRole endRole, string backingCollectionType)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (rel == null) { throw new ArgumentNullException("rel"); }


            RelationEnd relEnd = rel.GetEndFromRole(endRole).Result;
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd).Result;

            string name = relEnd.Navigator.Name;
            string exposedCollectionInterface = rel.NeedsPositionStorage(otherEnd.GetRole()).Result ? "IList" : "ICollection";
            string referencedInterface = otherEnd.Type.GetDataTypeString().Result;
            string backingName = "_" + name;

            string aSideType = rel.A.Type.GetDataTypeString().Result;
            string bSideType = rel.B.Type.GetDataTypeString().Result;
            string entryType = rel.GetRelationFullName() + host.Settings["extrasuffix"] + Zetbox.API.Helper.ImplementationSuffix;
            string providerCollectionType = (rel.NeedsPositionStorage(otherEnd.GetRole()).Result ? "IList<" : "ICollection<")
                + entryType + ">";

            bool eagerLoading = relEnd.Navigator != null && relEnd.Navigator.EagerLoading;
            bool serializeRelationEntries = rel.GetRelationType().Result == RelationType.n_m;

            string entryProxyType = entryType + "." + rel.GetRelationClassName() + "Proxy";

            string inverseNavigatorName = otherEnd.Navigator != null ? otherEnd.Navigator.Name : null;

            Call(host, ctx, serializationList, name, exposedCollectionInterface, referencedInterface, backingName, backingCollectionType, aSideType, bSideType, entryType, providerCollectionType, rel.ExportGuid, endRole, eagerLoading, serializeRelationEntries, entryProxyType, inverseNavigatorName);
        }

        public static string RelationToBackingCollectionType(Relation rel, RelationEnd otherEnd)
        {
            if (rel == null) { throw new ArgumentNullException("rel"); }
            if (otherEnd == null) { throw new ArgumentNullException("otherEnd"); }

            string result;

            if (rel.NeedsPositionStorage(otherEnd.GetRole()).Result)
            {
                result = String.Format("NHibernate{0}SideListWrapper", otherEnd.GetRole());
            }
            else
            {
                result = String.Format("NHibernate{0}SideCollectionWrapper", otherEnd.GetRole());
            }

            return result;
        }

        protected virtual void AddSerialization(Templates.Serialization.SerializationMembersList list, string memberName, bool eagerLoading)
        {
            if (list != null && eagerLoading)
            {
                list.Add("Serialization.EagerLoadingSerialization", Templates.Serialization.SerializerType.Binary, null, null, memberName, false, serializeRelationEntries, "this.Proxy." + memberName);
            }
        }
    }
}
