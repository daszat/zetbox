using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.API;
using Zetbox.App.Base;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Mappings
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox.net4\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ClassesHbm.cst")]
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
#line 28 "D:\Projects\zetbox.net4\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ClassesHbm.cst"
this.WriteObjects("<?xml version=\"1.0\"?>\r\n");
this.WriteObjects("<hibernate-mapping xmlns=\"urn:nhibernate-mapping-2.2\" \r\n");
this.WriteObjects("                   default-cascade=\"save-update\">\r\n");
#line 31 "D:\Projects\zetbox.net4\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ClassesHbm.cst"
foreach (var oc in GetObjectClasses(ctx).Where(c => c.BaseObjectClass == null).ToList().OrderBy(c => c.Module.Name).ThenBy(c => c.Name)) {          
#line 32 "D:\Projects\zetbox.net4\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ClassesHbm.cst"
ApplyObjectClassTemplate(oc);                                                               
#line 33 "D:\Projects\zetbox.net4\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ClassesHbm.cst"
}                                                                                                
#line 34 "D:\Projects\zetbox.net4\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ClassesHbm.cst"
this.WriteObjects("</hibernate-mapping>");

        }

    }
}