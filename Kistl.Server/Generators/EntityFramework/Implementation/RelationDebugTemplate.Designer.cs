using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Movables;


namespace Kistl.Server.Generators.EntityFramework.Implementation
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\RelationDebugTemplate.cst")]
    public partial class RelationDebugTemplate : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected NewRelation rel;


        public RelationDebugTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, NewRelation rel)
            : base(_host)
        {
			this.ctx = ctx;
			this.rel = rel;

        }
        
        public override void Generate()
        {
#line 13 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\RelationDebugTemplate.cst"
this.WriteObjects("    NewRelation: ",  rel.GetAssociationName() , " \r\n");
this.WriteObjects("    A: ",  rel.A.Multiplicity , " ",  rel.A.Type.ClassName , " as ",  rel.A.RoleName , " (site: ",  rel.A.DebugCreationSite , ")\r\n");
this.WriteObjects("    B: ",  rel.B.Multiplicity , " ",  rel.B.Type.ClassName , " as ",  rel.B.RoleName , " (site: ",  rel.B.DebugCreationSite , ")\r\n");
this.WriteObjects("    Preferred Storage: ",  rel.GetPreferredStorage() , "\r\n");

        }



    }
}