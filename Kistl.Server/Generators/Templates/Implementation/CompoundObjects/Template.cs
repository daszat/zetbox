using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Server.Generators.Templates.Implementation.CompoundObjects
{
    /// <summary>
    /// A template for "CompoundObject".
    /// </summary>
    public class Template
        : Kistl.Server.Generators.Templates.Implementation.TypeBase
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

    }
}
