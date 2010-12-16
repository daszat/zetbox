
namespace Kistl.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;

    public partial class Constructors
    {
        public virtual void ApplyCompoundObjectPropertyInitialisers()
        {
            Templates.Properties.CompoundObjectPropertyInitialisation.Call(Host, ctx, compoundObjectProperties, ImplementationSuffix, ImplementationPropertySuffix);
        }
    }
}
