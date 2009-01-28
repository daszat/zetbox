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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.Association.cst")]
    public partial class ModelCsdlAssociation : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected NewRelation rel;


        public ModelCsdlAssociation(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, NewRelation rel)
            : base(_host)
        {
			this.ctx = ctx;
			this.rel = rel;

        }
        
        public override void Generate()
        {
#line 17 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.Association.cst"
this.WriteObjects("    <!-- ",  rel.A.Multiplicity , ":",  rel.B.Multiplicity , " -->\r\n");
#line 19 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.Association.cst"
if (rel.GetPreferredStorage() == StorageHint.Separate)
	{

#line 22 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.Association.cst"
this.WriteObjects("    <Association Name=\"",  rel.GetCollectionEntryAssociationName(rel.A) , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.A.RoleName , "\"\r\n");
this.WriteObjects("           Type=\"Model.",  rel.A.Type.ClassName , "\" \r\n");
this.WriteObjects("           Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\"\r\n");
this.WriteObjects("           Type=\"Model.",  rel.GetCollectionEntryClassName() , "\"\r\n");
this.WriteObjects("           Multiplicity=\"*\" />\r\n");
this.WriteObjects("    </Association>\r\n");
this.WriteObjects("    <Association Name=\"",  rel.GetCollectionEntryAssociationName(rel.B) , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.B.RoleName , "\"\r\n");
this.WriteObjects("           Type=\"Model.",  rel.B.Type.ClassName , "\" \r\n");
this.WriteObjects("           Multiplicity=\"0..1\" />\r\n");
this.WriteObjects("      <End Role=\"CollectionEntry\"\r\n");
this.WriteObjects("           Type=\"Model.",  rel.GetCollectionEntryClassName() , "\"\r\n");
this.WriteObjects("           Multiplicity=\"*\" />\r\n");
this.WriteObjects("    </Association>\r\n");
#line 39 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.Association.cst"
}
	else
	{

#line 43 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.Association.cst"
this.WriteObjects("    <Association Name=\"",  rel.GetAssociationName() , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  rel.A.RoleName , "\"\r\n");
this.WriteObjects("           Type=\"Model.",  rel.A.Type.ClassName , "\" \r\n");
this.WriteObjects("           Multiplicity=\"",  rel.A.Multiplicity.ToCsdlRelationshipMultiplicity().ToXmlValue() , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  rel.B.RoleName , "\"\r\n");
this.WriteObjects("           Type=\"Model.",  rel.B.Type.ClassName , "\" \r\n");
this.WriteObjects("           Multiplicity=\"",  rel.B.Multiplicity.ToCsdlRelationshipMultiplicity().ToXmlValue() , "\" />\r\n");
this.WriteObjects("    </Association>\r\n");
#line 52 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.Association.cst"
}


        }



    }
}