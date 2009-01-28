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
this.WriteObjects("    <!-- ",  rel.A.Multiplicity , ":",  rel.B.Multiplicity , " -->\r\n");
#line 19 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.AssociationSet.cst"
if (rel.GetPreferredStorage() == StorageHint.Separate)
	{
		string assocNameA = rel.GetCollectionEntryAssociationName(rel.GetEnd(RelationEndRole.A));
		string assocNameB = rel.GetCollectionEntryAssociationName(rel.GetEnd(RelationEndRole.B));

#line 24 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.AssociationSet.cst"
this.WriteObjects("    <AssociationSet Name=\"",  assocNameA , "\" Association=\"Model.",  assocNameA , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.A.RoleName , "\" EntitySet=\"",  rel.A.Type.ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  rel.GetCollectionEntryClassName() , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  assocNameB , "\" Association=\"Model.",  assocNameB , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.B.RoleName , "\" EntitySet=\"",  rel.B.Type.ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\" EntitySet=\"",  rel.GetCollectionEntryClassName() , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 33 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.AssociationSet.cst"
}
	else
	{
		string assocName = rel.GetAssociationName();

#line 38 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.AssociationSet.cst"
this.WriteObjects("    <AssociationSet Name=\"",  assocName , "\" Association=\"Model.",  assocName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.A.RoleName , "\" EntitySet=\"",  rel.A.Type.ClassName , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  rel.B.RoleName , "\" EntitySet=\"",  rel.B.Type.ClassName , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");
#line 43 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.AssociationSet.cst"
}


        }



    }
}