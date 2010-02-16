using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\UpdateParentTemplate.cst")]
    public partial class UpdateParentTemplate : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected List<ObjectReferenceProperty> props;


        public UpdateParentTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, List<ObjectReferenceProperty> props)
            : base(_host)
        {
			this.ctx = ctx;
			this.props = props;

        }
        
        public override void Generate()
        {
#line 17 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\UpdateParentTemplate.cst"
this.WriteObjects("\r\n");
#line 19 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\UpdateParentTemplate.cst"
if (props.Count > 0)
{

#line 22 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\UpdateParentTemplate.cst"
this.WriteObjects("		public override void UpdateParent(string propertyName, int? id)\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			int? __oldValue, __newValue = id;\r\n");
this.WriteObjects("			\r\n");
this.WriteObjects("			switch(propertyName)\r\n");
this.WriteObjects("			{\r\n");
#line 29 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\UpdateParentTemplate.cst"
foreach(var prop in props)
	{
		ApplyCase(prop);
	}

#line 34 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\UpdateParentTemplate.cst"
this.WriteObjects("				default:\r\n");
this.WriteObjects("					base.UpdateParent(propertyName, id);\r\n");
this.WriteObjects("					break;\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("		}\r\n");
#line 40 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\UpdateParentTemplate.cst"
}


        }



    }
}