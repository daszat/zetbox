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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\JoinedSubclassHbm.cst")]
    public partial class JoinedSubclassHbm : Zetbox.Generator.ResourceTemplate
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

            _host.CallTemplate("Mappings.JoinedSubclassHbm", ctx, interfaceName, implementationName, schemaName, tableName, qualifiedImplementationName, isAbstract, properties, subClasses, needsRightsTable, needsConcurrency, qualifiedRightsClassName);
        }

        public JoinedSubclassHbm(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string interfaceName, string implementationName, string schemaName, string tableName, string qualifiedImplementationName, bool isAbstract, List<Property> properties, List<ObjectClass> subClasses, bool needsRightsTable, bool needsConcurrency, string qualifiedRightsClassName)
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
#line 17 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\JoinedSubclassHbm.cst"
this.WriteObjects("");
#line 42 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\JoinedSubclassHbm.cst"
this.WriteObjects("        <joined-subclass name=\"",  qualifiedImplementationName , "\"\n");
this.WriteObjects("                         proxy=\"",  qualifiedImplementationName , "\"\n");
this.WriteObjects("                         schema=\"`",  schemaName , "`\"\n");
this.WriteObjects("                         table=\"`",  tableName , "`\"\n");
this.WriteObjects("                         dynamic-update=\"",  needsConcurrency ? "true" : "false" , "\">\n");
this.WriteObjects("            \n");
this.WriteObjects("            <!-- base class reference -->\n");
this.WriteObjects("            <key column=\"`ID`\" />\n");
this.WriteObjects("\n");
this.WriteObjects("            <!-- define the properties -->\n");
#line 52 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\JoinedSubclassHbm.cst"
ApplyPropertyDefinitions(properties); 
#line 53 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\JoinedSubclassHbm.cst"
this.WriteObjects("\n");
this.WriteObjects("            <!-- define the subclasses -->\n");
#line 55 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\JoinedSubclassHbm.cst"
ApplyJoinedSubclasses(subClasses); 
#line 56 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\JoinedSubclassHbm.cst"
this.WriteObjects("\n");
this.WriteObjects("        </joined-subclass>\n");

        }

    }
}