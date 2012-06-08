
namespace Kistl.DalProvider.NHibernate.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.Generator.Extensions;
    using Templates = Kistl.Generator.Templates;

    public partial class ValueCollectionProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Serialization.SerializationMembersList serializationList,
            CompoundObjectProperty prop)
        {
            if (prop == null)
                throw new ArgumentNullException("prop");
            
            // CompoundObjects cannot be compared, therefore we have to avoid sorting the list here
            // although it would be required to keep the exported collection stable
            // TODO: implement a comparer for COs using a DefaultSortOrder property(-list) to re-enable this
            Call(host, ctx, serializationList, prop, prop.HasPersistentOrder, prop.IsList, false);
        }

        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Serialization.SerializationMembersList serializationList,
            ValueTypeProperty prop)
        {
            if (prop == null)
                throw new ArgumentNullException("prop");
            Call(host, ctx, serializationList, prop, prop.HasPersistentOrder, prop.IsList, !prop.HasPersistentOrder);
        }

        private static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Serialization.SerializationMembersList serializationList,
            Property prop, bool hasPersistentOrder, bool isList, bool orderByValue)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            if (!isList) { throw new ArgumentOutOfRangeException("prop", "prop must be a List-valued property"); }

            string name = prop.Name;
            string backingName = "_" + name;
            string exposedCollectionInterface = hasPersistentOrder ? "IList" : "ICollection";

            string thisInterface = prop.ObjectClass.Name;
            string referencedType = prop.GetElementTypeString();
            string referencedCollectionEntry = prop.GetCollectionEntryFullName();
            string referencedCollectionEntryImpl = referencedCollectionEntry + host.Settings["extrasuffix"] + Kistl.API.Helper.ImplementationSuffix;
            string referencedCollectionEntryProxy = referencedCollectionEntryImpl + "." + prop.GetCollectionEntryClassName() + "Proxy";

            string providerCollectionType = "ICollection<" + referencedCollectionEntryImpl + ">";
            string underlyingCollectionName = name + "Collection";
            string underlyingCollectionBackingName = backingName + "Collection";
            string moduleNamespace = prop.Module.Namespace;

            string backingCollectionType = (hasPersistentOrder ? "ClientValueListWrapper" : "ClientValueCollectionWrapper")
                + String.Format("<{0}, {1}, {2}, {3}, {4}>",
                    thisInterface,
                    referencedType,
                    referencedCollectionEntry,
                    referencedCollectionEntryImpl,
                    providerCollectionType);

            Call(
                host, ctx, serializationList,
                name, backingName, backingCollectionType, exposedCollectionInterface,
                thisInterface, referencedType, referencedCollectionEntry, referencedCollectionEntryImpl, referencedCollectionEntryProxy,
                providerCollectionType, underlyingCollectionName, underlyingCollectionBackingName,
                orderByValue, moduleNamespace);
        }

        protected virtual void AddSerialization(Templates.Serialization.SerializationMembersList list, string underlyingCollectionName)
        {
            // TODO: XML Namespace
            if (list != null)
            {
                Templates.Serialization.CollectionSerialization.Add(list, ctx, moduleNamespace, name, underlyingCollectionName, orderByValue, true);
            }
        }
    }
}
