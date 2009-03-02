using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


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
#line 16 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("using System.Data.Metadata.Edm;\r\n");
this.WriteObjects("using System.Data.Objects.DataClasses;\r\n");
this.WriteObjects("using System.Xml;\r\n");
this.WriteObjects("using System.Xml.Serialization;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("using Kistl.API;\r\n");
this.WriteObjects("using Kistl.DALProvider.EF;\r\n");
this.WriteObjects("\r\n");
#line 25 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
foreach (var rel in ctx.GetQuery<Relation>().ToList().OrderBy(r => r.GetAssociationName()))
	{

#line 28 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
this.WriteObjects("	/*\r\n");
#line 31 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
CallTemplate("Implementation.RelationDebugTemplate", ctx, rel);

#line 33 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("	*/\r\n");
#line 35 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
switch (rel.Storage)
		{
			case StorageType.MergeIntoA:
			case StorageType.MergeIntoB:
			case StorageType.Replicate:

#line 41 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
this.WriteObjects("// basic association\r\n");
this.WriteObjects("[assembly: EdmRelationship(\r\n");
this.WriteObjects("    \"Model\", \"",  rel.GetAssociationName() , "\",\r\n");
this.WriteObjects("    \"",  rel.A.RoleName , "\", RelationshipMultiplicity.",  rel.A.Multiplicity.ToCsdlRelationshipMultiplicity() , ", typeof(",  rel.A.Type.GetDataTypeString() + Kistl.API.Helper.ImplementationSuffix , "),\r\n");
this.WriteObjects("    \"",  rel.B.RoleName , "\", RelationshipMultiplicity.",  rel.B.Multiplicity.ToCsdlRelationshipMultiplicity() , ", typeof(",  rel.B.Type.GetDataTypeString() + Kistl.API.Helper.ImplementationSuffix , ")\r\n");
this.WriteObjects("    )]\r\n");
this.WriteObjects("\r\n");
#line 50 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
break;
			case StorageType.Separate:

#line 53 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
this.WriteObjects("// The association from A to the CollectionEntry\r\n");
this.WriteObjects("[assembly: EdmRelationship(\"Model\", \"",  rel.GetCollectionEntryAssociationName(RelationEndRole.A) , "\",\r\n");
this.WriteObjects("    \"",  rel.A.RoleName , "\", RelationshipMultiplicity.ZeroOrOne, typeof(",  rel.A.Type.GetDataTypeString() + Kistl.API.Helper.ImplementationSuffix , "),\r\n");
this.WriteObjects("    \"CollectionEntry\", RelationshipMultiplicity.Many, typeof(",  rel.GetCollectionEntryFullName() + Kistl.API.Helper.ImplementationSuffix , ")\r\n");
this.WriteObjects("    )]\r\n");
this.WriteObjects("// The association from B to the CollectionEntry\r\n");
this.WriteObjects("[assembly: EdmRelationship(\"Model\", \"",  rel.GetCollectionEntryAssociationName(RelationEndRole.B) , "\",\r\n");
this.WriteObjects("    \"",  rel.B.RoleName , "\", RelationshipMultiplicity.ZeroOrOne, typeof(",  rel.B.Type.GetDataTypeString() + Kistl.API.Helper.ImplementationSuffix , "),\r\n");
this.WriteObjects("    \"CollectionEntry\", RelationshipMultiplicity.Many, typeof(",  rel.GetCollectionEntryFullName() + Kistl.API.Helper.ImplementationSuffix , ")\r\n");
this.WriteObjects("    )]\r\n");
#line 65 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
break;
			default:
				throw new NotImplementedException(String.Format("Unknown StorageHint.{0} preference", rel.Storage));
		}
	}


#line 72 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
#line 74 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
foreach (var prop in ctx.GetQuery<ValueTypeProperty>()
		.Where(p => p.IsList)
		.OrderBy(p => p.ObjectClass.ClassName)
		.OrderBy(p => p.PropertyName))
	{

#line 80 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
this.WriteObjects("// object-value association\r\n");
this.WriteObjects("[assembly: EdmRelationship(\r\n");
this.WriteObjects("    \"Model\", \"",  prop.GetAssociationName() , "\",\r\n");
this.WriteObjects("    \"",  prop.ObjectClass.ClassName , "\", RelationshipMultiplicity.ZeroOrOne, typeof(",  prop.ObjectClass.Module.Namespace + "." + prop.ObjectClass.ClassName + Kistl.API.Helper.ImplementationSuffix , "),\r\n");
this.WriteObjects("    \"CollectionEntry\", RelationshipMultiplicity.Many, typeof(",  prop.GetCollectionEntryFullName() + Kistl.API.Helper.ImplementationSuffix , ")\r\n");
this.WriteObjects("    )]\r\n");
this.WriteObjects("\r\n");
#line 89 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
}


        }



    }
}