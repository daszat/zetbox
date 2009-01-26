using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;


namespace Kistl.Server.Generators.EntityFramework.Implementation.EfModel
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.AssociationSet.cst")]
    public partial class ModelCsdlAssociationSet : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected NewRelation rel;


        public ModelCsdlAssociationSet(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, NewRelation rel)
            : base(_host)
        {
			this.ctx = ctx;
			this.rel = rel;

        }
        
        public override void Generate()
        {
#line 17 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.AssociationSet.cst"
this.WriteObjects("    <!-- ",  rel.Left.Multiplicity , ":",  rel.Right.Multiplicity , " -->\r\n");
#line 19 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.AssociationSet.cst"
if (rel.GetPreferredStorage() == StorageHint.Separate)
	{

#line 22 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.AssociationSet.cst"
this.WriteObjects("    <AssociationSet Name=\"",  rel.GetRightToCollectionEntryAssociationName() , "\" Association=\"Model.",  rel.GetRightToCollectionEntryAssociationName() , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.Right.RoleName , "\" EntitySet=\"",  rel.Right.Referenced.ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"Right\" EntitySet=\"",  rel.GetCollectionEntryClassName() , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 27 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.AssociationSet.cst"
if (rel.IsTwoProngedAssociation(ctx))
		{

#line 30 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.AssociationSet.cst"
this.WriteObjects("    <AssociationSet Name=\"",  rel.GetLeftToCollectionEntryAssociationName() , "\" Association=\"Model.",  rel.GetLeftToCollectionEntryAssociationName() , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.Left.RoleName , "\" EntitySet=\"",  rel.Left.Referenced.ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"Left\" EntitySet=\"",  rel.GetCollectionEntryClassName() , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 35 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.AssociationSet.cst"
}
	}
	else
	{

#line 40 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.AssociationSet.cst"
this.WriteObjects("    <AssociationSet Name=\"",  rel.GetAssociationName() , "\" Association=\"Model.",  rel.GetAssociationName() , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.Right.RoleName , "\" EntitySet=\"",  rel.Right.Referenced.ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  rel.Left.RoleName , "\" EntitySet=\"",  rel.Left.Referenced.ClassName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 45 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.AssociationSet.cst"
}


        }



    }
}