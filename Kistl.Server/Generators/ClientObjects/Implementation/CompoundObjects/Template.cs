
namespace Kistl.Server.Generators.ClientObjects.Implementation.CompoundObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;

    public class Template
        : Kistl.Server.Generators.Templates.Implementation.CompoundObjects.Template
    {
        public Template(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, CompoundObject s)
            : base(_host, ctx, s)
        {
        }

        /// <returns>The base class to inherit from.</returns>
        protected override string GetBaseClass()
        {
            return "BaseClientCompoundObject";
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();

            string clsName = this.GetTypeName();

            // attach compound to parent object when being constructed
            this.WriteObjects("        public ", clsName, "(IPersistenceObject parent, string property)");
            this.WriteLine();
            this.WriteObjects("            : base(null) // TODO: pass parent's lazyCtx");
            this.WriteLine();
            this.WriteObjects("        {");
            this.WriteLine();
            this.WriteObjects("            AttachToObject(parent, property);");
            this.WriteLine();
            this.WriteObjects("        }");
            this.WriteLine();
        }
    }
}
