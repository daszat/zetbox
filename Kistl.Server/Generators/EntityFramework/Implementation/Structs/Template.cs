using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.EntityFramework.Implementation.Structs
{
    public class Template
        : Kistl.Server.Generators.Templates.Implementation.Structs.Template
    {
        public Template(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Struct s)
            : base(_host, ctx, s)
        {
        }

        protected override void ApplyIDPropertyTemplate()
        {
            this.WriteLine("/// <summary>A special value denoting a empty struct</summary>");
            this.WriteObjects("public static ", this.StructType.ClassName, Kistl.API.Helper.ImplementationSuffix, " NoValue { get { return null; } }");
            this.WriteLine();
        }

        protected override void ApplyClassAttributeTemplate()
        {
            WriteLine("    [EdmComplexType(NamespaceName=\"Model\", Name=\"{0}\")]", this.StructType.ClassName);
        }

        protected override IEnumerable<string> GetAdditionalImports()
        {
            return base.GetAdditionalImports().Concat(new string[]{
                "Kistl.API.Server",
                "Kistl.DALProvider.EF",
                "System.Data.Objects",
                "System.Data.Objects.DataClasses" 
            });
        }

        protected override string MungeClassName(string name)
        {
            return base.MungeClassName(name) + "__Implementation__";
        }

        /// <returns>The base class to inherit from.</returns>
        protected override string GetBaseClass()
        {
            return "BaseServerStructObject_EntityFramework";
        }

    }
}
