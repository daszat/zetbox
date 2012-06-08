using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arebis.CodeGeneration;

namespace Zetbox.App.Projekte.Client.Projekte.Reporting
{
    public class ReportTemplate : Zetbox.API.Common.Reporting.ReportTemplate
    {
        public ReportTemplate(IGenerationHost host)
            : base(host)
        {
        }

        protected virtual string FormatProjectName(Projekt prj)
        {
            return prj.Name;
        }
    }
}
