using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.API;
using Zetbox.App.Base;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Mappings
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ClassesHbm.cst")]
    public partial class ClassesHbm : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected string extraSuffix;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string extraSuffix)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Mappings.ClassesHbm", ctx, extraSuffix);
        }

        public ClassesHbm(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string extraSuffix)
            : base(_host)
        {
			this.ctx = ctx;
			this.extraSuffix = extraSuffix;

        }

        public override void Generate()
        {
#line 28 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ClassesHbm.cst"
this.WriteObjects("<?xml version=\"1.0\"?>\r\n");
this.WriteObjects("<hibernate-mapping xmlns=\"urn:nhibernate-mapping-2.2\" \r\n");
this.WriteObjects("                   default-cascade=\"save-update\">\r\n");
#line 31 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ClassesHbm.cst"
foreach (var oc in ctx.GetQuery<ObjectClass>().ToList().Where(c => c.BaseObjectClass == null).OrderBy(c => c.Module.Name).ThenBy(c => c.Name)) {          
#line 32 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ClassesHbm.cst"
ApplyObjectClassTemplate(oc);                                                               
#line 33 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ClassesHbm.cst"
}                                                                                                
#line 34 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ClassesHbm.cst"
this.WriteObjects("</hibernate-mapping>");

        }

    }
}