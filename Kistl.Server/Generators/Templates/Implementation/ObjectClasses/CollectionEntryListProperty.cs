using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;

namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    public partial class CollectionEntryListProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Implementation.SerializationMembersList serializationList,
            RelationEnd relEnd)
        {
            NewRelation rel = relEnd.Container;

            string name = relEnd.Navigator.PropertyName;
            string exposedCollectionInterface = relEnd.Other.HasPersistentOrder ? "IList" : "ICollection";
            string referencedInterface = relEnd.Other.Type.NameDataObject;
            string backingName = "_" + name;
            string backingCollectionType = "Client" + (relEnd.Other.HasPersistentOrder ? "List" : "Collection") + relEnd.Other.Role + "SideWrapper";
            string aSideType = rel.A.Type.NameDataObject;
            string bSideType = rel.B.Type.NameDataObject;
            string entryType = rel.GetCollectionEntryClassName() + Kistl.API.Helper.ImplementationSuffix;
            string providerCollectionType = "ICollection<" + entryType + ">";
            string relationName = rel.GetCollectionEntryClassName();

            host.CallTemplate("Implementation.ObjectClasses.CollectionEntryListProperty",
                ctx, serializationList,
                name, exposedCollectionInterface, referencedInterface,
                backingName, backingCollectionType, aSideType, bSideType, entryType,
                providerCollectionType,
                relationName);
        }
    }
}
