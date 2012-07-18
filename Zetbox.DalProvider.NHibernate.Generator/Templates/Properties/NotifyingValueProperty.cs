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
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Templates = Zetbox.Generator.Templates;

    public class NotifyingValueProperty
        : Templates.Properties.NotifyingValueProperty
    {
        public NotifyingValueProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Templates.Serialization.SerializationMembersList serializationList, string type, string name, string modulenamespace, string backingName, bool isCalculated, bool disableExport)
            : base(_host, ctx, serializationList, type, name, modulenamespace, "Proxy." + name, isCalculated, disableExport)
        {
        }

        protected override MemberAttributes ModifyMemberAttributes(MemberAttributes memberAttributes)
        {
            return base.ModifyMemberAttributes(memberAttributes) & ~MemberAttributes.Final;
        }

        protected override void ApplyBackingStoreDefinition()
        {
            // the proxy store the value, so we don't need a local backing store
            // base.ApplyBackingStoreDefinition();
        }

        protected override void ApplyBackingStoreIsDirtyDefinition()
        {
            // the proxy store the value, so we don't need a local backing store
            // base.ApplyBackingStoreIsDirtyDefinition();
        }

        protected override void AddSerialization(Templates.Serialization.SerializationMembersList list, string name)
        {
            if (list != null)
            {
                Templates.Serialization.SimplePropertySerialization
                    .AddToSerializers(list, 
                    disableExport ?  Templates.Serialization.SerializerType.Binary : Templates.Serialization.SerializerType.All, 
                        modulenamespace, name, type, backingName);
            }
        }
    }
}
