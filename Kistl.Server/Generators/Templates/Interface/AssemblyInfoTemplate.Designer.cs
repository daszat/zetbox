

namespace Kistl.Server.Generators.Templates.Interface
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Interface\AssemblyInfoTemplate.cst")]
    public partial class AssemblyInfoTemplate : Kistl.Server.Generators.KistlCodeTemplate
    {


        public AssemblyInfoTemplate(Arebis.CodeGeneration.IGenerationHost _host)
            : base(_host)
        {

        }
        
        public override void Generate()
        {
#line 6 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\AssemblyInfoTemplate.cst"
this.WriteObjects("[assembly: System.Reflection.AssemblyTitleAttribute(\"",  GetAssemblyTitle() , "\")]\r\n");
this.WriteObjects("[assembly: System.Reflection.AssemblyCompanyAttribute(\"dasz.at\")]\r\n");
this.WriteObjects("[assembly: System.Reflection.AssemblyProductAttribute(\"Kistl\")]\r\n");
this.WriteObjects("[assembly: System.Reflection.AssemblyCopyrightAttribute(\"Copyright © dasz.at 2008-2009\")]\r\n");
this.WriteObjects("[assembly: System.Reflection.AssemblyVersionAttribute(\"1.0.0.0\")]\r\n");
this.WriteObjects("[assembly: System.Reflection.AssemblyFileVersionAttribute(\"1.0.0.0\")]\r\n");
this.WriteObjects("[assembly: System.Runtime.InteropServices.ComVisibleAttribute(false)]\r\n");
this.WriteObjects("[assembly: System.CLSCompliantAttribute(true)]\r\n");
#line 15 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\AssemblyInfoTemplate.cst"
ApplyAdditionalAssemblyInfo();


        }



    }
}