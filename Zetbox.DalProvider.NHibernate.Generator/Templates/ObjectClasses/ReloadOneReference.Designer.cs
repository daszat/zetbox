using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ReloadOneReference.cst")]
    public partial class ReloadOneReference : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected string referencedInterface;
		protected string referencedImplementation;
		protected string name;
		protected string implNameUnused;
		protected string fkBackingName;
		protected string fkGuidBackingName;
		protected bool isExportable;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string referencedInterface, string referencedImplementation, string name, string implNameUnused, string fkBackingName, string fkGuidBackingName, bool isExportable)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.ReloadOneReference", ctx, referencedInterface, referencedImplementation, name, implNameUnused, fkBackingName, fkGuidBackingName, isExportable);
        }

        public ReloadOneReference(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string referencedInterface, string referencedImplementation, string name, string implNameUnused, string fkBackingName, string fkGuidBackingName, bool isExportable)
            : base(_host)
        {
			this.ctx = ctx;
			this.referencedInterface = referencedInterface;
			this.referencedImplementation = referencedImplementation;
			this.name = name;
			this.implNameUnused = implNameUnused;
			this.fkBackingName = fkBackingName;
			this.fkGuidBackingName = fkGuidBackingName;
			this.isExportable = isExportable;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ReloadOneReference.cst"
this.WriteObjects("");
#line 37 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ReloadOneReference.cst"
this.WriteObjects("\n");
#line 38 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ReloadOneReference.cst"
if (isExportable) { 
#line 39 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ReloadOneReference.cst"
this.WriteObjects("            if (",  fkGuidBackingName , ".HasValue)\n");
this.WriteObjects("                this.",  name , " = ((",  referencedImplementation , ")OurContext.FindPersistenceObject<",  referencedInterface , ">(",  fkGuidBackingName , ".Value));\n");
this.WriteObjects("            else\n");
#line 42 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ReloadOneReference.cst"
} 
#line 43 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ReloadOneReference.cst"
this.WriteObjects("            if (",  fkBackingName , ".HasValue)\n");
this.WriteObjects("                this.",  name , " = ((",  referencedImplementation , ")OurContext.FindPersistenceObject<",  referencedInterface , ">(",  fkBackingName , ".Value));\n");
this.WriteObjects("            else\n");
this.WriteObjects("                this.",  name , " = null;\n");

        }

    }
}