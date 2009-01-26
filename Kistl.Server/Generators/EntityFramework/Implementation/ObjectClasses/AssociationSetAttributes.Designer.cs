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
this.WriteObjects("\r\n");
this.WriteObjects("// NewRelation: ",  rel.GetAssociationName() , " \r\n");
this.WriteObjects("// Right: ",  rel.Right.Referenced.ClassName , ".",  rel.Right.Navigator != null ? rel.Right.Navigator.PropertyName : "" , "  (",  rel.Right.Multiplicity , ") (site: ",  rel.Right.DebugCreationSite , ")\r\n");
this.WriteObjects("// Left: ",  rel.Left.Referenced.ClassName , ".",  rel.Left.Navigator != null ? rel.Left.Navigator.PropertyName : "" , " (",  rel.Left.Multiplicity , ") (site: ",  rel.Left.DebugCreationSite , ")\r\n");
this.WriteObjects("// Preferred Storage: ",  rel.GetPreferredStorage() , "\r\n");
#line 36 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
switch (rel.GetPreferredStorage())
		{
			case StorageHint.MergeLeft:
			case StorageHint.MergeRight:

#line 41 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
this.WriteObjects("// basic association\r\n");
this.WriteObjects("[assembly: EdmRelationship(\r\n");
this.WriteObjects("    \"Model\", \"",  rel.GetAssociationName() , "\",\r\n");
this.WriteObjects("    \"",  rel.Right.RoleName , "\", RelationshipMultiplicity.",  rel.Right.Multiplicity.ToCsdlRelationshipMultiplicity() , ", typeof(",  rel.Right.Referenced.NameDataObject + Kistl.API.Helper.ImplementationSuffix , "),\r\n");
this.WriteObjects("    \"",  rel.Left.RoleName , "\", RelationshipMultiplicity.",  rel.Left.Multiplicity.ToCsdlRelationshipMultiplicity() , ", typeof(",  rel.Left.Referenced.NameDataObject + Kistl.API.Helper.ImplementationSuffix , ")\r\n");
this.WriteObjects("    )]\r\n");
this.WriteObjects("\r\n");
#line 50 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
break;
			case StorageHint.Separate:

#line 53 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
this.WriteObjects("// The association from Right to the CollectionEntry\r\n");
this.WriteObjects("[assembly: EdmRelationship(\"Model\", \"",  rel.GetRightToCollectionEntryAssociationName() , "\",\r\n");
this.WriteObjects("    \"",  rel.Right.RoleName , "\", RelationshipMultiplicity.ZeroOrOne, typeof(",  rel.Right.Referenced.NameDataObject + Kistl.API.Helper.ImplementationSuffix , "),\r\n");
this.WriteObjects("    \"Right\", RelationshipMultiplicity.Many, typeof(",  rel.GetCollectionEntryFullName() + Kistl.API.Helper.ImplementationSuffix , ")\r\n");
this.WriteObjects("    )]\r\n");
#line 60 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
if (rel.IsTwoProngedAssociation(ctx))
				{

#line 63 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("// The association from Left to the CollectionEntry\r\n");
this.WriteObjects("[assembly: EdmRelationship(\"Model\", \"",  rel.GetLeftToCollectionEntryAssociationName() , "\",\r\n");
this.WriteObjects("    \"",  rel.Left.RoleName , "\", RelationshipMultiplicity.ZeroOrOne, typeof(",  rel.Left.Referenced.NameDataObject + Kistl.API.Helper.ImplementationSuffix , "),\r\n");
this.WriteObjects("    \"Left\", RelationshipMultiplicity.Many, typeof(",  rel.GetCollectionEntryFullName() + Kistl.API.Helper.ImplementationSuffix , ")\r\n");
this.WriteObjects("    )]\r\n");
#line 69 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\AssociationSetAttributes.cst"
}

				break;
		}
	}



        }



    }
}