using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.CompoundObjects
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\CompoundObjects\DefaultMethods.cst")]
    public partial class DefaultMethods : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected DataType dt;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, DataType dt)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("CompoundObjects.DefaultMethods", ctx, dt);
        }

        public DefaultMethods(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, DataType dt)
            : base(_host)
        {
			this.ctx = ctx;
			this.dt = dt;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.Generator\Templates\CompoundObjects\DefaultMethods.cst"
this.WriteObjects("");
#line 30 "P:\zetbox\Zetbox.Generator\Templates\CompoundObjects\DefaultMethods.cst"
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
this.WriteObjects("		[System.Diagnostics.DebuggerHidden()]\r\n");
this.WriteObjects("        [EventBasedMethod(\"OnObjectIsValid_",  dt.Name , "\")]\r\n");
this.WriteObjects("        protected override ObjectIsValidResult ObjectIsValid()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();\r\n");
this.WriteObjects("			var b = base.ObjectIsValid();\r\n");
this.WriteObjects("            e.IsValid = b.IsValid;\r\n");
this.WriteObjects("			e.Errors.AddRange(b.Errors);\r\n");
this.WriteObjects("            if (OnObjectIsValid_",  dt.Name , " != null)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                OnObjectIsValid_",  dt.Name , "(this, e);\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            return new ObjectIsValidResult(e.IsValid, e.Errors);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        public static event ObjectIsValidHandler<",  dt.Name , "> OnObjectIsValid_",  dt.Name , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        #endregion // ",  this.GetType() , "\r\n");

        }

    }
}