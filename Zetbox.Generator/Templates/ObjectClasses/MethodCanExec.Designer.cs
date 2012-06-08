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
#line 32 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\r\n");
this.WriteObjects("		// CanExec\r\n");
#line 35 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
var eventName_CanExec = eventName + "_CanExec";

#line 37 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("		public static event CanExecMethodEventHandler<",  dt.Name , "> ",  eventName_CanExec , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [EventBasedMethod(\"",  eventName_CanExec , "\")]\r\n");
this.WriteObjects("        ",  GetModifiers() , " bool ",  m.Name , "CanExec\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("			get \r\n");
this.WriteObjects("			{\r\n");
this.WriteObjects("				var e = new MethodReturnEventArgs<bool>();\r\n");
this.WriteObjects("				if (",  eventName_CanExec , " != null)\r\n");
this.WriteObjects("				{\r\n");
this.WriteObjects("					",  eventName_CanExec , "(this, e);\r\n");
this.WriteObjects("				}\r\n");
this.WriteObjects("				else\r\n");
this.WriteObjects("				{\r\n");
#line 51 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
if (m.ObjectClass == dt || !(dt is ObjectClass)) { 
#line 52 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("					e.Result = true;\r\n");
#line 53 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
} else { 
#line 54 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("					e.Result = base.",  m.Name , "CanExec;\r\n");
#line 55 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
} 
#line 56 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("				}\r\n");
this.WriteObjects("				return e.Result;\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		// CanExecReason\r\n");
#line 63 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
var eventName_CanExecReason = eventName + "_CanExecReason";

#line 65 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("		public static event CanExecReasonMethodEventHandler<",  dt.Name , "> ",  eventName_CanExecReason , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [EventBasedMethod(\"",  eventName_CanExecReason , "\")]\r\n");
this.WriteObjects("        ",  GetModifiers() , " string ",  m.Name , "CanExecReason\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("			get \r\n");
this.WriteObjects("			{\r\n");
this.WriteObjects("				var e = new MethodReturnEventArgs<string>();\r\n");
this.WriteObjects("				if (",  eventName_CanExecReason , " != null)\r\n");
this.WriteObjects("				{\r\n");
this.WriteObjects("					",  eventName_CanExecReason , "(this, e);\r\n");
this.WriteObjects("				}\r\n");
this.WriteObjects("				else\r\n");
this.WriteObjects("				{\r\n");
#line 79 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
if (m.ObjectClass == dt || !(dt is ObjectClass)) { 
#line 80 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("					e.Result = string.Empty;\r\n");
#line 81 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
} else { 
#line 82 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("					e.Result = base.",  m.Name , "CanExecReason;\r\n");
#line 83 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
} 
#line 84 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("				}\r\n");
this.WriteObjects("				return e.Result;\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        // END ",  this.GetType() , "\r\n");

        }

    }
}