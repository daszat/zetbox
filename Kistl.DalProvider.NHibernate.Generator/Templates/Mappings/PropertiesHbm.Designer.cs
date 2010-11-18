using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.NHibernate.Generator.Templates.Mappings
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\PropertiesHbm.cst")]
    public partial class PropertiesHbm : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected IEnumerable<Property> properties;


        public PropertiesHbm(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, IEnumerable<Property> properties)
            : base(_host)
        {
			this.ctx = ctx;
			this.properties = properties;

        }
        
        public override void Generate()
        {
#line 19 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\PropertiesHbm.cst"
/*
     * TODO: Actually, all this should die and become a bunch of polymorphic calls.
     * See also Kistl.DalProvider.Ef.Generator.Templates.EfModel.ModelCsdlEntityTypeFields
     */

    foreach(var p in properties.OrderBy(p => p.Name))
    {
        // TODO: implement IsNullable everywhere
        if (p is ObjectReferenceProperty)
        {
            var prop = p as ObjectReferenceProperty;
            var rel = Kistl.App.Extensions.RelationExtensions.Lookup(ctx, prop);
            var relEnd = rel.GetEnd(prop);
            var otherEnd = rel.GetOtherEnd(relEnd);
            
            if (rel.Storage == StorageType.Separate)
            {
                Debug.Assert(relEnd != null);

#line 38 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\PropertiesHbm.cst"
this.WriteObjects("    <NavigationProperty Name=\"",  p.Name + ImplementationPropertySuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  rel.GetRelationAssociationName(relEnd.GetRole()) , "\"\r\n");
this.WriteObjects("                        FromRole=\"",  relEnd.RoleName , "\"\r\n");
this.WriteObjects("                        ToRole=\"CollectionEntry\" />\r\n");
#line 43 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\PropertiesHbm.cst"
}
            else
            {

#line 47 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\PropertiesHbm.cst"
this.WriteObjects("    <NavigationProperty Name=\"",  p.Name + ImplementationPropertySuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  rel.GetAssociationName() , "\"\r\n");
this.WriteObjects("                        FromRole=\"",  relEnd.RoleName , "\"\r\n");
this.WriteObjects("                        ToRole=\"",  otherEnd.RoleName , "\" />\r\n");
#line 53 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\PropertiesHbm.cst"
if (rel.NeedsPositionStorage(relEnd.GetRole()))
                {

#line 56 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\PropertiesHbm.cst"
this.WriteObjects("    <Property Name=\"",  Construct.ListPositionPropertyName(relEnd) , "\" Type=\"Int32\" Nullable=\"true\" />\r\n");
#line 58 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\PropertiesHbm.cst"
}
            }
        }
        else if (p is ValueTypeProperty)
        {
            var prop = (ValueTypeProperty)p;
            if (prop.IsList)
            {

#line 67 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\PropertiesHbm.cst"
this.WriteObjects("    <NavigationProperty Name=\"",  prop.Name + ImplementationPropertySuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  prop.GetAssociationName() , "\"\r\n");
this.WriteObjects("                        FromRole=\"",  prop.ObjectClass.Name , "\"\r\n");
this.WriteObjects("                        ToRole=\"CollectionEntry\" />\r\n");
#line 72 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\PropertiesHbm.cst"
}
            else
            {

#line 76 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\PropertiesHbm.cst"
this.WriteObjects("    ModelCsdl.PlainPropertyDefinitionFromValueType(ValueTypeProperty=",  p.Name , ")\r\n");
#line 78 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\PropertiesHbm.cst"
}
        }
        else if (p is CompoundObjectProperty)
        {
            var prop = (CompoundObjectProperty)p;
            if (prop.IsList)
            {

#line 86 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\PropertiesHbm.cst"
this.WriteObjects("    <NavigationProperty Name=\"",  prop.Name + ImplementationPropertySuffix , "\"\r\n");
this.WriteObjects("                        Relationship=\"Model.",  prop.GetAssociationName() , "\"\r\n");
this.WriteObjects("                        FromRole=\"",  prop.ObjectClass.Name , "\"\r\n");
this.WriteObjects("                        ToRole=\"CollectionEntry\" />\r\n");
#line 91 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\PropertiesHbm.cst"
}
            else
            {
            // Nullable Complex types are not supported by EF

#line 96 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\PropertiesHbm.cst"
this.WriteObjects("    <Property Name=\"",  p.Name + ImplementationPropertySuffix , "\"\r\n");
this.WriteObjects("              Type=\"Model.",  prop.CompoundObjectDefinition.Name , "\"\r\n");
this.WriteObjects("              Nullable=\"false\" />\r\n");
#line 100 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\PropertiesHbm.cst"
}
        }    
    }


        }



    }
}