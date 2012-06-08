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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst")]
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
#line 17 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
this.WriteObjects("");
#line 34 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
this.WriteObjects("\n");
this.WriteObjects("        public class ",  className , "Proxy\n");
this.WriteObjects("            : IProxyObject, ISortKey<int>\n");
this.WriteObjects("        {\n");
this.WriteObjects("            public ",  className , "Proxy()\n");
this.WriteObjects("            {\n");
#line 40 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
foreach(var p in nameAndInitialiserList) { 
#line 41 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
this.WriteObjects("                ",  p.Key , " = ",  p.Value , ";\n");
#line 42 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
} 
#line 43 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
this.WriteObjects("            }\n");
this.WriteObjects("\n");
this.WriteObjects("            public virtual int ID { get; set; }\n");
this.WriteObjects("\n");
this.WriteObjects("            public virtual Type ZetboxWrapper { get { return typeof(",  className , "",  ImplementationSuffix , "); } }\n");
this.WriteObjects("            public virtual Type ZetboxProxy { get { return typeof(",  className , "Proxy); } }\n");
this.WriteObjects("\n");
#line 50 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
foreach(var p in typeAndNameList) { 
#line 51 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
this.WriteObjects("            public virtual ",  p.Key , " ",  p.Value , " { get; set; }\n");
this.WriteObjects("\n");
#line 53 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
} 
#line 54 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClass.cst"
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("        // make proxy available for the provider\n");
this.WriteObjects("        public override IProxyObject NHibernateProxy { get { return Proxy; } }");

        }

    }
}