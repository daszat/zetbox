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

namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.Generator.Extensions;
    using Templates = Zetbox.Generator.Templates;

    public partial class ValueCollectionProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IZetboxContext ctx,
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
            IZetboxContext ctx,
            Templates.Serialization.SerializationMembersList serializationList,
            ValueTypeProperty prop)
        {
            if (prop == null)
                throw new ArgumentNullException("prop");
            Call(host, ctx, serializationList, prop, prop.HasPersistentOrder, prop.IsList, !prop.HasPersistentOrder);
        }

        private static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IZetboxContext ctx,
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
            string referencedCollectionEntryImpl = referencedCollectionEntry + host.Settings["extrasuffix"] + Zetbox.API.Helper.ImplementationSuffix;
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
                Templates.Serialization.CollectionSerialization.Add(list, ctx, moduleNamespace, name, underlyingCollectionName, orderByValue);
            }
        }
    }
}
