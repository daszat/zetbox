
namespace Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Templates = Zetbox.Generator.Templates;

    public class UpdateParentTemplate : Templates.ObjectClasses.UpdateParentTemplate
    {
        public UpdateParentTemplate(Arebis.CodeGeneration.IGenerationHost _host, List<Templates.ObjectClasses.UpdateParentTemplateParams> props)
            : base(_host, props)
        {
        }

        // NHibernate has different structure, accesses Proxy directly
        protected override void ApplyCase(Templates.ObjectClasses.UpdateParentTemplateParams prop)
        {
            string implType = prop.IfType+ ImplementationSuffix;

            this.WriteObjects("                case \"", prop.PropertyName, "\":");
            this.WriteLine();
            this.WriteObjects("                    {");
            this.WriteLine();
            this.WriteObjects("                        var __oldValue = (", implType, ")OurContext.AttachAndWrap(this.Proxy.", prop.PropertyName, ");");
            this.WriteLine();
            this.WriteObjects("                        var __newValue = (", implType, ")parentObj;");
            this.WriteLine();
            this.WriteObjects("                        NotifyPropertyChanging(\"", prop.PropertyName, "\", __oldValue, __newValue);");
            this.WriteLine();
            this.WriteObjects("                        this.Proxy.", prop.PropertyName, " = __newValue == null ? null : __newValue.Proxy;");
            this.WriteLine();
            this.WriteObjects("                        NotifyPropertyChanged(\"", prop.PropertyName, "\", __oldValue, __newValue);");
            this.WriteLine();
            this.WriteObjects("                    }");
            this.WriteLine();
            this.WriteObjects("                    break;");
            this.WriteLine();
        }
    }
}
