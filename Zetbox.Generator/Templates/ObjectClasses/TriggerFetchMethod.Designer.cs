using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\TriggerFetchMethod.cst")]
    public partial class TriggerFetchMethod : Zetbox.Generator.ResourceTemplate
    {
		protected List<string> propertyNames;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, List<string> propertyNames)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.TriggerFetchMethod", propertyNames);
        }

        public TriggerFetchMethod(Arebis.CodeGeneration.IGenerationHost _host, List<string> propertyNames)
            : base(_host)
        {
			this.propertyNames = propertyNames;

        }

        public override void Generate()
        {
#line 30 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\TriggerFetchMethod.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public override Zetbox.API.Async.ZbTask TriggerFetch(string propName)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            switch(propName)\r\n");
this.WriteObjects("            {\r\n");
#line 35 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\TriggerFetchMethod.cst"
foreach(var p in propertyNames) {   
#line 36 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\TriggerFetchMethod.cst"
this.WriteObjects("            case \"",  p , "\":\r\n");
this.WriteObjects("                return TriggerFetch",  p , "Async();\r\n");
#line 38 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\TriggerFetchMethod.cst"
}                                            
#line 39 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\TriggerFetchMethod.cst"
this.WriteObjects("            default:\r\n");
this.WriteObjects("                return base.TriggerFetch(propName);\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");

        }

    }
}