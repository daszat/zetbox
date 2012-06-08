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
#line 17 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("");
#line 37 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("        public ",  className , "()\n");
this.WriteObjects("            : this(null)\n");
this.WriteObjects("        {\n");
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("        /// <summary>Create a new unattached instance</summary>\n");
this.WriteObjects("        public ",  className , "(Func<IFrozenContext> lazyCtx)\n");
this.WriteObjects("            : this(lazyCtx, new ",  interfaceName , "Proxy())\n");
this.WriteObjects("        {\n");
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("        /// <summary>Create a instance, wrapping the specified proxy</summary>\n");
this.WriteObjects("        public ",  className , "(Func<IFrozenContext> lazyCtx, ",  interfaceName , "Proxy proxy)\n");
#line 50 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
if (String.IsNullOrEmpty(baseClassName)) { 
#line 51 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("            : base(lazyCtx) // do not pass proxy to base data object\n");
#line 52 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
} else { 
#line 53 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("            : base(lazyCtx, proxy) // pass proxy to parent\n");
#line 54 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
} 
#line 55 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("        {\n");
this.WriteObjects("            this.Proxy = proxy;\n");
#line 57 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
ApplyCompoundObjectPropertyInitialisers(); 
#line 58 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
ApplyDefaultValueSetFlagInitialisers(); 
#line 59 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
if (unsetValueIsNull) { 
#line 60 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("            // collection values are never null\n");
this.WriteObjects("            this.Proxy.Value.CompoundObject_IsNull = false;\n");
#line 62 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
} 
#line 63 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("        /// <summary>the NHibernate proxy of the represented entity</summary>\n");
#line 66 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
if (String.IsNullOrEmpty(baseClassName)) { 
#line 67 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("        internal readonly ",  interfaceName , "Proxy Proxy;\n");
#line 68 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
} else { 
#line 69 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("        internal new readonly ",  interfaceName , "Proxy Proxy;\n");
#line 70 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
} 

        }

    }
}