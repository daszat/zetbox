using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst")]
    public partial class Method : Zetbox.Generator.MemberTemplate
    {
		protected IZetboxContext ctx;
		protected Zetbox.App.Base.DataType dt;
		protected Zetbox.App.Base.Method m;
		protected int index;
		protected string indexSuffix;
		protected string eventName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.App.Base.DataType dt, Zetbox.App.Base.Method m, int index, string indexSuffix, string eventName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.Method", ctx, dt, m, index, indexSuffix, eventName);
        }

        public Method(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.App.Base.DataType dt, Zetbox.App.Base.Method m, int index, string indexSuffix, string eventName)
            : base(_host)
        {
			this.ctx = ctx;
			this.dt = dt;
			this.m = m;
			this.index = index;
			this.indexSuffix = indexSuffix;
			this.eventName = eventName;

        }

        public override void Generate()
        {
#line 34 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\r\n");
#line 36 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
foreach(var attr in GetMethodAttributes())
    {

#line 39 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
this.WriteObjects("        ",  attr , "\r\n");
#line 41 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
}

#line 43 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
this.WriteObjects("        ",  GetModifiers() , " async ",  GetReturnType() , " ",  m.Name , "(",  GetParameterDefinitions() , ")\r\n");
#line 46 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
string delegateName = m.Name + indexSuffix + "_Handler";
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

#line 66 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
this.WriteObjects("        {\r\n");
this.WriteObjects("            // base.",  m.Name , "();\r\n");
this.WriteObjects("            if (",  eventName , " != null)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                await ",  eventName , "(this",  argumentDefs , ");\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            else\r\n");
this.WriteObjects("            {\r\n");
#line 75 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
if (m.ObjectClass == dt || !(dt is ObjectClass))
        {

#line 78 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
this.WriteObjects("                throw new NotImplementedException(\"No handler registered on method ",  m.ObjectClass.Name , ".",  m.Name , "\");\r\n");
#line 80 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
}
        else
        {

#line 84 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
this.WriteObjects("                await base.",  m.Name , "(",  m.GetArguments() , ");\r\n");
#line 86 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
}

#line 88 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
#line 91 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
// define delegate type only on base class
        if (this.m.ObjectClass == this.dt)
        {

#line 95 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
this.WriteObjects("        public delegate System.Threading.Tasks.Task ",  delegateName , "<T>(T obj",  parameterDefs , ");\r\n");
#line 97 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
}
    }
    else
    {

        string returnArgsType = String.Format("MethodReturnEventArgs<{0}>", returnParam.GetParameterTypeString());

#line 104 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
this.WriteObjects("        {\r\n");
this.WriteObjects("            var e = new ",  returnArgsType , "();\r\n");
this.WriteObjects("            if (",  eventName , " != null)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                await ",  eventName , "(this, e",  argumentDefs , ");\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            else\r\n");
this.WriteObjects("            {\r\n");
#line 113 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
if (m.ObjectClass == dt || !(dt is ObjectClass))
        {

#line 116 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
this.WriteObjects("                throw new NotImplementedException(\"No handler registered on ",  m.ObjectClass.Name , ".",  m.Name , "\");\r\n");
#line 118 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
}
        else
        {

#line 122 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
this.WriteObjects("                e.Result = await base.",  m.Name , "(",  m.GetArguments() , ");\r\n");
#line 124 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
}

#line 126 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("            return e.Result;\r\n");
this.WriteObjects("        }\r\n");
#line 131 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
// define delegate type only on base class
        if (this.m.ObjectClass == this.dt)
        {

#line 135 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
this.WriteObjects("        public delegate System.Threading.Tasks.Task ",  delegateName , "<T>(T obj, ",  returnArgsType , " ret",  parameterDefs , ");\r\n");
#line 137 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
}
    }


#line 141 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
this.WriteObjects("        public static event ",  delegateName , "<",  dt.Name , "> ",  eventName , ";\r\n");
#line 143 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\Method.cst"
if(index == 0) {
	// Only for first overload
	MethodCanExec.Call(Host, ctx, dt, m, eventName);
} 
        // END <%= this.GetType() 

        }

    }
}