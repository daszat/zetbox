using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using Zetbox.App.Projekte.Client.Projekte.Reporting;

namespace Zetbox.App.Projekte
{
    /// <summary>
    /// Client implementation
    /// </summary>
    [Implementor]
    public class ProjektActions
    {
        private static Func<ReportingHost> _rptFactory;
        public ProjektActions(Func<ReportingHost> rptFactory)
        {
            _rptFactory = rptFactory;
        }

        [Invocation]
        public static void GetSummaryReport(Projekt obj, MethodReturnEventArgs<System.Object> e, string title, Zetbox.App.Base.DateTimeRange range)
        {
            using (var rpt = _rptFactory())
            {
                ProjectReport.Call(rpt);
                rpt.Open("ProjectReport.pdf");
            }
        }
    }
}
