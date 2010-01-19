using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\ObjectClasses\SecurityRulesProperties.cst")]
    public partial class SecurityRulesProperties : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected ObjectClass cls;
		protected string assocName;
		protected string targetRoleName;
		protected string referencedImplementation;
		protected string efNameRightsPropertyName;


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
#line 19 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\ObjectClasses\SecurityRulesProperties.cst"
this.WriteObjects("\r\n");
this.WriteObjects("		public override AccessRights CurrentAccessRights \r\n");
this.WriteObjects("		{ \r\n");
this.WriteObjects("			get \r\n");
this.WriteObjects("			{ \r\n");
this.WriteObjects("				return (AccessRights)",  efNameRightsPropertyName , ".First().Right; \r\n");
this.WriteObjects("			} \r\n");
this.WriteObjects("		}\r\n");
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