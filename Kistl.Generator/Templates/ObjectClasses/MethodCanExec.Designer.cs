using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\ObjectClasses\MethodCanExec.cst")]
    public partial class MethodCanExec : Kistl.Generator.MemberTemplate
    {
		protected IKistlContext ctx;
		protected Kistl.App.Base.DataType dt;
		protected Kistl.App.Base.Method m;
		protected string eventName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.App.Base.DataType dt, Kistl.App.Base.Method m, string eventName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.MethodCanExec", ctx, dt, m, eventName);
        }

        public MethodCanExec(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.App.Base.DataType dt, Kistl.App.Base.Method m, string eventName)
            : base(_host)
        {
			this.ctx = ctx;
			this.dt = dt;
			this.m = m;
			this.eventName = eventName;

        }

        public override void Generate()
        {
#line 16 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\r\n");
this.WriteObjects("		// CanExec\r\n");
#line 19 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\MethodCanExec.cst"
var eventName_CanExec = eventName + "_CanExec";

#line 21 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\MethodCanExec.cst"
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
#line 35 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\MethodCanExec.cst"
if (m.ObjectClass == dt || !(dt is ObjectClass)) { 
#line 36 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("					e.Result = true;\r\n");
#line 37 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\MethodCanExec.cst"
} else { 
#line 38 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("					e.Result = base.",  m.Name , "CanExec;\r\n");
#line 39 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\MethodCanExec.cst"
} 
#line 40 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("				}\r\n");
this.WriteObjects("				return e.Result;\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		// CanExecReason\r\n");
#line 47 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\MethodCanExec.cst"
var eventName_CanExecReason = eventName + "_CanExecReason";

#line 49 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\MethodCanExec.cst"
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
#line 63 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\MethodCanExec.cst"
if (m.ObjectClass == dt || !(dt is ObjectClass)) { 
#line 64 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("					e.Result = string.Empty;\r\n");
#line 65 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\MethodCanExec.cst"
} else { 
#line 66 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("					e.Result = base.",  m.Name , "CanExecReason;\r\n");
#line 67 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\MethodCanExec.cst"
} 
#line 68 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\MethodCanExec.cst"
this.WriteObjects("				}\r\n");
this.WriteObjects("				return e.Result;\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        // END ",  this.GetType() , "\r\n");

        }

    }
}