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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\RightsClass.cst")]
    public partial class RightsClass : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected string className;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string className)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.RightsClass", ctx, className);
        }

        public RightsClass(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string className)
            : base(_host)
        {
			this.ctx = ctx;
			this.className = className;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\RightsClass.cst"
this.WriteObjects("");
#line 32 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\RightsClass.cst"
this.WriteObjects("\n");
this.WriteObjects("        public class ",  className , "",  ImplementationSuffix , "\n");
this.WriteObjects("        {\n");
this.WriteObjects("            public ",  className , "",  ImplementationSuffix , "()\n");
this.WriteObjects("            {\n");
this.WriteObjects("            }\n");
this.WriteObjects("\n");
this.WriteObjects("            public virtual int ID { get; set; }\n");
this.WriteObjects("            public virtual int Identity { get; set; }\n");
this.WriteObjects("            public virtual int Right { get; set; }\n");
this.WriteObjects("        }\n");

        }

    }
}