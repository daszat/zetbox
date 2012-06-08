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


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string interfaceName, string implementationName, string schemaName, string tableName, string qualifiedImplementationName, bool isAbstract, List<Property> properties, List<ObjectClass> subClasses, bool needsRightsTable, bool needsConcurrency, string qualifiedRightsClassName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Mappings.ObjectClassHbm", ctx, interfaceName, implementationName, schemaName, tableName, qualifiedImplementationName, isAbstract, properties, subClasses, needsRightsTable, needsConcurrency, qualifiedRightsClassName);
        }

        public ObjectClassHbm(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string interfaceName, string implementationName, string schemaName, string tableName, string qualifiedImplementationName, bool isAbstract, List<Property> properties, List<ObjectClass> subClasses, bool needsRightsTable, bool needsConcurrency, string qualifiedRightsClassName)
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

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("");
#line 42 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("<?xml version=\"1.0\"?>\n");
this.WriteObjects("<hibernate-mapping xmlns=\"urn:nhibernate-mapping-2.2\" \n");
this.WriteObjects("                   default-cascade=\"save-update\" >\n");
this.WriteObjects("    <class name=\"",  qualifiedImplementationName , "\"\n");
this.WriteObjects("           proxy=\"",  qualifiedImplementationName , "\"\n");
this.WriteObjects("           schema=\"`",  schemaName , "`\"\n");
this.WriteObjects("           table=\"`",  tableName , "`\"\n");
this.WriteObjects("           abstract=\"",  isAbstract ? "true" : "false" , "\"\n");
this.WriteObjects("           dynamic-update=\"",  needsConcurrency ? "true" : "false" , "\"\n");
this.WriteObjects("           optimistic-lock=\"",  needsConcurrency ? "dirty" : "none" , "\"\n");
this.WriteObjects("           batch-size=\"100\">\n");
this.WriteObjects("\n");
#line 54 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
IdGeneratorHbm.Call(Host, "id", schemaName, tableName); 
#line 55 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("\n");
this.WriteObjects("        <!-- define the properties -->\n");
#line 57 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
ApplyPropertyDefinitions(properties); 
#line 58 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("\n");
#line 59 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
if (needsRightsTable) { 
#line 60 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("        <!-- map rights -->\n");
this.WriteObjects("        <set name=\"SecurityRightsCollectionImpl\"\n");
this.WriteObjects("             schema=\"`",  schemaName , "`\"\n");
this.WriteObjects("             table=\"`",  tableName , "_Rights`\"\n");
this.WriteObjects("             lazy=\"true\"\n");
this.WriteObjects("             fetch=\"select\" \n");
this.WriteObjects("             batch-size=\"100\" >\n");
this.WriteObjects("            <key column=\"`ID`\" />\n");
this.WriteObjects("            <composite-element class=\"",  qualifiedRightsClassName , "\">\n");
this.WriteObjects("                <property name=\"Identity\" column=\"`Identity`\" />\n");
this.WriteObjects("                <property name=\"Right\" column=\"`Right`\" />\n");
this.WriteObjects("            </composite-element>\n");
this.WriteObjects("        </set>\n");
this.WriteObjects("\n");
#line 74 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
} 
#line 75 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("        <!-- define the subclasses -->\n");
#line 76 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
ApplyJoinedSubclasses(subClasses); 
#line 77 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("\n");
this.WriteObjects("    </class>\n");
this.WriteObjects("</hibernate-mapping>");

        }

    }
}