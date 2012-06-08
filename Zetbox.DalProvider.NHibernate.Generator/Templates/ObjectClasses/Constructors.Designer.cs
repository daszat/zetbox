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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst")]
    public partial class Constructors : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected IEnumerable<CompoundInitialisationDescriptor> compoundObjectInitialisers;
		protected IEnumerable<string> valueSetFlags;
		protected string interfaceName;
		protected string className;
		protected string baseClassName;
		protected bool unsetValueIsNull;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, IEnumerable<CompoundInitialisationDescriptor> compoundObjectInitialisers, IEnumerable<string> valueSetFlags, string interfaceName, string className, string baseClassName, bool unsetValueIsNull)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.Constructors", ctx, compoundObjectInitialisers, valueSetFlags, interfaceName, className, baseClassName, unsetValueIsNull);
        }

        public Constructors(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, IEnumerable<CompoundInitialisationDescriptor> compoundObjectInitialisers, IEnumerable<string> valueSetFlags, string interfaceName, string className, string baseClassName, bool unsetValueIsNull)
            : base(_host)
        {
			this.ctx = ctx;
			this.compoundObjectInitialisers = compoundObjectInitialisers;
			this.valueSetFlags = valueSetFlags;
			this.interfaceName = interfaceName;
			this.className = className;
			this.baseClassName = baseClassName;
			this.unsetValueIsNull = unsetValueIsNull;

        }

        public override void Generate()
        {
#line 37 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("        public ",  className , "()\r\n");
this.WriteObjects("            : this(null)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>Create a new unattached instance</summary>\r\n");
this.WriteObjects("        public ",  className , "(Func<IFrozenContext> lazyCtx)\r\n");
this.WriteObjects("            : this(lazyCtx, new ",  interfaceName , "Proxy())\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>Create a instance, wrapping the specified proxy</summary>\r\n");
this.WriteObjects("        public ",  className , "(Func<IFrozenContext> lazyCtx, ",  interfaceName , "Proxy proxy)\r\n");
#line 50 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
if (String.IsNullOrEmpty(baseClassName)) { 
#line 51 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("            : base(lazyCtx) // do not pass proxy to base data object\r\n");
#line 52 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
} else { 
#line 53 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("            : base(lazyCtx, proxy) // pass proxy to parent\r\n");
#line 54 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
} 
#line 55 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("        {\r\n");
this.WriteObjects("            this.Proxy = proxy;\r\n");
#line 57 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
ApplyCompoundObjectPropertyInitialisers(); 
#line 58 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
ApplyDefaultValueSetFlagInitialisers(); 
#line 59 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
if (unsetValueIsNull) { 
#line 60 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("            // collection values are never null\r\n");
this.WriteObjects("            this.Proxy.Value.CompoundObject_IsNull = false;\r\n");
#line 62 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
} 
#line 63 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>the NHibernate proxy of the represented entity</summary>\r\n");
#line 66 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
if (String.IsNullOrEmpty(baseClassName)) { 
#line 67 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("        internal readonly ",  interfaceName , "Proxy Proxy;\r\n");
#line 68 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
} else { 
#line 69 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("        internal new readonly ",  interfaceName , "Proxy Proxy;\r\n");
#line 70 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
} 

        }

    }
}