using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ObjectListProperty.cst")]
    public partial class ObjectListProperty : Kistl.Generator.ResourceTemplate
    {
		protected Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList;
		protected string exposedListType;
		protected string referencedInterface;
		protected string name;


        public ObjectListProperty(Arebis.CodeGeneration.IGenerationHost _host, Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList, string exposedListType, string referencedInterface, string name)
            : base(_host)
        {
			this.serializationList = serializationList;
			this.exposedListType = exposedListType;
			this.referencedInterface = referencedInterface;
			this.name = name;

        }
        
        public override void Generate()
        {
#line 16 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ObjectListProperty.cst"
AddSerialization(serializationList, name, false);

#line 18 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ObjectListProperty.cst"
this.WriteObjects("        // ",  this.GetType() , "\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
this.WriteObjects("        public ",  exposedListType , "<",  referencedInterface , "> ",  name , " { get; set; }\r\n");

        }



    }
}