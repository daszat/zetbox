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

namespace Zetbox.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.Generator.Extensions;

    public partial class ValueCollectionProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IZetboxContext ctx,
            Serialization.SerializationMembersList serializationList,
            CompoundObjectProperty prop,
            string collectionWrapperClass,
            string listWrapperClass)
        {
            if (prop == null)
                throw new ArgumentNullException("prop");
            Call(host, ctx, serializationList, prop, prop.HasPersistentOrder, prop.IsList, collectionWrapperClass, listWrapperClass);
        }

        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IZetboxContext ctx,
            Serialization.SerializationMembersList serializationList,
            ValueTypeProperty prop,
            string collectionWrapperClass,
            string listWrapperClass)
        {
            if (prop == null)
                throw new ArgumentNullException("prop");
            Call(host, ctx, serializationList, prop, prop.HasPersistentOrder, prop.IsList, collectionWrapperClass, listWrapperClass);
        }

        private static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IZetboxContext ctx,
            Serialization.SerializationMembersList serializationList,
            Property prop, bool hasPersistentOrder, bool isList,
            string collectionWrapperClass,
            string listWrapperClass)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            if (!isList) { throw new ArgumentOutOfRangeException("prop", "prop must be a List-valued property"); }

            string name = prop.Name;
            string backingName = "_" + name;
            string backingCollectionType = (hasPersistentOrder ? listWrapperClass : collectionWrapperClass);

            string exposedCollectionInterface = hasPersistentOrder ? "IList" : "ICollection";

            string thisInterface = prop.ObjectClass.Name;
            string referencedType = prop.GetElementTypeString();
            string referencedCollectionEntry = prop.GetCollectionEntryFullName();
            string referencedCollectionEntryImpl = referencedCollectionEntry + host.Settings["extrasuffix"] + Zetbox.API.Helper.ImplementationSuffix;

            string providerCollectionType = "ObservableCollection<" + referencedCollectionEntryImpl + ">";
            string underlyingCollectionName = "_" + name + "Collection";

            string moduleNamespace = prop.Module.Namespace;

            Call(
                host, ctx, serializationList,
                name, backingName, backingCollectionType, exposedCollectionInterface,
                thisInterface, referencedType,
                referencedCollectionEntry, referencedCollectionEntryImpl,
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
        ///// <param name="thisInterface">which Zetbox interface this is</param>
        ///// <param name="referencedType">which type this list contains</param>
        ///// <param name="entryType">collection entries in this list</param>
        ///// <param name="providerCollectionType">the provider type of this collection</param>
        ///// <param name="underlyingCollectionName">how the underlying collection is called</param>
        ///// <param name="orderByValue">true if the collection sould be ordered during export</param>
        ///// <param name="moduleNamespace">the xml namespace of the module</param>
        //public static void Call(
        //    Arebis.CodeGeneration.IGenerationHost host,
        //    IZetboxContext ctx,
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
