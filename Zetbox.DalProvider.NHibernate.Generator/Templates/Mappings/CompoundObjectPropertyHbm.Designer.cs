using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.API;
using Zetbox.App.Base;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Mappings
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CompoundObjectPropertyHbm.cst")]
    public partial class CompoundObjectPropertyHbm : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected string prefix;
		protected string propName;
		protected string columnName;
		protected bool isList;
		protected string ceClassAttr;
		protected string valueClassAttr;
		protected string isNullColumnAttr;
		protected string ceReverseKeyColumnName;
		protected IEnumerable<Property> properties;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string prefix, string propName, string columnName, bool isList, string ceClassAttr, string valueClassAttr, string isNullColumnAttr, string ceReverseKeyColumnName, IEnumerable<Property> properties)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Mappings.CompoundObjectPropertyHbm", ctx, prefix, propName, columnName, isList, ceClassAttr, valueClassAttr, isNullColumnAttr, ceReverseKeyColumnName, properties);
        }

        public CompoundObjectPropertyHbm(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string prefix, string propName, string columnName, bool isList, string ceClassAttr, string valueClassAttr, string isNullColumnAttr, string ceReverseKeyColumnName, IEnumerable<Property> properties)
            : base(_host)
        {
			this.ctx = ctx;
			this.prefix = prefix;
			this.propName = propName;
			this.columnName = columnName;
			this.isList = isList;
			this.ceClassAttr = ceClassAttr;
			this.valueClassAttr = valueClassAttr;
			this.isNullColumnAttr = isNullColumnAttr;
			this.ceReverseKeyColumnName = ceReverseKeyColumnName;
			this.properties = properties;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CompoundObjectPropertyHbm.cst"
this.WriteObjects("");
#line 36 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CompoundObjectPropertyHbm.cst"
if (isList) {                                                                    
#line 37 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CompoundObjectPropertyHbm.cst"
this.WriteObjects("        <!-- CompoundObjectProperty IsList -->\n");
this.WriteObjects("        <set name=\"",  propName , "\" inverse=\"true\" batch-size=\"100\">\n");
this.WriteObjects("            <key column=\"`",  ceReverseKeyColumnName , "`\" />\n");
this.WriteObjects("            <one-to-many ",  ceClassAttr , " />\n");
this.WriteObjects("        </set>\n");
#line 42 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CompoundObjectPropertyHbm.cst"
} else {                                                                         
#line 43 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CompoundObjectPropertyHbm.cst"
this.WriteObjects("        <!-- CompoundObjectProperty isValue -->\n");
this.WriteObjects("        <component name=\"",  propName , "\" ",  valueClassAttr , ">\n");
this.WriteObjects("            <property name=\"CompoundObject_IsNull\"\n");
this.WriteObjects("                      ",  isNullColumnAttr , "\n");
this.WriteObjects("                      type=\"bool\"\n");
this.WriteObjects("                      optimistic-lock=\"false\" />\n");
#line 49 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CompoundObjectPropertyHbm.cst"
PropertiesHbm.Call(Host, ctx, prefix + columnName + "_", properties, false);         
#line 50 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CompoundObjectPropertyHbm.cst"
this.WriteObjects("        </component>\n");
#line 51 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\CompoundObjectPropertyHbm.cst"
}                                                                                

        }

    }
}