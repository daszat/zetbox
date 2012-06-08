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


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string name, string backingName, string backingCollectionType, string exposedCollectionInterface, string thisInterface, string referencedType, string entryType, string entryTypeImpl, string providerCollectionType, string underlyingCollectionName, bool orderByValue, string moduleNamespace)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ValueCollectionProperty", ctx, serializationList, name, backingName, backingCollectionType, exposedCollectionInterface, thisInterface, referencedType, entryType, entryTypeImpl, providerCollectionType, underlyingCollectionName, orderByValue, moduleNamespace);
        }

        public ValueCollectionProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string name, string backingName, string backingCollectionType, string exposedCollectionInterface, string thisInterface, string referencedType, string entryType, string entryTypeImpl, string providerCollectionType, string underlyingCollectionName, bool orderByValue, string moduleNamespace)
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

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.Generator\Templates\Properties\ValueCollectionProperty.cst"
this.WriteObjects("");
#line 45 "P:\zetbox\Zetbox.Generator\Templates\Properties\ValueCollectionProperty.cst"
AddSerialization(serializationList, underlyingCollectionName);
	var eventName = "On" + name + "_PostSetter";

#line 48 "P:\zetbox\Zetbox.Generator\Templates\Properties\ValueCollectionProperty.cst"
this.WriteObjects("		// ",  this.GetType() , "\n");
this.WriteObjects("		",  GetModifiers() , " ",  exposedCollectionInterface , "<",  referencedType , "> ",  name , "\n");
this.WriteObjects("		{\n");
this.WriteObjects("			get\n");
this.WriteObjects("			{\n");
this.WriteObjects("				if (",  backingName , " == null)\n");
this.WriteObjects("				{\n");
this.WriteObjects("				    ",  backingName , " \n");
this.WriteObjects("				        = new ",  backingCollectionType , "<",  thisInterface , ", ",  referencedType , ", ",  entryType , ", ",  entryTypeImpl , ", ",  providerCollectionType , ">(\n");
this.WriteObjects("							this.Context,\n");
this.WriteObjects("				            this, \n");
this.WriteObjects("				            () => { this.NotifyPropertyChanged(\"",  name , "\", null, null); if(",  eventName , " != null && IsAttached) ",  eventName, "(this); },\n");
this.WriteObjects("				            ",  underlyingCollectionName , ");\n");
this.WriteObjects("				}\n");
this.WriteObjects("				return ",  backingName , ";\n");
this.WriteObjects("			}\n");
this.WriteObjects("		}\n");
this.WriteObjects("\n");
this.WriteObjects("		private ",  backingCollectionType , "<",  thisInterface , ", ",  referencedType , ", ",  entryType , ", ",  entryTypeImpl , ", ",  providerCollectionType , "> ",  backingName , ";\n");
this.WriteObjects("		private ",  providerCollectionType , " ",  underlyingCollectionName , " = new ",  providerCollectionType , "();\n");

        }

    }
}