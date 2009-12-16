using System;
using System.Diagnostics;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectListProperty.cst")]
    public partial class ObjectListProperty : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Templates.Implementation.SerializationMembersList serializationList;
		protected string name;
		protected string wrapperName;
		protected string wrapperClass;
		protected string exposedListType;
		protected Relation rel;
		protected RelationEndRole endRole;
		protected string otherName;
		protected string referencedInterface;


        public ObjectListProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList, string name, string wrapperName, string wrapperClass, string exposedListType, Relation rel, RelationEndRole endRole, string otherName, string referencedInterface)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.name = name;
			this.wrapperName = wrapperName;
			this.wrapperClass = wrapperClass;
			this.exposedListType = exposedListType;
			this.rel = rel;
			this.endRole = endRole;
			this.otherName = otherName;
			this.referencedInterface = referencedInterface;

        }
        
        public override void Generate()
        {
#line 25 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectListProperty.cst"
RelationEnd relEnd = rel.GetEndFromRole(endRole);
    RelationEnd otherEnd = rel.GetOtherEnd(relEnd);
        
    string idsListName = name + "Ids";
	
	// whether or not the collection will be eagerly loaded
	bool eagerLoading = relEnd.Navigator != null && relEnd.Navigator.EagerLoading;
	        

#line 34 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectListProperty.cst"
this.WriteObjects("		// ",  this.GetType() , "\r\n");
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
this.WriteObjects("                    {\r\n");
#line 47 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectListProperty.cst"
if (eagerLoading) { 
#line 48 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectListProperty.cst"
this.WriteObjects("						serverList = ",  idsListName , ".Select(id => Context.Find<",  referencedInterface , ">(id)).ToList();\r\n");
this.WriteObjects("						",  idsListName , " = null; // allow id list to be garbage collected\r\n");
#line 50 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectListProperty.cst"
} else { 
#line 51 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectListProperty.cst"
this.WriteObjects("						serverList = Context.GetListOf<",  referencedInterface , ">(this, \"",  name , "\");\r\n");
#line 52 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectListProperty.cst"
} 
#line 53 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectListProperty.cst"
this.WriteObjects("					}\r\n");
this.WriteObjects("                    else\r\n");
this.WriteObjects("                    {\r\n");
this.WriteObjects("                        serverList = new List<",  referencedInterface , ">();\r\n");
this.WriteObjects("                    }\r\n");
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
this.WriteObjects("\r\n");
#line 71 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectListProperty.cst"
if (eagerLoading)
	{

#line 74 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectListProperty.cst"
this.WriteObjects("		private List<int> ",  name , "Ids;\r\n");
#line 76 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectListProperty.cst"
}

    AddSerialization(serializationList, name, eagerLoading);


        }



    }
}