using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.Templates.Interface.CollectionEntries
{
    public abstract partial class Template
    {
        protected abstract string GetCeClassName();

        /// <returns>which CollectionEntry interface to implement.</returns>
        protected abstract string GetCeInterface();

        /// <returns>true, if any side is ordered</returns>
        protected abstract bool IsOrdered();

        /// <returns>an one-line description of this interface</returns>
        protected abstract string GetDescription();

        protected virtual IEnumerable<string> GetAdditionalImports()
        {
            return new string[] { };
        }
    }
}
