using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Generators.Templates.Implementation;

namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    public partial class ValueCollectionProperty
    {

        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Implementation.SerializationMembersList serializationList,
            CompoundObjectProperty prop)
        {
            Call(host, ctx, serializationList, prop, prop.HasPersistentOrder, prop.IsList);
        }

        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Implementation.SerializationMembersList serializationList,
            ValueTypeProperty prop)
        {
            Call(host, ctx, serializationList, prop, prop.HasPersistentOrder, prop.IsList);
        }

        private static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Implementation.SerializationMembersList serializationList,
            Property prop, bool hasPersistentOrder, bool isList)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            if (!isList) { throw new ArgumentOutOfRangeException("prop", "prop must be a List-valued property"); }

            string name = prop.PropertyName;
            string backingName = "_" + name + "Wrapper";
            string backingCollectionType = (hasPersistentOrder ? "ClientValueListWrapper" : "ClientValueCollectionWrapper");

            string exposedCollectionInterface = hasPersistentOrder ? "IList" : "ICollection";

            string thisInterface = prop.ObjectClass.ClassName;
            string referencedType = prop.ReferencedTypeAsCSharp();
            string referencedCollectionEntry = prop.GetCollectionEntryClassName() + Kistl.API.Helper.ImplementationSuffix;

            string providerCollectionType = "IList<" + referencedCollectionEntry + ">";
            string underlyingCollectionName = "_" + name;

            Call(
                host, ctx, serializationList,
                name, backingName, backingCollectionType, exposedCollectionInterface,
                thisInterface, referencedType, referencedCollectionEntry,
                providerCollectionType, underlyingCollectionName, !hasPersistentOrder);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="ctx"></param>
        /// <param name="serializationList"></param>
        /// <param name="name">the name of the property to create</param>
        /// <param name="backingName">the name of the private backing store for the conversion wrapper list</param>
        /// <param name="backingCollectionType">the name of the wrapper class for wrapping the CollectionEntries</param>
        /// <param name="exposedCollectionInterface">which generic interface to use for the collection</param>
        /// <param name="thisInterface">which Kistl interface this is</param>
        /// <param name="referencedType">which type this list contains</param>
        /// <param name="entryType">collection entries in this list</param>
        /// <param name="providerCollectionType">the provider type of this collection</param>
        /// <param name="underlyingCollectionName">how the underlying collection is called</param>
        /// <param name="orderByB">true if the collection sould be ordered during export</param>
        public static void Call(
            Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Implementation.SerializationMembersList serializationList,
            string name, string backingName, string backingCollectionType, string exposedCollectionInterface,
            string thisInterface, string referencedType, string entryType,
            string providerCollectionType, string underlyingCollectionName, bool orderByB)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            host.CallTemplate("Implementation.ObjectClasses.ValueCollectionProperty",
                ctx, serializationList,
                name, backingName, backingCollectionType, exposedCollectionInterface,
                thisInterface, referencedType, entryType, providerCollectionType, underlyingCollectionName, orderByB);
        }

        protected virtual void AddSerialization(Templates.Implementation.SerializationMembersList list, string underlyingCollectionName)
        {
            // TODO: XML Namespace
            if (list != null)
            {
                list.Add("Implementation.ObjectClasses.CollectionSerialization", SerializerType.All, "http://dasz.at/Kistl", name, underlyingCollectionName, orderByB);
            }
        }
    }
}
