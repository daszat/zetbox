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
		protected int index;


        public MethodBody(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.App.Base.DataType dt, Kistl.App.Base.Method m, int index)
            : base(_host)
        {
			this.ctx = ctx;
			this.dt = dt;
			this.m = m;
			this.index = index;

        }
        
        public override void Generate()
        {
#line 12 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("\r\n");
#line 19 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
string indexSuffix = index == 0 ? String.Empty : index.ToString();
	string delegateName = m.MethodName + indexSuffix + "_Handler";
	string eventName = "On" + m.MethodName + indexSuffix + "_" + dt.ClassName;
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

#line 42 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("		{\r\n");
this.WriteObjects("            // base.",  m.MethodName , "();\r\n");
this.WriteObjects("            if (",  eventName , " != null)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("				",  eventName , "(this",  argumentDefs , ");\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("			else\r\n");
this.WriteObjects("			{\r\n");
#line 51 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
if (m.ObjectClass == dt || !(dt is ObjectClass))
        {

#line 54 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("                throw new NotImplementedException(\"No handler registered on ",  m.ObjectClass.ClassName , ".",  m.MethodName , "\");\r\n");
#line 56 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
}
        else
        {

#line 60 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("			    base.",  m.MethodName , "(",  m.GetArguments() , ");\r\n");
#line 62 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
}

#line 64 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("			}\r\n");
this.WriteObjects("        }\r\n");
#line 67 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
// define delegate type only on base class
		if (this.m.ObjectClass == this.dt)
		{

#line 71 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("		public delegate void ",  delegateName , "<T>(T obj",  parameterDefs , ");\r\n");
#line 73 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
}
	}
	else
	{

		string returnArgsType = String.Format("MethodReturnEventArgs<{0}>", returnParam.ReturnedTypeAsCSharp());

#line 80 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("        {\r\n");
this.WriteObjects("            var e = new ",  returnArgsType , "();\r\n");
this.WriteObjects("            if (",  eventName , " != null)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                ",  eventName , "(this, e",  argumentDefs , ");\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            else\r\n");
this.WriteObjects("            {\r\n");
#line 89 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
if (m.ObjectClass == dt || !(dt is ObjectClass))
        {

#line 92 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("                throw new NotImplementedException(\"No handler registered on ",  m.ObjectClass.ClassName , ".",  m.MethodName , "\");\r\n");
#line 94 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
}
        else
        {

#line 98 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("                e.Result = base.",  m.MethodName , "(",  m.GetArguments() , ");\r\n");
#line 100 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
}

#line 102 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("            return e.Result;\r\n");
this.WriteObjects("        }\r\n");
#line 107 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
// define delegate type only on base class
		if (this.m.ObjectClass == this.dt)
		{

#line 111 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("		public delegate void ",  delegateName , "<T>(T obj, ",  returnArgsType , " ret",  parameterDefs , ");\r\n");
#line 113 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
}
	}


#line 117 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\MethodBody.cst"
this.WriteObjects("		public static event ",  delegateName , "<",  dt.ClassName , "> ",  eventName , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");

        }



    }
}