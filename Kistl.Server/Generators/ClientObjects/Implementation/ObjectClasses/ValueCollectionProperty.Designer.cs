using System;
using System.Collections.Generic;
using System.Diagnostics;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ValueCollectionProperty.cst")]
    public partial class ValueCollectionProperty : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Templates.Implementation.SerializationMembersList serializationList;
		protected ValueTypeProperty prop;


        public ValueCollectionProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList, ValueTypeProperty prop)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.prop = prop;

        }
        
        public override void Generate()
        {
#line 18 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ValueCollectionProperty.cst"
Debug.Assert(prop.IsList);


	// the name of the property to create
	string name = prop.PropertyName;
	// the name of the private backing store for the conversion wrapper list
	string wrapperName = "_" + name + "Wrapper";
	// the name of the wrapper class for wrapping the EntityCollection
	string wrapperClass = "NewListPropertyCollection";

	// which generic interface to use for the collection
	string exposedListType = prop.IsIndexed ? "IList" : "ICollection";

	// which Kistl interface this is 
	string thisInterface = prop.ObjectClass.ClassName;
	// which type this list contains
	string referencedType = prop.ReferencedTypeAsCSharp();
	// collection entries in this list
	string referencedCollectionEntry = prop.GetCollectionEntryClassName() + Kistl.API.Helper.ImplementationSuffix;

    AddSerialization(serializationList, wrapperName);


#line 41 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ValueCollectionProperty.cst"
this.WriteObjects("        public ",  exposedListType , "<",  referencedType , "> ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (",  wrapperName , " == null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    ",  wrapperName , " = new ",  wrapperClass , "<",  thisInterface , ", ",  referencedType , ", ",  referencedCollectionEntry , ">(\r\n");
this.WriteObjects("                        this,\r\n");
this.WriteObjects("                        \"",  name , "\");\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return ",  wrapperName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        private ",  wrapperClass , "<",  thisInterface , ", ",  referencedType , ", ",  referencedCollectionEntry , "> ",  wrapperName , ";\r\n");

        }



    }
}