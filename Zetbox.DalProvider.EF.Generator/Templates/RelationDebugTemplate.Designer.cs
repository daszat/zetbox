using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator.Extensions;
using Zetbox.App.Extensions;


namespace Zetbox.DalProvider.Ef.Generator.Templates
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\RelationDebugTemplate.cst")]
    public partial class RelationDebugTemplate : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected Relation rel;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Relation rel)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("RelationDebugTemplate", ctx, rel);
        }

        public RelationDebugTemplate(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Relation rel)
            : base(_host)
        {
			this.ctx = ctx;
			this.rel = rel;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\RelationDebugTemplate.cst"
this.WriteObjects("");
#line 29 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\RelationDebugTemplate.cst"
this.WriteObjects("    Relation: ",  rel.GetAssociationName() , "\n");
this.WriteObjects("    A: ",  rel.A.Multiplicity , " ",  rel.A.Type.Name , " as ",  rel.A.RoleName , "\n");
this.WriteObjects("    B: ",  rel.B.Multiplicity , " ",  rel.B.Type.Name , " as ",  rel.B.RoleName , "\n");
this.WriteObjects("    Preferred Storage: ",  rel.Storage , "\n");

        }

    }
}