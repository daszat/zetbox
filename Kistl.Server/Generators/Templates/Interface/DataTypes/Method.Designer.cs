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
		protected IKistlContext ctx;
		protected Kistl.App.Base.Method m;
		protected int index;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.App.Base.Method m, int index)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Interface.DataTypes.Method", ctx, m, index);
        }

        public Method(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.App.Base.Method m, int index)
            : base(_host)
        {
			this.ctx = ctx;
			this.m = m;
			this.index = index;

        }

        public override void Generate()
        {
#line 17 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\DataTypes\Method.cst"
foreach(var attr in GetMethodAttributes())
	{

#line 20 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\DataTypes\Method.cst"
this.WriteObjects("		",  attr , "\r\n");
#line 22 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\DataTypes\Method.cst"
}

#line 24 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\DataTypes\Method.cst"
this.WriteObjects("		",  GetModifiers() , " ",  GetReturnType() , " ",  m.Name , "(",  GetParameterDefinitions() , ") ");
#line 24 "P:\Kistl\Kistl.Server\Generators\Templates\Interface\DataTypes\Method.cst"
ApplyBodyTemplate(); 

        }

    }
}