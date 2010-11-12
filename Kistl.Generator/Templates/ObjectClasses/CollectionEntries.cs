
namespace Kistl.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    public partial class CollectionEntries
    {
        protected virtual IEnumerable<string> GetAdditionalImports()
        {
            return RequiredNamespaces;
        }
    }
}
