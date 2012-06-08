using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.Ef.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst")]
    public partial class ObjectReferencePropertyTemplate : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList;
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
		protected bool isCalculated;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace, string ownInterface, string name, string implName, string eventName, string fkBackingName, string fkGuidBackingName, string referencedInterface, string referencedImplementation, string associationName, string targetRoleName, string positionPropertyName, string inverseNavigatorName, bool inverseNavigatorIsList, bool eagerLoading, bool relDataTypeExportable, bool callGetterSetterEvents, bool isCalculated)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ObjectReferencePropertyTemplate", ctx, serializationList, moduleNamespace, ownInterface, name, implName, eventName, fkBackingName, fkGuidBackingName, referencedInterface, referencedImplementation, associationName, targetRoleName, positionPropertyName, inverseNavigatorName, inverseNavigatorIsList, eagerLoading, relDataTypeExportable, callGetterSetterEvents, isCalculated);
        }

        public ObjectReferencePropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace, string ownInterface, string name, string implName, string eventName, string fkBackingName, string fkGuidBackingName, string referencedInterface, string referencedImplementation, string associationName, string targetRoleName, string positionPropertyName, string inverseNavigatorName, bool inverseNavigatorIsList, bool eagerLoading, bool relDataTypeExportable, bool callGetterSetterEvents, bool isCalculated)
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
			this.isCalculated = isCalculated;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("");
#line 48 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , " for ",  name , "\n");
this.WriteObjects("        // fkBackingName=",  fkBackingName , "; fkGuidBackingName=",  fkGuidBackingName , ";\n");
this.WriteObjects("        // referencedInterface=",  referencedInterface , "; moduleNamespace=",  moduleNamespace , ";\n");
this.WriteObjects("        // inverse Navigator=",  String.IsNullOrEmpty(inverseNavigatorName) ? "none" : inverseNavigatorName , "; ",  inverseNavigatorIsList ? "is list" : "is reference" , ";\n");
this.WriteObjects("        // PositionStorage=",  String.IsNullOrEmpty(positionPropertyName) ? "none" : positionPropertyName , ";\n");
this.WriteObjects("        // Target ",  relDataTypeExportable ? String.Empty : "not " , "exportable\n");
this.WriteObjects("\n");
this.WriteObjects("        // implement the user-visible interface\n");
this.WriteObjects("        [XmlIgnore()]\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\n");
this.WriteObjects("        public ",  referencedInterface , " ",  name , "\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get { return ",  implName , "; }\n");
this.WriteObjects("            set { ",  implName , " = (",  referencedImplementation , ")value; }\n");
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("        private int? ",  fkBackingName , ";\n");
this.WriteObjects("\n");
#line 66 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (relDataTypeExportable) { 
#line 67 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        private Guid? ",  fkGuidBackingName , " = null;\n");
#line 68 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 69 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("\n");
this.WriteObjects("        // internal implementation, EF sees only this property\n");
this.WriteObjects("        [EdmRelationshipNavigationProperty(\"Model\", \"",  associationName , "\", \"",  targetRoleName , "\")]\n");
this.WriteObjects("        public ",  referencedImplementation , " ",  implName , "\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get\n");
this.WriteObjects("            {\n");
this.WriteObjects("                if (!CurrentAccessRights.HasReadRights()) return null;\n");
this.WriteObjects("                ",  referencedImplementation , " __value;\n");
this.WriteObjects("                EntityReference<",  referencedImplementation , "> r\n");
this.WriteObjects("                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<",  referencedImplementation , ">(\n");
this.WriteObjects("                        \"Model.",  associationName , "\",\n");
this.WriteObjects("                        \"",  targetRoleName , "\");\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)\n");
this.WriteObjects("                    && !r.IsLoaded)\n");
this.WriteObjects("                {\n");
this.WriteObjects("                    r.Load();\n");
this.WriteObjects("                }\n");
this.WriteObjects("                if (r.Value != null) r.Value.AttachToContext(this.Context);\n");
this.WriteObjects("                __value = r.Value;\n");
#line 89 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (callGetterSetterEvents) { 
#line 90 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (",  eventName , "_Getter != null)\n");
this.WriteObjects("                {\n");
this.WriteObjects("                    var e = new PropertyGetterEventArgs<",  referencedInterface , ">(__value);\n");
this.WriteObjects("                    ",  eventName , "_Getter(this, e);\n");
this.WriteObjects("                    __value = (",  referencedImplementation , ")e.Result;\n");
this.WriteObjects("                }\n");
#line 96 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 97 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                return __value;\n");
this.WriteObjects("            }\n");
this.WriteObjects("            set\n");
this.WriteObjects("            {\n");
this.WriteObjects("                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();\n");
this.WriteObjects("                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();\n");
this.WriteObjects("\n");
this.WriteObjects("                EntityReference<",  referencedImplementation , "> r\n");
this.WriteObjects("                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<",  referencedImplementation , ">(\n");
this.WriteObjects("                        \"Model.",  associationName , "\",\n");
this.WriteObjects("                        \"",  targetRoleName , "\");\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)\n");
this.WriteObjects("                    && !r.IsLoaded)\n");
this.WriteObjects("                {\n");
this.WriteObjects("                    r.Load();\n");
this.WriteObjects("                }\n");
this.WriteObjects("                ",  referencedImplementation , " __oldValue = (",  referencedImplementation , ")r.Value;\n");
this.WriteObjects("                ",  referencedImplementation , " __newValue = (",  referencedImplementation , ")value;\n");
this.WriteObjects("\n");
this.WriteObjects("                // Changing Event fires before anything is touched\n");
this.WriteObjects("                NotifyPropertyChanging(\"",  name , "\", __oldValue, __newValue);\n");
#line 118 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (!String.IsNullOrEmpty(inverseNavigatorName)) { 
#line 119 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (__oldValue != null) {\n");
this.WriteObjects("                    __oldValue.NotifyPropertyChanging(\"",  inverseNavigatorName , "\", null, null);\n");
this.WriteObjects("                }\n");
this.WriteObjects("                if (__newValue != null) {\n");
this.WriteObjects("                    __newValue.NotifyPropertyChanging(\"",  inverseNavigatorName , "\", null, null);\n");
this.WriteObjects("                }\n");
#line 125 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 126 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("\n");
#line 127 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (callGetterSetterEvents) { 
#line 128 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (",  eventName , "_PreSetter != null)\n");
this.WriteObjects("                {\n");
this.WriteObjects("                    var e = new PropertyPreSetterEventArgs<",  referencedInterface , ">(__oldValue, __newValue);\n");
this.WriteObjects("                    ",  eventName , "_PreSetter(this, e);\n");
this.WriteObjects("                    __newValue = (",  referencedImplementation , ")e.Result;\n");
this.WriteObjects("                }\n");
this.WriteObjects("\n");
#line 135 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 136 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                r.Value = (",  referencedImplementation , ")__newValue;\n");
this.WriteObjects("\n");
#line 138 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (callGetterSetterEvents) { 
#line 139 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (",  eventName , "_PostSetter != null)\n");
this.WriteObjects("                {\n");
this.WriteObjects("                    var e = new PropertyPostSetterEventArgs<",  referencedInterface , ">(__oldValue, __newValue);\n");
this.WriteObjects("                    ",  eventName , "_PostSetter(this, e);\n");
this.WriteObjects("                }\n");
this.WriteObjects("\n");
#line 145 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 146 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                // everything is done. fire the Changed event\n");
this.WriteObjects("                NotifyPropertyChanged(\"",  name , "\", __oldValue, __newValue);\n");
#line 148 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (!String.IsNullOrEmpty(inverseNavigatorName)) { 
#line 149 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (__oldValue != null) {\n");
this.WriteObjects("                    __oldValue.NotifyPropertyChanged(\"",  inverseNavigatorName , "\", null, null);\n");
this.WriteObjects("                }\n");
this.WriteObjects("                if (__newValue != null) {\n");
this.WriteObjects("                    __newValue.NotifyPropertyChanged(\"",  inverseNavigatorName , "\", null, null);\n");
this.WriteObjects("                }\n");
#line 155 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 156 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
this.WriteObjects("\n");
#line 160 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
AddSerialization(serializationList, name, fkBackingName, fkGuidBackingName, referencedImplementation, associationName, targetRoleName);

    if (!String.IsNullOrEmpty(positionPropertyName))
    {
        Properties.NotifyingValueProperty.Call(
            Host, ctx, serializationList,
            "int?", positionPropertyName, moduleNamespace, false);
    }

#line 169 "P:\zetbox\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // END ",  this.GetType() , " for ",  name , "\n");

        }

    }
}