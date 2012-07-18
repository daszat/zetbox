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
    using Templates = Zetbox.Generator.Templates;

    public partial class ProxyProperty
    {
        private string EventName
        {
            get
            {
                return "On" + propertyName;
            }
        }

        protected override MemberAttributes ModifyMemberAttributes(MemberAttributes memberAttributes)
        {
            var result = base.ModifyMemberAttributes(memberAttributes);
            if (this.overrideParent)
            {
                result &= ~MemberAttributes.Final;
                result |= MemberAttributes.Override;
            }
            return result;
        }

        protected virtual void ApplyOnGetTemplate()
        {
            if (useEvents && !isCalculated)
            {
                this.WriteObjects("                if (", EventName, "_Getter != null)");
                this.WriteLine();
                this.WriteObjects("                {");
                this.WriteLine();
                this.WriteObjects("                    var __e = new PropertyGetterEventArgs<", propertyType, ">(__result);");
                this.WriteLine();
                this.WriteObjects("                    ", EventName, "_Getter(this, __e);");
                this.WriteLine();
                this.WriteObjects("                    __result = __e.Result;");
                this.WriteLine();
                this.WriteObjects("                }");
                this.WriteLine();
            }
        }

        protected virtual void ApplyOnAllSetTemplate()
        {
            if (hasDefaultValue)
            {
                // this has to happen before the value comparison, because we 
                // need to flag the *intent* of setting this property, even if the value set is == the default value
                this.WriteObjects("                ", isSetFlagName, " = true;");
                this.WriteLine();
            }
        }

        protected virtual void ApplyPreSetTemplate()
        {
            if (useEvents)
            {
                this.WriteObjects("                    if (", EventName, "_PreSetter != null && IsAttached)");
                this.WriteLine();
                this.WriteObjects("                    {");
                this.WriteLine();
                this.WriteObjects("                        var __e = new PropertyPreSetterEventArgs<", propertyType, ">(__oldValue, __newValue);");
                this.WriteLine();
                this.WriteObjects("                        ", EventName, "_PreSetter(this, __e);");
                this.WriteLine();
                this.WriteObjects("                        __newValue = __e.Result;");
                this.WriteLine();
                this.WriteObjects("                    }");
                this.WriteLine();
            }
        }

        protected virtual void ApplyPostSetTemplate()
        {
            if (useEvents)
            {
                this.WriteObjects("                    if (", EventName, "_PostSetter != null && IsAttached)");
                this.WriteLine();
                this.WriteObjects("                    {");
                this.WriteLine();
                this.WriteObjects("                        var __e = new PropertyPostSetterEventArgs<", propertyType, ">(__oldValue, __newValue);");
                this.WriteLine();
                this.WriteObjects("                        ", EventName, "_PostSetter(this, __e);");
                this.WriteLine();
                this.WriteObjects("                    }");
                this.WriteLine();
            }
        }

        protected virtual void ApplyTailTemplate()
        {
            if (hasDefaultValue)
            {
                this.WriteLine();
                this.WriteObjects("        private ", propertyType, " Fetch", propertyName, "OrDefault()");
                this.WriteLine();
                this.WriteObjects("        {");
                this.WriteLine();
                this.WriteObjects("            var __result = Proxy.", propertyName, ";");
                this.WriteLine();
                Templates.Properties.ComputeDefaultValue.Call(Host, ctx, interfaceName, className, propertyName, isNullable, isSetFlagName, propertyGuid, backingStoreType, backingStoreName);
                this.WriteObjects("            return __result;");
                this.WriteLine();
                this.WriteObjects("        }");
                this.WriteLine();
                this.WriteLine();

                this.WriteObjects("        private bool ", isSetFlagName, " = false;");
                this.WriteLine();
            }
            else if (isCalculated)
            {
                this.WriteLine();
                this.WriteObjects("        private ", propertyType, " Fetch", propertyName, "OrDefault()");
                this.WriteLine();
                this.WriteObjects("        {");
                this.WriteLine();
                this.WriteObjects("           var __result = Proxy.", propertyName, ";");
                this.WriteLine();
                this.WriteObjects("            if (_", propertyName, "_IsDirty && ", EventName, "_Getter != null)");
                this.WriteLine();
                this.WriteObjects("            {");
                this.WriteLine();
                this.WriteObjects("                var __e = new PropertyGetterEventArgs<", propertyType, ">(__result);");
                this.WriteLine();
                this.WriteObjects("                ", EventName, "_Getter(this, __e);");
                this.WriteLine();
                this.WriteObjects("                _", propertyName, "_IsDirty = false;");
                this.WriteLine();
                this.WriteObjects("                __result = Proxy.", propertyName, " = __e.Result;");
                this.WriteLine();
                this.WriteObjects("            }");
                this.WriteLine();
                this.WriteObjects("            return __result;");
                this.WriteLine(); 
                this.WriteObjects("        }");
                this.WriteLine();
                this.WriteLine();

                this.WriteObjects("        private bool ", isSetFlagName, " = false;");
                this.WriteLine();
            }
        }

        protected virtual void AddSerialization(
            Templates.Serialization.SerializationMembersList list,
            string memberName,
            string fkBackingName)
        {
            if (list != null)
            {
                if (hasDefaultValue)
                {
                    Templates.Serialization.SimplePropertyWithDefaultSerialization
                        .AddToSerializers(list,
                            disableExport ? Templates.Serialization.SerializerType.Binary : Templates.Serialization.SerializerType.All, 
                            moduleNamespace, propertyName, propertyType, "Proxy." + propertyName, isSetFlagName);
                }
                else
                {
                    Templates.Serialization.SimplePropertySerialization
                        .AddToSerializers(list,
                            disableExport ? Templates.Serialization.SerializerType.Binary : Templates.Serialization.SerializerType.All, 
                            moduleNamespace, propertyName, propertyType, "Proxy." + propertyName);
                }
            }
        }
    }
}
