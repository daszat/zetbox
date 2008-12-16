using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Server.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\MethodBody.cst")]
    public partial class MethodBody : Kistl.Server.Generators.KistlCodeTemplate
    {
		private Kistl.App.Base.Method m;


        public MethodBody(Arebis.CodeGeneration.IGenerationHost _host, Kistl.App.Base.Method m)
            : base(_host)
        {
			this.m = m;

        }
        
        public override void Generate()
        {
#line 13 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\MethodBody.cst"
this.WriteObjects("\r\n");
#line 15 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\MethodBody.cst"
this.WriteObjects("		{\r\n");
this.WriteObjects("		    // blah\r\n");
this.WriteObjects("		}");

        }



    }
}