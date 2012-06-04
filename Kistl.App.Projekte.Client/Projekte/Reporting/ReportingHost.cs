using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API.Common.Reporting;
using Kistl.API;

namespace Kistl.App.Projekte.Client.Projekte.Reporting
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
