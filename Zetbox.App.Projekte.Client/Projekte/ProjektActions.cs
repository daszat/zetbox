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
using System.Text;
using Zetbox.API;
using Zetbox.App.Projekte.Client.Projekte.Reporting;

namespace Zetbox.App.Projekte
{
    /// <summary>
    /// Client implementation
    /// </summary>
    [Implementor]
    [CLSCompliant(false)]
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
