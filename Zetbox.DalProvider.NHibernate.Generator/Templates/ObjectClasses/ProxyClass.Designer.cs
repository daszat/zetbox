using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst")]
    public partial class ProxyClass : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected string className;
		protected IEnumerable<KeyValuePair<string, string>> nameAndInitialiserList;
		protected IEnumerable<KeyValuePair<string, string>> typeAndNameList;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string className, IEnumerable<KeyValuePair<string, string>> nameAndInitialiserList, IEnumerable<KeyValuePair<string, string>> typeAndNameList)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.ProxyClass", ctx, className, nameAndInitialiserList, typeAndNameList);
        }

        public ProxyClass(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string className, IEnumerable<KeyValuePair<string, string>> nameAndInitialiserList, IEnumerable<KeyValuePair<string, string>> typeAndNameList)
            : base(_host)
        {
			this.ctx = ctx;
			this.className = className;
			this.nameAndInitialiserList = nameAndInitialiserList;
			this.typeAndNameList = typeAndNameList;

        }

        public override void Generate()
        {
#line 18 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public class ",  className , "Proxy\r\n");
this.WriteObjects("            : IProxyObject, ISortKey<int>\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            public ",  className , "Proxy()\r\n");
this.WriteObjects("            {\r\n");
#line 24 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
foreach(var p in nameAndInitialiserList) { 
#line 25 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
this.WriteObjects("                ",  p.Key , " = ",  p.Value , ";\r\n");
#line 26 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
} 
#line 27 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            public virtual int ID { get; set; }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            public virtual Type ZetboxWrapper { get { return typeof(",  className , "",  ImplementationSuffix , "); } }\r\n");
this.WriteObjects("            public virtual Type ZetboxProxy { get { return typeof(",  className , "Proxy); } }\r\n");
this.WriteObjects("\r\n");
#line 34 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
foreach(var p in typeAndNameList) { 
#line 35 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
this.WriteObjects("            public virtual ",  p.Key , " ",  p.Value , " { get; set; }\r\n");
this.WriteObjects("\r\n");
#line 37 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
} 
#line 38 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        // make proxy available for the provider\r\n");
this.WriteObjects("        public override IProxyObject NHibernateProxy { get { return Proxy; } }");

        }

    }
}