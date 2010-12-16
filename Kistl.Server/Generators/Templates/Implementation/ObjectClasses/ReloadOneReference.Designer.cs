using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\ReloadOneReference.cst")]
    public partial class ReloadOneReference : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected string referencedInterface;
		protected string referencedImplementation;
		protected string name;
		protected string implName;
		protected string fkBackingName;
		protected string fkGuidBackingName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string referencedInterface, string referencedImplementation, string name, string implName, string fkBackingName, string fkGuidBackingName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Implementation.ObjectClasses.ReloadOneReference", ctx, referencedInterface, referencedImplementation, name, implName, fkBackingName, fkGuidBackingName);
        }

        public ReloadOneReference(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string referencedInterface, string referencedImplementation, string name, string implName, string fkBackingName, string fkGuidBackingName)
            : base(_host)
        {
			this.ctx = ctx;
			this.referencedInterface = referencedInterface;
			this.referencedImplementation = referencedImplementation;
			this.name = name;
			this.implName = implName;
			this.fkBackingName = fkBackingName;
			this.fkGuidBackingName = fkGuidBackingName;

        }

        public override void Generate()
        {
#line 20 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\ReloadOneReference.cst"
this.WriteObjects("\r\n");
this.WriteObjects("			if (",  fkGuidBackingName , ".HasValue)\r\n");
this.WriteObjects("				",  implName , " = (",  referencedImplementation , ")Context.FindPersistenceObject<",  referencedInterface , ">(",  fkGuidBackingName , ".Value);\r\n");
this.WriteObjects("			else if (",  fkBackingName , ".HasValue)\r\n");
this.WriteObjects("				",  implName , " = (",  referencedImplementation , ")Context.Find<",  referencedInterface , ">(",  fkBackingName , ".Value);\r\n");
this.WriteObjects("			else\r\n");
this.WriteObjects("				",  implName , " = null;\r\n");

        }

    }
}