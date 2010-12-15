
namespace Kistl.DalProvider.NHibernate.Generator.Templates.CompoundObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.Generator.Extensions;
    using Templates = Kistl.Generator.Templates;

    public class Template
        : Templates.CompoundObjects.Template
    {
        public Template(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, CompoundObject s)
            : base(_host, ctx, s)
        {
        }

        protected override void ApplyConstructorTemplate()
        {
            // replace base constructor
            // base.ApplyExtraConstructorTemplate();

            string interfaceName = DataType.Name;
            string className = GetTypeName();
            string baseClassName = null;
            ObjectClasses.Constructors.Call(Host, ctx, this.DataType.Properties.OfType<CompoundObjectProperty>(), interfaceName, className, baseClassName);
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();

            string interfaceName = DataType.Name;
            this.WriteObjects("public class ", interfaceName, "Proxy { }");
            this.WriteLine();
        }

        protected override string GetExportGuidBackingStoreReference()
        {
            return "this.Proxy.ExportGuid";
        }
    }
}
