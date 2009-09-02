using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    public partial class CollectionEntryListProperty
    {
        /// <summary>
        /// TODO: Frage: Rollen schon beim Aufruf tauschen? Es wird primär mit otherEnd gearbeitet.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="ctx"></param>
        /// <param name="serializationList"></param>
        /// <param name="rel"></param>
        /// <param name="endRole"></param>
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Implementation.SerializationMembersList serializationList,
            Relation rel, RelationEndRole endRole)
        {
            RelationEnd relEnd = rel.GetEndFromRole(endRole);
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

            string name = relEnd.Navigator.PropertyName;
            string exposedCollectionInterface = rel.NeedsPositionStorage(otherEnd.GetRole()) ? "IList" : "ICollection";
            string referencedInterface = otherEnd.Type.GetDataTypeString();
            string backingName = "_" + name;
            string backingCollectionType = "undefined wrapper class";
            if (rel.NeedsPositionStorage(otherEnd.GetRole()))
            {
                if (otherEnd.GetRole() == RelationEndRole.A)
                {
                    backingCollectionType = "ClientRelationASideListWrapper";
                }
                else if (otherEnd.GetRole() == RelationEndRole.B)
                {
                    backingCollectionType = "ClientRelationBSideListWrapper";
                }
            }
            else
            {
                if (otherEnd.GetRole() == RelationEndRole.A)
                {
                    backingCollectionType = "ClientRelationASideCollectionWrapper";
                }
                else if (otherEnd.GetRole() == RelationEndRole.B)
                {
                    backingCollectionType = "ClientRelationBSideCollectionWrapper";
                }
            }
            
            string aSideType = rel.A.Type.GetDataTypeString();
            string bSideType = rel.B.Type.GetDataTypeString();
            string entryType = rel.GetRelationClassName() + Kistl.API.Helper.ImplementationSuffix;
            string providerCollectionType = (rel.NeedsPositionStorage(otherEnd.GetRole()) ? "IList<" : "ICollection<")
                + entryType + ">";

            bool eagerLoading = (relEnd.Navigator != null && relEnd.Navigator.EagerLoading)
                || (otherEnd.Navigator != null && otherEnd.Navigator.EagerLoading);

            host.CallTemplate("Implementation.ObjectClasses.CollectionEntryListProperty",
                ctx, serializationList,
                name, exposedCollectionInterface, referencedInterface,
                backingName, backingCollectionType, aSideType, bSideType, entryType,
                providerCollectionType,
                rel.ExportGuid, endRole,
                eagerLoading);
        }

        protected virtual void AddSerialization(Templates.Implementation.SerializationMembersList list, string memberName, bool eagerLoading)
        {
            if (list != null && eagerLoading)
            {
                list.Add("Implementation.ObjectClasses.EagerLoadingSerialization", Templates.Implementation.SerializerType.Binary, null, null, memberName, false);
            }
        }
    }
}
