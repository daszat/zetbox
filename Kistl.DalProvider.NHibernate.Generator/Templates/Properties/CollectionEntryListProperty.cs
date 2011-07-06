
namespace Kistl.DalProvider.NHibernate.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Generator.Extensions;
    using Templates = Kistl.Generator.Templates;

    public partial class CollectionEntryListProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Serialization.SerializationMembersList serializationList,
            Relation rel, RelationEndRole endRole)
        {
            if (rel == null) { throw new ArgumentNullException("rel"); }

            RelationEnd relEnd = rel.GetEndFromRole(endRole);
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

            string backingCollectionType = RelationToBackingCollectionType(rel, otherEnd);

            Call(host, ctx, serializationList, rel, endRole, backingCollectionType);
        }

        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Serialization.SerializationMembersList serializationList,
            Relation rel, RelationEndRole endRole, string backingCollectionType)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (rel == null) { throw new ArgumentNullException("rel"); }


            RelationEnd relEnd = rel.GetEndFromRole(endRole);
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

            string name = relEnd.Navigator.Name;
            string exposedCollectionInterface = rel.NeedsPositionStorage(otherEnd.GetRole()) ? "IList" : "ICollection";
            string referencedInterface = otherEnd.Type.GetDataTypeString();
            string backingName = "_" + name;

            string aSideType = rel.A.Type.GetDataTypeString();
            string bSideType = rel.B.Type.GetDataTypeString();
            string entryType = rel.GetRelationClassName() + host.Settings["extrasuffix"] + Kistl.API.Helper.ImplementationSuffix;
            string providerCollectionType = (rel.NeedsPositionStorage(otherEnd.GetRole()) ? "IList<" : "ICollection<")
                + entryType + ">";

            bool eagerLoading = relEnd.Navigator != null && relEnd.Navigator.EagerLoading;
            bool serializeRelationEntries = rel.GetRelationType() == RelationType.n_m;

            string entryProxyType = entryType + "." + rel.GetRelationClassName() + "Proxy";

            string inverseNavigatorName = otherEnd.Navigator != null ? otherEnd.Navigator.Name : null;

            Call(host, ctx, serializationList, name, exposedCollectionInterface, referencedInterface, backingName, backingCollectionType, aSideType, bSideType, entryType, providerCollectionType, rel.ExportGuid, endRole, eagerLoading, serializeRelationEntries, entryProxyType, inverseNavigatorName);
        }

        public static string RelationToBackingCollectionType(Relation rel, RelationEnd otherEnd)
        {
            if (rel == null) { throw new ArgumentNullException("rel"); }
            if (otherEnd == null) { throw new ArgumentNullException("otherEnd"); }

            string result;

            if (rel.NeedsPositionStorage(otherEnd.GetRole()))
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
