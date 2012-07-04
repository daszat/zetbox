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
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    public partial class CompoundObjectPropertyTemplate
    {
        public static void Call(
            Arebis.CodeGeneration.IGenerationHost host, IZetboxContext ctx,
            Serialization.SerializationMembersList serializationList,
            CompoundObjectProperty prop)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (prop == null) { throw new ArgumentNullException("prop"); }

            string propName = prop.Name;

            Call(host, ctx, serializationList, prop, propName, prop.IsList, prop.HasPersistentOrder);
        }

        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IZetboxContext ctx, Serialization.SerializationMembersList serializationList, CompoundObjectProperty prop, string overridePropName, bool isList, bool hasPersistentOrder)
        {
            string xmlNamespace = prop.Module.Namespace;
            string backingPropertyName = overridePropName + Zetbox.API.Helper.ImplementationSuffix;
            string backingStoreName = "_" + overridePropName;

            string coType = prop.GetElementTypeString();
            string coImplementationType = coType + host.Settings["extrasuffix"] + Zetbox.API.Helper.ImplementationSuffix;

            if (isList && hasPersistentOrder)
            {
                coType = string.Format("IList<{0}>", coType);
                coImplementationType = string.Format("IList<{0}>", coImplementationType);
            }
            else if (isList && !hasPersistentOrder)
            {
                coType = string.Format("ICollection<{0}>", coType);
                coImplementationType = string.Format("ICollection<{0}>", coImplementationType);
            }

            Call(host, ctx, serializationList,
                xmlNamespace, overridePropName, backingPropertyName, backingStoreName,
                coType, coImplementationType);
        }

        protected virtual void AddSerialization(
            Serialization.SerializationMembersList list,
            string memberType, string memberName,
            string backingStoreType, string backingStoreName)
        {
            if (list != null)
            {
                var xmlname = memberName;

                list.Add("Serialization.CompoundObjectSerialization", Serialization.SerializerType.All,
                    this.xmlNamespace, xmlname, memberType, memberName, backingStoreType, backingStoreName);
            }
        }
    }
}
