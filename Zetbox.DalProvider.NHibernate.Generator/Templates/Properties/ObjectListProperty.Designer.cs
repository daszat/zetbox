using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst")]
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
#line 17 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("");
#line 39 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("\n");
#line 41 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
var eventName = "On" + name + "_PostSetter";

#line 43 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("        // ",  this.GetType() , "\n");
this.WriteObjects("        // implement the user-visible interface\n");
this.WriteObjects("        [XmlIgnore()]\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\n");
this.WriteObjects("        ",  GetModifiers() , " ",  exposedListType , "<",  referencedInterface , "> ",  name , "\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get\n");
this.WriteObjects("            {\n");
this.WriteObjects("                if (",  wrapperName , " == null)\n");
this.WriteObjects("                {\n");
this.WriteObjects("                    ",  wrapperName , " = new ",  wrapperClass , "<",  referencedInterface , ">(\n");
this.WriteObjects("                        \"",  otherName , "\",\n");
this.WriteObjects("                        ");
#line 55 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
if (!String.IsNullOrEmpty(positionPropertyName)) { 
#line 55 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("\"",  positionPropertyName , "\"");
#line 55 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
} else { 
#line 55 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("null");
#line 55 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
} 
#line 55 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects(",\n");
this.WriteObjects("                        this,\n");
this.WriteObjects("                        () => this.NotifyPropertyChanging(\"",  name , "\", null, null),\n");
this.WriteObjects("                        () => { this.NotifyPropertyChanged(\"",  name , "\", null, null); if(",  eventName , " != null && IsAttached) ",  eventName, "(this); },\n");
this.WriteObjects("                        new ProjectedCollection<",  referencedProxy , ", ",  referencedInterface , ">(\n");
this.WriteObjects("                            () => Proxy.",  name , ",\n");
this.WriteObjects("                            p => (",  referencedInterface , ")OurContext.AttachAndWrap(p),\n");
this.WriteObjects("                            d => (",  referencedProxy , ")((NHibernatePersistenceObject)d).NHibernateProxy));\n");
this.WriteObjects("                }\n");
this.WriteObjects("                return ",  wrapperName , ";\n");
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
this.WriteObjects("    \n");
this.WriteObjects("        private ",  wrapperClass , "<",  referencedInterface , "> ",  wrapperName , ";\n");
#line 69 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
if (eagerLoading) { 
#line 70 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
this.WriteObjects("        private List<int> ",  name , "Ids;\n");
this.WriteObjects("        private bool ",  name , "_was_eagerLoaded = false;\n");
#line 72 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
} 
#line 73 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectListProperty.cst"
AddSerialization(serializationList, name, eagerLoading); 

        }

    }
}