using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst")]
    public partial class AttachToContextTemplate : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected ObjectClass cls;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, ObjectClass cls)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.AttachToContextTemplate", ctx, cls);
        }

        public AttachToContextTemplate(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, ObjectClass cls)
            : base(_host)
        {
			this.ctx = ctx;
			this.cls = cls;

        }

        public override void Generate()
        {
#line 14 "P:\Zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public override void AttachToContext(IZetboxContext ctx)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.AttachToContext(ctx);\r\n");
#line 19 "P:\Zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst"
foreach(var prop in cls.Properties.OfType<ValueTypeProperty>().Where(p => p.IsList).OrderBy(p => p.Name))
            {

#line 22 "P:\Zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst"
this.WriteObjects("            if (_",  prop.Name , " != null)\r\n");
this.WriteObjects("                ",  prop.Name , "Impl.ForEach<IPersistenceObject>(i => ctx.Attach(i));\r\n");
#line 25 "P:\Zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst"
}

            foreach(var prop in cls.Properties.OfType<CompoundObjectProperty>().Where(p => p.IsList).OrderBy(p => p.Name))
            {

#line 30 "P:\Zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst"
this.WriteObjects("            if (_",  prop.Name , " != null)\r\n");
this.WriteObjects("                ",  prop.Name , "Impl.ForEach<IPersistenceObject>(i => ctx.Attach(i));\r\n");
#line 33 "P:\Zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst"
}

#line 35 "P:\Zetbox\Zetbox.DalProvider.EF.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst"
this.WriteObjects("        }\r\n");

        }

    }
}