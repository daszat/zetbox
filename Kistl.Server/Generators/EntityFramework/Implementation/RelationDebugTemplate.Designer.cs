using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;
using Kistl.App.Extensions;


namespace Kistl.Server.Generators.EntityFramework.Implementation
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\RelationDebugTemplate.cst")]
    public partial class RelationDebugTemplate : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Relation rel;


        public RelationDebugTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Relation rel)
            : base(_host)
        {
			this.ctx = ctx;
			this.rel = rel;

        }
        
        public override void Generate()
        {
#line 14 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\RelationDebugTemplate.cst"
this.WriteObjects("    Relation: ",  rel.GetAssociationName() , "\r\n");
this.WriteObjects("    A: ",  rel.A.Multiplicity , " ",  rel.A.Type.ClassName , " as ",  rel.A.RoleName , "\r\n");
this.WriteObjects("    B: ",  rel.B.Multiplicity , " ",  rel.B.Type.ClassName , " as ",  rel.B.RoleName , "\r\n");
this.WriteObjects("    Preferred Storage: ",  rel.Storage , "\r\n");

        }



    }
}