using System;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Mappings
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst")]
    public partial class IdGeneratorHbm : Zetbox.Generator.ResourceTemplate
    {
		protected string idType;
		protected string schemaName;
		protected string tableName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, string idType, string schemaName, string tableName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Mappings.IdGeneratorHbm", idType, schemaName, tableName);
        }

        public IdGeneratorHbm(Arebis.CodeGeneration.IGenerationHost _host, string idType, string schemaName, string tableName)
            : base(_host)
        {
			this.idType = idType;
			this.schemaName = schemaName;
			this.tableName = tableName;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
this.WriteObjects("");
#line 25 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
switch(idType) {                                                             
#line 26 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
case "id":                                                              
#line 27 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
this.WriteObjects("        <id name=\"ID\" column=\"`ID`\" type=\"Int32\" unsaved-value=\"0\">\n");
this.WriteObjects("            <generator class=\"native\">\n");
this.WriteObjects("                <param name=\"schema\">`",  schemaName , "`</param>\n");
this.WriteObjects("                <param name=\"sequence\">`",  tableName , "_ID_seq`</param>\n");
this.WriteObjects("            </generator>\n");
this.WriteObjects("        </id>\n");
#line 33 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
break;                                                              
#line 34 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
case "collection-id":                                                   
#line 35 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
this.WriteObjects("        <collection-id column=\"`ID`\" type=\"Int32\" unsaved-value=\"0\">\n");
this.WriteObjects("            <generator class=\"native\">\n");
this.WriteObjects("                <param name=\"schema\">`",  schemaName , "`</param>\n");
this.WriteObjects("                <param name=\"sequence\">`",  tableName , "_ID_seq`</param>\n");
this.WriteObjects("            </generator>\n");
this.WriteObjects("        </id>\n");
#line 41 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
break;                                                              
#line 42 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
default:                                                                
#line 43 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
throw new ArgumentOutOfRangeException("idType");                    
#line 44 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
}                                                                           

        }

    }
}