using System;
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
		protected ObjectClass cls;


        public UpdateParentTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls)
            : base(_host)
        {
			this.ctx = ctx;
			this.cls = cls;

        }
        
        public override void Generate()
        {
#line 16 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\UpdateParentTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("		public override void UpdateParent(string propertyName, int? id)\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			switch(propertyName)\r\n");
this.WriteObjects("			{\r\n");
#line 22 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\UpdateParentTemplate.cst"
foreach(var prop in cls.Properties.OfType<ObjectReferenceProperty>().OrderBy(p => p.PropertyName))
	{
		ApplyCase(prop);
	}

#line 27 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\UpdateParentTemplate.cst"
this.WriteObjects("				default:\r\n");
this.WriteObjects("					base.UpdateParent(propertyName, id);\r\n");
this.WriteObjects("					break;\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("		}\r\n");

        }



    }
}