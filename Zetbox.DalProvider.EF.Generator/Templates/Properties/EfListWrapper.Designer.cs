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
#line 17 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EfListWrapper.cst"
this.WriteObjects("");
#line 32 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\EfListWrapper.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\n");
this.WriteObjects("        [EdmRelationshipNavigationPropertyAttribute(\"Model\", \"",  associationName , "\", \"",  roleName , "\")]\n");
this.WriteObjects("        public EntityCollection<",  referencedCollectionEntry , "> ",  name , "\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get\n");
this.WriteObjects("            {\n");
this.WriteObjects("                EntityCollection<",  referencedCollectionEntry , "> c\n");
this.WriteObjects("                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<",  referencedCollectionEntry , ">(\n");
this.WriteObjects("                        \"Model.",  associationName , "\",\n");
this.WriteObjects("                        \"",  roleName , "\");\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)\n");
this.WriteObjects("                    && !c.IsLoaded)\n");
this.WriteObjects("                {\n");
this.WriteObjects("                    c.Load();\n");
this.WriteObjects("                }\n");
this.WriteObjects("                c.ForEach(i => i.AttachToContext(Context));\n");
this.WriteObjects("                return c;\n");
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
this.WriteObjects("        // END ",  this.GetType() , "\n");

        }

    }
}