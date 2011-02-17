using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/DefaultMethods.cst")]
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
#line 14 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/DefaultMethods.cst"
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
#line 33 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/DefaultMethods.cst"
ApplyPrePreSaveTemplate(); 
#line 34 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/DefaultMethods.cst"
this.WriteObjects("            base.NotifyPreSave();\r\n");
this.WriteObjects("            if (OnPreSave_",  dt.Name , " != null) OnPreSave_",  dt.Name , "(this);\r\n");
#line 36 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/DefaultMethods.cst"
ApplyPostPreSaveTemplate(); 
#line 37 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/DefaultMethods.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("        public static event ObjectEventHandler<",  dt.Name , "> OnPreSave_",  dt.Name , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [EventBasedMethod(\"OnPostSave_",  dt.Name , "\")]\r\n");
this.WriteObjects("        public override void NotifyPostSave()\r\n");
this.WriteObjects("        {\r\n");
#line 43 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/DefaultMethods.cst"
ApplyPrePostSaveTemplate(); 
#line 44 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/DefaultMethods.cst"
this.WriteObjects("            base.NotifyPostSave();\r\n");
this.WriteObjects("            if (OnPostSave_",  dt.Name , " != null) OnPostSave_",  dt.Name , "(this);\r\n");
#line 46 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/DefaultMethods.cst"
ApplyPostPostSaveTemplate(); 
#line 47 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/DefaultMethods.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("        public static event ObjectEventHandler<",  dt.Name , "> OnPostSave_",  dt.Name , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [EventBasedMethod(\"OnCreated_",  dt.Name , "\")]\r\n");
this.WriteObjects("        public override void NotifyCreated()\r\n");
this.WriteObjects("        {\r\n");
#line 53 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/DefaultMethods.cst"
ApplyPreCreatedTemplate(); 
#line 54 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/DefaultMethods.cst"
this.WriteObjects("            base.NotifyCreated();\r\n");
this.WriteObjects("            if (OnCreated_",  dt.Name , " != null) OnCreated_",  dt.Name , "(this);\r\n");
#line 56 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/DefaultMethods.cst"
ApplyPostCreatedTemplate(); 
#line 57 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/DefaultMethods.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("        public static event ObjectEventHandler<",  dt.Name , "> OnCreated_",  dt.Name , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [EventBasedMethod(\"OnDeleting_",  dt.Name , "\")]\r\n");
this.WriteObjects("        public override void NotifyDeleting()\r\n");
this.WriteObjects("        {\r\n");
#line 63 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/DefaultMethods.cst"
ApplyPreDeletingTemplate(); 
#line 64 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/DefaultMethods.cst"
this.WriteObjects("            base.NotifyDeleting();\r\n");
this.WriteObjects("            if (OnDeleting_",  dt.Name , " != null) OnDeleting_",  dt.Name , "(this);\r\n");
#line 66 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/DefaultMethods.cst"
ApplyPostDeletingTemplate(); 
#line 67 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/DefaultMethods.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("        public static event ObjectEventHandler<",  dt.Name , "> OnDeleting_",  dt.Name , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        #endregion // ",  this.GetType() , "\r\n");

        }

    }
}