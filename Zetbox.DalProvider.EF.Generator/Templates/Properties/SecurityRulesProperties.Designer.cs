using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.Ef.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\SecurityRulesProperties.cst")]
    public partial class SecurityRulesProperties : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected ObjectClass cls;
		protected string assocName;
		protected string targetRoleName;
		protected string referencedImplementation;
		protected string efNameRightsPropertyName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, ObjectClass cls, string assocName, string targetRoleName, string referencedImplementation, string efNameRightsPropertyName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.SecurityRulesProperties", ctx, cls, assocName, targetRoleName, referencedImplementation, efNameRightsPropertyName);
        }

        public SecurityRulesProperties(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, ObjectClass cls, string assocName, string targetRoleName, string referencedImplementation, string efNameRightsPropertyName)
            : base(_host)
        {
			this.ctx = ctx;
			this.cls = cls;
			this.assocName = assocName;
			this.targetRoleName = targetRoleName;
			this.referencedImplementation = referencedImplementation;
			this.efNameRightsPropertyName = efNameRightsPropertyName;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\SecurityRulesProperties.cst"
this.WriteObjects("");
#line 35 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\SecurityRulesProperties.cst"
this.WriteObjects("\n");
this.WriteObjects("        private Zetbox.API.AccessRights? __currentAccessRights;\n");
this.WriteObjects("        public override Zetbox.API.AccessRights CurrentAccessRights\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get\n");
this.WriteObjects("            {\n");
this.WriteObjects("				if(Context == null) return Zetbox.API.AccessRights.Full;\n");
this.WriteObjects("                if (__currentAccessRights == null)\n");
this.WriteObjects("                {\n");
this.WriteObjects("					__currentAccessRights = base.CurrentAccessRights;\n");
this.WriteObjects("					var secRight = SecurityRightsCollectionImpl.FirstOrDefault(i => i.Identity == Context.Internals().IdentityID); // TODO: should be SingleOrDefault() instead of FirstOrDefault()\n");
this.WriteObjects("                    __currentAccessRights |= secRight != null ? (Zetbox.API.AccessRights)secRight.Right : Zetbox.API.AccessRights.None;\n");
this.WriteObjects("                }\n");
this.WriteObjects("                return __currentAccessRights.Value;\n");
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("        [EdmRelationshipNavigationProperty(\"Model\", \"",  assocName , "\", \"",  targetRoleName , "\")]\n");
this.WriteObjects("        public EntityCollection<",  referencedImplementation , "> ",  efNameRightsPropertyName , "\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get\n");
this.WriteObjects("            {\n");
this.WriteObjects("                var c = ((IEntityWithRelationships)(this)).RelationshipManager\n");
this.WriteObjects("                    .GetRelatedCollection<",  referencedImplementation , ">(\n");
this.WriteObjects("                        \"Model.",  assocName , "\",\n");
this.WriteObjects("                        \"",  targetRoleName , "\");\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)\n");
this.WriteObjects("                    && !c.IsLoaded)\n");
this.WriteObjects("                {\n");
this.WriteObjects("                    c.Load();\n");
this.WriteObjects("                }\n");
this.WriteObjects("                return c;\n");
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");

        }

    }
}