using System;
using System.Collections.Generic;
using System.Linq;


namespace Zetbox.App.Tests.Client.Projekte.Reporting.Common
{
    [Arebis.CodeGeneration.TemplateInfo(@"C:\Projects\zetbox\Tests\Zetbox.App.Tests.Client\Projekte\Reporting\Common\PageSetup.cst")]
    public partial class PageSetup : Zetbox.App.Tests.Client.Projekte.Reporting.ReportTemplate
    {
		protected string Orientation;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, string Orientation)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Common.PageSetup", Orientation);
        }

        public PageSetup(Arebis.CodeGeneration.IGenerationHost _host, string Orientation)
            : base(_host)
        {
			this.Orientation = Orientation;

        }

        public override void Generate()
        {
#line 25 "C:\Projects\zetbox\Tests\Zetbox.App.Tests.Client\Projekte\Reporting\Common\PageSetup.cst"
this.WriteObjects("\r\n");
this.WriteObjects("PageSetup\r\n");
this.WriteObjects("{\r\n");
this.WriteObjects("	Orientation = ",  Orientation , "\r\n");
this.WriteObjects("	PageFormat = A4\r\n");
this.WriteObjects("	TopMargin = 40\r\n");
this.WriteObjects("	StartingNumber = 1\r\n");
this.WriteObjects("}\r\n");

        }

    }
}