using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst")]
    public partial class DefaultMethods : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected DataType dt;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, DataType dt)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.DefaultMethods", ctx, dt);
        }

        public DefaultMethods(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, DataType dt)
            : base(_host)
        {
			this.ctx = ctx;
			this.dt = dt;

        }

        public override void Generate()
        {
#line 30 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
this.WriteObjects("        #region ",  this.GetType() , "\r\n");
#line 31 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
ApplyRequisites(); 
#line 32 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
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
this.WriteObjects("        [System.Diagnostics.DebuggerHidden()]\r\n");
this.WriteObjects("        [EventBasedMethod(\"OnObjectIsValid_",  dt.Name , "\")]\r\n");
this.WriteObjects("        protected override ObjectIsValidResult ObjectIsValid()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();\r\n");
this.WriteObjects("            var b = base.ObjectIsValid();\r\n");
this.WriteObjects("            e.Errors.AddRange(b.Errors);\r\n");
this.WriteObjects("            if (OnObjectIsValid_",  dt.Name , " != null)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                OnObjectIsValid_",  dt.Name , "(this, e);\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            return new ObjectIsValidResult(e.IsValid, e.Errors);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        public static event ObjectIsValidHandler<",  dt.Name , "> OnObjectIsValid_",  dt.Name , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [EventBasedMethod(\"OnNotifyPreSave_",  dt.Name , "\")]\r\n");
this.WriteObjects("        public override void NotifyPreSave()\r\n");
this.WriteObjects("        {\r\n");
#line 65 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
ApplyPrePreSaveTemplate(); 
#line 66 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
this.WriteObjects("            base.NotifyPreSave();\r\n");
this.WriteObjects("            if (OnNotifyPreSave_",  dt.Name , " != null) OnNotifyPreSave_",  dt.Name , "(this);\r\n");
#line 68 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
ApplyPostPreSaveTemplate(); 
#line 69 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("        public static event ObjectEventHandler<",  dt.Name , "> OnNotifyPreSave_",  dt.Name , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [EventBasedMethod(\"OnNotifyPostSave_",  dt.Name , "\")]\r\n");
this.WriteObjects("        public override void NotifyPostSave()\r\n");
this.WriteObjects("        {\r\n");
#line 75 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
ApplyPrePostSaveTemplate(); 
#line 76 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
this.WriteObjects("            base.NotifyPostSave();\r\n");
this.WriteObjects("            if (OnNotifyPostSave_",  dt.Name , " != null) OnNotifyPostSave_",  dt.Name , "(this);\r\n");
#line 78 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
ApplyPostPostSaveTemplate(); 
#line 79 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("        public static event ObjectEventHandler<",  dt.Name , "> OnNotifyPostSave_",  dt.Name , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [EventBasedMethod(\"OnNotifyCreated_",  dt.Name , "\")]\r\n");
this.WriteObjects("        public override void NotifyCreated()\r\n");
this.WriteObjects("        {\r\n");
#line 85 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
ApplyPreCreatedTemplate(); 
#line 86 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
this.WriteObjects("            base.NotifyCreated();\r\n");
this.WriteObjects("            if (OnNotifyCreated_",  dt.Name , " != null) OnNotifyCreated_",  dt.Name , "(this);\r\n");
#line 88 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
ApplyPostCreatedTemplate(); 
#line 89 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("        public static event ObjectEventHandler<",  dt.Name , "> OnNotifyCreated_",  dt.Name , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [EventBasedMethod(\"OnNotifyDeleting_",  dt.Name , "\")]\r\n");
this.WriteObjects("        public override void NotifyDeleting()\r\n");
this.WriteObjects("        {\r\n");
#line 95 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
ApplyPreDeletingTemplate(); 
#line 96 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
this.WriteObjects("            base.NotifyDeleting();\r\n");
this.WriteObjects("            if (OnNotifyDeleting_",  dt.Name , " != null) OnNotifyDeleting_",  dt.Name , "(this);\r\n");
#line 98 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
ApplyPostDeletingTemplate(); 
#line 99 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("        public static event ObjectEventHandler<",  dt.Name , "> OnNotifyDeleting_",  dt.Name , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        #endregion // ",  this.GetType() , "\r\n");

        }

    }
}