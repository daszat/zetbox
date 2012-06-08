using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zetbox.App.Projekte.Client.DerivedReportTest
{
    public class ProjectReport : Zetbox.App.Projekte.Client.Projekte.Reporting.ProjectReport
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
