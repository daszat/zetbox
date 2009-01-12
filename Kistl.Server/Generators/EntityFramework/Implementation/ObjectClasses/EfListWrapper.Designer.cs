using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EfListWrapper.cst")]
    public partial class EfListWrapper : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected ObjectClass containingClass;
		protected Type type;
		protected String name;
		protected Property property;


        public EfListWrapper(Arebis.CodeGeneration.IGenerationHost _host, ObjectClass containingClass, Type type, String name, Property property)
            : base(_host)
        {
			this.containingClass = containingClass;
			this.type = type;
			this.name = name;
			this.property = property;

        }
        
        public override void Generate()
        {
#line 12 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EfListWrapper.cst"
this.WriteObjects("\r\n");
#line 17 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EfListWrapper.cst"
this.WriteObjects("		[EdmRelationshipNavigationPropertyAttribute(\"Model\", \"",  Construct.AssociationName(containingClass, property) , "\", \"",  Construct.AssociationChildRoleName(property) , "\")]\r\n");
this.WriteObjects("        public EntityCollection<Kistl.App.Base.Constraint__Implementation__> ",  name , "__Implementation__\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                EntityCollection<Kistl.App.Base.Constraint__Implementation__> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Kistl.App.Base.Constraint__Implementation__>(\"Model.FK_Constraint_BaseProperty_ConstrainedProperty\", \"B_Constraint\");\r\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded) c.Load(); \r\n");
this.WriteObjects("                return c;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        ");

        }



    }
}