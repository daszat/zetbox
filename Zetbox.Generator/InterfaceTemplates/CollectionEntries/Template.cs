
namespace Zetbox.Generator.InterfaceTemplates.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator.Extensions;

    public abstract partial class Template
    {
        protected abstract string GetDefinitionGuid();

        protected abstract string GetCeClassName();

        /// <returns>which CollectionEntry interface to implement.</returns>
        protected abstract string GetCeInterface();

        /// <returns>true, if any side is ordered</returns>
        protected abstract bool IsOrdered();

        /// <returns>an one-line description of this interface</returns>
        protected abstract string GetDescription();

        protected virtual IEnumerable<string> GetAdditionalImports()
        {
            return RequiredNamespaces;
        }
    }
}
