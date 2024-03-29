using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectListProperty.cst")]
    public partial class ObjectListProperty : Zetbox.Generator.MemberTemplate
    {
		protected IZetboxContext ctx;
		protected Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList;
		protected string name;
		protected string wrapperName;
		protected string wrapperClass;
		protected string exposedListType;
		protected Relation rel;
		protected RelationEndRole endRole;
		protected string positionPropertyName;
		protected string otherName;
		protected string referencedInterface;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string name, string wrapperName, string wrapperClass, string exposedListType, Relation rel, RelationEndRole endRole, string positionPropertyName, string otherName, string referencedInterface)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ObjectListProperty", ctx, serializationList, name, wrapperName, wrapperClass, exposedListType, rel, endRole, positionPropertyName, otherName, referencedInterface);
        }

        public ObjectListProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string name, string wrapperName, string wrapperClass, string exposedListType, Relation rel, RelationEndRole endRole, string positionPropertyName, string otherName, string referencedInterface)
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
#line 41 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectListProperty.cst"
RelationEnd relEnd = rel.GetEndFromRole(endRole).Result;
    // RelationEnd otherEnd = rel.GetOtherEnd(relEnd).Result;

    string idsListName = name + "Ids";
    string taskName = "_triggerFetch" + name + "Task";

    // whether or not the collection will be eagerly loaded
    bool eagerLoading = relEnd.Navigator != null && relEnd.Navigator.EagerLoading;

    var eventName = "On" + name + "_PostSetter";

#line 52 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("        // ",  this.GetType() , "\r\n");
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("		[System.Runtime.Serialization.IgnoreDataMember]\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  exposedListType , "<",  referencedInterface , "> ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (",  wrapperName , " == null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var task = TriggerFetch",  name , "Async();\r\n");
this.WriteObjects("                    task.TryRunSynchronously();\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return ",  wrapperName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        ",  GetModifiers() , " async System.Threading.Tasks.Task<",  exposedListType , "<",  referencedInterface , ">> GetProp_",  name , "()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            await TriggerFetch",  name , "Async();\r\n");
this.WriteObjects("            return ",  wrapperName , ";\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        System.Threading.Tasks.Task ",  taskName , ";\r\n");
this.WriteObjects("        public System.Threading.Tasks.Task TriggerFetch",  name , "Async()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            if (",  taskName , " != null) return ",  taskName , ";\r\n");
this.WriteObjects("            System.Threading.Tasks.Task task;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            List<",  referencedInterface , "> serverList = null;\r\n");
this.WriteObjects("            if (Helper.IsPersistedObject(this))\r\n");
this.WriteObjects("            {\r\n");
#line 85 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectListProperty.cst"
if (eagerLoading) { 
#line 86 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("                if (",  idsListName , " != null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    task = System.Threading.Tasks.Task.FromResult(",  idsListName , ".Select(id => Context.Find<",  referencedInterface , ">(id)).ToList()).OnResult(t =>\r\n");
this.WriteObjects("                    {\r\n");
this.WriteObjects("                        serverList = t.Result;\r\n");
this.WriteObjects("                        ",  idsListName , " = null; // allow id list to be garbage collected\r\n");
this.WriteObjects("                    });\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                else\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    task = Context.GetListOfAsync<",  referencedInterface , ">(this, \"",  name , "\")\r\n");
this.WriteObjects("                        .OnResult(t =>\r\n");
this.WriteObjects("                        {\r\n");
this.WriteObjects("                            serverList = t.Result;\r\n");
this.WriteObjects("                        });\r\n");
this.WriteObjects("                }\r\n");
#line 102 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectListProperty.cst"
} else { 
#line 103 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("                task = Context.GetListOfAsync<",  referencedInterface , ">(this, \"",  name , "\")\r\n");
this.WriteObjects("                    .OnResult(t =>\r\n");
this.WriteObjects("                    {\r\n");
this.WriteObjects("                        serverList = t.Result;\r\n");
this.WriteObjects("                    });\r\n");
#line 108 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectListProperty.cst"
} 
#line 109 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("            else\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                task = System.Threading.Tasks.Task.FromResult(new List<",  referencedInterface , ">()).OnResult(t =>\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    serverList = t.Result;\r\n");
this.WriteObjects("                });\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            task = task.OnResult(t =>\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                ",  wrapperName , " = new ",  wrapperClass , "<",  referencedInterface , ">(\r\n");
this.WriteObjects("                    \"",  otherName , "\",\r\n");
this.WriteObjects("                    ");
#line 122 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectListProperty.cst"
if (!String.IsNullOrEmpty(positionPropertyName)) { 
#line 122 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("\"",  positionPropertyName , "\"");
#line 122 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectListProperty.cst"
} else { 
#line 122 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("null");
#line 122 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectListProperty.cst"
} 
#line 122 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects(",\r\n");
this.WriteObjects("                    this,\r\n");
this.WriteObjects("                    On",  name , "CollectionChanged,\r\n");
this.WriteObjects("                    serverList);\r\n");
this.WriteObjects("            });\r\n");
this.WriteObjects("            return ",  taskName , " = task;\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        internal void On",  name , "CollectionChanged()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            NotifyPropertyChanged(\"",  name , "\", null, null);\r\n");
this.WriteObjects("            if (",  eventName , " != null && IsAttached)\r\n");
this.WriteObjects("                ",  eventName , "(this);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        private ",  wrapperClass , "<",  referencedInterface , "> ",  wrapperName , ";\r\n");
#line 139 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectListProperty.cst"
if (eagerLoading)
    {

#line 142 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("        private List<int> ",  name , "Ids;\r\n");
this.WriteObjects("        private bool ",  name , "_was_eagerLoaded = false;\r\n");
#line 145 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectListProperty.cst"
}

    AddSerialization(serializationList, name, eagerLoading);


        }

    }
}