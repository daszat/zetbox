using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.App.Projekte.Client.DerivedReportTest
{
    public class ProjectReport : Kistl.App.Projekte.Client.Projekte.Reporting.ProjectReport
    {
        public ProjectReport(Arebis.CodeGeneration.IGenerationHost _host)
            : base(_host)
        {

        }

        protected override string GetTitle()
        {
            return base.GetTitle() + " derived";
        }
    }
}
