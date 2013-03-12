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

namespace Zetbox.DalProvider.Ef.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator.Extensions;
    using Zetbox.Generator.Templates.Serialization;
    using Templates = Zetbox.Generator.Templates;

    public class NotifyingDataProperty
        : Templates.Properties.NotifyingDataProperty
    {
        public NotifyingDataProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Templates.Serialization.SerializationMembersList list, Property prop)
            : base(_host, ctx, list, prop)
        {
        }

        protected override void ApplyAttributesTemplate()
        {
            base.ApplyAttributesTemplate();
            EfScalarPropHelper.ApplyAttributesTemplate(this);
        }

        protected override void ApplyBackingStoreDefinition()
        {
            EfScalarPropHelper.ApplyBackingStoreDefinition(this, type, backingName, name);
        }
    }

    public class NotifyingEnumProperty
         : Templates.Properties.NotifyingDataProperty
    {
        private readonly string efName;

        public static new void Call(Arebis.CodeGeneration.IGenerationHost host, IZetboxContext ctx, Templates.Serialization.SerializationMembersList serializationList, Property prop)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Properties.NotifyingEnumProperty",
                ctx, serializationList, prop);
        }

        public NotifyingEnumProperty(Arebis.CodeGeneration.IGenerationHost host, IZetboxContext ctx, Templates.Serialization.SerializationMembersList serializationList, Property prop)
            : base(host, ctx, serializationList, prop)
        {
            efName = name + ImplementationPropertySuffix;
        }

        protected override void ApplyBackingStoreDefinition()
        {
            EfScalarPropHelper.ApplyBackingStoreDefinition(this, type, backingName, efName);
        }

        protected override void ApplyTailTemplate()
        {
            base.ApplyTailTemplate();
            // redirect EF to an impl property exposing and casting from/to int(?)
            EnumPropertyShim.Call(Host, ctx, type, name, efName, isNullable);
        }

        protected override void AddSerialization(Templates.Serialization.SerializationMembersList list, string name)
        {
            if (list != null)
            {
                if (hasDefaultValue)
                {
                    Templates.Serialization.EnumWithDefaultBinarySerialization.AddToSerializers(
                        list,
                        disableExport ? SerializerType.Binary : SerializerType.All,
                        modulenamespace,
                        name,
                        backingName,
                        type,
                        IsSetFlagName);
                }
                else
                {
                    Templates.Serialization.EnumBinarySerialization.AddToSerializers(
                        list,
                        disableExport ? SerializerType.Binary : SerializerType.All,
                        modulenamespace,
                        name,
                        backingName,
                        type);
                }
            }
        }
    }
}
