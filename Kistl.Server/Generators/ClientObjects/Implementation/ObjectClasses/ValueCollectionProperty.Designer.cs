using System;
using System.Collections.Generic;
using System.Diagnostics;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ValueCollectionProperty.cst")]
    public partial class ValueCollectionProperty : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Templates.Implementation.SerializationMembersList serializationList;
		protected string name;
		protected string backingName;
		protected string backingCollectionType;
		protected string exposedCollectionInterface;
		protected string thisInterface;
		protected string referencedType;
		protected string entryType;
		protected string providerCollectionType;
		protected string underlyingCollectionName;
		protected bool orderByB;
		protected string moduleNamespace;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList, string name, string backingName, string backingCollectionType, string exposedCollectionInterface, string thisInterface, string referencedType, string entryType, string providerCollectionType, string underlyingCollectionName, bool orderByB, string moduleNamespace)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Implementation.ObjectClasses.ValueCollectionProperty", ctx, serializationList, name, backingName, backingCollectionType, exposedCollectionInterface, thisInterface, referencedType, entryType, providerCollectionType, underlyingCollectionName, orderByB, moduleNamespace);
        }

        public ValueCollectionProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList, string name, string backingName, string backingCollectionType, string exposedCollectionInterface, string thisInterface, string referencedType, string entryType, string providerCollectionType, string underlyingCollectionName, bool orderByB, string moduleNamespace)
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
			this.providerCollectionType = providerCollectionType;
			this.underlyingCollectionName = underlyingCollectionName;
			this.orderByB = orderByB;
			this.moduleNamespace = moduleNamespace;

        }

        public override void Generate()
        {
#line 28 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ValueCollectionProperty.cst"
AddSerialization(serializationList, underlyingCollectionName);


#line 31 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ValueCollectionProperty.cst"
this.WriteObjects("		// ",  this.GetType() , "\r\n");
this.WriteObjects("		public ",  exposedCollectionInterface , "<",  referencedType , "> ",  name , "\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			get\r\n");
this.WriteObjects("			{\r\n");
this.WriteObjects("				if (",  backingName , " == null)\r\n");
this.WriteObjects("				{\r\n");
this.WriteObjects("				    ",  backingName , " \r\n");
this.WriteObjects("				        = new ",  backingCollectionType , "<",  thisInterface , ", ",  referencedType , ", ",  entryType , ", ",  providerCollectionType , ">(\r\n");
this.WriteObjects("							this.Context,\r\n");
this.WriteObjects("				            this, \r\n");
this.WriteObjects("				            // () => this.NotifyPropertyChanged(\"",  name , "\", null, null),\r\n");
this.WriteObjects("				            ",  underlyingCollectionName , ");\r\n");
this.WriteObjects("				}\r\n");
this.WriteObjects("				return ",  backingName , ";\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("		private ",  backingCollectionType , "<",  thisInterface , ", ",  referencedType , ", ",  entryType , ", ",  providerCollectionType , "> ",  backingName , ";\r\n");
this.WriteObjects("		private ",  providerCollectionType , " ",  underlyingCollectionName , " = new List<",  entryType , ">();\r\n");

        }

    }
}