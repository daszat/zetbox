using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst")]
    public partial class ProxyClassChild : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected string interfaceName;
		protected string parentInterfaceName;
		protected string parentProxyName;
		protected IEnumerable<KeyValuePair<string, string>> typeAndNameList;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string interfaceName, string parentInterfaceName, string parentProxyName, IEnumerable<KeyValuePair<string, string>> typeAndNameList)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.ProxyClassChild", ctx, interfaceName, parentInterfaceName, parentProxyName, typeAndNameList);
        }

        public ProxyClassChild(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string interfaceName, string parentInterfaceName, string parentProxyName, IEnumerable<KeyValuePair<string, string>> typeAndNameList)
            : base(_host)
        {
			this.ctx = ctx;
			this.interfaceName = interfaceName;
			this.parentInterfaceName = parentInterfaceName;
			this.parentProxyName = parentProxyName;
			this.typeAndNameList = typeAndNameList;

        }

        public override void Generate()
        {
#line 19 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public interface ",  interfaceName , "Interface\r\n");
this.WriteObjects("            : ",  parentInterfaceName , "\r\n");
this.WriteObjects("        {\r\n");
#line 23 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst"
foreach(var p in typeAndNameList) { 
#line 24 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst"
this.WriteObjects("            ",  p.Key , " ",  p.Value , " { get; set; }\r\n");
this.WriteObjects("\r\n");
#line 26 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst"
} 
#line 27 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public class ",  interfaceName , "Proxy\r\n");
this.WriteObjects("            : ",  parentProxyName , ", ",  interfaceName , "Interface\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("			public override Type Interface { get { return typeof(",  interfaceName , "Interface); } }\r\n");
this.WriteObjects("			public override Type ZBoxWrapper { get { return typeof(",  interfaceName , "",  ImplementationSuffix , "); } }\r\n");
this.WriteObjects("\r\n");
#line 35 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst"
foreach(var p in typeAndNameList) { 
#line 36 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst"
this.WriteObjects("            public virtual ",  p.Key , " ",  p.Value , " { get; set; }\r\n");
this.WriteObjects("\r\n");
#line 38 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst"
} 
#line 39 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst"
this.WriteObjects("        }\r\n");

        }

    }
}