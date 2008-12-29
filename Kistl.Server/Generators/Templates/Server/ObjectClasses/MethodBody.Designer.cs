using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Server.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"E:\Projekte\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\MethodBody.cst")]
    public partial class MethodBody : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected Kistl.App.Base.Method m;


        public MethodBody(Arebis.CodeGeneration.IGenerationHost _host, Kistl.App.Base.Method m)
            : base(_host)
        {
			this.m = m;

        }
        
        public override void Generate()
        {
#line 13 "E:\Projekte\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\MethodBody.cst"
this.WriteObjects("\r\n");
#line 15 "E:\Projekte\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\MethodBody.cst"
this.WriteObjects("		{\r\n");
this.WriteObjects("		    // blah\r\n");
this.WriteObjects("		}\r\n");
#line 19 "E:\Projekte\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\MethodBody.cst"
string delegateName = m.MethodName + "_Handler";
string eventName = "On" + m.MethodName + "_" + m.ObjectClass.ClassName;

#line 22 "E:\Projekte\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\MethodBody.cst"
this.WriteObjects("		public event ",  delegateName , "<",  m.ObjectClass.ClassName , "> ",  eventName , ";\r\n");
#line 25 "E:\Projekte\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\MethodBody.cst"
string parameterDefs = m.GetParameterDefinitions();
if (!String.IsNullOrEmpty(parameterDefs))
{
    // add leading comma for later usage
	parameterDefs = ", " + parameterDefs;	
}

var ret = m.GetReturnParameter();
if (ret == null)
{

#line 36 "E:\Projekte\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\MethodBody.cst"
this.WriteObjects("		public delegate void ",  delegateName , "<T>(T obj",  parameterDefs , ");\r\n");
#line 38 "E:\Projekte\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\MethodBody.cst"
}
else
{

#line 42 "E:\Projekte\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\MethodBody.cst"
this.WriteObjects("		public delegate void ",  delegateName , "<T>(T obj, MethodReturnArgs<",  ret.GetParameterType().FullName , ">",  parameterDefs , ");\r\n");
#line 44 "E:\Projekte\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\MethodBody.cst"
}


        }



    }
}