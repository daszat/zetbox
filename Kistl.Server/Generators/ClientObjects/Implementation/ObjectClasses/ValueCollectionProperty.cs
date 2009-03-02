using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    public partial class ValueCollectionProperty
    {

        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Implementation.SerializationMembersList serializationList,
            ValueTypeProperty prop)
        {
            Debug.Assert(prop.IsList);


            string name = prop.PropertyName;
            string backingName = "_" + name + "Wrapper";
            string backingCollectionType = (prop.IsIndexed ? "ClientListBSideWrapper" : "ClientCollectionBSideWrapper");

            string exposedCollectionInterface = prop.IsIndexed ? "IList" : "ICollection";

            string thisInterface = prop.ObjectClass.ClassName;
            string referencedType = prop.ReferencedTypeAsCSharp();
            string referencedCollectionEntry = prop.GetCollectionEntryClassName() + Kistl.API.Helper.ImplementationSuffix;

            string providerCollectionType = "ICollection<" + referencedCollectionEntry + ">";
            string underlyingCollectionName = "_" + name;

            Call(
                host, ctx, serializationList,
                name, backingName, backingCollectionType, exposedCollectionInterface,
                thisInterface, referencedType, referencedCollectionEntry,
                providerCollectionType, underlyingCollectionName);
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
        /// <param name="exposedListType">which generic interface to use for the collection</param>
        /// <param name="thisInterface">which Kistl interface this is</param>
        /// <param name="referencedType">which type this list contains</param>
        /// <param name="referencedCollectionEntry">collection entries in this list</param>
        public static void Call(
            Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Implementation.SerializationMembersList serializationList,
            string name, string backingName, string backingCollectionType, string exposedCollectionInterface,
            string thisInterface, string referencedType, string entryType,
            string providerCollectionType, string underlyingCollectionName)
        {
            host.CallTemplate("Implementation.ObjectClasses.ValueCollectionProperty",
                ctx, serializationList,
                name, backingName, backingCollectionType, exposedCollectionInterface,
                thisInterface, referencedType, entryType, providerCollectionType, underlyingCollectionName);
        }

        protected virtual void AddSerialization(Templates.Implementation.SerializationMembersList list, string underlyingCollectionName)
        {
            if (list != null)
            {
                list.Add("Implementation.ObjectClasses.CollectionSerialization", underlyingCollectionName);
            }
        }
    }
}
