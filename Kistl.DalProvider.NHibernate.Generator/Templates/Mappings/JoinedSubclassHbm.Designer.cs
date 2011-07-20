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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\JoinedSubclassHbm.cst")]
    public partial class JoinedSubclassHbm : Kistl.Generator.ResourceTemplate
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

            _host.CallTemplate("Mappings.JoinedSubclassHbm", ctx, interfaceName, implementationName, schemaName, tableName, qualifiedImplementationName, isAbstract, properties, subClasses, needsRightsTable, needsConcurrency, qualifiedRightsClassName);
        }

        public JoinedSubclassHbm(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string interfaceName, string implementationName, string schemaName, string tableName, string qualifiedImplementationName, bool isAbstract, List<Property> properties, List<ObjectClass> subClasses, bool needsRightsTable, bool needsConcurrency, string qualifiedRightsClassName)
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
#line 26 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\JoinedSubclassHbm.cst"
this.WriteObjects("        <joined-subclass name=\"",  qualifiedImplementationName , "\"\r\n");
this.WriteObjects("                         proxy=\"",  qualifiedImplementationName , "\"\r\n");
this.WriteObjects("                         schema=\"`",  schemaName , "`\"\r\n");
this.WriteObjects("                         table=\"`",  tableName , "`\">\r\n");
this.WriteObjects("            \r\n");
this.WriteObjects("            <!-- base class reference -->\r\n");
this.WriteObjects("            <key column=\"`ID`\" />\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            <!-- define the properties -->\r\n");
#line 35 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\JoinedSubclassHbm.cst"
ApplyPropertyDefinitions(properties); 
#line 36 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\JoinedSubclassHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("            <!-- define the subclasses -->\r\n");
#line 38 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\JoinedSubclassHbm.cst"
ApplyJoinedSubclasses(subClasses); 
#line 39 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\JoinedSubclassHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        </joined-subclass>\r\n");

        }

    }
}