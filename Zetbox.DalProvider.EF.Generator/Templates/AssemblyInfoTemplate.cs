using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Zetbox.Generator.Extensions;

namespace Zetbox.DalProvider.Ef.Generator.Templates
{
    public class AssemblyInfoTemplate : Zetbox.Generator.Templates.AssemblyInfoTemplate
    {

        public AssemblyInfoTemplate(Arebis.CodeGeneration.IGenerationHost _host, Zetbox.API.IZetboxContext ctx)
            : base(_host, ctx)
        {
        }

        public override string GetAssemblyTitle()
        {
            return "Zetbox.Server.Objects";
        }

        public override void ApplyAdditionalAssemblyInfo()
        {
            WriteLine("[assembly: System.Data.Objects.DataClasses.EdmSchema]");
        }

    }
}


