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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst")]
    public partial class ProxyClass : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected string interfaceName;
		protected IEnumerable<KeyValuePair<string, string>> typeAndNameList;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string interfaceName, IEnumerable<KeyValuePair<string, string>> typeAndNameList)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.ProxyClass", ctx, interfaceName, typeAndNameList);
        }

        public ProxyClass(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string interfaceName, IEnumerable<KeyValuePair<string, string>> typeAndNameList)
            : base(_host)
        {
			this.ctx = ctx;
			this.interfaceName = interfaceName;
			this.typeAndNameList = typeAndNameList;

        }

        public override void Generate()
        {
#line 17 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public interface ",  interfaceName , "Interface\r\n");
this.WriteObjects("            : IProxyObject\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            // int ID { get; set; }\r\n");
this.WriteObjects("\r\n");
#line 23 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
foreach(var p in typeAndNameList) { 
#line 24 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
this.WriteObjects("            ",  p.Key , " ",  p.Value , " { get; set; }\r\n");
this.WriteObjects("\r\n");
#line 26 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
} 
#line 27 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public class ",  interfaceName , "Proxy\r\n");
this.WriteObjects("            : ",  interfaceName , "Interface, IProxyObject\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            public virtual int ID { get; set; }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("			public virtual Type Interface { get { return typeof(",  interfaceName , "Interface); } }\r\n");
this.WriteObjects("			public virtual Type ZBoxWrapper { get { return typeof(",  interfaceName , "",  ImplementationSuffix , "); } }\r\n");
this.WriteObjects("\r\n");
#line 37 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
foreach(var p in typeAndNameList) { 
#line 38 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
this.WriteObjects("            public virtual ",  p.Key , " ",  p.Value , " { get; set; }\r\n");
this.WriteObjects("\r\n");
#line 40 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
} 
#line 41 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
this.WriteObjects("        }\r\n");

        }

    }
}