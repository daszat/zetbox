using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Generators.Templates.Implementation;


namespace Kistl.DalProvider.NHibernate.Generator.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Implementation\ObjectClasses\ObjectListProperty.cst")]
    public partial class ObjectListProperty : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected SerializationMembersList serializationList;
		protected string exposedListType;
		protected string referencedInterface;
		protected string name;


        public ObjectListProperty(Arebis.CodeGeneration.IGenerationHost _host, SerializationMembersList serializationList, string exposedListType, string referencedInterface, string name)
            : base(_host)
        {
			this.serializationList = serializationList;
			this.exposedListType = exposedListType;
			this.referencedInterface = referencedInterface;
			this.name = name;

        }
        
        public override void Generate()
        {
#line 17 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Implementation\ObjectClasses\ObjectListProperty.cst"
AddSerialization(serializationList, name, false);

#line 19 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Implementation\ObjectClasses\ObjectListProperty.cst"
this.WriteObjects("        // ",  this.GetType() , "\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
this.WriteObjects("        public ",  exposedListType , "<",  referencedInterface , "> ",  name , " { get; set; }\r\n");

        }



    }
}