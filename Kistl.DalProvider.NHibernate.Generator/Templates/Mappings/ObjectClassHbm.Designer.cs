using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.NHibernate.Generator.Templates.Mappings
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst")]
    public partial class ObjectClassHbm : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
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


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string interfaceName, string implementationName, string schemaName, string tableName, string qualifiedImplementationName, bool isAbstract, List<Property> properties, List<ObjectClass> subClasses, bool needsRightsTable, bool needsConcurrency, string qualifiedRightsClassName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Mappings.ObjectClassHbm", ctx, interfaceName, implementationName, schemaName, tableName, qualifiedImplementationName, isAbstract, properties, subClasses, needsRightsTable, needsConcurrency, qualifiedRightsClassName);
        }

        public ObjectClassHbm(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string interfaceName, string implementationName, string schemaName, string tableName, string qualifiedImplementationName, bool isAbstract, List<Property> properties, List<ObjectClass> subClasses, bool needsRightsTable, bool needsConcurrency, string qualifiedRightsClassName)
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
#line 26 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("<?xml version=\"1.0\"?>\r\n");
this.WriteObjects("<hibernate-mapping xmlns=\"urn:nhibernate-mapping-2.2\" \r\n");
this.WriteObjects("                   default-cascade=\"save-update\" >\r\n");
this.WriteObjects("    <class name=\"",  qualifiedImplementationName , "\"\r\n");
this.WriteObjects("           proxy=\"",  qualifiedImplementationName , "\"\r\n");
this.WriteObjects("           schema=\"`",  schemaName , "`\"\r\n");
this.WriteObjects("           table=\"`",  tableName , "`\"\r\n");
this.WriteObjects("           abstract=\"",  isAbstract ? "true" : "false" , "\"\r\n");
this.WriteObjects("           dynamic-update=\"",  needsConcurrency ? "true" : "false" , "\"\r\n");
this.WriteObjects("           optimistic-lock=\"",  needsConcurrency ? "dirty" : "none" , "\">\r\n");
this.WriteObjects("\r\n");
#line 37 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
IdGeneratorHbm.Call(Host, "id", schemaName, tableName); 
#line 38 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        <!-- define the properties -->\r\n");
#line 40 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
ApplyPropertyDefinitions(properties); 
#line 41 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("\r\n");
#line 42 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
if (needsRightsTable) { 
#line 43 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
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
#line 57 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
} 
#line 58 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("        <!-- define the subclasses -->\r\n");
#line 59 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
ApplyJoinedSubclasses(subClasses); 
#line 60 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    </class>\r\n");
this.WriteObjects("</hibernate-mapping>");

        }

    }
}