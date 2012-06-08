
namespace Zetbox.Generator.InterfaceTemplates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AssemblyInfoTemplate
        : Templates.AssemblyInfoTemplate
    {
        public AssemblyInfoTemplate(Arebis.CodeGeneration.IGenerationHost _host, Zetbox.API.IZetboxContext ctx)
            : base(_host, ctx)
        {
        }

        public override void ApplyAdditionalAssemblyInfo()
        {
            base.ApplyAdditionalAssemblyInfo();
            this.WriteLine("[assembly: System.CLSCompliantAttribute(true)]");
            this.WriteLine("[assembly: Zetbox.API.ZetboxGeneratedVersion(\"" + Guid.NewGuid() + "\")]");
        }
    }
}
