using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst")]
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
#line 32 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("using System.Data.Metadata.Edm;\r\n");
this.WriteObjects("using System.Data.Objects.DataClasses;\r\n");
this.WriteObjects("using System.Xml;\r\n");
this.WriteObjects("using System.Xml.Serialization;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("using Zetbox.API;\r\n");
this.WriteObjects("using Zetbox.DalProvider.Ef;\r\n");
this.WriteObjects("\r\n");
#line 41 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
foreach (var rel in ctx.GetQuery<Relation>().ToList().OrderBy(r => r.GetAssociationName()))
	{

#line 44 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
this.WriteObjects("	/*\r\n");
#line 47 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
RelationDebugTemplate.Call(Host, ctx, rel);

#line 49 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("	*/\r\n");
#line 51 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
switch (rel.Storage)
		{
			case StorageType.MergeIntoA:
			case StorageType.MergeIntoB:
			case StorageType.Replicate:

#line 57 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
this.WriteObjects("// basic association\r\n");
this.WriteObjects("[assembly: EdmRelationship(\r\n");
this.WriteObjects("    \"Model\", \"",  rel.GetAssociationName() , "\",\r\n");
this.WriteObjects("    \"",  rel.A.RoleName , "\", RelationshipMultiplicity.",  rel.A.Multiplicity.ToCsdlRelationshipMultiplicity() , ", typeof(",  rel.A.Type.GetDataTypeString() + ImplementationSuffix , "),\r\n");
this.WriteObjects("    \"",  rel.B.RoleName , "\", RelationshipMultiplicity.",  rel.B.Multiplicity.ToCsdlRelationshipMultiplicity() , ", typeof(",  rel.B.Type.GetDataTypeString() + ImplementationSuffix , ")\r\n");
this.WriteObjects("    )]\r\n");
this.WriteObjects("\r\n");
#line 66 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
break;
			case StorageType.Separate:

#line 69 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
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
#line 81 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
break;
			default:
				throw new NotImplementedException(String.Format("Unknown StorageHint.{0} preference", rel.Storage));
		}
	}


#line 88 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
#line 90 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
foreach (var prop in ctx.GetQuery<ValueTypeProperty>()
		.Where(p => p.IsList && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
		.OrderBy(p => p.ObjectClass.Name)
		.ThenBy(p => p.Name))
	{

#line 97 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
this.WriteObjects("// object-value association\r\n");
this.WriteObjects("[assembly: EdmRelationship(\r\n");
this.WriteObjects("    \"Model\", \"",  prop.GetAssociationName() , "\",\r\n");
this.WriteObjects("    \"",  prop.ObjectClass.Name , "\", RelationshipMultiplicity.ZeroOrOne, typeof(",  prop.ObjectClass.Module.Namespace + "." + prop.ObjectClass.Name + ImplementationSuffix , "),\r\n");
this.WriteObjects("    \"CollectionEntry\", RelationshipMultiplicity.Many, typeof(",  prop.GetCollectionEntryFullName() + ImplementationSuffix , ")\r\n");
this.WriteObjects("    )]\r\n");
this.WriteObjects("\r\n");
#line 106 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
}

#line 108 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
#line 110 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
foreach (var prop in ctx.GetQuery<CompoundObjectProperty>()
		.Where(p => p.IsList) // && !p.IsCalculated)
        .Where(p => p.ObjectClass is ObjectClass)
		.OrderBy(p => p.ObjectClass.Name)
		.ThenBy(p => p.Name))
	{

#line 117 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
this.WriteObjects("// object-struct association\r\n");
this.WriteObjects("[assembly: EdmRelationship(\r\n");
this.WriteObjects("    \"Model\", \"",  prop.GetAssociationName() , "\",\r\n");
this.WriteObjects("    \"",  prop.ObjectClass.Name , "\", RelationshipMultiplicity.ZeroOrOne, typeof(",  prop.ObjectClass.Module.Namespace + "." + prop.ObjectClass.Name + ImplementationSuffix , "),\r\n");
this.WriteObjects("    \"CollectionEntry\", RelationshipMultiplicity.Many, typeof(",  prop.GetCollectionEntryFullName() + ImplementationSuffix , ")\r\n");
this.WriteObjects("    )]\r\n");
this.WriteObjects("\r\n");
#line 126 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
}

#line 128 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("\r\n");
#line 130 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
foreach (var cls in ctx.GetQuery<ObjectClass>().Where(c => c.BaseObjectClass == null)
		.OrderBy(c => c.Name))
	{
		if (cls.NeedsRightsTable())
		{

#line 136 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
this.WriteObjects("[assembly: global::System.Data.Objects.DataClasses.EdmRelationshipAttribute(\"Model\", \"",  Construct.SecurityRulesFKName(cls) , "\", \r\n");
this.WriteObjects("	\"",  cls.Name , "\", \r\n");
this.WriteObjects("	global::System.Data.Metadata.Edm.RelationshipMultiplicity.One, \r\n");
this.WriteObjects("	typeof(",  cls.Module.Namespace + "." + cls.Name + ImplementationSuffix , "), \r\n");
this.WriteObjects("	\"",  Construct.SecurityRulesClassName(cls) , "\", \r\n");
this.WriteObjects("	global::System.Data.Metadata.Edm.RelationshipMultiplicity.Many, \r\n");
this.WriteObjects("	typeof(",  cls.Module.Namespace + "." + Construct.SecurityRulesClassName(cls) + ImplementationSuffix , "))]\r\n");
#line 144 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AssociationSetAttributes.cst"
}
	}


        }

    }
}