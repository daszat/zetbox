using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.Templates.Implementation
{
    public partial class AssemblyInfoTemplate
    {
        public virtual string GetAssemblyTitle()
        {
            return "Badly configured implementation Assembly";
        }

        public virtual void ApplyAdditionalAssemblyInfo()
        {
        }
    }
}
