using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.ClientObjects.Implementation
{
    public class AssemblyInfoTemplate : Kistl.Server.Generators.Templates.Implementation.AssemblyInfoTemplate
    {

        public AssemblyInfoTemplate(Arebis.CodeGeneration.IGenerationHost _host)
            : base(_host)
        {
        }

        public override string GetAssemblyTitle()
        {
            return "Kistl.Client.Objects";
        }

    }
}
