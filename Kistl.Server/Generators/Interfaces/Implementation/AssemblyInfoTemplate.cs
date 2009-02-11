using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.Interfaces.Implementation
{
    public class AssemblyInfoTemplate
        : Templates.Interface.AssemblyInfoTemplate
    {
        public AssemblyInfoTemplate(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx)
            : base(_host, ctx)
        {
        }
    }
}
