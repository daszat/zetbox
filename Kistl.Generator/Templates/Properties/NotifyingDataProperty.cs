
namespace Kistl.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Generator.Extensions;

    public class NotifyingDataProperty
        : NotifyingValueProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx, Serialization.SerializationMembersList serializationList, Property prop)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Properties.NotifyingDataProperty",
                ctx, serializationList, prop);
        }

        private Property _prop;

        public NotifyingDataProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Serialization.SerializationMembersList serializationList, Property prop)
            : base(_host, ctx, serializationList, prop.ReferencedTypeAsCSharp(), prop.Name, prop.Module.Namespace, "_" + prop.Name)
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

            // TODO: morph to real template
            if (HasDefaultValue)
            {
                this.WriteObjects("                if (!", IsSetFlagName, ") {\r\n");
                this.WriteObjects("                    var __p = FrozenContext.FindPersistenceObject<Kistl.App.Base.Property>(new Guid(\"", _prop.ExportGuid, "\"));\r\n");
                this.WriteObjects("                    if (__p != null) {\r\n");
                this.WriteObjects("                        ", IsSetFlagName, " = true;\r\n");
                this.WriteObjects("                        // http://connect.microsoft.com/VisualStudio/feedback/details/593117/cannot-directly-cast-boxed-int-to-nullable-enum\r\n");
                this.WriteObjects("                        object __tmp_value = __p.DefaultValue.GetDefaultValue();\r\n");
                if (this._prop.IsNullable())
                {
                    this.WriteObjects("                        if (__tmp_value == null)\r\n");
                    this.WriteObjects("                            __result = this.", backingName, " = null;\r\n");
                    this.WriteObjects("                        else\r\n    "); // Fix indent
                }
                this.WriteObjects("                        __result = this.", backingName, " = (", type.TrimEnd('?'), ")__tmp_value;\r\n");
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

        protected override void AddSerialization(Serialization.SerializationMembersList list, string name)
        {
            if (list != null)
            {
                if (HasDefaultValue)
                {
                    list.Add("Serialization.NotifyingDataPropertyWithDefaultSerialization",
                        Serialization.SerializerType.All,
                        _prop.Module.Namespace,
                        name,
                        backingName,
                        IsSetFlagName);
                }
                else
                {
                    list.Add("Serialization.NotifyingDataPropertySerialization",
                        Serialization.SerializerType.All,
                        _prop.Module.Namespace,
                        name,
                        backingName);
                }
            }
        }
    }
}
