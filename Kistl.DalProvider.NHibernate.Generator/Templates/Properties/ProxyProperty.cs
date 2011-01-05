
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

        protected virtual void ApplyPreSetTemplate()
        {
            if (useEvents)
            {
                this.WriteObjects("                    if(", EventName, "_PreSetter != null && IsAttached)\r\n");
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
                this.WriteObjects("                    if(", EventName, "_PostSetter != null && IsAttached)\r\n");
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
                Templates.Serialization.SimplePropertySerialization
                    .AddToSerializers(list, Templates.Serialization.SerializerType.All, moduleNamespace, propertyName, propertyType, "Proxy." + propertyName);
            }
        }
    }
}
