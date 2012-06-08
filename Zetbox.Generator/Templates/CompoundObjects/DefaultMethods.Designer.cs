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
this.WriteObjects("        #region ",  this.GetType() , "\n");
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
this.WriteObjects("		[System.Diagnostics.DebuggerHidden()]\n");
this.WriteObjects("        [EventBasedMethod(\"OnObjectIsValid_",  dt.Name , "\")]\n");
this.WriteObjects("        protected override ObjectIsValidResult ObjectIsValid()\n");
this.WriteObjects("        {\n");
this.WriteObjects("            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();\n");
this.WriteObjects("			var b = base.ObjectIsValid();\n");
this.WriteObjects("            e.IsValid = b.IsValid;\n");
this.WriteObjects("			e.Errors.AddRange(b.Errors);\n");
this.WriteObjects("            if (OnObjectIsValid_",  dt.Name , " != null)\n");
this.WriteObjects("            {\n");
this.WriteObjects("                OnObjectIsValid_",  dt.Name , "(this, e);\n");
this.WriteObjects("            }\n");
this.WriteObjects("            return new ObjectIsValidResult(e.IsValid, e.Errors);\n");
this.WriteObjects("        }\n");
this.WriteObjects("        public static event ObjectIsValidHandler<",  dt.Name , "> OnObjectIsValid_",  dt.Name , ";\n");
this.WriteObjects("\n");
this.WriteObjects("        #endregion // ",  this.GetType() , "\n");

        }

    }
}