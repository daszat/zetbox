using System;
using System.Collections.Generic;
using System.Linq;


namespace Zetbox.App.Projekte.Client.Projekte.Reporting
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Zetbox\Zetbox.App.Projekte.Client\Projekte\Reporting\ProjectReport.cst")]
    public partial class ProjectReport : Zetbox.App.Projekte.Client.Projekte.Reporting.ReportTemplate
    {


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ProjectReport");
        }

        public ProjectReport(Arebis.CodeGeneration.IGenerationHost _host)
            : base(_host)
        {

        }

        public override void Generate()
        {
#line 8 "P:\Zetbox\Zetbox.App.Projekte.Client\Projekte\Reporting\ProjectReport.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\\document [\r\n");
#line 10 "P:\Zetbox\Zetbox.App.Projekte.Client\Projekte\Reporting\ProjectReport.cst"
Common.DocumentInfo.Call(Host, "Invoice", null); 
#line 11 "P:\Zetbox\Zetbox.App.Projekte.Client\Projekte\Reporting\ProjectReport.cst"
this.WriteObjects("] {\r\n");
#line 12 "P:\Zetbox\Zetbox.App.Projekte.Client\Projekte\Reporting\ProjectReport.cst"
Common.DocumentStyles.Call(Host); 
#line 13 "P:\Zetbox\Zetbox.App.Projekte.Client\Projekte\Reporting\ProjectReport.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\\section [\r\n");
#line 15 "P:\Zetbox\Zetbox.App.Projekte.Client\Projekte\Reporting\ProjectReport.cst"
PageSetup(); 
#line 16 "P:\Zetbox\Zetbox.App.Projekte.Client\Projekte\Reporting\ProjectReport.cst"
this.WriteObjects("	] {\r\n");
this.WriteObjects("        \\paragraph [ Style = \"Title\" Format { SpaceBefore = \"2cm\" } ] {\r\n");
this.WriteObjects("            ",  GetTitle() , "\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("}");

        }

    }
}