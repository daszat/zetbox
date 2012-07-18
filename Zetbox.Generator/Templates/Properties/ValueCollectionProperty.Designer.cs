using System;
using System.Collections.Generic;
using System.Diagnostics;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\Properties\ValueCollectionProperty.cst")]
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
		protected string entryType;
		protected string entryTypeImpl;
		protected string providerCollectionType;
		protected string underlyingCollectionName;
		protected bool orderByValue;
		protected string moduleNamespace;
		protected bool disableExport;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string name, string backingName, string backingCollectionType, string exposedCollectionInterface, string thisInterface, string referencedType, string entryType, string entryTypeImpl, string providerCollectionType, string underlyingCollectionName, bool orderByValue, string moduleNamespace, bool disableExport)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ValueCollectionProperty", ctx, serializationList, name, backingName, backingCollectionType, exposedCollectionInterface, thisInterface, referencedType, entryType, entryTypeImpl, providerCollectionType, underlyingCollectionName, orderByValue, moduleNamespace, disableExport);
        }

        public ValueCollectionProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string name, string backingName, string backingCollectionType, string exposedCollectionInterface, string thisInterface, string referencedType, string entryType, string entryTypeImpl, string providerCollectionType, string underlyingCollectionName, bool orderByValue, string moduleNamespace, bool disableExport)
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
			this.entryType = entryType;
			this.entryTypeImpl = entryTypeImpl;
			this.providerCollectionType = providerCollectionType;
			this.underlyingCollectionName = underlyingCollectionName;
			this.orderByValue = orderByValue;
			this.moduleNamespace = moduleNamespace;
			this.disableExport = disableExport;

        }

        public override void Generate()
        {
#line 46 "P:\zetbox\Zetbox.Generator\Templates\Properties\ValueCollectionProperty.cst"
AddSerialization(serializationList, underlyingCollectionName);
	var eventName = "On" + name + "_PostSetter";

#line 49 "P:\zetbox\Zetbox.Generator\Templates\Properties\ValueCollectionProperty.cst"
this.WriteObjects("		// ",  this.GetType() , "\r\n");
this.WriteObjects("		",  GetModifiers() , " ",  exposedCollectionInterface , "<",  referencedType , "> ",  name , "\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			get\r\n");
this.WriteObjects("			{\r\n");
this.WriteObjects("				if (",  backingName , " == null)\r\n");
this.WriteObjects("				{\r\n");
this.WriteObjects("				    ",  backingName , " \r\n");
this.WriteObjects("				        = new ",  backingCollectionType , "<",  thisInterface , ", ",  referencedType , ", ",  entryType , ", ",  entryTypeImpl , ", ",  providerCollectionType , ">(\r\n");
this.WriteObjects("							this.Context,\r\n");
this.WriteObjects("				            this, \r\n");
this.WriteObjects("				            () => { this.NotifyPropertyChanged(\"",  name , "\", null, null); if(",  eventName , " != null && IsAttached) ",  eventName, "(this); },\r\n");
this.WriteObjects("				            ",  underlyingCollectionName , ");\r\n");
this.WriteObjects("				}\r\n");
this.WriteObjects("				return ",  backingName , ";\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		private ",  backingCollectionType , "<",  thisInterface , ", ",  referencedType , ", ",  entryType , ", ",  entryTypeImpl , ", ",  providerCollectionType , "> ",  backingName , ";\r\n");
this.WriteObjects("		private ",  providerCollectionType , " ",  underlyingCollectionName , " = new ",  providerCollectionType , "();\r\n");

        }

    }
}