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


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Mappings
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\PropertiesHbm.cst")]
    public partial class PropertiesHbm : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected string prefix;
		protected IEnumerable<Property> properties;
		protected bool needsConcurrency;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string prefix, IEnumerable<Property> properties, bool needsConcurrency)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Mappings.PropertiesHbm", ctx, prefix, properties, needsConcurrency);
        }

        public PropertiesHbm(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string prefix, IEnumerable<Property> properties, bool needsConcurrency)
            : base(_host)
        {
			this.ctx = ctx;
			this.prefix = prefix;
			this.properties = properties;
			this.needsConcurrency = needsConcurrency;

        }

        public override void Generate()
        {
#line 37 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\PropertiesHbm.cst"
/*
     * TODO: Actually, all this should die and become a bunch of polymorphic calls.
     * See also Zetbox.DalProvider.Ef.Generator.Templates.EfModel.ModelCsdlEntityTypeFields
     */

    foreach(var p in properties.OrderBy(p => p.Name))
    {
        // TODO: implement IsNullable everywhere
        if (p is ObjectReferenceProperty)
        {
            var prop = p as ObjectReferenceProperty;
            ApplyObjectReferenceProperty(prefix, prop);
        }
        else if (p is ValueTypeProperty)
        {
            var prop = (ValueTypeProperty)p;
            ApplyValueTypeProperty(prefix, prop);
        }
        else if (p is CompoundObjectProperty)
        {
            var prop = (CompoundObjectProperty)p;
            ApplyCompoundObjectProperty(prefix, prop);
        }
    }


        }

    }
}