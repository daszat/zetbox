using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.Ef.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EfListWrapper.cst")]
    public partial class EfListWrapper : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected String name;
		protected String associationName;
		protected String roleName;
		protected String referencedCollectionEntry;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, String name, String associationName, String roleName, String referencedCollectionEntry)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.EfListWrapper", ctx, name, associationName, roleName, referencedCollectionEntry);
        }

        public EfListWrapper(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, String name, String associationName, String roleName, String referencedCollectionEntry)
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
#line 16 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EfListWrapper.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\r\n");
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
this.WriteObjects("                c.ForEach(i => i.AttachToContext(Context));\r\n");
this.WriteObjects("                return c;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        // END ",  this.GetType() , "\r\n");

        }

    }
}