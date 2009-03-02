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
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Implementation.SerializationMembersList serializationList,
            Relation rel, RelationEndRole endRole)
        {
            RelationEnd relEnd = rel.GetEnd(endRole);
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

            string name = relEnd.Navigator.PropertyName;
            string exposedCollectionInterface = otherEnd.HasPersistentOrder ? "IList" : "ICollection";
            string referencedInterface = otherEnd.Type.GetDataTypeString();
            string backingName = "_" + name;
            string backingCollectionType = "undefined wrapper class";
            if (otherEnd.HasPersistentOrder)
            {
                if ((RelationEndRole)otherEnd.Role == RelationEndRole.A)
                {
                    backingCollectionType = "ClientListASideWrapper";
                }
                else if ((RelationEndRole)otherEnd.Role == RelationEndRole.B)
                {
                    backingCollectionType = "ClientListBSideWrapper";
                }
            }
            else
            {
                if ((RelationEndRole)otherEnd.Role == RelationEndRole.A)
                {
                    backingCollectionType = "ClientCollectionASideWrapper";
                }
                else if ((RelationEndRole)otherEnd.Role == RelationEndRole.B)
                {
                    backingCollectionType = "ClientCollectionBSideWrapper";
                }
            }

            string aSideType = rel.A.Type.GetDataTypeString();
            string bSideType = rel.B.Type.GetDataTypeString();
            string entryType = rel.GetCollectionEntryClassName() + Kistl.API.Helper.ImplementationSuffix;
            string providerCollectionType = "ICollection<" + entryType + ">";

            host.CallTemplate("Implementation.ObjectClasses.CollectionEntryListProperty",
                ctx, serializationList,
                name, exposedCollectionInterface, referencedInterface,
                backingName, backingCollectionType, aSideType, bSideType, entryType,
                providerCollectionType,
                rel.ID, endRole);
        }
    }
}
