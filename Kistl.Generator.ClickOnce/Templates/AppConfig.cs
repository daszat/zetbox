
namespace Kistl.Generator.ClickOnce.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public partial class AppConfig
    {
        protected virtual string GetServerAddress()
        {
            // TODO: Hardcoded
            return "http://localhost:6666/KistlService";
        }
    }
}
