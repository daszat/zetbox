
namespace Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    public partial class ProxyClassChild
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string interfaceName, string parentInterfaceName, string parentProxyName, IEnumerable<KeyValuePair<string, string>> typeAndNameList)
        {
            if (_host == null) { throw new ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.ProxyClassChild", ctx, interfaceName, parentInterfaceName, parentProxyName, typeAndNameList);
        }
    }
}
