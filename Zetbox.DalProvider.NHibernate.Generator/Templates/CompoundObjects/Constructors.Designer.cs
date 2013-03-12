using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.CompoundObjects
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CompoundObjects\Constructors.cst")]
    public partial class Constructors : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected IEnumerable<CompoundObjectProperty> compoundObjectProperties;
		protected string interfaceName;
		protected string className;
		protected string baseClassName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, IEnumerable<CompoundObjectProperty> compoundObjectProperties, string interfaceName, string className, string baseClassName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("CompoundObjects.Constructors", ctx, compoundObjectProperties, interfaceName, className, baseClassName);
        }

        public Constructors(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, IEnumerable<CompoundObjectProperty> compoundObjectProperties, string interfaceName, string className, string baseClassName)
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
#line 35 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CompoundObjects\Constructors.cst"
this.WriteObjects("        // used by NHibernate\r\n");
this.WriteObjects("        public ",  className , "()\r\n");
this.WriteObjects("            : this(null, null, null, null)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        // used by CreateUnattachedInstance\r\n");
this.WriteObjects("        public ",  className , "(Func<IFrozenContext> lazyCtx)\r\n");
this.WriteObjects("            : this(null, null, lazyCtx, null)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>Create a instance, wrapping the specified proxy</summary>\r\n");
this.WriteObjects("        public ",  className , "(IPersistenceObject parent, string property, Func<IFrozenContext> lazyCtx, ",  interfaceName , "Proxy proxy)\r\n");
#line 49 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CompoundObjects\Constructors.cst"
if (String.IsNullOrEmpty(baseClassName)) { 
#line 50 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CompoundObjects\Constructors.cst"
this.WriteObjects("            : base(lazyCtx) // do not pass proxy to base data object\r\n");
#line 51 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CompoundObjects\Constructors.cst"
} else { 
#line 52 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CompoundObjects\Constructors.cst"
this.WriteObjects("            : base(lazyCtx, proxy) // pass proxy to base nhibernate object\r\n");
#line 53 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CompoundObjects\Constructors.cst"
} 
#line 54 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CompoundObjects\Constructors.cst"
this.WriteObjects("        {\r\n");
this.WriteObjects("            this.Proxy = proxy;\r\n");
this.WriteObjects("            AttachToObject(parent, property);\r\n");
#line 57 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CompoundObjects\Constructors.cst"
ApplyCompoundObjectPropertyInitialisers("lazyCtx"); 
#line 58 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CompoundObjects\Constructors.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>the NHibernate proxy of the represented entity</summary>\r\n");
#line 61 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CompoundObjects\Constructors.cst"
if (String.IsNullOrEmpty(baseClassName)) { 
#line 62 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CompoundObjects\Constructors.cst"
this.WriteObjects("        internal readonly ",  interfaceName , "Proxy Proxy;\r\n");
#line 63 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CompoundObjects\Constructors.cst"
} else { 
#line 64 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CompoundObjects\Constructors.cst"
this.WriteObjects("        internal new readonly ",  interfaceName , "Proxy Proxy;\r\n");
#line 65 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\CompoundObjects\Constructors.cst"
} 

        }

    }
}