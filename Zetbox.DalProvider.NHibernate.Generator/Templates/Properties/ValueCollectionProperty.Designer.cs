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
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ValueCollectionProperty.cst")]
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
		protected bool disableExport;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string name, string backingName, string backingCollectionType, string exposedCollectionInterface, string thisInterface, string referencedType, string referencedCollectionEntry, string referencedCollectionEntryImpl, string referencedCollectionEntryProxy, string providerCollectionType, string underlyingCollectionName, string underlyingCollectionBackingName, bool orderByValue, string moduleNamespace, bool disableExport)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ValueCollectionProperty", ctx, serializationList, name, backingName, backingCollectionType, exposedCollectionInterface, thisInterface, referencedType, referencedCollectionEntry, referencedCollectionEntryImpl, referencedCollectionEntryProxy, providerCollectionType, underlyingCollectionName, underlyingCollectionBackingName, orderByValue, moduleNamespace, disableExport);
        }

        public ValueCollectionProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string name, string backingName, string backingCollectionType, string exposedCollectionInterface, string thisInterface, string referencedType, string referencedCollectionEntry, string referencedCollectionEntryImpl, string referencedCollectionEntryProxy, string providerCollectionType, string underlyingCollectionName, string underlyingCollectionBackingName, bool orderByValue, string moduleNamespace, bool disableExport)
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
			this.disableExport = disableExport;

        }

        public override void Generate()
        {
#line 46 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ValueCollectionProperty.cst"
this.WriteObjects("\r\n");
#line 48 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ValueCollectionProperty.cst"
var eventName = "On" + name + "_PostSetter";

#line 50 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ValueCollectionProperty.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\r\n");
#line 51 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ValueCollectionProperty.cst"
AddSerialization(serializationList, underlyingCollectionName); 
#line 52 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ValueCollectionProperty.cst"
this.WriteObjects("        ",  GetModifiers() , " ",  exposedCollectionInterface , "<",  referencedType , "> ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (",  backingName , " == null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    ",  backingName , " = new ",  backingCollectionType , "(\r\n");
this.WriteObjects("                            this.Context,\r\n");
this.WriteObjects("                            this,\r\n");
this.WriteObjects("                            () => { this.NotifyPropertyChanged(\"",  name , "\", null, null); if(",  eventName , " != null && IsAttached) ",  eventName, "(this); },\r\n");
this.WriteObjects("                            ",  underlyingCollectionName , ");\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return ",  backingName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        private ProjectedCollection<",  referencedCollectionEntryProxy , ", ",  referencedCollectionEntryImpl , "> ",  underlyingCollectionName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get {\r\n");
this.WriteObjects("                if (",  underlyingCollectionBackingName , " == null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    ",  underlyingCollectionBackingName , " = new ProjectedCollection<",  referencedCollectionEntryProxy , ", ",  referencedCollectionEntryImpl , ">(\r\n");
this.WriteObjects("                        () => this.Proxy.",  name , ",\r\n");
this.WriteObjects("                        p => (",  referencedCollectionEntryImpl , ")OurContext.AttachAndWrap(p),\r\n");
this.WriteObjects("                        d => (",  referencedCollectionEntryProxy , ")((NHibernatePersistenceObject)d).NHibernateProxy);\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return ",  underlyingCollectionBackingName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        private ",  backingCollectionType , " ",  backingName , ";\r\n");
this.WriteObjects("        private ProjectedCollection<",  referencedCollectionEntryProxy , ", ",  referencedCollectionEntryImpl , "> ",  underlyingCollectionBackingName , ";\r\n");
this.WriteObjects("        // END ",  this.GetType() , "\r\n");

        }

    }
}