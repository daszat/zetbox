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
    [Arebis.CodeGeneration.TemplateInfo(@"/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/ObjectClasses/ProxyClass.cst")]
    public partial class ProxyClass : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected string className;
		protected IEnumerable<KeyValuePair<string, string>> nameAndInitialiserList;
		protected IEnumerable<KeyValuePair<string, string>> typeAndNameList;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string className, IEnumerable<KeyValuePair<string, string>> nameAndInitialiserList, IEnumerable<KeyValuePair<string, string>> typeAndNameList)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.ProxyClass", ctx, className, nameAndInitialiserList, typeAndNameList);
        }

        public ProxyClass(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string className, IEnumerable<KeyValuePair<string, string>> nameAndInitialiserList, IEnumerable<KeyValuePair<string, string>> typeAndNameList)
            : base(_host)
        {
			this.ctx = ctx;
			this.className = className;
			this.nameAndInitialiserList = nameAndInitialiserList;
			this.typeAndNameList = typeAndNameList;

        }

        public override void Generate()
        {
#line 18 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/ObjectClasses/ProxyClass.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public class ",  className , "Proxy\r\n");
this.WriteObjects("            : IProxyObject\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            public ",  className , "Proxy()\r\n");
this.WriteObjects("            {\r\n");
#line 24 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/ObjectClasses/ProxyClass.cst"
foreach(var p in nameAndInitialiserList) { 
#line 25 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/ObjectClasses/ProxyClass.cst"
this.WriteObjects("                ",  p.Key , " = ",  p.Value , ";\r\n");
#line 26 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/ObjectClasses/ProxyClass.cst"
} 
#line 27 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/ObjectClasses/ProxyClass.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            public virtual int ID { get; set; }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            public virtual Type ZBoxWrapper { get { return typeof(",  className , "",  ImplementationSuffix , "); } }\r\n");
this.WriteObjects("            public virtual Type ZBoxProxy { get { return typeof(",  className , "Proxy); } }\r\n");
this.WriteObjects("\r\n");
#line 34 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/ObjectClasses/ProxyClass.cst"
foreach(var p in typeAndNameList) { 
#line 35 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/ObjectClasses/ProxyClass.cst"
this.WriteObjects("            public virtual ",  p.Key , " ",  p.Value , " { get; set; }\r\n");
this.WriteObjects("\r\n");
#line 37 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/ObjectClasses/ProxyClass.cst"
} 
#line 38 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/ObjectClasses/ProxyClass.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        // make proxy available for the provider\r\n");
this.WriteObjects("        public override IProxyObject NHibernateProxy { get { return Proxy; } }");

        }

    }
}