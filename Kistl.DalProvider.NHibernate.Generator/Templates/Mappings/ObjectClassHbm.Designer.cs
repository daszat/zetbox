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
		protected string qualifiedInterfaceName;
		protected string qualifiedImplementationName;
		protected bool isAbstract;
		protected List<Property> properties;
		protected List<ObjectClass> subClasses;


        public ObjectClassHbm(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string interfaceName, string implementationName, string tableName, string qualifiedInterfaceName, string qualifiedImplementationName, bool isAbstract, List<Property> properties, List<ObjectClass> subClasses)
            : base(_host)
        {
			this.ctx = ctx;
			this.interfaceName = interfaceName;
			this.implementationName = implementationName;
			this.tableName = tableName;
			this.qualifiedInterfaceName = qualifiedInterfaceName;
			this.qualifiedImplementationName = qualifiedImplementationName;
			this.isAbstract = isAbstract;
			this.properties = properties;
			this.subClasses = subClasses;

        }
        
        public override void Generate()
        {
#line 23 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("<?xml version=\"1.0\"?>\r\n");
this.WriteObjects("<hibernate-mapping xmlns=\"urn:nhibernate-mapping-2.2\" \r\n");
this.WriteObjects("                   schema=\"`dbo`\">\r\n");
this.WriteObjects("    <class name=\"",  qualifiedImplementationName , "\"\r\n");
this.WriteObjects("           table=\"`",  tableName , "`\"\r\n");
this.WriteObjects("           abstract=\"",  isAbstract ? "true" : "false" , "\">\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        <id name=\"`ID`\"\r\n");
this.WriteObjects("            type=\"Int32\">\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            <generator class=\"native\" />\r\n");
this.WriteObjects("        </id>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        <!-- define the properties -->\r\n");
#line 37 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
ApplyPropertyDefinitions(properties); 
#line 38 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        <!-- define the subclasses -->\r\n");
#line 40 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
ApplyJoinedSubclasses(subClasses); 
#line 41 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Mappings\ObjectClassHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    </class>\r\n");
this.WriteObjects("</hibernate-mapping>");

        }



    }
}