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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst")]
    public partial class ObjectClassHbm : Zetbox.Generator.ResourceTemplate
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

            _host.CallTemplate("Mappings.ObjectClassHbm", ctx, interfaceName, implementationName, schemaName, tableName, qualifiedImplementationName, isAbstract, properties, subClasses, needsRightsTable, needsConcurrency, qualifiedRightsClassName, mappingType);
        }

        public ObjectClassHbm(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string interfaceName, string implementationName, string schemaName, string tableName, string qualifiedImplementationName, bool isAbstract, List<Property> properties, List<ObjectClass> subClasses, bool needsRightsTable, bool needsConcurrency, string qualifiedRightsClassName, Zetbox.App.Base.TableMapping mappingType)
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
#line 43 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("    <class name=\"",  qualifiedImplementationName , "\"\r\n");
this.WriteObjects("           proxy=\"",  qualifiedImplementationName , "\"\r\n");
#line 45 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
if(mappingType == Zetbox.App.Base.TableMapping.TPH) { 
#line 46 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("           discriminator-value=\"",  schemaName , ".",  tableName , "\"\r\n");
#line 47 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
} 
#line 48 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("           schema=\"`",  schemaName , "`\"\r\n");
this.WriteObjects("           table=\"`",  tableName , "`\"\r\n");
this.WriteObjects("           abstract=\"",  isAbstract ? "true" : "false" , "\"\r\n");
this.WriteObjects("           dynamic-update=\"",  needsConcurrency ? "true" : "false" , "\"\r\n");
this.WriteObjects("           optimistic-lock=\"",  needsConcurrency ? "dirty" : "none" , "\"\r\n");
this.WriteObjects("           batch-size=\"100\">\r\n");
this.WriteObjects("\r\n");
#line 55 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
IdGeneratorHbm.Call(Host, "id", schemaName, tableName); 
#line 56 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
DiscriminatorColumnHbm.Call(Host, mappingType); 
#line 57 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        <!-- define the properties -->\r\n");
#line 59 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
ApplyPropertyDefinitions(properties); 
#line 60 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("\r\n");
#line 61 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
if (needsRightsTable) { 
#line 62 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("        <!-- map rights -->\r\n");
this.WriteObjects("        <set name=\"SecurityRightsCollectionImpl\"\r\n");
this.WriteObjects("             schema=\"`",  schemaName , "`\"\r\n");
this.WriteObjects("             table=\"`",  tableName , "_Rights`\"\r\n");
this.WriteObjects("             lazy=\"true\"\r\n");
this.WriteObjects("             fetch=\"select\" \r\n");
this.WriteObjects("             batch-size=\"100\" >\r\n");
this.WriteObjects("            <key column=\"`ID`\" />\r\n");
this.WriteObjects("            <composite-element class=\"",  qualifiedRightsClassName , "\">\r\n");
this.WriteObjects("                <property name=\"Identity\" column=\"`Identity`\" />\r\n");
this.WriteObjects("                <property name=\"Right\" column=\"`Right`\" />\r\n");
this.WriteObjects("            </composite-element>\r\n");
this.WriteObjects("        </set>\r\n");
this.WriteObjects("\r\n");
#line 76 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
} 
#line 77 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("        <!-- define the subclasses -->\r\n");
#line 78 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
ApplySubclasses(subClasses); 
#line 79 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    </class>\r\n");

        }

    }
}