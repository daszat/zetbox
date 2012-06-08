using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst")]
    public partial class ObjectListProperty : Zetbox.Generator.MemberTemplate
    {
		protected IZetboxContext ctx;
		protected Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList;
		protected string name;
		protected bool eagerLoading;
		protected string wrapperName;
		protected string wrapperClass;
		protected string exposedListType;
		protected string positionPropertyName;
		protected string otherName;
		protected string referencedInterface;
		protected string referencedProxy;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string name, bool eagerLoading, string wrapperName, string wrapperClass, string exposedListType, string positionPropertyName, string otherName, string referencedInterface, string referencedProxy)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ObjectListProperty", ctx, serializationList, name, eagerLoading, wrapperName, wrapperClass, exposedListType, positionPropertyName, otherName, referencedInterface, referencedProxy);
        }

        public ObjectListProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string name, bool eagerLoading, string wrapperName, string wrapperClass, string exposedListType, string positionPropertyName, string otherName, string referencedInterface, string referencedProxy)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.name = name;
			this.eagerLoading = eagerLoading;
			this.wrapperName = wrapperName;
			this.wrapperClass = wrapperClass;
			this.exposedListType = exposedListType;
			this.positionPropertyName = positionPropertyName;
			this.otherName = otherName;
			this.referencedInterface = referencedInterface;
			this.referencedProxy = referencedProxy;

        }

        public override void Generate()
        {
#line 23 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("\r\n");
#line 25 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
var eventName = "On" + name + "_PostSetter";

#line 27 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
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
this.WriteObjects("                    ",  wrapperName , " = new ",  wrapperClass , "<",  referencedInterface , ">(\r\n");
this.WriteObjects("                        \"",  otherName , "\",\r\n");
this.WriteObjects("                        ");
#line 39 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
if (!String.IsNullOrEmpty(positionPropertyName)) { 
#line 39 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("\"",  positionPropertyName , "\"");
#line 39 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
} else { 
#line 39 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("null");
#line 39 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
} 
#line 39 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects(",\r\n");
this.WriteObjects("                        this,\r\n");
this.WriteObjects("                        () => this.NotifyPropertyChanging(\"",  name , "\", null, null),\r\n");
this.WriteObjects("                        () => { this.NotifyPropertyChanged(\"",  name , "\", null, null); if(",  eventName , " != null && IsAttached) ",  eventName, "(this); },\r\n");
this.WriteObjects("                        new ProjectedCollection<",  referencedProxy , ", ",  referencedInterface , ">(\r\n");
this.WriteObjects("                            () => Proxy.",  name , ",\r\n");
this.WriteObjects("                            p => (",  referencedInterface , ")OurContext.AttachAndWrap(p),\r\n");
this.WriteObjects("                            d => (",  referencedProxy , ")((NHibernatePersistenceObject)d).NHibernateProxy));\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return ",  wrapperName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("        private ",  wrapperClass , "<",  referencedInterface , "> ",  wrapperName , ";\r\n");
#line 53 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
if (eagerLoading) { 
#line 54 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("        private List<int> ",  name , "Ids;\r\n");
this.WriteObjects("        private bool ",  name , "_was_eagerLoaded = false;\r\n");
#line 56 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
} 
#line 57 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
AddSerialization(serializationList, name, eagerLoading); 

        }

    }
}