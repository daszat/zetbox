using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"C:\temp\Kistl2\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst")]
    public partial class MethodBody : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Kistl.App.Base.DataType dt;
		protected Kistl.App.Base.Method m;


        public MethodBody(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.App.Base.DataType dt, Kistl.App.Base.Method m)
            : base(_host)
        {
			this.ctx = ctx;
			this.dt = dt;
			this.m = m;

        }
        
        public override void Generate()
        {
#line 12 "C:\temp\Kistl2\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("\r\n");
#line 18 "C:\temp\Kistl2\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
string delegateName = m.MethodName + "_Handler";
	string eventName = "On" + m.MethodName + "_" + dt.ClassName;
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

#line 39 "C:\temp\Kistl2\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("		{\r\n");
this.WriteObjects("            // base.",  m.MethodName , "();\r\n");
this.WriteObjects("            if (",  eventName , " != null)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("				",  eventName , "(this",  argumentDefs , ");\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("        }\r\n");
#line 47 "C:\temp\Kistl2\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
// define delegate type only on base class
		if (this.m.ObjectClass == this.dt)
		{

#line 51 "C:\temp\Kistl2\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("		public delegate void ",  delegateName , "<T>(T obj",  parameterDefs , ");\r\n");
#line 53 "C:\temp\Kistl2\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
}
	}
	else
	{

		string returnArgsType = String.Format("MethodReturnEventArgs<{0}>", returnParam.ReturnedTypeAsCSharp());

#line 60 "C:\temp\Kistl2\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("        {\r\n");
this.WriteObjects("            var e = new ",  returnArgsType , "();\r\n");
this.WriteObjects("            if (",  eventName , " != null)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                ",  eventName , "(this, e",  argumentDefs , ");\r\n");
this.WriteObjects("            };\r\n");
this.WriteObjects("            return e.Result;\r\n");
this.WriteObjects("        }\r\n");
#line 70 "C:\temp\Kistl2\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
// define delegate type only on base class
		if (this.m.ObjectClass == this.dt)
		{

#line 74 "C:\temp\Kistl2\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("		public delegate void ",  delegateName , "<T>(T obj, ",  returnArgsType , " ret",  parameterDefs , ");\r\n");
#line 76 "C:\temp\Kistl2\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
}
	}


#line 80 "C:\temp\Kistl2\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("		public event ",  delegateName , "<",  dt.ClassName , "> ",  eventName , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");

        }



    }
}