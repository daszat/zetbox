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
		protected String name;
		protected String associationName;
		protected String roleName;
		protected String referencedCollectionEntry;


        public EfListWrapper(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, String name, String associationName, String roleName, String referencedCollectionEntry)
            : base(_host)
        {
			this.ctx = ctx;
			this.name = name;
			this.associationName = associationName;
			this.roleName = roleName;
			this.referencedCollectionEntry = referencedCollectionEntry;

        }
        
        public override void Generate()
        {
#line 12 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EfListWrapper.cst"
this.WriteObjects("\r\n");
#line 18 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\EfListWrapper.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        [EdmRelationshipNavigationPropertyAttribute(\"Model\", \"",  associationName , "\", \"",  roleName , "\")]\r\n");
this.WriteObjects("        public EntityCollection<",  referencedCollectionEntry , "> ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                EntityCollection<",  referencedCollectionEntry , "> c\r\n");
this.WriteObjects("                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<",  referencedCollectionEntry , ">(\r\n");
this.WriteObjects("                        \"Model.",  associationName , "\",\r\n");
this.WriteObjects("                        \"",  roleName , "\");\r\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)\r\n");
this.WriteObjects("                    && !c.IsLoaded)\r\n");
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