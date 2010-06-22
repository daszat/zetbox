
namespace Kistl.DalProvider.EF.Generator.Implementation.CompoundObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.Server.Generators.Extensions;

    public class Template
        : Kistl.Server.Generators.Templates.Implementation.CompoundObjects.Template
    {
        public Template(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, CompoundObject s)
            : base(_host, ctx, s)
        {
        }

        protected override void ApplyClassAttributeTemplate()
        {
            WriteLine("    [EdmComplexType(NamespaceName=\"Model\", Name=\"{0}\")]", this.CompoundObjectType.Name);
        }

        protected override IEnumerable<string> GetAdditionalImports()
        {
            return base.GetAdditionalImports().Concat(new string[]{
                "Kistl.API.Server",
                "Kistl.DalProvider.EF",
                "System.Data.Objects",
                "System.Data.Objects.DataClasses" 
            });
        }

        protected override string MungeClassName(string name)
        {
            return base.MungeClassName(name) + Kistl.API.Helper.ImplementationSuffix;
        }

        /// <returns>The base class to inherit from.</returns>
        protected override string GetBaseClass()
        {
            return "BaseServerCompoundObject_EntityFramework";
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();

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
            this.WriteObjects("        }");
            this.WriteLine();

            this.WriteObjects("        [EdmScalarProperty(IsNullable = false)]");
            this.WriteLine();
            this.WriteObjects("        public bool CompoundObject_IsNull { get; set; }");
            this.WriteLine();
        }

    }
}
