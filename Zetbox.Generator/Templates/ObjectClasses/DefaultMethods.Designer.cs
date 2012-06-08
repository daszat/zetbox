using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst")]
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
#line 17 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
this.WriteObjects("");
#line 30 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
this.WriteObjects("        #region ",  this.GetType() , "\n");
#line 31 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
ApplyRequisites(); 
#line 32 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
this.WriteObjects("\n");
this.WriteObjects("        [System.Diagnostics.DebuggerHidden()]\n");
this.WriteObjects("        [EventBasedMethod(\"OnToString_",  dt.Name , "\")]\n");
this.WriteObjects("        public override string ToString()\n");
this.WriteObjects("        {\n");
this.WriteObjects("            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();\n");
this.WriteObjects("            e.Result = base.ToString();\n");
this.WriteObjects("            if (OnToString_",  dt.Name , " != null)\n");
this.WriteObjects("            {\n");
this.WriteObjects("                OnToString_",  dt.Name , "(this, e);\n");
this.WriteObjects("            }\n");
this.WriteObjects("            return e.Result;\n");
this.WriteObjects("        }\n");
this.WriteObjects("        public static event ToStringHandler<",  dt.Name , "> OnToString_",  dt.Name , ";\n");
this.WriteObjects("\n");
this.WriteObjects("        [System.Diagnostics.DebuggerHidden()]\n");
this.WriteObjects("        [EventBasedMethod(\"OnObjectIsValid_",  dt.Name , "\")]\n");
this.WriteObjects("        protected override ObjectIsValidResult ObjectIsValid()\n");
this.WriteObjects("        {\n");
this.WriteObjects("            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();\n");
this.WriteObjects("            var b = base.ObjectIsValid();\n");
this.WriteObjects("            e.IsValid = b.IsValid;\n");
this.WriteObjects("            e.Errors.AddRange(b.Errors);\n");
this.WriteObjects("            if (OnObjectIsValid_",  dt.Name , " != null)\n");
this.WriteObjects("            {\n");
this.WriteObjects("                OnObjectIsValid_",  dt.Name , "(this, e);\n");
this.WriteObjects("            }\n");
this.WriteObjects("            return new ObjectIsValidResult(e.IsValid, e.Errors);\n");
this.WriteObjects("        }\n");
this.WriteObjects("        public static event ObjectIsValidHandler<",  dt.Name , "> OnObjectIsValid_",  dt.Name , ";\n");
this.WriteObjects("\n");
this.WriteObjects("        [EventBasedMethod(\"OnNotifyPreSave_",  dt.Name , "\")]\n");
this.WriteObjects("        public override void NotifyPreSave()\n");
this.WriteObjects("        {\n");
#line 66 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
ApplyPrePreSaveTemplate(); 
#line 67 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
this.WriteObjects("            base.NotifyPreSave();\n");
this.WriteObjects("            if (OnNotifyPreSave_",  dt.Name , " != null) OnNotifyPreSave_",  dt.Name , "(this);\n");
#line 69 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
ApplyPostPreSaveTemplate(); 
#line 70 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
this.WriteObjects("        }\n");
this.WriteObjects("        public static event ObjectEventHandler<",  dt.Name , "> OnNotifyPreSave_",  dt.Name , ";\n");
this.WriteObjects("\n");
this.WriteObjects("        [EventBasedMethod(\"OnNotifyPostSave_",  dt.Name , "\")]\n");
this.WriteObjects("        public override void NotifyPostSave()\n");
this.WriteObjects("        {\n");
#line 76 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
ApplyPrePostSaveTemplate(); 
#line 77 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
this.WriteObjects("            base.NotifyPostSave();\n");
this.WriteObjects("            if (OnNotifyPostSave_",  dt.Name , " != null) OnNotifyPostSave_",  dt.Name , "(this);\n");
#line 79 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
ApplyPostPostSaveTemplate(); 
#line 80 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
this.WriteObjects("        }\n");
this.WriteObjects("        public static event ObjectEventHandler<",  dt.Name , "> OnNotifyPostSave_",  dt.Name , ";\n");
this.WriteObjects("\n");
this.WriteObjects("        [EventBasedMethod(\"OnNotifyCreated_",  dt.Name , "\")]\n");
this.WriteObjects("        public override void NotifyCreated()\n");
this.WriteObjects("        {\n");
#line 86 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
ApplyPreCreatedTemplate(); 
#line 87 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
this.WriteObjects("            base.NotifyCreated();\n");
this.WriteObjects("            if (OnNotifyCreated_",  dt.Name , " != null) OnNotifyCreated_",  dt.Name , "(this);\n");
#line 89 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
ApplyPostCreatedTemplate(); 
#line 90 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
this.WriteObjects("        }\n");
this.WriteObjects("        public static event ObjectEventHandler<",  dt.Name , "> OnNotifyCreated_",  dt.Name , ";\n");
this.WriteObjects("\n");
this.WriteObjects("        [EventBasedMethod(\"OnNotifyDeleting_",  dt.Name , "\")]\n");
this.WriteObjects("        public override void NotifyDeleting()\n");
this.WriteObjects("        {\n");
#line 96 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
ApplyPreDeletingTemplate(); 
#line 97 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
this.WriteObjects("            base.NotifyDeleting();\n");
this.WriteObjects("            if (OnNotifyDeleting_",  dt.Name , " != null) OnNotifyDeleting_",  dt.Name , "(this);\n");
#line 99 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
ApplyPostDeletingTemplate(); 
#line 100 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\DefaultMethods.cst"
this.WriteObjects("        }\n");
this.WriteObjects("        public static event ObjectEventHandler<",  dt.Name , "> OnNotifyDeleting_",  dt.Name , ";\n");
this.WriteObjects("\n");
this.WriteObjects("        #endregion // ",  this.GetType() , "\n");

        }

    }
}