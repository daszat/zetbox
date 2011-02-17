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


namespace Kistl.DalProvider.NHibernate.Generator.Templates.Mappings
{
    [Arebis.CodeGeneration.TemplateInfo(@"/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Mappings/PropertiesHbm.cst")]
    public partial class PropertiesHbm : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected string prefix;
		protected IEnumerable<Property> properties;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string prefix, IEnumerable<Property> properties)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Mappings.PropertiesHbm", ctx, prefix, properties);
        }

        public PropertiesHbm(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string prefix, IEnumerable<Property> properties)
            : base(_host)
        {
			this.ctx = ctx;
			this.prefix = prefix;
			this.properties = properties;

        }

        public override void Generate()
        {
#line 20 "/srv/CCNet/Projects/zbox/repo/Kistl.DalProvider.NHibernate.Generator/Templates/Mappings/PropertiesHbm.cst"
/*
     * TODO: Actually, all this should die and become a bunch of polymorphic calls.
     * See also Kistl.DalProvider.Ef.Generator.Templates.EfModel.ModelCsdlEntityTypeFields
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