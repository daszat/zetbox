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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst")]
    public partial class Constructors : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected IEnumerable<CompoundObjectProperty> compoundObjectProperties;
		protected string interfaceName;
		protected string className;
		protected string baseClassName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, IEnumerable<CompoundObjectProperty> compoundObjectProperties, string interfaceName, string className, string baseClassName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.Constructors", ctx, compoundObjectProperties, interfaceName, className, baseClassName);
        }

        public Constructors(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, IEnumerable<CompoundObjectProperty> compoundObjectProperties, string interfaceName, string className, string baseClassName)
            : base(_host)
        {
			this.ctx = ctx;
			this.compoundObjectProperties = compoundObjectProperties;
			this.interfaceName = interfaceName;
			this.className = className;
			this.baseClassName = baseClassName;

        }

        public override void Generate()
        {
#line 19 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("		public ",  className , "()\r\n");
this.WriteObjects("			: this(null)\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>Create a new unattached instance</summary>\r\n");
this.WriteObjects("        public ",  className , "(Func<IFrozenContext> lazyCtx)\r\n");
this.WriteObjects("            : this(lazyCtx, new ",  interfaceName , "Proxy())\r\n");
this.WriteObjects("        {\r\n");
#line 28 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
ApplyCompoundObjectPropertyInitialisers(); 
#line 29 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>Create a instance, wrapping the specified proxy</summary>\r\n");
this.WriteObjects("        public ",  className , "(Func<IFrozenContext> lazyCtx, ",  interfaceName , "Proxy proxy)\r\n");
this.WriteObjects("            : base(lazyCtx)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            this.Proxy = proxy;\r\n");
#line 36 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
ApplyCompoundObjectPropertyInitialisers(); 
#line 37 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("        /// <summary>the NHibernate proxy of the represented entity</summary>\r\n");
this.WriteObjects("        private readonly ",  interfaceName , "Proxy Proxy;\r\n");

        }

    }
}