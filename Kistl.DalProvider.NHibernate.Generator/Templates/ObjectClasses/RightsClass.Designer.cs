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
    [Arebis.CodeGeneration.TemplateInfo(@"/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/ObjectClasses/RightsClass.cst")]
    public partial class RightsClass : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected string className;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string className)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.RightsClass", ctx, className);
        }

        public RightsClass(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string className)
            : base(_host)
        {
			this.ctx = ctx;
			this.className = className;

        }

        public override void Generate()
        {
#line 16 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/ObjectClasses/RightsClass.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public class ",  className , "",  ImplementationSuffix , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            public ",  className , "",  ImplementationSuffix , "()\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            public virtual int ID { get; set; }\r\n");
this.WriteObjects("            public virtual int Identity { get; set; }\r\n");
this.WriteObjects("            public virtual int Right { get; set; }\r\n");
this.WriteObjects("        }\r\n");

        }

    }
}