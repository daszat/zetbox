
namespace Kistl.DalProvider.NHibernate.Generator.Templates.Properties
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Templates = Kistl.Generator.Templates;

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
            if (hasDefaultValue)
            {
                Templates.Properties.ComputeDefaultValue.Call(Host, ctx, className, propertyName, isNullable, isSetFlagName, propertyGuid, backingStoreType, backingStoreName);
            }
            if (useEvents)
            {
                this.WriteObjects("                if (", EventName, "_Getter != null)\r\n");
                this.WriteObjects("                {\r\n");
                this.WriteObjects("                    var __e = new PropertyGetterEventArgs<", propertyType, ">(__result);\r\n");
                this.WriteObjects("                    ", EventName, "_Getter(this, __e);\r\n");
                this.WriteObjects("                    __result = __e.Result;\r\n");
                this.WriteObjects("                }\r\n");
            }
        }

        protected virtual void ApplyOnAllSetTemplate()
        {
            if (hasDefaultValue)
            {
                // this has to happen before the value comparison, because we 
                // need to flag the *intent* of setting this property, even if the value set is == the default value
                this.WriteObjects("                ", isSetFlagName, " = true;\r\n");
            }
        }

        protected virtual void ApplyPreSetTemplate()
        {
            if (useEvents)
            {
                this.WriteObjects("                    if (", EventName, "_PreSetter != null && IsAttached)\r\n");
                this.WriteObjects("                    {\r\n");
                this.WriteObjects("                        var __e = new PropertyPreSetterEventArgs<", propertyType, ">(__oldValue, __newValue);\r\n");
                this.WriteObjects("                        ", EventName, "_PreSetter(this, __e);\r\n");
                this.WriteObjects("                        __newValue = __e.Result;\r\n");
                this.WriteObjects("                    }\r\n");
            }
        }

        protected virtual void ApplyPostSetTemplate()
        {
            if (useEvents)
            {
                this.WriteObjects("                    if (", EventName, "_PostSetter != null && IsAttached)\r\n");
                this.WriteObjects("                    {\r\n");
                this.WriteObjects("                        var __e = new PropertyPostSetterEventArgs<", propertyType, ">(__oldValue, __newValue);\r\n");
                this.WriteObjects("                        ", EventName, "_PostSetter(this, __e);\r\n");
                this.WriteObjects("                    }\r\n");
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
                        .AddToSerializers(list, Templates.Serialization.SerializerType.All, moduleNamespace, propertyName, propertyType, "Proxy." + propertyName, isSetFlagName);
                }
                else
                {
                    Templates.Serialization.SimplePropertySerialization
                        .AddToSerializers(list, Templates.Serialization.SerializerType.All, moduleNamespace, propertyName, propertyType, "Proxy." + propertyName);
                }
            }
        }
    }
}
