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
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Generator.Extensions;

    public partial class NotifyingValueProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IZetboxContext ctx, Serialization.SerializationMembersList serializationList,
            string type, string name, string modulenamespace, bool isCalculated, bool disableExport)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            string backingName = "_" + name;

            Call(host, ctx, serializationList, type, name, modulenamespace, backingName, isCalculated, disableExport);
        }

        protected virtual void ApplySecurityCheckTemplate()
        {
        }

        /// <summary>
        /// Is called to insert requisites into the containing class, like wrappers or similar.
        /// </summary>
        protected virtual void ApplyRequisitesTemplate() { }

        /// <summary>
        /// Is called to apply optional decoration in front of the property declaration, like Attributes.
        /// </summary>
        protected virtual void ApplyAttributesTemplate() { }

        protected virtual void ApplyTailTemplate() { }

        protected virtual void AddSerialization(Serialization.SerializationMembersList list, string name)
        {
            if (list != null)
                list.Add(disableExport ? Serialization.SerializerType.Binary : Serialization.SerializerType.All, modulenamespace, name, type, backingName);
        }

        protected virtual void ApplyOnGetTemplate() { }
        protected virtual void ApplyOnAllSetTemplate() { }
        protected virtual void ApplyPreSetTemplate()
        {
            ApplyDateTimeKindSpecifier();
        }

        /// <summary>
        /// Forces all DateTimeKind.Unspecified values into local time, without changing the actual value.
        /// </summary>
        protected virtual void ApplyDateTimeKindSpecifier()
        {
            switch (type)
            {
                case "DateTime":
                    this.WriteObjects("                    if (__newValue.Kind == DateTimeKind.Unspecified)\r\n");
                    this.WriteObjects("                        __newValue = DateTime.SpecifyKind(__newValue, DateTimeKind.Local);\r\n");
                    break;
                case "DateTime?":
                case "Nullable<DateTime>":
                    this.WriteObjects("                    if (__newValue.HasValue && __newValue.Value.Kind == DateTimeKind.Unspecified)\r\n");
                    this.WriteObjects("                        __newValue = DateTime.SpecifyKind(__newValue.Value, DateTimeKind.Local);\r\n");
                    break;
            }
        }
        protected virtual void ApplyPostSetTemplate() { }

        protected virtual void ApplyBackingStoreDefinition()
        {
            this.WriteLine("        private {0} {1};", type, backingName);
        }

        protected virtual void ApplyBackingStoreIsDirtyDefinition()
        {
            if (isCalculated)
                this.WriteLine("        private bool {0}_IsDirty = false;", backingName);
        }
    }
}
