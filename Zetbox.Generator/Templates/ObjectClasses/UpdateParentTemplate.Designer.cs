using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\UpdateParentTemplate.cst")]
    public partial class UpdateParentTemplate : Zetbox.Generator.ResourceTemplate
    {
		protected List<UpdateParentTemplateParams> props;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, List<UpdateParentTemplateParams> props)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.UpdateParentTemplate", props);
        }

        public UpdateParentTemplate(Arebis.CodeGeneration.IGenerationHost _host, List<UpdateParentTemplateParams> props)
            : base(_host)
        {
			this.props = props;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\UpdateParentTemplate.cst"
this.WriteObjects("");
#line 32 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\UpdateParentTemplate.cst"
this.WriteObjects("\n");
#line 33 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\UpdateParentTemplate.cst"
if (props.Count > 0) {               
#line 34 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\UpdateParentTemplate.cst"
this.WriteObjects("        public override void UpdateParent(string propertyName, IDataObject parentObj)\n");
this.WriteObjects("        {\n");
this.WriteObjects("            switch(propertyName)\n");
this.WriteObjects("            {\n");
#line 38 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\UpdateParentTemplate.cst"
foreach(var prop in props) {    
#line 39 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\UpdateParentTemplate.cst"
ApplyCase(prop);                
#line 40 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\UpdateParentTemplate.cst"
}                                    
#line 41 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\UpdateParentTemplate.cst"
this.WriteObjects("                default:\n");
this.WriteObjects("                    base.UpdateParent(propertyName, parentObj);\n");
this.WriteObjects("                    break;\n");
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
#line 46 "P:\zetbox\Zetbox.Generator\Templates\ObjectClasses\UpdateParentTemplate.cst"
}                                    

        }

    }
}