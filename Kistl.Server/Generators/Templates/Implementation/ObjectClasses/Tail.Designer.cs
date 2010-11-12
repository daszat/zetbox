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
		protected string implName;


        public Tail(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls, string implName)
            : base(_host)
        {
			this.ctx = ctx;
			this.cls = cls;
			this.implName = implName;

        }
        
        public override void Generate()
        {
#line 15 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\Tail.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        // tail template\r\n");
this.WriteObjects("   		// ",  this.GetType() , "\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerHidden()]\r\n");
this.WriteObjects("        [EventBasedMethod(\"OnToString_",  cls.Name , "\")]\r\n");
this.WriteObjects("        public override string ToString()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();\r\n");
this.WriteObjects("            e.Result = base.ToString();\r\n");
this.WriteObjects("            if (OnToString_",  cls.Name , " != null)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                OnToString_",  cls.Name , "(this, e);\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            return e.Result;\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        public static event ToStringHandler<",  cls.Name , "> OnToString_",  cls.Name , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [EventBasedMethod(\"OnPreSave_",  cls.Name , "\")]\r\n");
this.WriteObjects("        public override void NotifyPreSave()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.NotifyPreSave();\r\n");
this.WriteObjects("            if (OnPreSave_",  cls.Name , " != null) OnPreSave_",  cls.Name , "(this);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        public static event ObjectEventHandler<",  cls.Name , "> OnPreSave_",  cls.Name , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [EventBasedMethod(\"OnPostSave_",  cls.Name , "\")]\r\n");
this.WriteObjects("        public override void NotifyPostSave()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.NotifyPostSave();\r\n");
this.WriteObjects("            if (OnPostSave_",  cls.Name , " != null) OnPostSave_",  cls.Name , "(this);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        public static event ObjectEventHandler<",  cls.Name , "> OnPostSave_",  cls.Name , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [EventBasedMethod(\"OnCreated_",  cls.Name , "\")]\r\n");
this.WriteObjects("        public override void NotifyCreated()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.NotifyCreated();\r\n");
this.WriteObjects("            if (OnCreated_",  cls.Name , " != null) OnCreated_",  cls.Name , "(this);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        public static event ObjectEventHandler<",  cls.Name , "> OnCreated_",  cls.Name , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [EventBasedMethod(\"OnDeleting_",  cls.Name , "\")]\r\n");
this.WriteObjects("        public override void NotifyDeleting()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.NotifyDeleting();\r\n");
this.WriteObjects("            if (OnDeleting_",  cls.Name , " != null) OnDeleting_",  cls.Name , "(this);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        public static event ObjectEventHandler<",  cls.Name , "> OnDeleting_",  cls.Name , ";\r\n");
this.WriteObjects("\r\n");
#line 66 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\Tail.cst"
Implementation.ObjectClasses.CustomTypeDescriptor.Call(Host, ctx, cls, "PropertyDescriptor" + ImplementationSuffix, implName);


        }



    }
}