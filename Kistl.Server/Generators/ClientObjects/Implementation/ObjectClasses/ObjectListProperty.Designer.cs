using System;
using System.Diagnostics;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;


namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectListProperty.cst")]
    public partial class ObjectListProperty : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Templates.Implementation.SerializationMembersList serializationList;
		protected ObjectReferenceProperty prop;


        public ObjectListProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList, ObjectReferenceProperty prop)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.prop = prop;

        }
        
        public override void Generate()
        {
#line 18 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectListProperty.cst"
Debug.Assert(prop.IsList);

    var rel = NewRelation.Lookup(ctx, prop);
    var relEnd = rel.GetEnd(prop);
    var otherEnd = relEnd.Other;
    var otherName = otherEnd.Navigator == null ? relEnd.RoleName : otherEnd.Navigator.PropertyName;

	// the name of the property to create
	string name = prop.PropertyName;
	// the name of the private backing store for the conversion wrapper list
	string wrapperName = "_" + name + "Wrapper";
	// the name of the wrapper class for wrapping the EntityCollection
	string wrapperClass = "BackReferenceCollection";
	
	// which generic interface to use for the collection
	string exposedListType = prop.IsIndexed ? "IList" : "ICollection";

	// which Kistl interface this list contains
	string referencedInterface = otherEnd.Type.NameDataObject;

	

#line 40 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectListProperty.cst"
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
this.WriteObjects("        public ",  exposedListType , "<",  referencedInterface , "> ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (",  wrapperName , " == null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    List<",  referencedInterface , "> serverList;\r\n");
this.WriteObjects("                    if (Helper.IsPersistedObject(this))\r\n");
this.WriteObjects("                        serverList = Context.GetListOf<",  referencedInterface , ">(this, \"",  name , "\");\r\n");
this.WriteObjects("                    else\r\n");
this.WriteObjects("                        serverList = new List<",  referencedInterface , ">();\r\n");
this.WriteObjects("                        \r\n");
this.WriteObjects("                    ",  wrapperName , " = new ",  wrapperClass , "<",  referencedInterface , ">(\r\n");
this.WriteObjects("                        \"",  otherName , "\",\r\n");
this.WriteObjects("                        this,\r\n");
this.WriteObjects("                        serverList);\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return ",  wrapperName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        private ",  wrapperClass , "<",  referencedInterface , "> ",  wrapperName , ";\r\n");

        }



    }
}