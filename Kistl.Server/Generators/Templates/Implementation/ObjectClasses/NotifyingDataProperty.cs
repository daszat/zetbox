using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    public class NotifyingDataProperty
        : NotifyingValueProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx, SerializationMembersList serializationList, Property prop)
        {
            host.CallTemplate("Implementation.ObjectClasses.NotifyingDataProperty",
                ctx, serializationList, prop);
        }

        private Property _prop;

        public NotifyingDataProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializationMembersList serializationList, Property prop)
            : base(_host, ctx, serializationList, prop.ReferencedTypeAsCSharp(), prop.PropertyName, prop.Module.Namespace)
        {
            _prop = prop;
        }

        private string EventName()
        {
            return "On" + name;
        }

        private string IsSetFlagName()
        {
            return "_is" + name + "Set";
        }
        protected override void ApplyOnGetTemplate()
        {
            base.ApplyOnGetTemplate();

            if (_prop.DefaultValue != null)
            {
                this.WriteObjects("                if (!", IsSetFlagName(), ") {\r\n");
                this.WriteObjects("                    var __p = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid(\"", _prop.ExportGuid, "\"));\r\n");
                this.WriteObjects("                    if (__p != null) {\r\n");
                this.WriteObjects("                        ", IsSetFlagName(), " = true;\r\n");
                this.WriteObjects("                        __result = this.", BackingMemberFromName(name), " = (", type, ")__p.DefaultValue.GetDefaultValue();\r\n");
                this.WriteObjects("                    } else {\r\n");
                this.WriteObjects("                        System.Diagnostics.Trace.TraceWarning(\"Unable to get default value for property '", _prop.ObjectClass.ClassName, ".", _prop.PropertyName, "'\");\r\n");
                this.WriteObjects("                    }\r\n");
                this.WriteObjects("                }\r\n");
            }
            this.WriteObjects("                if (", EventName(), "_Getter != null)\r\n");
            this.WriteObjects("                {\r\n");
            this.WriteObjects("                    var __e = new PropertyGetterEventArgs<", type, ">(__result);\r\n");
            this.WriteObjects("                    ", EventName(), "_Getter(this, __e);\r\n");
            this.WriteObjects("                    __result = __e.Result;\r\n");
            this.WriteObjects("                }\r\n");
        }

        protected override void ApplyPreSetTemplate()
        {
            base.ApplyPreSetTemplate();
            if (_prop.DefaultValue != null)
            {
                this.WriteObjects("                    ", IsSetFlagName(), " = true;\r\n");
            }
            this.WriteObjects("                    if(", EventName(), "_PreSetter != null)\r\n");
            this.WriteObjects("                    {\r\n");
            this.WriteObjects("                        var __e = new PropertyPreSetterEventArgs<", type, ">(__oldValue, __newValue);\r\n");
            this.WriteObjects("                        ", EventName(), "_PreSetter(this, __e);\r\n");
            this.WriteObjects("                        __newValue = __e.Result;\r\n");
            this.WriteObjects("                    }\r\n");
        }

        protected override void ApplyPostSetTemplate()
        {
            base.ApplyPostSetTemplate();
            this.WriteObjects("                    if(", EventName(), "_PostSetter != null)\r\n");
            this.WriteObjects("                    {\r\n");
            this.WriteObjects("                        var __e = new PropertyPostSetterEventArgs<", type, ">(__oldValue, __newValue);\r\n");
            this.WriteObjects("                        ", EventName(), "_PostSetter(this, __e);\r\n");
            this.WriteObjects("                    }\r\n");
        }

        protected override void ApplyRequisitesTemplate()
        {
            base.ApplyRequisitesTemplate();
            if (_prop.DefaultValue != null)
            {
                this.WriteObjects("        private bool ", IsSetFlagName(), " = false;\r\n");
            }
        }
    }
}
