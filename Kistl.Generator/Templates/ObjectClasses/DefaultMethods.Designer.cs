using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\ObjectClasses\DefaultMethods.cst")]
    public partial class DefaultMethods : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected DataType dt;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, DataType dt)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.DefaultMethods", ctx, dt);
        }

        public DefaultMethods(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, DataType dt)
            : base(_host)
        {
			this.ctx = ctx;
			this.dt = dt;

        }

        public override void Generate()
        {
#line 14 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\DefaultMethods.cst"
this.WriteObjects("        #region ",  this.GetType() , "\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerHidden()]\r\n");
this.WriteObjects("        [EventBasedMethod(\"OnToString_",  dt.Name , "\")]\r\n");
this.WriteObjects("        public override string ToString()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();\r\n");
this.WriteObjects("            e.Result = base.ToString();\r\n");
this.WriteObjects("            if (OnToString_",  dt.Name , " != null)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                OnToString_",  dt.Name , "(this, e);\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            return e.Result;\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        public static event ToStringHandler<",  dt.Name , "> OnToString_",  dt.Name , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [EventBasedMethod(\"OnPreSave_",  dt.Name , "\")]\r\n");
this.WriteObjects("        public override void NotifyPreSave()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.NotifyPreSave();\r\n");
this.WriteObjects("            if (OnPreSave_",  dt.Name , " != null) OnPreSave_",  dt.Name , "(this);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        public static event ObjectEventHandler<",  dt.Name , "> OnPreSave_",  dt.Name , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [EventBasedMethod(\"OnPostSave_",  dt.Name , "\")]\r\n");
this.WriteObjects("        public override void NotifyPostSave()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.NotifyPostSave();\r\n");
this.WriteObjects("            if (OnPostSave_",  dt.Name , " != null) OnPostSave_",  dt.Name , "(this);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        public static event ObjectEventHandler<",  dt.Name , "> OnPostSave_",  dt.Name , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [EventBasedMethod(\"OnCreated_",  dt.Name , "\")]\r\n");
this.WriteObjects("        public override void NotifyCreated()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.NotifyCreated();\r\n");
this.WriteObjects("            if (OnCreated_",  dt.Name , " != null) OnCreated_",  dt.Name , "(this);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        public static event ObjectEventHandler<",  dt.Name , "> OnCreated_",  dt.Name , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [EventBasedMethod(\"OnDeleting_",  dt.Name , "\")]\r\n");
this.WriteObjects("        public override void NotifyDeleting()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.NotifyDeleting();\r\n");
this.WriteObjects("            if (OnDeleting_",  dt.Name , " != null) OnDeleting_",  dt.Name , "(this);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        public static event ObjectEventHandler<",  dt.Name , "> OnDeleting_",  dt.Name , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        #endregion // ",  this.GetType() , "\r\n");

        }

    }
}