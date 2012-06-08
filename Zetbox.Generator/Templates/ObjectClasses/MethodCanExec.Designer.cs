using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst")]
    public partial class MethodCanExec : Zetbox.Generator.MemberTemplate
    {
		protected IZetboxContext ctx;
		protected Zetbox.App.Base.DataType dt;
		protected Zetbox.App.Base.Method m;
		protected string eventName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.App.Base.DataType dt, Zetbox.App.Base.Method m, string eventName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.MethodCanExec", ctx, dt, m, eventName);
        }

        public MethodCanExec(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.App.Base.DataType dt, Zetbox.App.Base.Method m, string eventName)
            : base(_host)
        {
			this.ctx = ctx;
			this.dt = dt;
			this.m = m;
			this.eventName = eventName;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("");
#line 32 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\n");
this.WriteObjects("		// CanExec\n");
#line 35 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
var eventName_CanExec = eventName + "_CanExec";

#line 37 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("		public static event CanExecMethodEventHandler<",  dt.Name , "> ",  eventName_CanExec , ";\n");
this.WriteObjects("\n");
this.WriteObjects("        [EventBasedMethod(\"",  eventName_CanExec , "\")]\n");
this.WriteObjects("        ",  GetModifiers() , " bool ",  m.Name , "CanExec\n");
this.WriteObjects("        {\n");
this.WriteObjects("			get \n");
this.WriteObjects("			{\n");
this.WriteObjects("				var e = new MethodReturnEventArgs<bool>();\n");
this.WriteObjects("				if (",  eventName_CanExec , " != null)\n");
this.WriteObjects("				{\n");
this.WriteObjects("					",  eventName_CanExec , "(this, e);\n");
this.WriteObjects("				}\n");
this.WriteObjects("				else\n");
this.WriteObjects("				{\n");
#line 51 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
if (m.ObjectClass == dt || !(dt is ObjectClass)) { 
#line 52 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("					e.Result = true;\n");
#line 53 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
} else { 
#line 54 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("					e.Result = base.",  m.Name , "CanExec;\n");
#line 55 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
} 
#line 56 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("				}\n");
this.WriteObjects("				return e.Result;\n");
this.WriteObjects("			}\n");
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("		// CanExecReason\n");
#line 63 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
var eventName_CanExecReason = eventName + "_CanExecReason";

#line 65 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("		public static event CanExecReasonMethodEventHandler<",  dt.Name , "> ",  eventName_CanExecReason , ";\n");
this.WriteObjects("\n");
this.WriteObjects("        [EventBasedMethod(\"",  eventName_CanExecReason , "\")]\n");
this.WriteObjects("        ",  GetModifiers() , " string ",  m.Name , "CanExecReason\n");
this.WriteObjects("        {\n");
this.WriteObjects("			get \n");
this.WriteObjects("			{\n");
this.WriteObjects("				var e = new MethodReturnEventArgs<string>();\n");
this.WriteObjects("				if (",  eventName_CanExecReason , " != null)\n");
this.WriteObjects("				{\n");
this.WriteObjects("					",  eventName_CanExecReason , "(this, e);\n");
this.WriteObjects("				}\n");
this.WriteObjects("				else\n");
this.WriteObjects("				{\n");
#line 79 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
if (m.ObjectClass == dt || !(dt is ObjectClass)) { 
#line 80 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("					e.Result = string.Empty;\n");
#line 81 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
} else { 
#line 82 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("					e.Result = base.",  m.Name , "CanExecReason;\n");
#line 83 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
} 
#line 84 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("				}\n");
this.WriteObjects("				return e.Result;\n");
this.WriteObjects("			}\n");
this.WriteObjects("        }\n");
this.WriteObjects("        // END ",  this.GetType() , "\n");

        }

    }
}