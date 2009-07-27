using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst")]
    public partial class ObjectReferencePropertyTemplate : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Templates.Implementation.SerializationMembersList serializationList;
		protected String name;
		protected String associationName;
		protected String targetRoleName;
		protected String referencedInterface;
		protected String referencedImplementation;
		protected bool hasPositionStorage;
		protected bool relDataTypeExportable;
		protected string moduleNamespace;
		protected bool eagerLoading;
		protected bool callGetterSetterEvents;


        public ObjectReferencePropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList, String name, String associationName, String targetRoleName, String referencedInterface, String referencedImplementation, bool hasPositionStorage, bool relDataTypeExportable, string moduleNamespace, bool eagerLoading, bool callGetterSetterEvents)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.name = name;
			this.associationName = associationName;
			this.targetRoleName = targetRoleName;
			this.referencedInterface = referencedInterface;
			this.referencedImplementation = referencedImplementation;
			this.hasPositionStorage = hasPositionStorage;
			this.relDataTypeExportable = relDataTypeExportable;
			this.moduleNamespace = moduleNamespace;
			this.eagerLoading = eagerLoading;
			this.callGetterSetterEvents = callGetterSetterEvents;

        }
        
        public override void Generate()
        {
#line 25 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
string efName = name + Kistl.API.Helper.ImplementationSuffix;
	string fkBackingName = "_fk_" + name;
	string fGuidkBackingName = "_fk_guid_" + name;
	string eventName = "On" + name;


#line 31 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("   		// ",  this.GetType() , "\r\n");
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
this.WriteObjects("        public ",  referencedInterface , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                return ",  efName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                // TODO: NotifyPropertyChanged()\r\n");
this.WriteObjects("                // TODO: only accept EF objects from same Context\r\n");
this.WriteObjects("                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();\r\n");
this.WriteObjects("                ",  efName , " = (",  referencedImplementation , ")value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        private int? ",  fkBackingName , ";\r\n");
this.WriteObjects("        private Guid? ",  fGuidkBackingName , " = null;\r\n");
#line 53 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
AddSerialization(serializationList, name);

#line 55 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // EF sees only this property\r\n");
this.WriteObjects("        [EdmRelationshipNavigationProperty(\"Model\", \"",  associationName , "\", \"",  targetRoleName , "\")]\r\n");
this.WriteObjects("        public ",  referencedImplementation , " ",  efName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                EntityReference<",  referencedImplementation , "> r\r\n");
this.WriteObjects("                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<",  referencedImplementation , ">(\r\n");
this.WriteObjects("                        \"Model.",  associationName , "\",\r\n");
this.WriteObjects("                        \"",  targetRoleName , "\");\r\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)\r\n");
this.WriteObjects("                    && !r.IsLoaded)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    r.Load(); \r\n");
this.WriteObjects("                    if(r.Value != null) r.Value.AttachToContext(this.Context);\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                var __value = r.Value;\r\n");
#line 73 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if(callGetterSetterEvents)
				{

#line 76 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("				if(",  eventName , "_Getter != null)\r\n");
this.WriteObjects("				{\r\n");
this.WriteObjects("					var e = new PropertyGetterEventArgs<",  referencedInterface , ">(__value);\r\n");
this.WriteObjects("					",  eventName , "_Getter(this, e);\r\n");
this.WriteObjects("					__value = (",  referencedImplementation , ")e.Result;\r\n");
this.WriteObjects("				}\r\n");
#line 83 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 85 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                return __value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                EntityReference<",  referencedImplementation , "> r\r\n");
this.WriteObjects("                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<",  referencedImplementation , ">(\r\n");
this.WriteObjects("                        \"Model.",  associationName , "\",\r\n");
this.WriteObjects("                        \"",  targetRoleName , "\");\r\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)\r\n");
this.WriteObjects("                    && !r.IsLoaded)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    r.Load(); \r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                ",  referencedInterface , " __oldValue = (",  referencedInterface , ")r.Value;\r\n");
this.WriteObjects("                ",  referencedInterface , " __newValue = (",  referencedInterface , ")value;\r\n");
this.WriteObjects("\r\n");
#line 102 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if(callGetterSetterEvents)
				{

#line 105 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if(",  eventName , "_PreSetter != null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("					var e = new PropertyPreSetterEventArgs<",  referencedInterface , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("					",  eventName , "_PreSetter(this, e);\r\n");
this.WriteObjects("					__newValue = e.Result;\r\n");
this.WriteObjects("                }\r\n");
#line 112 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 114 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                r.Value = (",  referencedImplementation , ")__newValue;\r\n");
#line 116 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if(callGetterSetterEvents)
				{

#line 119 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if(",  eventName , "_PostSetter != null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("					var e = new PropertyPostSetterEventArgs<",  referencedInterface , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("					",  eventName , "_PostSetter(this, e);\r\n");
this.WriteObjects("                }\r\n");
#line 125 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 126 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                                \r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
#line 131 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
string posStorageName = name + Kistl.API.Helper.PositionSuffix;

	if (hasPositionStorage)
	{
		Templates.Implementation.ObjectClasses.NotifyingValueProperty.Call(Host, ctx,
		    serializationList,
			"int?", posStorageName, moduleNamespace);
	}

#line 140 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        \r\n");

        }



    }
}