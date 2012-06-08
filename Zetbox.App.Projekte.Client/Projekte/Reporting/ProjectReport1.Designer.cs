// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
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