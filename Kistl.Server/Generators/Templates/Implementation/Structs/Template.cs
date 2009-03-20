using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Server.Generators.Templates.Implementation.Structs
{
    /// <summary>
    /// A template for "structs".
    /// </summary>
    public class Template
        : Kistl.Server.Generators.Templates.Implementation.TypeBase
    {
        protected Struct StructType { get; private set; }

        public Template(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Struct structType)
            : base(_host, ctx, structType)
        {
            this.StructType = structType;
        }

        protected override string[] GetInterfaces()
        {
            return base.GetInterfaces().Concat(new string[] { "IStruct" }).ToArray();
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();
        }
    }
}
