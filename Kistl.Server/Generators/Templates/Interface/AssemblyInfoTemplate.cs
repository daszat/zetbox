using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.Templates.Interface
{
    public partial class AssemblyInfoTemplate
    {
        public virtual string GetAssemblyTitle()
        {
            return "Kistl.Objects";
        }

        public virtual void ApplyAdditionalAssemblyInfo()
        {
        }
    }
}
