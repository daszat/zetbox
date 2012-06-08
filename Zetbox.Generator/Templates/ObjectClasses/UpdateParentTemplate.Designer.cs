using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\ObjectClasses\UpdateParentTemplate.cst")]
    public partial class UpdateParentTemplate : Kistl.Generator.ResourceTemplate
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
#line 16 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\UpdateParentTemplate.cst"
this.WriteObjects("\r\n");
#line 17 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\UpdateParentTemplate.cst"
if (props.Count > 0) {               
#line 18 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\UpdateParentTemplate.cst"
this.WriteObjects("        public override void UpdateParent(string propertyName, IDataObject parentObj)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            switch(propertyName)\r\n");
this.WriteObjects("            {\r\n");
#line 22 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\UpdateParentTemplate.cst"
foreach(var prop in props) {    
#line 23 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\UpdateParentTemplate.cst"
ApplyCase(prop);                
#line 24 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\UpdateParentTemplate.cst"
}                                    
#line 25 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\UpdateParentTemplate.cst"
this.WriteObjects("                default:\r\n");
this.WriteObjects("                    base.UpdateParent(propertyName, parentObj);\r\n");
this.WriteObjects("                    break;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
#line 30 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\UpdateParentTemplate.cst"
}                                    

        }

    }
}