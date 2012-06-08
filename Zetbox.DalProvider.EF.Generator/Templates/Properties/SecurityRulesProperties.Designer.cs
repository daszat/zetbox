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
this.WriteObjects("\r\n");
this.WriteObjects("        private Zetbox.API.AccessRights? __currentAccessRights;\r\n");
this.WriteObjects("        public override Zetbox.API.AccessRights CurrentAccessRights\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("				if(Context == null) return Zetbox.API.AccessRights.Full;\r\n");
this.WriteObjects("                if (__currentAccessRights == null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("					__currentAccessRights = base.CurrentAccessRights;\r\n");
this.WriteObjects("					var secRight = SecurityRightsCollectionImpl.FirstOrDefault(i => i.Identity == Context.Internals().IdentityID); // TODO: should be SingleOrDefault() instead of FirstOrDefault()\r\n");
this.WriteObjects("                    __currentAccessRights |= secRight != null ? (Zetbox.API.AccessRights)secRight.Right : Zetbox.API.AccessRights.None;\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return __currentAccessRights.Value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [EdmRelationshipNavigationProperty(\"Model\", \"",  assocName , "\", \"",  targetRoleName , "\")]\r\n");
this.WriteObjects("        public EntityCollection<",  referencedImplementation , "> ",  efNameRightsPropertyName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                var c = ((IEntityWithRelationships)(this)).RelationshipManager\r\n");
this.WriteObjects("                    .GetRelatedCollection<",  referencedImplementation , ">(\r\n");
this.WriteObjects("                        \"Model.",  assocName , "\",\r\n");
this.WriteObjects("                        \"",  targetRoleName , "\");\r\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)\r\n");
this.WriteObjects("                    && !c.IsLoaded)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    c.Load();\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return c;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");

        }

    }
}