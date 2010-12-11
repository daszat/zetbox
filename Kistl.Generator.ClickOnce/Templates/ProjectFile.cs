
namespace Kistl.Generator.ClickOnce.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public partial class ProjectFile
    {
        protected virtual string GetAssemblyName()
        {
            return "Kistl.Client.ClickOnce";
        }

        protected virtual string GetPublishUrl()
        {
            // TODO: Hardcoded
            return "\\\\srv2008\\c%24\\inetpub\\wwwroot\\TestClickOnce\\";
        }

        protected virtual string GetInstallUrl()
        {
            // TODO: Hardcoded
            return "http://srv2008/TestClickOnce/";
        }

        /// <summary>
        /// Override this to add one or more Properties to a global &lt;PropertyGroup/>
        /// </summary>
        protected virtual void ApplyAdditionalProperties() { }

        /// <summary>
        /// Add additional assembly references. By default this only adds a reference to the currently generating interface assembly.
        /// 
        /// Extend this to add one or more &lt;References/>s to the project file
        /// </summary>
        protected virtual void ApplyAdditionalReferences()
        {

        }
    
        /// <summary>
        /// Override this to add one or more &lt;ItemGroup/>s to the project file
        /// </summary>
        protected virtual void ApplyAdditionalItemGroups() { }

        /// <summary>
        /// Override this to add one or more &lt;PropertyGroup/>s to the project file
        /// </summary>
        protected virtual void ApplyAdditionalPropertyGroups() { }
    }
}
