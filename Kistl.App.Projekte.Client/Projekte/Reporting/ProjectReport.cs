
namespace Kistl.App.Projekte.Client.Projekte.Reporting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public partial class ProjectReport
    {
        protected virtual void PageSetup()
        {
            Common.PageSetup.Call(Host, "Portrait");
        }

        protected virtual string GetTitle()
        {
            return "Hello Project";
        }
    }
}
