
namespace Kistl.DalProvider.Memory.Generator.Implementation.CompoundObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Templates = Kistl.Server.Generators.Templates;

    public class Template
        : Templates.Implementation.CompoundObjects.Template
    {
        public Template(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, CompoundObject s)
            : base(_host, ctx, s)
        {
        }

        //protected override IEnumerable<string> GetAdditionalImports()
        //{
        //    return base.GetAdditionalImports().Concat(new string[]{
        //        "Kistl.API", // exists already 
        //    });
        //}

        protected override string MungeClassName(string name)
        {
            return base.MungeClassName(name)
                + Kistl.API.Helper.ImplementationSuffix
                + MemoryGenerator.ExtraSuffix;
        }

        /// <returns>The base class to inherit from.</returns>
        protected override string GetBaseClass()
        {
            return "BaseCompoundObject";
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();

            string clsName = this.GetTypeName();

            // attach compound to parent object
            this.WriteObjects("        public ", clsName, "(IPersistenceObject parent, string property)");
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
