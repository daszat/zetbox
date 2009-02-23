using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst")]
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
#line 12 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("\r\n");
#line 18 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
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

#line 39 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("		{\r\n");
this.WriteObjects("            // base.",  m.MethodName , "();\r\n");
this.WriteObjects("            if (",  eventName , " != null)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("				",  eventName , "(this",  argumentDefs , ");\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("			else\r\n");
this.WriteObjects("			{\r\n");
#line 48 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
if (m.ObjectClass == dt || !(dt is ObjectClass))
        {

#line 51 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("                throw new NotImplementedException(\"No handler registered on ",  m.ObjectClass.ClassName , ".",  m.MethodName , "\");\r\n");
#line 53 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
}
        else
        {

#line 57 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("			    base.",  m.MethodName , "(",  m.GetArguments() , ");\r\n");
#line 59 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
}

#line 61 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("			}\r\n");
this.WriteObjects("        }\r\n");
#line 64 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
// define delegate type only on base class
		if (this.m.ObjectClass == this.dt)
		{

#line 68 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("		public delegate void ",  delegateName , "<T>(T obj",  parameterDefs , ");\r\n");
#line 70 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
}
	}
	else
	{

		string returnArgsType = String.Format("MethodReturnEventArgs<{0}>", returnParam.ReturnedTypeAsCSharp());

#line 77 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("        {\r\n");
this.WriteObjects("            var e = new ",  returnArgsType , "();\r\n");
this.WriteObjects("            if (",  eventName , " != null)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                ",  eventName , "(this, e",  argumentDefs , ");\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            else\r\n");
this.WriteObjects("            {\r\n");
#line 86 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
if (m.ObjectClass == dt || !(dt is ObjectClass))
        {

#line 89 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("                throw new NotImplementedException(\"No handler registered on ",  m.ObjectClass.ClassName , ".",  m.MethodName , "\");\r\n");
#line 91 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
}
        else
        {

#line 95 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("                e.Result = base.",  m.MethodName , "(",  m.GetArguments() , ");\r\n");
#line 97 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
}

#line 99 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("            return e.Result;\r\n");
this.WriteObjects("        }\r\n");
#line 104 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
// define delegate type only on base class
		if (this.m.ObjectClass == this.dt)
		{

#line 108 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("		public delegate void ",  delegateName , "<T>(T obj, ",  returnArgsType , " ret",  parameterDefs , ");\r\n");
#line 110 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
}
	}


#line 114 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("		public event ",  delegateName , "<",  dt.ClassName , "> ",  eventName , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");

        }



    }
}