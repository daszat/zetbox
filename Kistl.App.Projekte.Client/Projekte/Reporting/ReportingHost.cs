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
        public ReportingHost(IFileOpener fileOpener)
            : base(null, null, fileOpener)
        {
        }

        // TODO: Move that class into a common reporting assembly and create a own derived class with configuration
        /// <summary>
        /// Creates a new reporting host
        /// </summary>
        /// <param name="overrideTemplateNamespace">null or empty, if default templates should be used, else a assembly with templates.</param>
        /// <param name="overrideTemplateAssembly">null, if default templates should be used, else a assembly with templates.</param>
        /// <param name="fileOpener"></param>
        public ReportingHost(string overrideTemplateNamespace, System.Reflection.Assembly overrideTemplateAssembly, IFileOpener fileOpener)
            : base(overrideTemplateNamespace, overrideTemplateAssembly, fileOpener)
        {
        }
    }
}
