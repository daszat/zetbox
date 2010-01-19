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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\ObjectClasses\SecurityRulesClass.cst")]
    public partial class SecurityRulesClass : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected ObjectClass cls;
		protected string assocName;
		protected string targetRoleName;
		protected string referencedImplementation;


        public SecurityRulesClass(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls, string assocName, string targetRoleName, string referencedImplementation)
            : base(_host)
        {
			this.ctx = ctx;
			this.cls = cls;
			this.assocName = assocName;
			this.targetRoleName = targetRoleName;
			this.referencedImplementation = referencedImplementation;

        }
        
        public override void Generate()
        {
#line 18 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\ObjectClasses\SecurityRulesClass.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    [System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName=\"Model\", Name=\"",  targetRoleName , "\")]\r\n");
this.WriteObjects("    public class ",  referencedImplementation , " : System.Data.Objects.DataClasses.EntityObject\r\n");
this.WriteObjects("    {\r\n");
this.WriteObjects("        [System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]\r\n");
this.WriteObjects("        public int ID\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                return this._ID;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                this.ReportPropertyChanging(\"ID\");\r\n");
this.WriteObjects("                this._ID = value;\r\n");
this.WriteObjects("                this.ReportPropertyChanged(\"ID\");\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        private int _ID;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]\r\n");
this.WriteObjects("        public int Identity\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                return this._Identity;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                this.ReportPropertyChanging(\"Identity\");\r\n");
this.WriteObjects("                this._Identity = value;\r\n");
this.WriteObjects("                this.ReportPropertyChanged(\"Identity\");\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        private int _Identity;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]\r\n");
this.WriteObjects("        public int Right\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                return this._Right;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                this.ReportPropertyChanging(\"Right\");\r\n");
this.WriteObjects("                this._Right = value;\r\n");
this.WriteObjects("                this.ReportPropertyChanged(\"Right\");\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        private int _Right;\r\n");
this.WriteObjects("    }");

        }



    }
}