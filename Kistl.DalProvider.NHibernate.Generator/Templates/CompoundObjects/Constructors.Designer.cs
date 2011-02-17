using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.NHibernate.Generator.Templates.CompoundObjects
{
    [Arebis.CodeGeneration.TemplateInfo(@"/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/CompoundObjects/Constructors.cst")]
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

            _host.CallTemplate("CompoundObjects.Constructors", ctx, compoundObjectProperties, interfaceName, className, baseClassName);
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
#line 19 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/CompoundObjects/Constructors.cst"
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
#line 33 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/CompoundObjects/Constructors.cst"
if (String.IsNullOrEmpty(baseClassName)) { 
#line 34 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/CompoundObjects/Constructors.cst"
this.WriteObjects("            : base(lazyCtx) // do not pass proxy to base data object\r\n");
#line 35 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/CompoundObjects/Constructors.cst"
} else { 
#line 36 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/CompoundObjects/Constructors.cst"
this.WriteObjects("            : base(lazyCtx, proxy) // pass proxy to base nhibernate object\r\n");
#line 37 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/CompoundObjects/Constructors.cst"
} 
#line 38 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/CompoundObjects/Constructors.cst"
this.WriteObjects("        {\r\n");
this.WriteObjects("            this.Proxy = proxy;\r\n");
this.WriteObjects("            AttachToObject(parent, property);\r\n");
#line 41 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/CompoundObjects/Constructors.cst"
ApplyCompoundObjectPropertyInitialisers(); 
#line 42 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/CompoundObjects/Constructors.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>the NHibernate proxy of the represented entity</summary>\r\n");
#line 45 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/CompoundObjects/Constructors.cst"
if (String.IsNullOrEmpty(baseClassName)) { 
#line 46 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/CompoundObjects/Constructors.cst"
this.WriteObjects("        internal readonly ",  interfaceName , "Proxy Proxy;\r\n");
#line 47 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/CompoundObjects/Constructors.cst"
} else { 
#line 48 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/CompoundObjects/Constructors.cst"
this.WriteObjects("        internal new readonly ",  interfaceName , "Proxy Proxy;\r\n");
#line 49 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/CompoundObjects/Constructors.cst"
} 

        }

    }
}