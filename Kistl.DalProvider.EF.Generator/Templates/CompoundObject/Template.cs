
namespace Kistl.DalProvider.Ef.Generator.Templates.CompoundObjects
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

        protected override void ApplyClassAttributeTemplate()
        {
            WriteLine("    [EdmComplexType(NamespaceName=\"Model\", Name=\"{0}\")]", this.CompoundObjectType.Name);
        }

        /// <returns>The base class to inherit from.</returns>
        protected override string GetBaseClass()
        {
            return "BaseServerCompoundObject_EntityFramework";
        }

        protected override void ApplyExtraConstructorTemplate()
        {
            // avoid base constructor not implementing bool isNull
            // base.ApplyExtraConstructorTemplate();
            string clsName = this.GetTypeName();

            this.WriteObjects("        public ", clsName, "(bool isNull, IPersistenceObject parent, string property)");
            this.WriteLine();
            this.WriteObjects("            : base(null) // TODO: pass parent's lazyCtx");
            this.WriteLine();
            this.WriteObjects("        {");
            this.WriteLine();
            this.WriteObjects("            AttachToObject(parent, property);");
            this.WriteLine();
            this.WriteObjects("            CompoundObject_IsNull = isNull;");
            this.WriteLine();
            ApplyConstructorBodyTemplate();
            this.WriteObjects("        }");
            this.WriteLine();

            this.WriteObjects("        [EdmScalarProperty(IsNullable = false)]");
            this.WriteLine();
            this.WriteObjects("        public bool CompoundObject_IsNull { get; set; }");
            this.WriteLine();
        }

        protected override void ApplyEnumerationPropertyTemplate(EnumerationProperty prop)
        {
            this.WriteLine("        // enumeration property");
            Properties.EnumerationPropertyTemplate.Call(Host, ctx,
                this.MembersToSerialize,
                prop, true);
        }
    }
}
