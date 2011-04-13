
namespace Kistl.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.Generator.Extensions;

    public partial class ValueCollectionProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Serialization.SerializationMembersList serializationList,
            CompoundObjectProperty prop)
        {
            if (prop == null)
                throw new ArgumentNullException("prop");
            Call(host, ctx, serializationList, prop, prop.HasPersistentOrder, prop.IsList);
        }

        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Serialization.SerializationMembersList serializationList,
            ValueTypeProperty prop)
        {
            if (prop == null)
                throw new ArgumentNullException("prop");
            Call(host, ctx, serializationList, prop, prop.HasPersistentOrder, prop.IsList);
        }

        private static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Serialization.SerializationMembersList serializationList,
            Property prop, bool hasPersistentOrder, bool isList)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            if (!isList) { throw new ArgumentOutOfRangeException("prop", "prop must be a List-valued property"); }

            string name = prop.Name;
            string backingName = "_" + name;
            string backingCollectionType = (hasPersistentOrder ? "ClientValueListWrapper" : "ClientValueCollectionWrapper");

            string exposedCollectionInterface = hasPersistentOrder ? "IList" : "ICollection";

            string thisInterface = prop.ObjectClass.Name;
            string referencedType = prop.ReferencedTypeAsCSharp();
            string referencedCollectionEntry = prop.GetCollectionEntryClassName() + host.Settings["extrasuffix"] + Kistl.API.Helper.ImplementationSuffix;

            string providerCollectionType = "ObservableCollection<" + referencedCollectionEntry + ">";
            string underlyingCollectionName = "_" + name + "Collection";

            string moduleNamespace = prop.Module.Namespace;

            Call(
                host, ctx, serializationList,
                name, backingName, backingCollectionType, exposedCollectionInterface,
                thisInterface, referencedType, referencedCollectionEntry,
                providerCollectionType, underlyingCollectionName, !hasPersistentOrder,
                moduleNamespace);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="host"></param>
        ///// <param name="ctx"></param>
        ///// <param name="serializationList"></param>
        ///// <param name="name">the name of the property to create</param>
        ///// <param name="backingName">the name of the private backing store for the conversion wrapper list</param>
        ///// <param name="backingCollectionType">the name of the wrapper class for wrapping the CollectionEntries</param>
        ///// <param name="exposedCollectionInterface">which generic interface to use for the collection</param>
        ///// <param name="thisInterface">which Kistl interface this is</param>
        ///// <param name="referencedType">which type this list contains</param>
        ///// <param name="entryType">collection entries in this list</param>
        ///// <param name="providerCollectionType">the provider type of this collection</param>
        ///// <param name="underlyingCollectionName">how the underlying collection is called</param>
        ///// <param name="orderByValue">true if the collection sould be ordered during export</param>
        ///// <param name="moduleNamespace">the xml namespace of the module</param>
        //public static void Call(
        //    Arebis.CodeGeneration.IGenerationHost host,
        //    IKistlContext ctx,
        //    Serialization.SerializationMembersList serializationList,
        //    string name, string backingName, string backingCollectionType, string exposedCollectionInterface,
        //    string thisInterface, string referencedType, string entryType,
        //    string providerCollectionType, string underlyingCollectionName, bool orderByValue, string moduleNamespace)
        //{
        //    if (host == null) { throw new ArgumentNullException("host"); }
        //    if (ctx == null) { throw new ArgumentNullException("ctx"); }

        //    host.CallTemplate("Properties.ValueCollectionProperty",
        //        ctx, serializationList,
        //        name, backingName, backingCollectionType, exposedCollectionInterface,
        //        thisInterface, referencedType, entryType, providerCollectionType, underlyingCollectionName, orderByValue, moduleNamespace);
        //}

        protected virtual void AddSerialization(Serialization.SerializationMembersList list, string underlyingCollectionName)
        {
            // TODO: XML Namespace
            if (list != null)
            {
                Serialization.CollectionSerialization.Add(list, ctx, moduleNamespace, name, underlyingCollectionName, orderByValue, true);
            }
        }
    }
}
