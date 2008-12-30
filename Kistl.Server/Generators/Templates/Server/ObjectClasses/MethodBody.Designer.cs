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
		protected Kistl.App.Base.Method m;


        public MethodBody(Arebis.CodeGeneration.IGenerationHost _host, Kistl.App.Base.Method m)
            : base(_host)
        {
			this.m = m;

        }
        
        public override void Generate()
        {
#line 13 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\MethodBody.cst"
this.WriteObjects("\r\n");
#line 16 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\MethodBody.cst"
string delegateName = m.MethodName + "_Handler";
string eventName = "On" + m.MethodName + "_" + m.ObjectClass.ClassName;
var returnParam = m.Parameter.SingleOrDefault(parameter => parameter.IsReturnParameter);

string parameterDefs = m.GetParameterDefinitions();
if (!String.IsNullOrEmpty(parameterDefs))
{
    // add leading comma for later usage
	parameterDefs = ", " + parameterDefs;	
}

string argumentDefs = m.GetArguments();
if (!String.IsNullOrEmpty(argumentDefs))
{
    // add leading comma for later usage
	argumentDefs = ", " + argumentDefs;	
}

if (returnParam == null)
{
#line 36 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\MethodBody.cst"
this.WriteObjects("		{\r\n");
this.WriteObjects("            // base.",  m.MethodName , "();\r\n");
this.WriteObjects("            if (",  eventName , " != null)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("				",  eventName , "(this",  argumentDefs , ");\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("		public event ",  delegateName , "<",  m.ObjectClass.ClassName , "> ",  eventName , ";\r\n");
this.WriteObjects("		public delegate void ",  delegateName , "<T>(T obj",  parameterDefs , ");\r\n");
#line 45 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\MethodBody.cst"
}
else
{

	string returnArgsType = String.Format("MethodReturnEventArgs<{0}>", returnParam.GetParameterTypeString());

#line 51 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\MethodBody.cst"
this.WriteObjects("		{\r\n");
this.WriteObjects("            var e = new ",  returnArgsType , "();\r\n");
this.WriteObjects("            if (",  eventName , " != null)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                ",  eventName , "(this, e",  argumentDefs , ");\r\n");
this.WriteObjects("            };\r\n");
this.WriteObjects("            return e.Result;\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("		public event ",  delegateName , "<",  m.ObjectClass.ClassName , "> ",  eventName , ";\r\n");
this.WriteObjects("		public delegate void ",  delegateName , "<T>(T obj, ",  returnArgsType , " ret",  parameterDefs , ");\r\n");
#line 63 "P:\Kistl\Kistl.Server\Generators\Templates\Server\ObjectClasses\MethodBody.cst"
}



        }



    }
}