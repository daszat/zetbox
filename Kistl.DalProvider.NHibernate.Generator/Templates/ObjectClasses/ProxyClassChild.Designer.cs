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
		protected string className;
		protected string parentClassName;
		protected IEnumerable<KeyValuePair<string, string>> typeAndNameList;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string className, string parentClassName, IEnumerable<KeyValuePair<string, string>> typeAndNameList)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.ProxyClassChild", ctx, className, parentClassName, typeAndNameList);
        }

        public ProxyClassChild(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string className, string parentClassName, IEnumerable<KeyValuePair<string, string>> typeAndNameList)
            : base(_host)
        {
			this.ctx = ctx;
			this.className = className;
			this.parentClassName = parentClassName;
			this.typeAndNameList = typeAndNameList;

        }

        public override void Generate()
        {
#line 18 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public class ",  className , "Proxy\r\n");
this.WriteObjects("            : ",  parentClassName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            public override Type ZBoxWrapper { get { return typeof(",  className , "",  ImplementationSuffix , "); } }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("			public override Type ZBoxProxy { get { return typeof(",  className , "Proxy); } }\r\n");
this.WriteObjects("\r\n");
#line 26 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst"
foreach(var p in typeAndNameList) { 
#line 27 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst"
this.WriteObjects("            public virtual ",  p.Key , " ",  p.Value , " { get; set; }\r\n");
this.WriteObjects("\r\n");
#line 29 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst"
} 
#line 30 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ProxyClassChild.cst"
this.WriteObjects("        }\r\n");

        }

    }
}