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

namespace Zetbox.App.Projekte.Client.Projekte.Reporting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Common.Reporting;

    public class ReportingHost : AbstractReportingHost
    {
        /// <summary>
        /// Creates a new reporting host
        /// </summary>
        public ReportingHost(IFileOpener fileOpener, ITempFileService tempFileService, IReportingErrorReporter errorReporter)
            : base(null, null, fileOpener, tempFileService, errorReporter)
        {
        }

        /// <summary>
        /// Creates a new reporting host
        /// </summary>
        /// <param name="overrideTemplateNamespace">null or empty, if default templates should be used, else a assembly with templates.</param>
        /// <param name="overrideTemplateAssembly">null, if default templates should be used, else a assembly with templates.</param>
        /// <param name="fileOpener"></param>
        /// <param name="tempFileService"></param>
        /// <param name="errorReporter"></param>
        public ReportingHost(string overrideTemplateNamespace, System.Reflection.Assembly overrideTemplateAssembly, IFileOpener fileOpener, ITempFileService tempFileService, IReportingErrorReporter errorReporter)
            : base(overrideTemplateNamespace, overrideTemplateAssembly, fileOpener, tempFileService, errorReporter)
        {
        }
    }
}
