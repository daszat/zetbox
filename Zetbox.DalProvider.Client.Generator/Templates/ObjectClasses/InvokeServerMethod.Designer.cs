using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.Client.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\InvokeServerMethod.cst")]
    public partial class InvokeServerMethod : Zetbox.Generator.Templates.ObjectClasses.Method
    {


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.InvokeServerMethod");
        }

        public InvokeServerMethod(Arebis.CodeGeneration.IGenerationHost _host)
            : base(_host)
        {

        }

        public override void Generate()
        {
#line 28 "P:\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\InvokeServerMethod.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  GetReturnType() , " ",  m.Name , "(",  GetParameterDefinitions() , ")\r\n");
#line 32 "P:\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\InvokeServerMethod.cst"
var returnParam = m.Parameter.SingleOrDefault(parameter => parameter.IsReturnParameter);

    string argumentDefs = m.GetArguments();
    if (!String.IsNullOrEmpty(argumentDefs))
    {
        // add leading comma for later usage
        argumentDefs = ", " + argumentDefs;
    }

    string argumentTypes = m.GetArgumentTypes();
    // add leading comma for later usage
    argumentTypes = "new Type[] {" + argumentTypes + "}";


    if (returnParam == null)
    {

#line 49 "P:\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\InvokeServerMethod.cst"
this.WriteObjects("        {\r\n");
this.WriteObjects("            Context.ClientInternals().InvokeServerMethod(this, \"",  m.Name , "\", typeof(",  GetReturnType() , "), ",  argumentTypes, "",  argumentDefs , ");\r\n");
this.WriteObjects("        }\r\n");
#line 53 "P:\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\InvokeServerMethod.cst"
}
    else
    {

#line 57 "P:\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\InvokeServerMethod.cst"
this.WriteObjects("        {\r\n");
this.WriteObjects("            return (",  GetReturnType() , ")Context.ClientInternals().InvokeServerMethod(this, \"",  m.Name , "\", typeof(",  GetReturnType() , "), ",  argumentTypes , "",  argumentDefs , ");\r\n");
this.WriteObjects("        }\r\n");
#line 61 "P:\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\InvokeServerMethod.cst"
}

if(index == 0) {
	// Only for first overload
	Zetbox.Generator.Templates.ObjectClasses.MethodCanExec.Call(Host, ctx, dt, m, eventName);
}

#line 68 "P:\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\InvokeServerMethod.cst"
this.WriteObjects("        // END ",  this.GetType() , "\r\n");

        }

    }
}