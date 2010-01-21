
namespace Kistl.DalProvider.Frozen.Generator.Implementation.CompoundObjects
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

        protected override IEnumerable<string> GetAdditionalImports()
        {
            return base.GetAdditionalImports().Concat(new string[]{
                "Kistl.DalProvider.Frozen"
            });
        }

        protected override string MungeClassName(string name)
        {
            return base.MungeClassName(name) + Kistl.API.Helper.ImplementationSuffix;
        }

        /// <returns>The base class to inherit from.</returns>
        protected override string GetBaseClass()
        {
            return "BaseFrozenCompoundObject";
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();
            // implement internal constructor to allow the FrozenContext to initialize the objects
            this.WriteObjects("        internal ", this.GetTypeName(), "(int id)");
            this.WriteLine();
            this.WriteObjects("            : base(id)");
            this.WriteLine();
            this.WriteLine("        { }");
        }
    }
}
