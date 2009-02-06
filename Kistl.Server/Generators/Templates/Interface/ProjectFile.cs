using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.Templates.Interface
{
    public partial class ProjectFile
    {

        protected virtual string GetAssemblyName()
        {
            return "Kistl.Objects";
        }

        /// <summary>
        /// Override this to add one or more &lt;References/>s to the prject file
        /// </summary>
        protected virtual void ApplyAdditionalReferences() { }

        /// <summary>
        /// Override this to add one or more &lt;ItemGroup/>s to the prject file
        /// </summary>
        protected virtual void ApplyAdditionalItemGroups() { }
    }
}
