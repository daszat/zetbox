using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.Ef.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\SecurityRulesProperties.cst")]
    public partial class SecurityRulesProperties : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected ObjectClass cls;
		protected string assocName;
		protected string targetRoleName;
		protected string referencedImplementation;
		protected string efNameRightsPropertyName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls, string assocName, string targetRoleName, string referencedImplementation, string efNameRightsPropertyName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.SecurityRulesProperties", ctx, cls, assocName, targetRoleName, referencedImplementation, efNameRightsPropertyName);
        }

        public SecurityRulesProperties(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls, string assocName, string targetRoleName, string referencedImplementation, string efNameRightsPropertyName)
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
#line 19 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\SecurityRulesProperties.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public override Kistl.API.AccessRights CurrentAccessRights \r\n");
this.WriteObjects("        { \r\n");
this.WriteObjects("            get \r\n");
this.WriteObjects("            { \r\n");
this.WriteObjects("                return (Kistl.API.AccessRights)",  efNameRightsPropertyName , ".First().Right; \r\n");
this.WriteObjects("            } \r\n");
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