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
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.ObjectClasses.NotifyingDataProperty",
                ctx, serializationList, prop);
        }

        private Property _prop;

        public NotifyingDataProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializationMembersList serializationList, Property prop)
            : base(_host, ctx, serializationList, prop.ReferencedTypeAsCSharp(), prop.Name, prop.Module.Namespace)
        {
            _prop = prop;
        }

        private string EventName
        {
            get
            {
                return "On" + name;
            }
        }

        private string IsSetFlagName
        {
            get
            {
                return "_is" + name + "Set";
            }
        }

        private bool HasDefaultValue
        {
            get
            {
                return _prop.DefaultValue != null;
            }
        }
        protected override void ApplyOnGetTemplate()
        {
            base.ApplyOnGetTemplate();

            if (HasDefaultValue)
            {
                this.WriteObjects("                if (!", IsSetFlagName, ") {\r\n");
                this.WriteObjects("                    var __p = FrozenContext.FindPersistenceObject<Kistl.App.Base.Property>(new Guid(\"", _prop.ExportGuid, "\"));\r\n");
                this.WriteObjects("                    if (__p != null) {\r\n");
                this.WriteObjects("                        ", IsSetFlagName, " = true;\r\n");
                this.WriteObjects("                        __result = this.", BackingMemberFromName(name), " = (", type, ")__p.DefaultValue.GetDefaultValue();\r\n");
                this.WriteObjects("                    } else {\r\n");
                this.WriteObjects("                        Kistl.API.Utils.Logging.Log.Warn(\"Unable to get default value for property '", _prop.ObjectClass.Name, ".", _prop.Name, "'\");\r\n");
                this.WriteObjects("                    }\r\n");
                this.WriteObjects("                }\r\n");
            }
            this.WriteObjects("                if (", EventName, "_Getter != null)\r\n");
            this.WriteObjects("                {\r\n");
            this.WriteObjects("                    var __e = new PropertyGetterEventArgs<", type, ">(__result);\r\n");
            this.WriteObjects("                    ", EventName, "_Getter(this, __e);\r\n");
            this.WriteObjects("                    __result = __e.Result;\r\n");
            this.WriteObjects("                }\r\n");
        }

        protected override void ApplyOnAllSetTemplate()
        {
            base.ApplyOnAllSetTemplate();
            if (HasDefaultValue)
            {
                // this has to happen before the value comparison, because we 
                // need to flag the *intent* of setting this property
                this.WriteObjects("                ", IsSetFlagName, " = true;\r\n");
            }
        }

        protected override void ApplyPreSetTemplate()
        {
            base.ApplyPreSetTemplate();

            this.WriteObjects("                    if(", EventName, "_PreSetter != null && IsAttached)\r\n");
            this.WriteObjects("                    {\r\n");
            this.WriteObjects("                        var __e = new PropertyPreSetterEventArgs<", type, ">(__oldValue, __newValue);\r\n");
            this.WriteObjects("                        ", EventName, "_PreSetter(this, __e);\r\n");
            this.WriteObjects("                        __newValue = __e.Result;\r\n");
            this.WriteObjects("                    }\r\n");
        }

        protected override void ApplyPostSetTemplate()
        {
            base.ApplyPostSetTemplate();
            this.WriteObjects("                    if(", EventName, "_PostSetter != null && IsAttached)\r\n");
            this.WriteObjects("                    {\r\n");
            this.WriteObjects("                        var __e = new PropertyPostSetterEventArgs<", type, ">(__oldValue, __newValue);\r\n");
            this.WriteObjects("                        ", EventName, "_PostSetter(this, __e);\r\n");
            this.WriteObjects("                    }\r\n");
        }

        protected override void ApplyRequisitesTemplate()
        {
            base.ApplyRequisitesTemplate();
            if (HasDefaultValue)
            {
                this.WriteObjects("        private bool ", IsSetFlagName, " = false;\r\n");
            }
        }

        protected override void AddSerialization(SerializationMembersList list, string name)
        {
            if (list != null)
            {
                if (HasDefaultValue)
                {
                    list.Add(new SerializationMember("Implementation.ObjectClasses.NotifyingDataPropertyWithDefaultSerialization",
                        SerializerType.All,
                        _prop.Module.Namespace,
                        name,
                        BackingMemberFromName(name),
                        IsSetFlagName));
                }
                else
                {
                    list.Add(new SerializationMember("Implementation.ObjectClasses.NotifyingDataPropertySerialization",
                        SerializerType.All,
                        _prop.Module.Namespace,
                        name,
                        BackingMemberFromName(name)));
                }
            }
        }
    }
}
