using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\GetPropertyErrorTemplate.cst")]
    public partial class GetPropertyErrorTemplate : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected ObjectClass cls;


        public GetPropertyErrorTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls)
            : base(_host)
        {
			this.ctx = ctx;
			this.cls = cls;

        }
        
        public override void Generate()
        {
#line 14 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\GetPropertyErrorTemplate.cst"
this.WriteObjects("\r\n");
#line 16 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\GetPropertyErrorTemplate.cst"
var properties = cls.Properties.OrderBy(p => p.Name).ToList();
	if (properties.Count > 0) {

#line 19 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\GetPropertyErrorTemplate.cst"
this.WriteObjects("		protected override string GetPropertyError(string propertyName) \r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			switch(propertyName)\r\n");
this.WriteObjects("			{\r\n");
#line 24 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\GetPropertyErrorTemplate.cst"
foreach(var property in properties)
		{
			string propertyName = property.Name;
			

#line 29 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\GetPropertyErrorTemplate.cst"
this.WriteObjects("				case \"",  propertyName , "\":\r\n");
this.WriteObjects("				{\r\n");
this.WriteObjects("					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid(\"",  property.ExportGuid , "\")).Constraints\r\n");
this.WriteObjects("						.Where(c => !c.IsValid(this, this.",  propertyName , "))\r\n");
this.WriteObjects("						.Select(c => c.GetErrorText(this, this.",  propertyName , "))\r\n");
this.WriteObjects("						.ToArray();\r\n");
this.WriteObjects("					\r\n");
this.WriteObjects("					return String.Join(\"; \", errors);\r\n");
this.WriteObjects("				}\r\n");
#line 39 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\GetPropertyErrorTemplate.cst"
}

#line 41 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\GetPropertyErrorTemplate.cst"
this.WriteObjects("				default:\r\n");
this.WriteObjects("					return base.GetPropertyError(propertyName);\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("		}\r\n");
#line 46 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\GetPropertyErrorTemplate.cst"
}


        }



    }
}