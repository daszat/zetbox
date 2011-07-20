using System;


namespace Kistl.DalProvider.NHibernate.Generator.Templates.Mappings
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst")]
    public partial class IdGeneratorHbm : Kistl.Generator.ResourceTemplate
    {
		protected string idType;
		protected string tableName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, string idType, string tableName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Mappings.IdGeneratorHbm", idType, tableName);
        }

        public IdGeneratorHbm(Arebis.CodeGeneration.IGenerationHost _host, string idType, string tableName)
            : base(_host)
        {
			this.idType = idType;
			this.tableName = tableName;

        }

        public override void Generate()
        {
#line 8 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
switch(idType) {                                                             
#line 9 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
case "id":                                                              
#line 10 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
this.WriteObjects("        <id name=\"ID\" column=\"`ID`\" type=\"Int32\" unsaved-value=\"0\">\r\n");
this.WriteObjects("            <generator class=\"native\">\r\n");
this.WriteObjects("                <param name=\"sequence\">`",  tableName , "_ID_seq`</param>\r\n");
this.WriteObjects("            </generator>\r\n");
this.WriteObjects("        </id>\r\n");
#line 15 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
break;                                                              
#line 16 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
case "collection-id":                                                   
#line 17 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
this.WriteObjects("        <collection-id column=\"`ID`\" type=\"Int32\" unsaved-value=\"0\">\r\n");
this.WriteObjects("            <generator class=\"native\">\r\n");
this.WriteObjects("                <param name=\"sequence\">`",  tableName , "_ID_seq`</param>\r\n");
this.WriteObjects("            </generator>\r\n");
this.WriteObjects("        </id>\r\n");
#line 22 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
break;                                                              
#line 23 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
default:                                                                
#line 24 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
throw new ArgumentOutOfRangeException("idType");                    
#line 25 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\IdGeneratorHbm.cst"
}                                                                           

        }

    }
}