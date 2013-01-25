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

namespace Zetbox.API.Common.Reporting
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using MigraDoc.DocumentObjectModel.IO;
    using Zetbox.API.Utils;

    [CLSCompliant(false)]
    public interface IReportingErrorReporter
    {
        void ReportErrors(DdlReaderErrors errors, Exception ex, Stream mddl);
    }

    [CLSCompliant(false)]
    public class LoggingErrorReporter : IReportingErrorReporter
    {
        public void ReportErrors(DdlReaderErrors errors, Exception ex, Stream mddl)
        {
            if (ex != null)
            {
                Logging.Log.Error("Unable to create report:", ex);
            }

            if (errors != null && errors.ErrorCount > 0)
            {
                StringBuilder sb = new StringBuilder();

                foreach (DdlReaderError e in errors)
                {
                    switch (e.ErrorLevel)
                    {
                        case DdlErrorLevel.Error:
                            sb.Append("E: ");
                            break;
                        case DdlErrorLevel.Warning:
                            sb.Append("W: ");
                            break;
                        default:
                            sb.Append("?: ");
                            break;
                    }
                    sb.AppendLine(e.ToString());
                }
                Logging.Log.Info(sb.ToString());
            }
        }
    }
}
