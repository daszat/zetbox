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
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox.net4\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst")]
    public partial class Constructors : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected IEnumerable<CompoundInitialisationDescriptor> compoundObjectInitialisers;
		protected IEnumerable<string> valueSetFlags;
		protected string interfaceName;
		protected string className;
		protected string baseClassName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, IEnumerable<CompoundInitialisationDescriptor> compoundObjectInitialisers, IEnumerable<string> valueSetFlags, string interfaceName, string className, string baseClassName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.Constructors", ctx, compoundObjectInitialisers, valueSetFlags, interfaceName, className, baseClassName);
        }

        public Constructors(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, IEnumerable<CompoundInitialisationDescriptor> compoundObjectInitialisers, IEnumerable<string> valueSetFlags, string interfaceName, string className, string baseClassName)
            : base(_host)
        {
			this.ctx = ctx;
			this.compoundObjectInitialisers = compoundObjectInitialisers;
			this.valueSetFlags = valueSetFlags;
			this.interfaceName = interfaceName;
			this.className = className;
			this.baseClassName = baseClassName;

        }

        public override void Generate()
        {
#line 36 "D:\Projects\zetbox.net4\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
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
#line 49 "D:\Projects\zetbox.net4\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
if (String.IsNullOrEmpty(baseClassName)) { 
#line 50 "D:\Projects\zetbox.net4\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("            : base(lazyCtx) // do not pass proxy to base data object\r\n");
#line 51 "D:\Projects\zetbox.net4\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
} else { 
#line 52 "D:\Projects\zetbox.net4\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("            : base(lazyCtx, proxy) // pass proxy to parent\r\n");
#line 53 "D:\Projects\zetbox.net4\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
} 
#line 54 "D:\Projects\zetbox.net4\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("        {\r\n");
this.WriteObjects("            this.Proxy = proxy;\r\n");
#line 56 "D:\Projects\zetbox.net4\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
ApplyCompoundObjectPropertyInitialisers("lazyCtx"); 
#line 57 "D:\Projects\zetbox.net4\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
ApplyDefaultValueSetFlagInitialisers(); 
#line 58 "D:\Projects\zetbox.net4\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>the NHibernate proxy of the represented entity</summary>\r\n");
#line 61 "D:\Projects\zetbox.net4\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
if (String.IsNullOrEmpty(baseClassName)) { 
#line 62 "D:\Projects\zetbox.net4\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("        internal readonly ",  interfaceName , "Proxy Proxy;\r\n");
#line 63 "D:\Projects\zetbox.net4\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
} else { 
#line 64 "D:\Projects\zetbox.net4\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
this.WriteObjects("        internal new readonly ",  interfaceName , "Proxy Proxy;\r\n");
#line 65 "D:\Projects\zetbox.net4\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\Constructors.cst"
} 

        }

    }
}