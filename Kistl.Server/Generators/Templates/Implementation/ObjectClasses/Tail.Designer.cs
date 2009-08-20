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
this.WriteObjects("   		// ",  this.GetType() , "\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerHidden()]\r\n");
this.WriteObjects("        [EventBasedMethod(\"OnToString_",  cls.ClassName , "\")]\r\n");
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
this.WriteObjects("        [EventBasedMethod(\"OnPreSave_",  cls.ClassName , "\")]\r\n");
this.WriteObjects("        public override void NotifyPreSave()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.NotifyPreSave();\r\n");
this.WriteObjects("            if (OnPreSave_",  cls.ClassName , " != null) OnPreSave_",  cls.ClassName , "(this);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        public event ObjectEventHandler<",  cls.ClassName , "> OnPreSave_",  cls.ClassName , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [EventBasedMethod(\"OnPostSave_",  cls.ClassName , "\")]\r\n");
this.WriteObjects("        public override void NotifyPostSave()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.NotifyPostSave();\r\n");
this.WriteObjects("            if (OnPostSave_",  cls.ClassName , " != null) OnPostSave_",  cls.ClassName , "(this);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        public event ObjectEventHandler<",  cls.ClassName , "> OnPostSave_",  cls.ClassName , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [EventBasedMethod(\"OnCreated_",  cls.ClassName , "\")]\r\n");
this.WriteObjects("        public override void NotifyCreated()\r\n");
this.WriteObjects("        {\r\n");
#line 51 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\Tail.cst"
if(cls.Properties.Count(p => p.DefaultValue != null) > 0)
			{

#line 54 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\Tail.cst"
this.WriteObjects("            try\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("				Kistl.App.Base.Property p = null;\r\n");
#line 58 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\Tail.cst"
foreach (var prop in cls.Properties.Where(p => p.DefaultValue != null))
				{

#line 61 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\Tail.cst"
this.WriteObjects("				p = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid(\"",  prop.ExportGuid , "\"));\r\n");
this.WriteObjects("				if(p != null && p.DefaultValue != null) { this.",  prop.PropertyName , " = (",  prop.ReferencedTypeAsCSharp() , ")p.DefaultValue.GetDefaultValue(); } else { System.Diagnostics.Trace.TraceWarning(\"",  string.Format("Unable to get default value for property '{0}.{1}'", prop.ObjectClass.ClassName, prop.PropertyName) , "\"); }\r\n");
#line 64 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\Tail.cst"
}

#line 66 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\Tail.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("            catch (TypeLoadException)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                // TODO: Find a better way to ignore bootstrap errors.\r\n");
this.WriteObjects("                // During bootstrapping no MethodInvocation is registred\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            catch (NotImplementedException)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                // TODO: Find a better way to ignore bootstrap errors.\r\n");
this.WriteObjects("                // During bootstrapping no MethodInvocation is registred\r\n");
this.WriteObjects("            }\r\n");
#line 78 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\Tail.cst"
}

#line 80 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\Tail.cst"
this.WriteObjects("            base.NotifyCreated();\r\n");
this.WriteObjects("            if (OnCreated_",  cls.ClassName , " != null) OnCreated_",  cls.ClassName , "(this);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        public event ObjectEventHandler<",  cls.ClassName , "> OnCreated_",  cls.ClassName , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [EventBasedMethod(\"OnDeleting_",  cls.ClassName , "\")]\r\n");
this.WriteObjects("        public override void NotifyDeleting()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.NotifyDeleting();\r\n");
this.WriteObjects("            if (OnDeleting_",  cls.ClassName , " != null) OnDeleting_",  cls.ClassName , "(this);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        public event ObjectEventHandler<",  cls.ClassName , "> OnDeleting_",  cls.ClassName , ";\r\n");
this.WriteObjects("\r\n");
#line 94 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\Tail.cst"
Implementation.ObjectClasses.GetPropertyErrorTemplate.Call(Host, ctx, cls);


        }



    }
}