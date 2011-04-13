
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
            Call(host, ctx, serializationList, prop, prop.HasPersistentOrder, prop.IsList);
        }

        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Serialization.SerializationMembersList serializationList,
            ValueTypeProperty prop)
        {
            if (prop == null)
                throw new ArgumentNullException("prop");
            Call(host, ctx, serializationList, prop, prop.HasPersistentOrder, prop.IsList);
        }

        private static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Serialization.SerializationMembersList serializationList,
            Property prop, bool hasPersistentOrder, bool isList)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            if (!isList) { throw new ArgumentOutOfRangeException("prop", "prop must be a List-valued property"); }

            string name = prop.Name;
            string backingName = "_" + name;
            string exposedCollectionInterface = hasPersistentOrder ? "IList" : "ICollection";

            string thisInterface = prop.ObjectClass.Name;
            string referencedType = prop.ReferencedTypeAsCSharp();
            string referencedCollectionEntry = prop.GetCollectionEntryClassName() + host.Settings["extrasuffix"] + Kistl.API.Helper.ImplementationSuffix;
            string referencedCollectionEntryProxy = prop.GetCollectionEntryClassName() + host.Settings["extrasuffix"] + Kistl.API.Helper.ImplementationSuffix + "." + prop.GetCollectionEntryClassName() + "Proxy";

            string providerCollectionType = string.Format("ProjectedList<{0}, {1}>", referencedCollectionEntryProxy, referencedCollectionEntry);
            string underlyingCollectionName = name + "Collection";
            string underlyingCollectionBackingName = backingName + "Collection";
            string moduleNamespace = prop.Module.Namespace;

            string backingCollectionType = (hasPersistentOrder ? "ClientValueListWrapper" : "ClientValueCollectionWrapper")
                + String.Format("<{0}, {1}, {2}, {3}>",
                    thisInterface,
                    referencedType,
                    referencedCollectionEntry,
                    providerCollectionType);

            Call(
                host, ctx, serializationList,
                name, backingName, backingCollectionType, exposedCollectionInterface,
                thisInterface, referencedType, referencedCollectionEntry, referencedCollectionEntryProxy,
                providerCollectionType, underlyingCollectionName, underlyingCollectionBackingName,
                !hasPersistentOrder, moduleNamespace);
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
