
namespace Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Templates = Kistl.Generator.Templates;

    public partial class Constructors
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, IEnumerable<CompoundObjectProperty> compoundObjectProperties, string interfaceName, string className, string baseClassName)
        {
            if (_host == null) { throw new ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.Constructors", ctx, compoundObjectProperties, interfaceName, className, baseClassName);
        }

        public virtual void ApplyCompoundObjectPropertyInitialisers()
        {
            //Templates.Properties.CompoundObjectPropertyInitialisation.Call(Host, ctx, compoundObjectProperties, ImplementationSuffix, ImplementationPropertySuffix);
        }
    }
}
