using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Zetbox.API;
using Zetbox.API.SchemaManagement;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst")]
    public partial class AssociationSetAttributes : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.AssociationSetAttributes", ctx);
        }

        public AssociationSetAttributes(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }

        public override void Generate()
        {
#line 33 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("using System.Data.Metadata.Edm;\r\n");
this.WriteObjects("using System.Data.Objects.DataClasses;\r\n");
this.WriteObjects("using System.Xml;\r\n");
this.WriteObjects("using System.Xml.Serialization;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("using Zetbox.API;\r\n");
this.WriteObjects("using Zetbox.DalProvider.Ef;\r\n");
this.WriteObjects("\r\n");
#line 42 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
foreach (var rel in GetRelations(ctx).ToList().OrderBy(r => r.GetAssociationName()))
	{

#line 45 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
this.WriteObjects("	/*\r\n");
#line 48 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
RelationDebugTemplate.Call(Host, ctx, rel);

#line 50 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("	*/\r\n");
#line 52 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
switch (rel.Storage)
		{
			case StorageType.MergeIntoA:
			case StorageType.MergeIntoB:
			case StorageType.Replicate:

#line 58 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
this.WriteObjects("// basic association\r\n");
this.WriteObjects("[assembly: EdmRelationship(\r\n");
this.WriteObjects("    \"Model\", \"",  rel.GetAssociationName() , "\",\r\n");
this.WriteObjects("    \"",  rel.A.RoleName , "\", RelationshipMultiplicity.",  rel.A.Multiplicity.ToCsdlRelationshipMultiplicity() , ", typeof(",  rel.A.Type.GetDataTypeString() + ImplementationSuffix , "),\r\n");
this.WriteObjects("    \"",  rel.B.RoleName , "\", RelationshipMultiplicity.",  rel.B.Multiplicity.ToCsdlRelationshipMultiplicity() , ", typeof(",  rel.B.Type.GetDataTypeString() + ImplementationSuffix , ")\r\n");
this.WriteObjects("    )]\r\n");
this.WriteObjects("\r\n");
#line 67 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
break;
			case StorageType.Separate:

#line 70 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
this.WriteObjects("// The association from A to the CollectionEntry\r\n");
this.WriteObjects("[assembly: EdmRelationship(\"Model\", \"",  rel.GetRelationAssociationName(RelationEndRole.A) , "\",\r\n");
this.WriteObjects("    \"",  rel.A.RoleName , "\", RelationshipMultiplicity.ZeroOrOne, typeof(",  rel.A.Type.GetDataTypeString() + ImplementationSuffix , "),\r\n");
this.WriteObjects("    \"CollectionEntry\", RelationshipMultiplicity.Many, typeof(",  rel.GetRelationFullName() + ImplementationSuffix , ")\r\n");
this.WriteObjects("    )]\r\n");
this.WriteObjects("// The association from B to the CollectionEntry\r\n");
this.WriteObjects("[assembly: EdmRelationship(\"Model\", \"",  rel.GetRelationAssociationName(RelationEndRole.B) , "\",\r\n");
this.WriteObjects("    \"",  rel.B.RoleName , "\", RelationshipMultiplicity.ZeroOrOne, typeof(",  rel.B.Type.GetDataTypeString() + ImplementationSuffix , "),\r\n");
this.WriteObjects("    \"CollectionEntry\", RelationshipMultiplicity.Many, typeof(",  rel.GetRelationFullName() + ImplementationSuffix , ")\r\n");
this.WriteObjects("    )]\r\n");
#line 82 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
break;
			default:
				throw new NotImplementedException(String.Format("Unknown StorageHint.{0} preference", rel.Storage));
		}
	}


#line 89 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
#line 91 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
foreach (var prop in GetValueTypeProperties(ctx)
		.Where(p => p.IsList && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
		.OrderBy(p => p.ObjectClass.Name)
		.ThenBy(p => p.Name))
	{

#line 98 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
this.WriteObjects("// object-value association\r\n");
this.WriteObjects("[assembly: EdmRelationship(\r\n");
this.WriteObjects("    \"Model\", \"",  prop.GetAssociationName() , "\",\r\n");
this.WriteObjects("    \"",  prop.ObjectClass.Name , "\", RelationshipMultiplicity.ZeroOrOne, typeof(",  prop.ObjectClass.Module.Namespace + "." + prop.ObjectClass.Name + ImplementationSuffix , "),\r\n");
this.WriteObjects("    \"CollectionEntry\", RelationshipMultiplicity.Many, typeof(",  prop.GetCollectionEntryFullName() + ImplementationSuffix , ")\r\n");
this.WriteObjects("    )]\r\n");
this.WriteObjects("\r\n");
#line 107 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
}

#line 109 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
#line 111 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
foreach (var prop in GetCompoundObjectProperties(ctx)
		.Where(p => p.IsList) // && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
		.OrderBy(p => p.ObjectClass.Name)
		.ThenBy(p => p.Name))
	{

#line 118 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
this.WriteObjects("// object-struct association\r\n");
this.WriteObjects("[assembly: EdmRelationship(\r\n");
this.WriteObjects("    \"Model\", \"",  prop.GetAssociationName() , "\",\r\n");
this.WriteObjects("    \"",  prop.ObjectClass.Name , "\", RelationshipMultiplicity.ZeroOrOne, typeof(",  prop.ObjectClass.Module.Namespace + "." + prop.ObjectClass.Name + ImplementationSuffix , "),\r\n");
this.WriteObjects("    \"CollectionEntry\", RelationshipMultiplicity.Many, typeof(",  prop.GetCollectionEntryFullName() + ImplementationSuffix , ")\r\n");
this.WriteObjects("    )]\r\n");
this.WriteObjects("\r\n");
#line 127 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
}

#line 129 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
#line 131 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
foreach (var cls in GetObjectClasses(ctx).Where(c => c.BaseObjectClass == null)
		.OrderBy(c => c.Name))
	{
		if (cls.NeedsRightsTable())
		{

#line 137 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("[assembly: global::System.Data.Objects.DataClasses.EdmRelationshipAttribute(\"Model\", \"",  Construct.SecurityRulesFKName(cls) , "\", \r\n");
this.WriteObjects("	\"",  cls.Name , "\", \r\n");
this.WriteObjects("	global::System.Data.Metadata.Edm.RelationshipMultiplicity.One, \r\n");
this.WriteObjects("	typeof(",  cls.Module.Namespace + "." + cls.Name + ImplementationSuffix , "), \r\n");
this.WriteObjects("	\"",  Construct.SecurityRulesClassName(cls) , "\", \r\n");
this.WriteObjects("	global::System.Data.Metadata.Edm.RelationshipMultiplicity.Many, \r\n");
this.WriteObjects("	typeof(",  cls.Module.Namespace + "." + Construct.SecurityRulesClassName(cls) + ImplementationSuffix , "))]\r\n");
#line 145 "D:\Projects\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
}
	}


        }

    }
}