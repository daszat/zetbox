using System;
using System.Collections.Generic;
using System.Diagnostics;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.NHibernate.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ValueCollectionProperty.cst")]
    public partial class ValueCollectionProperty : Kistl.Generator.MemberTemplate
    {
		protected IKistlContext ctx;
		protected Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList;
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
		protected string ListOrCollection;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList, string name, string backingName, string backingCollectionType, string exposedCollectionInterface, string thisInterface, string referencedType, string referencedCollectionEntry, string referencedCollectionEntryImpl, string referencedCollectionEntryProxy, string providerCollectionType, string underlyingCollectionName, string underlyingCollectionBackingName, bool orderByValue, string moduleNamespace, string ListOrCollection)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ValueCollectionProperty", ctx, serializationList, name, backingName, backingCollectionType, exposedCollectionInterface, thisInterface, referencedType, referencedCollectionEntry, referencedCollectionEntryImpl, referencedCollectionEntryProxy, providerCollectionType, underlyingCollectionName, underlyingCollectionBackingName, orderByValue, moduleNamespace, ListOrCollection);
        }

        public ValueCollectionProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList, string name, string backingName, string backingCollectionType, string exposedCollectionInterface, string thisInterface, string referencedType, string referencedCollectionEntry, string referencedCollectionEntryImpl, string referencedCollectionEntryProxy, string providerCollectionType, string underlyingCollectionName, string underlyingCollectionBackingName, bool orderByValue, string moduleNamespace, string ListOrCollection)
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
			this.ListOrCollection = ListOrCollection;

        }

        public override void Generate()
        {
#line 30 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ValueCollectionProperty.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\r\n");
#line 31 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ValueCollectionProperty.cst"
AddSerialization(serializationList, underlyingCollectionName); 
#line 32 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ValueCollectionProperty.cst"
this.WriteObjects("        ",  GetModifiers() , " ",  exposedCollectionInterface , "<",  referencedType , "> ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (",  backingName , " == null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    ",  backingName , " = new ",  backingCollectionType , "(\r\n");
this.WriteObjects("                            this.Context,\r\n");
this.WriteObjects("                            this,\r\n");
this.WriteObjects("                            () => this.NotifyPropertyChanged(\"",  name , "\", null, null),\r\n");
this.WriteObjects("                            ",  underlyingCollectionName , ");\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return ",  backingName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        private Projected",  ListOrCollection , "<",  referencedCollectionEntryProxy , ", ",  referencedCollectionEntryImpl , "> ",  underlyingCollectionName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get {\r\n");
this.WriteObjects("                if (",  underlyingCollectionBackingName , " == null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    ",  underlyingCollectionBackingName , " = new Projected",  ListOrCollection , "<",  referencedCollectionEntryProxy , ", ",  referencedCollectionEntryImpl , ">(\r\n");
#line 54 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ValueCollectionProperty.cst"
if (ListOrCollection == "List" ) { 
#line 55 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ValueCollectionProperty.cst"
this.WriteObjects("                        MagicCollectionFactory.WrapAsList(this.Proxy.",  name , "),\r\n");
#line 56 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ValueCollectionProperty.cst"
} else { 
#line 57 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ValueCollectionProperty.cst"
this.WriteObjects("                        this.Proxy.",  name , ",\r\n");
#line 58 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ValueCollectionProperty.cst"
} 
#line 59 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ValueCollectionProperty.cst"
this.WriteObjects("                        p => (",  referencedCollectionEntryImpl , ")OurContext.AttachAndWrap(p),\r\n");
this.WriteObjects("                        d => (",  referencedCollectionEntryProxy , ")((NHibernatePersistenceObject)d).NHibernateProxy);\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return ",  underlyingCollectionBackingName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        private ",  backingCollectionType , " ",  backingName , ";\r\n");
this.WriteObjects("        private Projected",  ListOrCollection , "<",  referencedCollectionEntryProxy , ", ",  referencedCollectionEntryImpl , "> ",  underlyingCollectionBackingName , ";\r\n");
this.WriteObjects("        // END ",  this.GetType() , "\r\n");

        }

    }
}