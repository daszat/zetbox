using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.FrozenObjects.Implementation.Structs
{
    public class Template
        : Templates.Implementation.Structs.Template
    {
        public Template(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Struct s)
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
            return "BaseFrozenStruct";
        }

    }
}
