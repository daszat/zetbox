using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst")]
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
#line 17 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst"
this.WriteObjects("");
#line 30 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst"
this.WriteObjects("\n");
this.WriteObjects("        public override void AttachToContext(IZetboxContext ctx)\n");
this.WriteObjects("        {\n");
this.WriteObjects("            base.AttachToContext(ctx);\n");
this.WriteObjects("            var nhCtx = (NHibernateContext)ctx;\n");
#line 35 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst"
foreach(var prop in cls.Properties.OfType<ValueTypeProperty>().Where(p => p.IsList).OrderBy(p => p.Name)) { 
#line 36 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst"
this.WriteObjects("            if (_",  prop.Name , " != null)\n");
this.WriteObjects("                this.Proxy.",  prop.Name , ".ForEach<IProxyObject>(i => nhCtx.AttachAndWrap(i));\n");
#line 38 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst"
} 
#line 39 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst"
foreach(var prop in cls.Properties.OfType<CompoundObjectProperty>().Where(p => p.IsList).OrderBy(p => p.Name)) { 
#line 40 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst"
this.WriteObjects("            if (_",  prop.Name , " != null)\n");
this.WriteObjects("                this.Proxy.",  prop.Name , ".ForEach<IProxyObject>(i => nhCtx.AttachAndWrap(i));\n");
#line 42 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst"
} 
#line 43 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\AttachToContextTemplate.cst"
this.WriteObjects("        }\n");

        }

    }
}