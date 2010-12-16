using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.Ef.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst")]
    public partial class ObjectReferencePropertyTemplate : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList;
		protected string moduleNamespace;
		protected string ownInterface;
		protected string name;
		protected string implName;
		protected string eventName;
		protected string fkBackingName;
		protected string fkGuidBackingName;
		protected string referencedInterface;
		protected string referencedImplementation;
		protected string associationName;
		protected string targetRoleName;
		protected string positionPropertyName;
		protected string inverseNavigatorName;
		protected bool inverseNavigatorIsList;
		protected bool eagerLoading;
		protected bool relDataTypeExportable;
		protected bool callGetterSetterEvents;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace, string ownInterface, string name, string implName, string eventName, string fkBackingName, string fkGuidBackingName, string referencedInterface, string referencedImplementation, string associationName, string targetRoleName, string positionPropertyName, string inverseNavigatorName, bool inverseNavigatorIsList, bool eagerLoading, bool relDataTypeExportable, bool callGetterSetterEvents)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ObjectReferencePropertyTemplate", ctx, serializationList, moduleNamespace, ownInterface, name, implName, eventName, fkBackingName, fkGuidBackingName, referencedInterface, referencedImplementation, associationName, targetRoleName, positionPropertyName, inverseNavigatorName, inverseNavigatorIsList, eagerLoading, relDataTypeExportable, callGetterSetterEvents);
        }

        public ObjectReferencePropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace, string ownInterface, string name, string implName, string eventName, string fkBackingName, string fkGuidBackingName, string referencedInterface, string referencedImplementation, string associationName, string targetRoleName, string positionPropertyName, string inverseNavigatorName, bool inverseNavigatorIsList, bool eagerLoading, bool relDataTypeExportable, bool callGetterSetterEvents)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.moduleNamespace = moduleNamespace;
			this.ownInterface = ownInterface;
			this.name = name;
			this.implName = implName;
			this.eventName = eventName;
			this.fkBackingName = fkBackingName;
			this.fkGuidBackingName = fkGuidBackingName;
			this.referencedInterface = referencedInterface;
			this.referencedImplementation = referencedImplementation;
			this.associationName = associationName;
			this.targetRoleName = targetRoleName;
			this.positionPropertyName = positionPropertyName;
			this.inverseNavigatorName = inverseNavigatorName;
			this.inverseNavigatorIsList = inverseNavigatorIsList;
			this.eagerLoading = eagerLoading;
			this.relDataTypeExportable = relDataTypeExportable;
			this.callGetterSetterEvents = callGetterSetterEvents;

        }

        public override void Generate()
        {
#line 31 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , " for ",  name , "\r\n");
this.WriteObjects("        // fkBackingName=",  fkBackingName , "; fkGuidBackingName=",  fkGuidBackingName , ";\r\n");
this.WriteObjects("        // referencedInterface=",  referencedInterface , "; moduleNamespace=",  moduleNamespace , ";\r\n");
this.WriteObjects("        // inverse Navigator=",  String.IsNullOrEmpty(inverseNavigatorName) ? "none" : inverseNavigatorName , "; ",  inverseNavigatorIsList ? "is list" : "is reference" , ";\r\n");
this.WriteObjects("        // PositionStorage=",  String.IsNullOrEmpty(positionPropertyName) ? "none" : positionPropertyName , ";\r\n");
this.WriteObjects("        // Target ",  relDataTypeExportable ? String.Empty : "not " , "exportable\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
this.WriteObjects("        public ",  referencedInterface , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get { return ",  implName , "; }\r\n");
this.WriteObjects("            set { ",  implName , " = (",  referencedImplementation , ")value; }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        private int? ",  fkBackingName , ";\r\n");
this.WriteObjects("\r\n");
#line 49 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (relDataTypeExportable) { 
#line 50 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        private Guid? ",  fkGuidBackingName , " = null;\r\n");
#line 51 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 52 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        // internal implementation, EF sees only this property\r\n");
this.WriteObjects("        [EdmRelationshipNavigationProperty(\"Model\", \"",  associationName , "\", \"",  targetRoleName , "\")]\r\n");
this.WriteObjects("        public ",  referencedImplementation , " ",  implName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                ",  referencedImplementation , " __value;\r\n");
this.WriteObjects("                EntityReference<",  referencedImplementation , "> r\r\n");
this.WriteObjects("                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<",  referencedImplementation , ">(\r\n");
this.WriteObjects("                        \"Model.",  associationName , "\",\r\n");
this.WriteObjects("                        \"",  targetRoleName , "\");\r\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)\r\n");
this.WriteObjects("                    && !r.IsLoaded)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    r.Load();\r\n");
this.WriteObjects("                    if (r.Value != null)\r\n");
this.WriteObjects("                        r.Value.AttachToContext(this.Context);\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                __value = r.Value;\r\n");
#line 73 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if(callGetterSetterEvents)
                {

#line 76 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if(",  eventName , "_Getter != null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var e = new PropertyGetterEventArgs<",  referencedInterface , ">(__value);\r\n");
this.WriteObjects("                    ",  eventName , "_Getter(this, e);\r\n");
this.WriteObjects("                    __value = (",  referencedImplementation , ")e.Result;\r\n");
this.WriteObjects("                }\r\n");
#line 83 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}

#line 85 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                return __value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();\r\n");
this.WriteObjects("                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                EntityReference<",  referencedImplementation , "> r\r\n");
this.WriteObjects("                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<",  referencedImplementation , ">(\r\n");
this.WriteObjects("                        \"Model.",  associationName , "\",\r\n");
this.WriteObjects("                        \"",  targetRoleName , "\");\r\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)\r\n");
this.WriteObjects("                    && !r.IsLoaded)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    r.Load();\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                ",  referencedInterface , " __oldValue = (",  referencedInterface , ")r.Value;\r\n");
this.WriteObjects("                ",  referencedInterface , " __newValue = (",  referencedInterface , ")value;\r\n");
this.WriteObjects("\r\n");
#line 105 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if(callGetterSetterEvents)
                {

#line 108 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if(",  eventName , "_PreSetter != null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var e = new PropertyPreSetterEventArgs<",  referencedInterface , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("                    ",  eventName , "_PreSetter(this, e);\r\n");
this.WriteObjects("                    __newValue = e.Result;\r\n");
this.WriteObjects("                }\r\n");
#line 115 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}

#line 117 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                r.Value = (",  referencedImplementation , ")__newValue;\r\n");
#line 119 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if(callGetterSetterEvents)
                {

#line 122 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if(",  eventName , "_PostSetter != null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var e = new PropertyPostSetterEventArgs<",  referencedInterface , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("                    ",  eventName , "_PostSetter(this, e);\r\n");
this.WriteObjects("                }\r\n");
#line 128 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}

#line 130 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
#line 134 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
AddSerialization(serializationList, name);

    if (!String.IsNullOrEmpty(positionPropertyName))
    {
        Properties.NotifyingValueProperty.Call(
            Host, ctx, serializationList,
            "int?", positionPropertyName, moduleNamespace);
    }

#line 143 "P:\Kistl\Kistl.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // END ",  this.GetType() , " for ",  name , "\r\n");

        }

    }
}