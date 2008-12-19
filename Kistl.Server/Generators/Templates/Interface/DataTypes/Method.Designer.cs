using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Interface.DataTypes
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Interface\DataTypes\Method.cst")]
    public partial class Method : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected Kistl.App.Base.Method m;


        public Method(Arebis.CodeGeneration.IGenerationHost host, Kistl.App.Base.Method m)
            : base(host)
        {
			this.m = m;

        }
        
        public override void Generate()
        {
#line 13 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\DataTypes\Method.cst"
this.WriteObjects("\r\n");
#line 15 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\DataTypes\Method.cst"
this.WriteObjects("		",  GetModifiers() , " ",  GetReturnType() , " ",  m.MethodName , "(",  GetParameterDefinitions() , ") ");
#line 15 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\DataTypes\Method.cst"
ApplyBodyTemplate(); 

        }



    }
}