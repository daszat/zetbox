using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\Properties\ObjectListProperty.cst")]
    public partial class ObjectListProperty : Kistl.Generator.MemberTemplate
    {
		protected IKistlContext ctx;
		protected Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList;
		protected string name;
		protected string wrapperName;
		protected string wrapperClass;
		protected string exposedListType;
		protected Relation rel;
		protected RelationEndRole endRole;
		protected string positionPropertyName;
		protected string otherName;
		protected string referencedInterface;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList, string name, string wrapperName, string wrapperClass, string exposedListType, Relation rel, RelationEndRole endRole, string positionPropertyName, string otherName, string referencedInterface)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ObjectListProperty", ctx, serializationList, name, wrapperName, wrapperClass, exposedListType, rel, endRole, positionPropertyName, otherName, referencedInterface);
        }

        public ObjectListProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList, string name, string wrapperName, string wrapperClass, string exposedListType, Relation rel, RelationEndRole endRole, string positionPropertyName, string otherName, string referencedInterface)
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
			this.positionPropertyName = positionPropertyName;
			this.otherName = otherName;
			this.referencedInterface = referencedInterface;

        }

        public override void Generate()
        {
#line 25 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectListProperty.cst"
RelationEnd relEnd = rel.GetEndFromRole(endRole);
    RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

    string idsListName = name + "Ids";
    
    // whether or not the collection will be eagerly loaded
    bool eagerLoading = relEnd.Navigator != null && relEnd.Navigator.EagerLoading;
    

#line 34 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("        // ",  this.GetType() , "\r\n");
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  exposedListType , "<",  referencedInterface , "> ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (",  wrapperName , " == null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    List<",  referencedInterface , "> serverList;\r\n");
this.WriteObjects("                    if (Helper.IsPersistedObject(this))\r\n");
this.WriteObjects("                    {\r\n");
#line 47 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectListProperty.cst"
if (eagerLoading) { 
#line 48 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("                        if(",  idsListName , " != null)\r\n");
this.WriteObjects("                        {\r\n");
this.WriteObjects("                            serverList = ",  idsListName , ".Select(id => Context.Find<",  referencedInterface , ">(id)).ToList();\r\n");
this.WriteObjects("                            ",  idsListName , " = null; // allow id list to be garbage collected\r\n");
this.WriteObjects("                        }\r\n");
this.WriteObjects("                        else\r\n");
this.WriteObjects("                        {\r\n");
this.WriteObjects("                            serverList = Context.GetListOf<",  referencedInterface , ">(this, \"",  name , "\");\r\n");
this.WriteObjects("                        }\r\n");
#line 57 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectListProperty.cst"
} else { 
#line 58 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("                        serverList = Context.GetListOf<",  referencedInterface , ">(this, \"",  name , "\");\r\n");
#line 59 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectListProperty.cst"
} 
#line 60 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("                    }\r\n");
this.WriteObjects("                    else\r\n");
this.WriteObjects("                    {\r\n");
this.WriteObjects("                        serverList = new List<",  referencedInterface , ">();\r\n");
this.WriteObjects("                    }\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("                    ",  wrapperName , " = new ",  wrapperClass , "<",  referencedInterface , ">(\r\n");
this.WriteObjects("                        \"",  otherName , "\",\r\n");
this.WriteObjects("                        ");
#line 68 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectListProperty.cst"
if (!String.IsNullOrEmpty(positionPropertyName)) { 
#line 68 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("\"",  positionPropertyName , "\"");
#line 68 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectListProperty.cst"
} else { 
#line 68 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("null");
#line 68 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectListProperty.cst"
} 
#line 68 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects(",\r\n");
this.WriteObjects("                        this,\r\n");
this.WriteObjects("                        () => this.NotifyPropertyChanged(\"",  name , "\", null, null),\r\n");
this.WriteObjects("                        serverList);\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return ",  wrapperName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("        private ",  wrapperClass , "<",  referencedInterface , "> ",  wrapperName , ";\r\n");
this.WriteObjects("\r\n");
#line 80 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectListProperty.cst"
if (eagerLoading)
    {

#line 83 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("        private List<int> ",  name , "Ids;\r\n");
this.WriteObjects("        private bool ",  name , "_was_eagerLoaded = false;\r\n");
#line 86 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectListProperty.cst"
}

    AddSerialization(serializationList, name, eagerLoading);


        }

    }
}