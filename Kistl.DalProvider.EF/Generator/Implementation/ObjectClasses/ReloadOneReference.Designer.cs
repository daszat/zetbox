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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\ObjectClasses\ReloadOneReference.cst")]
    public partial class ReloadOneReference : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected string referencedInterface;
		protected string referencedImplementation;
		protected string name;
		protected string efName;
		protected string fkBackingName;
		protected string fkGuidBackingName;


        public ReloadOneReference(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string referencedInterface, string referencedImplementation, string name, string efName, string fkBackingName, string fkGuidBackingName)
            : base(_host)
        {
			this.ctx = ctx;
			this.referencedInterface = referencedInterface;
			this.referencedImplementation = referencedImplementation;
			this.name = name;
			this.efName = efName;
			this.fkBackingName = fkBackingName;
			this.fkGuidBackingName = fkGuidBackingName;

        }
        
        public override void Generate()
        {
#line 20 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\ObjectClasses\ReloadOneReference.cst"
this.WriteObjects("\r\n");
this.WriteObjects("			if (",  fkGuidBackingName , ".HasValue)\r\n");
this.WriteObjects("				",  efName , " = (",  referencedImplementation , ")Context.FindPersistenceObject<",  referencedInterface , ">(",  fkGuidBackingName , ".Value);\r\n");
this.WriteObjects("			else if (",  fkBackingName , ".HasValue)\r\n");
this.WriteObjects("				",  efName , " = (",  referencedImplementation , ")Context.Find<",  referencedInterface , ">(",  fkBackingName , ".Value);\r\n");
this.WriteObjects("			else\r\n");
this.WriteObjects("				",  efName , " = null;\r\n");

        }



    }
}