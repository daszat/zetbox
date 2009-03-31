using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\Tail.cst")]
    public partial class Tail : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected ObjectClass cls;


        public Tail(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls)
            : base(_host)
        {
			this.ctx = ctx;
			this.cls = cls;

        }
        
        public override void Generate()
        {
#line 14 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\Tail.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        // tail template\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerHidden()]\r\n");
this.WriteObjects("        public override string ToString()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();\r\n");
this.WriteObjects("            e.Result = base.ToString();\r\n");
this.WriteObjects("            if (OnToString_",  cls.ClassName , " != null)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                OnToString_",  cls.ClassName , "(this, e);\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            return e.Result;\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        public event ToStringHandler<",  cls.ClassName , "> OnToString_",  cls.ClassName , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public override void NotifyPreSave()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.NotifyPreSave();\r\n");
this.WriteObjects("            if (OnPreSave_",  cls.ClassName , " != null) OnPreSave_",  cls.ClassName , "(this);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        public event ObjectEventHandler<",  cls.ClassName , "> OnPreSave_",  cls.ClassName , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public override void NotifyPostSave()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.NotifyPostSave();\r\n");
this.WriteObjects("            if (OnPostSave_",  cls.ClassName , " != null) OnPostSave_",  cls.ClassName , "(this);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        public event ObjectEventHandler<",  cls.ClassName , "> OnPostSave_",  cls.ClassName , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");

        }



    }
}