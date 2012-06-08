using System;
using System.Collections.Generic;
using System.Diagnostics;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ValueCollectionProperty.cst")]
    public partial class ValueCollectionProperty : Zetbox.Generator.MemberTemplate
    {
		protected IZetboxContext ctx;
		protected Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList;
		protected string name;
		protected string backingName;
		protected string backingCollectionType;
		protected string exposedCollectionInterface;
		protected string thisInterface;
		protected string referencedType;
		protected string referencedCollectionEntry;
		protected string referencedCollectionEntryImpl;
		protected string referencedCollectionEntryProxy;
		protected string providerCollectionType;
		protected string underlyingCollectionName;
		protected string underlyingCollectionBackingName;
		protected bool orderByValue;
		protected string moduleNamespace;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string name, string backingName, string backingCollectionType, string exposedCollectionInterface, string thisInterface, string referencedType, string referencedCollectionEntry, string referencedCollectionEntryImpl, string referencedCollectionEntryProxy, string providerCollectionType, string underlyingCollectionName, string underlyingCollectionBackingName, bool orderByValue, string moduleNamespace)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ValueCollectionProperty", ctx, serializationList, name, backingName, backingCollectionType, exposedCollectionInterface, thisInterface, referencedType, referencedCollectionEntry, referencedCollectionEntryImpl, referencedCollectionEntryProxy, providerCollectionType, underlyingCollectionName, underlyingCollectionBackingName, orderByValue, moduleNamespace);
        }

        public ValueCollectionProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string name, string backingName, string backingCollectionType, string exposedCollectionInterface, string thisInterface, string referencedType, string referencedCollectionEntry, string referencedCollectionEntryImpl, string referencedCollectionEntryProxy, string providerCollectionType, string underlyingCollectionName, string underlyingCollectionBackingName, bool orderByValue, string moduleNamespace)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.name = name;
			this.backingName = backingName;
			this.backingCollectionType = backingCollectionType;
			this.exposedCollectionInterface = exposedCollectionInterface;
			this.thisInterface = thisInterface;
			this.referencedType = referencedType;
			this.referencedCollectionEntry = referencedCollectionEntry;
			this.referencedCollectionEntryImpl = referencedCollectionEntryImpl;
			this.referencedCollectionEntryProxy = referencedCollectionEntryProxy;
			this.providerCollectionType = providerCollectionType;
			this.underlyingCollectionName = underlyingCollectionName;
			this.underlyingCollectionBackingName = underlyingCollectionBackingName;
			this.orderByValue = orderByValue;
			this.moduleNamespace = moduleNamespace;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ValueCollectionProperty.cst"
this.WriteObjects("");
#line 45 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ValueCollectionProperty.cst"
this.WriteObjects("\n");
#line 47 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ValueCollectionProperty.cst"
var eventName = "On" + name + "_PostSetter";

#line 49 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ValueCollectionProperty.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\n");
#line 50 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ValueCollectionProperty.cst"
AddSerialization(serializationList, underlyingCollectionName); 
#line 51 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ValueCollectionProperty.cst"
this.WriteObjects("        ",  GetModifiers() , " ",  exposedCollectionInterface , "<",  referencedType , "> ",  name , "\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get\n");
this.WriteObjects("            {\n");
this.WriteObjects("                if (",  backingName , " == null)\n");
this.WriteObjects("                {\n");
this.WriteObjects("                    ",  backingName , " = new ",  backingCollectionType , "(\n");
this.WriteObjects("                            this.Context,\n");
this.WriteObjects("                            this,\n");
this.WriteObjects("                            () => { this.NotifyPropertyChanged(\"",  name , "\", null, null); if(",  eventName , " != null && IsAttached) ",  eventName, "(this); },\n");
this.WriteObjects("                            ",  underlyingCollectionName , ");\n");
this.WriteObjects("                }\n");
this.WriteObjects("                return ",  backingName , ";\n");
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("        private ProjectedCollection<",  referencedCollectionEntryProxy , ", ",  referencedCollectionEntryImpl , "> ",  underlyingCollectionName , "\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get {\n");
this.WriteObjects("                if (",  underlyingCollectionBackingName , " == null)\n");
this.WriteObjects("                {\n");
this.WriteObjects("                    ",  underlyingCollectionBackingName , " = new ProjectedCollection<",  referencedCollectionEntryProxy , ", ",  referencedCollectionEntryImpl , ">(\n");
this.WriteObjects("                        () => this.Proxy.",  name , ",\n");
this.WriteObjects("                        p => (",  referencedCollectionEntryImpl , ")OurContext.AttachAndWrap(p),\n");
this.WriteObjects("                        d => (",  referencedCollectionEntryProxy , ")((NHibernatePersistenceObject)d).NHibernateProxy);\n");
this.WriteObjects("                }\n");
this.WriteObjects("                return ",  underlyingCollectionBackingName , ";\n");
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("        private ",  backingCollectionType , " ",  backingName , ";\n");
this.WriteObjects("        private ProjectedCollection<",  referencedCollectionEntryProxy , ", ",  referencedCollectionEntryImpl , "> ",  underlyingCollectionBackingName , ";\n");
this.WriteObjects("        // END ",  this.GetType() , "\n");

        }

    }
}