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
		protected IKistlContext ctx;
		protected ObjectClass containingClass;
		protected Property property;


        public EfListWrapper(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass containingClass, Property property)
            : base(_host)
        {
			this.ctx = ctx;
			this.containingClass = containingClass;
			this.property = property;

        }
        
        public override void Generate()
        {
#line 12 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EfListWrapper.cst"
this.WriteObjects("\r\n");
#line 17 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EfListWrapper.cst"
var info = AssociationInfo.CreateInfo(ctx, property);
	string typeName = info.Child.Type.NameDataObject + Kistl.API.Helper.ImplementationSuffix;

#line 20 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EfListWrapper.cst"
this.WriteObjects("        [EdmRelationshipNavigationPropertyAttribute(\"Model\", \"",  info.AssociationName , "\", \"",  info.Child.RoleName , "\")]\r\n");
this.WriteObjects("        public EntityCollection<",  typeName , "> ",  property.PropertyName , "",  Kistl.API.Helper.ImplementationSuffix , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                EntityCollection<",  typeName , "> c\r\n");
this.WriteObjects("                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<",  typeName , ">(\r\n");
this.WriteObjects("                        \"Model.",  info.AssociationName , "\",\r\n");
this.WriteObjects("                        \"",  info.Child.RoleName , "\");\r\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    c.Load();\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return c;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");

        }



    }
}