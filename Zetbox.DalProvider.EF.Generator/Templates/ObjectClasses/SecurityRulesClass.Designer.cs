using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\SecurityRulesClass.cst")]
    public partial class SecurityRulesClass : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected ObjectClass cls;
		protected string assocName;
		protected string targetRoleName;
		protected string referencedImplementation;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, ObjectClass cls, string assocName, string targetRoleName, string referencedImplementation)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.SecurityRulesClass", ctx, cls, assocName, targetRoleName, referencedImplementation);
        }

        public SecurityRulesClass(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, ObjectClass cls, string assocName, string targetRoleName, string referencedImplementation)
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
#line 17 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\SecurityRulesClass.cst"
this.WriteObjects("");
#line 34 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\SecurityRulesClass.cst"
this.WriteObjects("\n");
this.WriteObjects("    [System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName=\"Model\", Name=\"",  targetRoleName , "\")]\n");
this.WriteObjects("    public class ",  referencedImplementation , " : System.Data.Objects.DataClasses.EntityObject\n");
this.WriteObjects("    {\n");
this.WriteObjects("        [System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]\n");
this.WriteObjects("        public int ID\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get\n");
this.WriteObjects("            {\n");
this.WriteObjects("                return this._ID;\n");
this.WriteObjects("            }\n");
this.WriteObjects("            set\n");
this.WriteObjects("            {\n");
this.WriteObjects("                this.ReportPropertyChanging(\"ID\");\n");
this.WriteObjects("                this._ID = value;\n");
this.WriteObjects("                this.ReportPropertyChanged(\"ID\");\n");
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
this.WriteObjects("        private int _ID;\n");
this.WriteObjects("\n");
this.WriteObjects("        [System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]\n");
this.WriteObjects("        public int Identity\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get\n");
this.WriteObjects("            {\n");
this.WriteObjects("                return this._Identity;\n");
this.WriteObjects("            }\n");
this.WriteObjects("            set\n");
this.WriteObjects("            {\n");
this.WriteObjects("                this.ReportPropertyChanging(\"Identity\");\n");
this.WriteObjects("                this._Identity = value;\n");
this.WriteObjects("                this.ReportPropertyChanged(\"Identity\");\n");
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
this.WriteObjects("        private int _Identity;\n");
this.WriteObjects("\n");
this.WriteObjects("        [System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]\n");
this.WriteObjects("        public int Right\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get\n");
this.WriteObjects("            {\n");
this.WriteObjects("                return this._Right;\n");
this.WriteObjects("            }\n");
this.WriteObjects("            set\n");
this.WriteObjects("            {\n");
this.WriteObjects("                this.ReportPropertyChanging(\"Right\");\n");
this.WriteObjects("                this._Right = value;\n");
this.WriteObjects("                this.ReportPropertyChanged(\"Right\");\n");
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
this.WriteObjects("        private int _Right;\n");
this.WriteObjects("    }");

        }

    }
}