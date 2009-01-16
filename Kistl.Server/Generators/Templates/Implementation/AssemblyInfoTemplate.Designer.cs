

namespace Kistl.Server.Generators.Templates.Implementation
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\AssemblyInfoTemplate.cst")]
    public partial class AssemblyInfoTemplate : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected Kistl.API.IKistlContext ctx;


        public AssemblyInfoTemplate(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }
        
        public override void Generate()
        {
#line 7 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\AssemblyInfoTemplate.cst"
this.WriteObjects("[assembly: System.Reflection.AssemblyTitleAttribute(\"",  GetAssemblyTitle() , "\")]\r\n");
this.WriteObjects("[assembly: System.Reflection.AssemblyCompanyAttribute(\"dasz.at\")]\r\n");
this.WriteObjects("[assembly: System.Reflection.AssemblyProductAttribute(\"Kistl\")]\r\n");
this.WriteObjects("[assembly: System.Reflection.AssemblyCopyrightAttribute(\"Copyright © dasz.at 2008-2009\")]\r\n");
this.WriteObjects("[assembly: System.Reflection.AssemblyVersionAttribute(\"1.0.0.0\")]\r\n");
this.WriteObjects("[assembly: System.Reflection.AssemblyFileVersionAttribute(\"1.0.0.0\")]\r\n");
this.WriteObjects("[assembly: System.Runtime.InteropServices.ComVisibleAttribute(false)]\r\n");
this.WriteObjects("[assembly: System.CLSCompliantAttribute(true)]\r\n");
#line 16 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\AssemblyInfoTemplate.cst"
ApplyAdditionalAssemblyInfo();


        }



    }
}