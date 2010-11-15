
namespace Kistl.Generator.Templates.CompoundObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Templates = Kistl.Generator.Templates;

    /// <summary>
    /// A template for "CompoundObject".
    /// </summary>
    public class Template
        : Kistl.Generator.Templates.TypeBase
    {
        protected CompoundObject CompoundObjectType { get; private set; }

        public Template(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, CompoundObject cType)
            : base(_host, ctx, cType)
        {
            this.CompoundObjectType = cType;
        }

        protected override string[] GetInterfaces()
        {
            return base.GetInterfaces().Concat(new string[] { "ICompoundObject" }).OrderBy(s => s).ToArray();
        }

        /// <returns>The base class to inherit from.</returns>
        protected override string GetBaseClass()
        {
            return "CompoundObject" + ImplementationSuffix;
        }

        protected override void ApplyExtraConstructorTemplate()
        {
            base.ApplyExtraConstructorTemplate();

            string clsName = this.GetTypeName();

            // attach compound to parent object
            this.WriteObjects("        public ", clsName, "(IPersistenceObject parent, string property) : this(false, parent, property) {}");
            this.WriteLine();
            this.WriteObjects("        public ", clsName, "(bool ignore, IPersistenceObject parent, string property)");
            this.WriteLine();
            this.WriteObjects("            : base(null) // TODO: pass parent's lazyCtx");
            this.WriteLine();
            this.WriteObjects("        {");
            this.WriteLine();
            this.WriteObjects("            AttachToObject(parent, property);");
            this.WriteLine();
            ApplyConstructorBodyTemplate();
            this.WriteObjects("        }");
            this.WriteLine();
        }
    }
}
