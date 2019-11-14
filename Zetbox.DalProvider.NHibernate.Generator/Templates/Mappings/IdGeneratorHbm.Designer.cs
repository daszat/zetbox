using System;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Mappings
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst")]
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
#line 25 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
switch(idType) {                                                             
#line 26 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
case "id":                                                              
#line 27 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
this.WriteObjects("        <id name=\"ID\" column=\"`ID`\" type=\"Int32\" unsaved-value=\"0\">\r\n");
this.WriteObjects("            <generator class=\"native\">\r\n");
this.WriteObjects("                <param name=\"schema\">`",  schemaName , "`</param>\r\n");
this.WriteObjects("                <param name=\"sequence\">`",  tableName , "_ID_seq`</param>\r\n");
this.WriteObjects("            </generator>\r\n");
this.WriteObjects("        </id>\r\n");
#line 33 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
break;                                                              
#line 34 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
case "collection-id":                                                   
#line 35 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
this.WriteObjects("        <collection-id column=\"`ID`\" type=\"Int32\" unsaved-value=\"0\">\r\n");
this.WriteObjects("            <generator class=\"native\">\r\n");
this.WriteObjects("                <param name=\"schema\">`",  schemaName , "`</param>\r\n");
this.WriteObjects("                <param name=\"sequence\">`",  tableName , "_ID_seq`</param>\r\n");
this.WriteObjects("            </generator>\r\n");
this.WriteObjects("        </id>\r\n");
#line 41 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
break;                                                              
#line 42 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
default:                                                                
#line 43 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
throw new ArgumentOutOfRangeException("idType");                    
#line 44 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
}                                                                           

        }

    }
}