using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ReloadOneReference.cst")]
    public partial class ReloadOneReference : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected string referencedInterface;
		protected string referencedImplementation;
		protected string name;
		protected string implNameUnused;
		protected string fkBackingName;
		protected string fkGuidBackingName;
		protected bool isExportable;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string referencedInterface, string referencedImplementation, string name, string implNameUnused, string fkBackingName, string fkGuidBackingName, bool isExportable)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.ReloadOneReference", ctx, referencedInterface, referencedImplementation, name, implNameUnused, fkBackingName, fkGuidBackingName, isExportable);
        }

        public ReloadOneReference(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string referencedInterface, string referencedImplementation, string name, string implNameUnused, string fkBackingName, string fkGuidBackingName, bool isExportable)
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
#line 21 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ReloadOneReference.cst"
this.WriteObjects("\r\n");
#line 22 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ReloadOneReference.cst"
if (isExportable) { 
#line 23 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ReloadOneReference.cst"
this.WriteObjects("            if (",  fkGuidBackingName , ".HasValue)\r\n");
this.WriteObjects("                this.Proxy.",  name , " = ((",  referencedImplementation , ")OurContext.FindPersistenceObject<",  referencedInterface , ">(",  fkGuidBackingName , ".Value)).Proxy;\r\n");
this.WriteObjects("            else\r\n");
#line 26 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ReloadOneReference.cst"
} 
#line 27 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ReloadOneReference.cst"
this.WriteObjects("            if (",  fkBackingName , ".HasValue)\r\n");
this.WriteObjects("                this.Proxy.",  name , " = ((",  referencedImplementation , ")OurContext.FindPersistenceObject<",  referencedInterface , ">(",  fkBackingName , ".Value)).Proxy;\r\n");
this.WriteObjects("            else\r\n");
this.WriteObjects("                this.Proxy.",  name , " = null;\r\n");

        }

    }
}