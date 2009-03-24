using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Server.Generators.ClientObjects.Implementation.Structs
{
    public class Template
        : Kistl.Server.Generators.Templates.Implementation.Structs.Template
    {
        public Template(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Struct s)
            : base(_host, ctx, s)
        {
        }

        protected override IEnumerable<string> GetAdditionalImports()
        {
            return base.GetAdditionalImports().Concat(new string[]{
                "Kistl.API.Client",
            });
        }

        protected override string MungeClassName(string name)
        {
            return base.MungeClassName(name) + "__Implementation__";
        }

        /// <returns>The base class to inherit from.</returns>
        protected override string GetBaseClass()
        {
            return "BaseClientStructObject";
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();

            string clsName = this.GetTypeName();

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
