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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst")]
    public partial class ProxyClassChild : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected string className;
		protected string parentClassName;
		protected IEnumerable<KeyValuePair<string, string>> nameAndInitialiserList;
		protected IEnumerable<KeyValuePair<string, string>> typeAndNameList;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string className, string parentClassName, IEnumerable<KeyValuePair<string, string>> nameAndInitialiserList, IEnumerable<KeyValuePair<string, string>> typeAndNameList)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.ProxyClassChild", ctx, className, parentClassName, nameAndInitialiserList, typeAndNameList);
        }

        public ProxyClassChild(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string className, string parentClassName, IEnumerable<KeyValuePair<string, string>> nameAndInitialiserList, IEnumerable<KeyValuePair<string, string>> typeAndNameList)
            : base(_host)
        {
			this.ctx = ctx;
			this.className = className;
			this.parentClassName = parentClassName;
			this.nameAndInitialiserList = nameAndInitialiserList;
			this.typeAndNameList = typeAndNameList;

        }

        public override void Generate()
        {
#line 35 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public class ",  className , "Proxy\r\n");
this.WriteObjects("            : ",  parentClassName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            public ",  className , "Proxy()\r\n");
this.WriteObjects("            {\r\n");
#line 41 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst"
foreach(var p in nameAndInitialiserList) { 
#line 42 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst"
this.WriteObjects("                ",  p.Key , " = ",  p.Value , ";\r\n");
#line 43 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst"
} 
#line 44 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            public override Type ZetboxWrapper { get { return typeof(",  className , "",  ImplementationSuffix , "); } }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            public override Type ZetboxProxy { get { return typeof(",  className , "Proxy); } }\r\n");
this.WriteObjects("\r\n");
#line 50 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst"
foreach(var p in typeAndNameList) { 
#line 51 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst"
this.WriteObjects("            public virtual ",  p.Key , " ",  p.Value , " { get; set; }\r\n");
this.WriteObjects("\r\n");
#line 53 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst"
} 
#line 54 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst"
this.WriteObjects("        }\r\n");

        }

    }
}