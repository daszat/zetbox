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
using Zetbox.API.Common.Reporting;
using Zetbox.API;

namespace Zetbox.App.Projekte.Client.Projekte.Reporting
{
    public class ReportingHost : AbstractReportingHost
    {
        /// <summary>
        /// Creates a new reporting host
        /// </summary>
        /// <param name="fileOpener"></param>
        /// <param name="tmpService"></param>
        public ReportingHost(IFileOpener fileOpener, ITempFileService tmpService)
            : base(null, null, fileOpener, tmpService)
        {
        }

        /// <summary>
        /// Creates a new reporting host
        /// </summary>
        /// <param name="overrideTemplateNamespace">null or empty, if default templates should be used, else a assembly with templates.</param>
        /// <param name="overrideTemplateAssembly">null, if default templates should be used, else a assembly with templates.</param>
        /// <param name="fileOpener"></param>
        /// <param name="tmpService"></param>
        public ReportingHost(string overrideTemplateNamespace, System.Reflection.Assembly overrideTemplateAssembly, IFileOpener fileOpener, ITempFileService tmpService)
            : base(overrideTemplateNamespace, overrideTemplateAssembly, fileOpener, tmpService)
        {
        }
    }
}
