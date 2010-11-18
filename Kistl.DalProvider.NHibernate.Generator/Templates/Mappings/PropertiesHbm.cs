
namespace Kistl.DalProvider.NHibernate.Generator.Templates.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Kistl.API;
    using Kistl.App.Base;

    public partial class PropertiesHbm
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, IEnumerable<Property> properties)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (properties == null) { throw new ArgumentNullException("properties"); }

            host.CallTemplate("Mappings.PropertiesHbm", ctx, properties);
        }
    }
}
