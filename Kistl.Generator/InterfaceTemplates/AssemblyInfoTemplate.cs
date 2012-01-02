
namespace Kistl.Generator.InterfaceTemplates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AssemblyInfoTemplate
        : Templates.AssemblyInfoTemplate
    {
        public AssemblyInfoTemplate(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx)
            : base(_host, ctx)
        {
        }

        public override void ApplyAdditionalAssemblyInfo()
        {
            base.ApplyAdditionalAssemblyInfo();
            this.WriteLine("[assembly: System.CLSCompliantAttribute(true)]");
            this.WriteLine("[assembly: Kistl.API.KistlGeneratedVersion(\"" + Guid.NewGuid() + "\")]");
        }
    }
}
