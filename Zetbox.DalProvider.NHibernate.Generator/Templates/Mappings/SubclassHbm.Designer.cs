using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Mappings
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\SubclassHbm.cst")]
    public partial class SubclassHbm : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected string interfaceName;
		protected string implementationName;
		protected string schemaName;
		protected string tableName;
		protected string qualifiedImplementationName;
		protected bool isAbstract;
		protected List<Property> properties;
		protected List<ObjectClass> subClasses;
		protected bool needsRightsTable;
		protected bool needsConcurrency;
		protected string qualifiedRightsClassName;
		protected Zetbox.App.Base.TableMapping mappingType;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string interfaceName, string implementationName, string schemaName, string tableName, string qualifiedImplementationName, bool isAbstract, List<Property> properties, List<ObjectClass> subClasses, bool needsRightsTable, bool needsConcurrency, string qualifiedRightsClassName, Zetbox.App.Base.TableMapping mappingType)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Mappings.SubclassHbm", ctx, interfaceName, implementationName, schemaName, tableName, qualifiedImplementationName, isAbstract, properties, subClasses, needsRightsTable, needsConcurrency, qualifiedRightsClassName, mappingType);
        }

        public SubclassHbm(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string interfaceName, string implementationName, string schemaName, string tableName, string qualifiedImplementationName, bool isAbstract, List<Property> properties, List<ObjectClass> subClasses, bool needsRightsTable, bool needsConcurrency, string qualifiedRightsClassName, Zetbox.App.Base.TableMapping mappingType)
            : base(_host)
        {
			this.ctx = ctx;
			this.interfaceName = interfaceName;
			this.implementationName = implementationName;
			this.schemaName = schemaName;
			this.tableName = tableName;
			this.qualifiedImplementationName = qualifiedImplementationName;
			this.isAbstract = isAbstract;
			this.properties = properties;
			this.subClasses = subClasses;
			this.needsRightsTable = needsRightsTable;
			this.needsConcurrency = needsConcurrency;
			this.qualifiedRightsClassName = qualifiedRightsClassName;
			this.mappingType = mappingType;

        }

        public override void Generate()
        {
#line 43 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\SubclassHbm.cst"
this.WriteObjects("        <",  GetTagName() , "\r\n");
this.WriteObjects("                name=\"",  qualifiedImplementationName , "\"\r\n");
this.WriteObjects("                proxy=\"",  qualifiedImplementationName , "\"\r\n");
#line 46 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\SubclassHbm.cst"
if(mappingType == Zetbox.App.Base.TableMapping.TPH) { 
#line 47 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\SubclassHbm.cst"
this.WriteObjects("                discriminator-value=\"",  schemaName , ".",  tableName , "\"\r\n");
#line 48 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\SubclassHbm.cst"
} else { 
#line 49 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\SubclassHbm.cst"
this.WriteObjects("                schema=\"`",  schemaName , "`\"\r\n");
this.WriteObjects("                table=\"`",  tableName , "`\"\r\n");
#line 51 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\SubclassHbm.cst"
} 
#line 52 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\SubclassHbm.cst"
this.WriteObjects("                dynamic-update=\"",  needsConcurrency ? "true" : "false" , "\">\r\n");
this.WriteObjects("            \r\n");
#line 54 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\SubclassHbm.cst"
if(mappingType == Zetbox.App.Base.TableMapping.TPT) { 
#line 55 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\SubclassHbm.cst"
this.WriteObjects("            <!-- base class reference -->\r\n");
this.WriteObjects("            <key column=\"`ID`\" />\r\n");
#line 57 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\SubclassHbm.cst"
} 
#line 58 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\SubclassHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("            <!-- define the properties -->\r\n");
#line 60 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\SubclassHbm.cst"
ApplyPropertyDefinitions(properties); 
#line 61 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\SubclassHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("            <!-- define the subclasses -->\r\n");
#line 63 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\SubclassHbm.cst"
ApplyJoinedSubclasses(subClasses); 
#line 64 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\SubclassHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        </",  GetTagName() , ">\r\n");

        }

    }
}