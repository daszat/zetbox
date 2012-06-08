using System;
using System.Collections.Generic;
using System.Linq;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Mappings
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ValueTypePropertyHbm.cst")]
    public partial class ValueTypePropertyHbm : Zetbox.Generator.ResourceTemplate
    {
		protected string prefix;
		protected string propName;
		protected string columnName;
		protected bool isList;
		protected string typeAttr;
		protected string ceClassAttr;
		protected string ceReverseKeyColumnName;
		protected string listPositionColumnName;
		protected bool optimisticLock;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, string prefix, string propName, string columnName, bool isList, string typeAttr, string ceClassAttr, string ceReverseKeyColumnName, string listPositionColumnName, bool optimisticLock)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Mappings.ValueTypePropertyHbm", prefix, propName, columnName, isList, typeAttr, ceClassAttr, ceReverseKeyColumnName, listPositionColumnName, optimisticLock);
        }

        public ValueTypePropertyHbm(Arebis.CodeGeneration.IGenerationHost _host, string prefix, string propName, string columnName, bool isList, string typeAttr, string ceClassAttr, string ceReverseKeyColumnName, string listPositionColumnName, bool optimisticLock)
            : base(_host)
        {
			this.prefix = prefix;
			this.propName = propName;
			this.columnName = columnName;
			this.isList = isList;
			this.typeAttr = typeAttr;
			this.ceClassAttr = ceClassAttr;
			this.ceReverseKeyColumnName = ceReverseKeyColumnName;
			this.listPositionColumnName = listPositionColumnName;
			this.optimisticLock = optimisticLock;

        }

        public override void Generate()
        {
#line 33 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ValueTypePropertyHbm.cst"
if (isList) {                                                                    
#line 34 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ValueTypePropertyHbm.cst"
this.WriteObjects("        <!-- ValueTypeProperty isList -->\r\n");
this.WriteObjects("        <set name=\"",  propName , "\" ",  typeAttr , " inverse=\"true\" batch-size=\"100\">\r\n");
this.WriteObjects("            <key column=\"`",  ceReverseKeyColumnName , "`\" />\r\n");
this.WriteObjects("            <one-to-many ",  ceClassAttr , " />\r\n");
this.WriteObjects("        </set>\r\n");
#line 39 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ValueTypePropertyHbm.cst"
} else {                                                                         
#line 40 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ValueTypePropertyHbm.cst"
this.WriteObjects("        <!-- ValueTypeProperty isValue -->\r\n");
this.WriteObjects("        <property name=\"",  propName , "\" ",  typeAttr , "\r\n");
this.WriteObjects("                  column=\"`",  prefix , "",  columnName , "`\" \r\n");
this.WriteObjects("                  optimistic-lock=\"",  optimisticLock ? "true" : "false" , "\"/>\r\n");
#line 44 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ValueTypePropertyHbm.cst"
}                                                                                

        }

    }
}