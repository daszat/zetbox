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
    using Zetbox.Generator.Extensions;

    public class NotifyingDataProperty
        : NotifyingValueProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IZetboxContext ctx, Serialization.SerializationMembersList serializationList, Property prop)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Properties.NotifyingDataProperty",
                ctx, serializationList, prop);
        }

        protected readonly string className;
        protected readonly bool isNullable;
        protected readonly bool hasDefaultValue;
        protected readonly Guid propExportGuid;

        public NotifyingDataProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Serialization.SerializationMembersList serializationList, Property prop)
            : this(_host, ctx, serializationList, prop.GetElementTypeString(), prop.Name, prop.Module.Namespace, "_" + prop.Name, prop.IsCalculated(), prop.DisableExport == true, prop.ObjectClass.Name, prop.IsNullable(), prop.DefaultValue != null && !prop.IsCalculated(), prop.ExportGuid)
        {
        }

        public NotifyingDataProperty(
            Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Serialization.SerializationMembersList serializationList,
            string type, string name, string modulenamespace, string backingName, bool isCalculated, bool disableExport,
            string className, bool isNullable, bool hasDefaultValue, Guid propExportGuid)
            : base(_host, ctx, serializationList, type, name, modulenamespace, backingName, isCalculated, disableExport)
        {
            this.className = className;
            this.isNullable = isNullable;
            this.hasDefaultValue = hasDefaultValue;
            this.propExportGuid = propExportGuid;
        }

        private string EventName
        {
            get
            {
                return "On" + name;
            }
        }

        protected string IsSetFlagName
        {
            get
            {
                return "_is" + name + "Set";
            }
        }

        protected override void ApplyOnGetTemplate()
        {
            base.ApplyOnGetTemplate();

            if (hasDefaultValue)
            {
                ComputeDefaultValue.Call(Host, ctx,
                    className,
                    className,
                    name,
                    isNullable,
                    IsSetFlagName,
                    propExportGuid,
                    type,
                    backingName);
            }
            if (isCalculated)
                this.WriteObjects("                if (", backingName, "_IsDirty && ", EventName, "_Getter != null)\r\n");
            else
                this.WriteObjects("                if (", EventName, "_Getter != null)\r\n");
            this.WriteObjects("                {\r\n");
            this.WriteObjects("                    var __e = new PropertyGetterEventArgs<", type, ">(__result);\r\n");
            this.WriteObjects("                    ", EventName, "_Getter(this, __e);\r\n");
            if (isCalculated)
                this.WriteObjects("                    ", backingName, "_IsDirty = false;\r\n");
            this.WriteObjects("                    __result = ", backingName, " = __e.Result;\r\n");
            this.WriteObjects("                }\r\n");
        }

        protected override void ApplyOnAllSetTemplate()
        {
            base.ApplyOnAllSetTemplate();
            if (hasDefaultValue)
            {
                // this has to happen before the value comparison, because we 
                // need to flag the *intent* of setting this property, even if the value set is == the default value
                this.WriteObjects("                ", IsSetFlagName, " = true;\r\n");
            }
        }

        protected override void ApplyPreSetTemplate()
        {
            base.ApplyPreSetTemplate();

            this.WriteObjects("                    if (", EventName, "_PreSetter != null && IsAttached)\r\n");
            this.WriteObjects("                    {\r\n");
            this.WriteObjects("                        var __e = new PropertyPreSetterEventArgs<", type, ">(__oldValue, __newValue);\r\n");
            this.WriteObjects("                        ", EventName, "_PreSetter(this, __e);\r\n");
            this.WriteObjects("                        __newValue = __e.Result;\r\n");
            this.WriteObjects("                    }\r\n");
        }

        protected override void ApplyPostSetTemplate()
        {
            base.ApplyPostSetTemplate();
            this.WriteObjects("                    if (", EventName, "_PostSetter != null && IsAttached)\r\n");
            this.WriteObjects("                    {\r\n");
            this.WriteObjects("                        var __e = new PropertyPostSetterEventArgs<", type, ">(__oldValue, __newValue);\r\n");
            this.WriteObjects("                        ", EventName, "_PostSetter(this, __e);\r\n");
            this.WriteObjects("                    }\r\n");
        }

        protected override void ApplyBackingStoreDefinition()
        {
            base.ApplyBackingStoreDefinition();
        }

        protected override void ApplyTailTemplate()
        {
            base.ApplyTailTemplate();
            if (hasDefaultValue)
            {
                this.WriteObjects("        private bool ", IsSetFlagName, " = false;\r\n");
            }
        }

        protected override void AddSerialization(Serialization.SerializationMembersList list, string name)
        {
            if (list != null)
            {
                if (hasDefaultValue)
                {
                    list.Add("Serialization.SimplePropertyWithDefaultSerialization",
                        disableExport ? Templates.Serialization.SerializerType.Binary : Serialization.SerializerType.All,
                        modulenamespace,
                        name,
                        type,
                        backingName,
                        IsSetFlagName);
                }
                else
                {
                    list.Add("Serialization.SimplePropertySerialization",
                        disableExport ? Serialization.SerializerType.Binary : Serialization.SerializerType.All,
                        modulenamespace,
                        name,
                        type,
                        backingName);
                }
            }
        }
    }
}
