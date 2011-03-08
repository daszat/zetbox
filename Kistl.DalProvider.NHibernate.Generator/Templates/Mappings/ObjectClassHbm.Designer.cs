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
		protected string tableName;
		protected string qualifiedImplementationName;
		protected bool isAbstract;
		protected List<Property> properties;
		protected List<ObjectClass> subClasses;
		protected bool needsRightsTable;
		protected string qualifiedRightsClassName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string interfaceName, string implementationName, string tableName, string qualifiedImplementationName, bool isAbstract, List<Property> properties, List<ObjectClass> subClasses, bool needsRightsTable, string qualifiedRightsClassName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Mappings.ObjectClassHbm", ctx, interfaceName, implementationName, tableName, qualifiedImplementationName, isAbstract, properties, subClasses, needsRightsTable, qualifiedRightsClassName);
        }

        public ObjectClassHbm(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string interfaceName, string implementationName, string tableName, string qualifiedImplementationName, bool isAbstract, List<Property> properties, List<ObjectClass> subClasses, bool needsRightsTable, string qualifiedRightsClassName)
            : base(_host)
        {
			this.ctx = ctx;
			this.interfaceName = interfaceName;
			this.implementationName = implementationName;
			this.tableName = tableName;
			this.qualifiedImplementationName = qualifiedImplementationName;
			this.isAbstract = isAbstract;
			this.properties = properties;
			this.subClasses = subClasses;
			this.needsRightsTable = needsRightsTable;
			this.qualifiedRightsClassName = qualifiedRightsClassName;

        }

        public override void Generate()
        {
#line 24 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("<?xml version=\"1.0\"?>\r\n");
this.WriteObjects("<hibernate-mapping xmlns=\"urn:nhibernate-mapping-2.2\" \r\n");
this.WriteObjects("                   default-cascade=\"save-update\"\r\n");
this.WriteObjects("                   schema=\"`dbo`\">\r\n");
this.WriteObjects("    <class name=\"",  qualifiedImplementationName , "\"\r\n");
this.WriteObjects("           proxy=\"",  qualifiedImplementationName , "\"\r\n");
this.WriteObjects("           table=\"`",  tableName , "`\"\r\n");
this.WriteObjects("           abstract=\"",  isAbstract ? "true" : "false" , "\">\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        <id name=\"ID\"\r\n");
this.WriteObjects("            column=\"`ID`\"\r\n");
this.WriteObjects("            type=\"Int32\">\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            <generator class=\"native\">\r\n");
this.WriteObjects("                <param name=\"sequence\">`",  tableName , "_ID_seq`</param>\r\n");
this.WriteObjects("            </generator>\r\n");
this.WriteObjects("        </id>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        <!-- define the properties -->\r\n");
#line 43 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
ApplyPropertyDefinitions(properties); 
#line 44 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("\r\n");
#line 45 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
if (needsRightsTable) { 
#line 46 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("        <!-- map rights -->\r\n");
this.WriteObjects("        <set name=\"SecurityRightsCollectionImpl\"\r\n");
this.WriteObjects("             table=\"`",  tableName , "_Rights`\"\r\n");
this.WriteObjects("             lazy=\"true\"\r\n");
this.WriteObjects("             fetch=\"select\">\r\n");
this.WriteObjects("            <key column=\"`ID`\" />\r\n");
this.WriteObjects("            <composite-element class=\"",  qualifiedRightsClassName , "\">\r\n");
this.WriteObjects("                <property name=\"Identity\" column=\"`Identity`\" />\r\n");
this.WriteObjects("                <property name=\"Right\" column=\"`Right`\" />\r\n");
this.WriteObjects("            </composite-element>\r\n");
this.WriteObjects("        </set>\r\n");
this.WriteObjects("\r\n");
#line 58 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
} 
#line 59 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("        <!-- define the subclasses -->\r\n");
#line 60 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
ApplyJoinedSubclasses(subClasses); 
#line 61 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    </class>\r\n");
this.WriteObjects("</hibernate-mapping>");

        }

    }
}