
namespace Kistl.Generator.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public partial class AssemblyInfoTemplate
    {
        public virtual string GetAssemblyTitle()
        {
            return this.ImplementationNamespace;
        }

        public virtual void ApplyAdditionalAssemblyInfo()
        {
        }
    }
}
