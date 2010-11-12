using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst")]
    public partial class AttachToContextTemplate : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected ObjectClass cls;


        public AttachToContextTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls)
            : base(_host)
        {
			this.ctx = ctx;
			this.cls = cls;

        }
        
        public override void Generate()
        {
#line 14 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public override void AttachToContext(IKistlContext ctx)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.AttachToContext(ctx);\r\n");
#line 19 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst"
foreach(var prop in cls.Properties.OfType<ValueTypeProperty>().Where(p => p.IsList).OrderBy(p => p.Name))
            {

#line 22 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst"
this.WriteObjects("            if (_",  prop.Name , " != null)\r\n");
this.WriteObjects("                _",  prop.Name , ".ForEach<IValueCollectionEntry>(i => ctx.Attach(i));\r\n");
#line 25 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst"
}

            foreach(var prop in cls.Properties.OfType<CompoundObjectProperty>().Where(p => p.IsList).OrderBy(p => p.Name))
            {

#line 30 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst"
this.WriteObjects("            if (_",  prop.Name , " != null)\r\n");
this.WriteObjects("                _",  prop.Name , ".ForEach<IValueCollectionEntry>(i => ctx.Attach(i));\r\n");
#line 33 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst"
}

#line 35 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst"
this.WriteObjects("        }\r\n");

        }



    }
}