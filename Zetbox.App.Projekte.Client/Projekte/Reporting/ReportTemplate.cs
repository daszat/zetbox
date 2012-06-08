using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arebis.CodeGeneration;

namespace Kistl.App.Projekte.Client.Projekte.Reporting
{
    public class ReportTemplate : Kistl.API.Common.Reporting.ReportTemplate
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
