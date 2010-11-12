using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.Generator.Extensions;

namespace Kistl.DalProvider.Ef.Generator.Templates
{
    public class AssemblyInfoTemplate : Kistl.Generator.Templates.AssemblyInfoTemplate
    {

        public AssemblyInfoTemplate(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx)
            : base(_host, ctx)
        {
        }

        public override string GetAssemblyTitle()
        {
            return "Kistl.Server.Objects";
        }

        public override void ApplyAdditionalAssemblyInfo()
        {
            WriteLine("[assembly: System.Data.Objects.DataClasses.EdmSchema]");
        }

    }
}


