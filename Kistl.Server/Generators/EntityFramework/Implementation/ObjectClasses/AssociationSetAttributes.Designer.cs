using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;


namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst")]
    public partial class AssociationSetAttributes : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;


        public AssociationSetAttributes(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }
        
        public override void Generate()
        {
#line 17 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("using System.Data.Metadata.Edm;\r\n");
this.WriteObjects("using System.Data.Objects.DataClasses;\r\n");
this.WriteObjects("using System.Xml;\r\n");
this.WriteObjects("using System.Xml.Serialization;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("using Kistl.API;\r\n");
this.WriteObjects("using Kistl.DALProvider.EF;\r\n");
this.WriteObjects("\r\n");
#line 26 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
foreach (var rel in NewRelation.GetAll(ctx).OrderBy(r => r.GetAssociationName()))
	{

#line 29 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
this.WriteObjects("	/*\r\n");
#line 32 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
CallTemplate("Implementation.RelationDebugTemplate", ctx, rel);

#line 34 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("	*/\r\n");
#line 36 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
switch (rel.GetPreferredStorage())
		{
			case StorageHint.MergeLeft:
			case StorageHint.MergeRight:
			case StorageHint.Replicate:

#line 42 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
this.WriteObjects("// basic association\r\n");
this.WriteObjects("[assembly: EdmRelationship(\r\n");
this.WriteObjects("    \"Model\", \"",  rel.GetAssociationName() , "\",\r\n");
this.WriteObjects("    \"",  rel.A.RoleName , "\", RelationshipMultiplicity.",  rel.A.Multiplicity.ToCsdlRelationshipMultiplicity() , ", typeof(",  rel.A.Type.NameDataObject + Kistl.API.Helper.ImplementationSuffix , "),\r\n");
this.WriteObjects("    \"",  rel.B.RoleName , "\", RelationshipMultiplicity.",  rel.B.Multiplicity.ToCsdlRelationshipMultiplicity() , ", typeof(",  rel.B.Type.NameDataObject + Kistl.API.Helper.ImplementationSuffix , ")\r\n");
this.WriteObjects("    )]\r\n");
this.WriteObjects("\r\n");
#line 51 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
break;
			case StorageHint.Separate:

#line 54 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
this.WriteObjects("// The association from A to the CollectionEntry\r\n");
this.WriteObjects("[assembly: EdmRelationship(\"Model\", \"",  rel.GetCollectionEntryAssociationName(rel.A) , "\",\r\n");
this.WriteObjects("    \"",  rel.A.RoleName , "\", RelationshipMultiplicity.ZeroOrOne, typeof(",  rel.A.Type.NameDataObject + Kistl.API.Helper.ImplementationSuffix , "),\r\n");
this.WriteObjects("    \"CollectionEntry\", RelationshipMultiplicity.Many, typeof(",  rel.GetCollectionEntryFullName() + Kistl.API.Helper.ImplementationSuffix , ")\r\n");
this.WriteObjects("    )]\r\n");
this.WriteObjects("// The association from B to the CollectionEntry\r\n");
this.WriteObjects("[assembly: EdmRelationship(\"Model\", \"",  rel.GetCollectionEntryAssociationName(rel.B) , "\",\r\n");
this.WriteObjects("    \"",  rel.B.RoleName , "\", RelationshipMultiplicity.ZeroOrOne, typeof(",  rel.B.Type.NameDataObject + Kistl.API.Helper.ImplementationSuffix , "),\r\n");
this.WriteObjects("    \"CollectionEntry\", RelationshipMultiplicity.Many, typeof(",  rel.GetCollectionEntryFullName() + Kistl.API.Helper.ImplementationSuffix , ")\r\n");
this.WriteObjects("    )]\r\n");
#line 66 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
break;
			default:
				throw new NotImplementedException(String.Format("Unknown StorageHint.{0} preference", rel.GetPreferredStorage()));
		}
	}



        }



    }
}