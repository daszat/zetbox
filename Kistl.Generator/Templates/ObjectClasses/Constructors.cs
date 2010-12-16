
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
        //public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string className, IEnumerable<CompoundObjectProperty> compoundObjectProperties)
        //{
        //    if (_host == null) { throw new ArgumentNullException("_host"); }

        //    _host.CallTemplate("ObjectClasses.Constructors", ctx, className, compoundObjectProperties);
        //}

        public virtual void ApplyCompoundObjectPropertyInitialisers()
        {
            Templates.Properties.CompoundObjectPropertyInitialisation.Call(Host, ctx, compoundObjectProperties, ImplementationSuffix, ImplementationPropertySuffix);
        }
    }
}
