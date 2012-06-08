using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\ReloadOneReference.cst")]
    public partial class ReloadOneReference : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected string referencedInterface;
		protected string referencedImplementation;
		protected string name;
		protected string implName;
		protected string fkBackingName;
		protected string fkGuidBackingName;
		protected bool isExportable;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string referencedInterface, string referencedImplementation, string name, string implName, string fkBackingName, string fkGuidBackingName, bool isExportable)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.ReloadOneReference", ctx, referencedInterface, referencedImplementation, name, implName, fkBackingName, fkGuidBackingName, isExportable);
        }

        public ReloadOneReference(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string referencedInterface, string referencedImplementation, string name, string implName, string fkBackingName, string fkGuidBackingName, bool isExportable)
            : base(_host)
        {
			this.ctx = ctx;
			this.referencedInterface = referencedInterface;
			this.referencedImplementation = referencedImplementation;
			this.name = name;
			this.implName = implName;
			this.fkBackingName = fkBackingName;
			this.fkGuidBackingName = fkGuidBackingName;
			this.isExportable = isExportable;

        }

        public override void Generate()
        {
#line 37 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\ReloadOneReference.cst"
this.WriteObjects("\r\n");
#line 38 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\ReloadOneReference.cst"
if (isExportable) { 
#line 39 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\ReloadOneReference.cst"
this.WriteObjects("            if (",  fkGuidBackingName , ".HasValue)\r\n");
this.WriteObjects("                ",  implName , " = (",  referencedImplementation , ")Context.FindPersistenceObject<",  referencedInterface , ">(",  fkGuidBackingName , ".Value);\r\n");
this.WriteObjects("            else\r\n");
#line 42 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\ReloadOneReference.cst"
} 
#line 43 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\ReloadOneReference.cst"
this.WriteObjects("            if (",  fkBackingName , ".HasValue)\r\n");
this.WriteObjects("                ",  implName , " = (",  referencedImplementation , ")Context.Find<",  referencedInterface , ">(",  fkBackingName , ".Value);\r\n");
this.WriteObjects("            else\r\n");
this.WriteObjects("                ",  implName , " = null;\r\n");

        }

    }
}