using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.Ef.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst")]
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
		protected string publicFKBackingName;
		protected string fkGuidBackingName;
		protected string referencedInterface;
		protected string referencedImplementation;
		protected string associationName;
		protected string targetRoleName;
		protected string positionPropertyName;
		protected string inverseNavigatorName;
		protected bool inverseNavigatorIsList;
		protected bool notifyInverseCollection;
		protected bool eagerLoading;
		protected bool relDataTypeExportable;
		protected bool callGetterSetterEvents;
		protected bool isCalculated;
		protected bool disableExport;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace, string ownInterface, string name, string implName, string eventName, string fkBackingName, string publicFKBackingName, string fkGuidBackingName, string referencedInterface, string referencedImplementation, string associationName, string targetRoleName, string positionPropertyName, string inverseNavigatorName, bool inverseNavigatorIsList, bool notifyInverseCollection, bool eagerLoading, bool relDataTypeExportable, bool callGetterSetterEvents, bool isCalculated, bool disableExport)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ObjectReferencePropertyTemplate", ctx, serializationList, moduleNamespace, ownInterface, name, implName, eventName, fkBackingName, publicFKBackingName, fkGuidBackingName, referencedInterface, referencedImplementation, associationName, targetRoleName, positionPropertyName, inverseNavigatorName, inverseNavigatorIsList, notifyInverseCollection, eagerLoading, relDataTypeExportable, callGetterSetterEvents, isCalculated, disableExport);
        }

        public ObjectReferencePropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace, string ownInterface, string name, string implName, string eventName, string fkBackingName, string publicFKBackingName, string fkGuidBackingName, string referencedInterface, string referencedImplementation, string associationName, string targetRoleName, string positionPropertyName, string inverseNavigatorName, bool inverseNavigatorIsList, bool notifyInverseCollection, bool eagerLoading, bool relDataTypeExportable, bool callGetterSetterEvents, bool isCalculated, bool disableExport)
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
			this.publicFKBackingName = publicFKBackingName;
			this.fkGuidBackingName = fkGuidBackingName;
			this.referencedInterface = referencedInterface;
			this.referencedImplementation = referencedImplementation;
			this.associationName = associationName;
			this.targetRoleName = targetRoleName;
			this.positionPropertyName = positionPropertyName;
			this.inverseNavigatorName = inverseNavigatorName;
			this.inverseNavigatorIsList = inverseNavigatorIsList;
			this.notifyInverseCollection = notifyInverseCollection;
			this.eagerLoading = eagerLoading;
			this.relDataTypeExportable = relDataTypeExportable;
			this.callGetterSetterEvents = callGetterSetterEvents;
			this.isCalculated = isCalculated;
			this.disableExport = disableExport;

        }

        public override void Generate()
        {
#line 51 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , " for ",  name , "\r\n");
this.WriteObjects("        // fkBackingName=",  fkBackingName , "; fkGuidBackingName=",  fkGuidBackingName , ";\r\n");
this.WriteObjects("        // referencedInterface=",  referencedInterface , "; moduleNamespace=",  moduleNamespace , ";\r\n");
#line 54 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (notifyInverseCollection && !String.IsNullOrEmpty(inverseNavigatorName)) { 
#line 55 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // will get inverse collection for notifications for ",  inverseNavigatorName , "\r\n");
#line 56 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} else if (!notifyInverseCollection && !String.IsNullOrEmpty(inverseNavigatorName)) { 
#line 57 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // inverse Navigator=",  String.IsNullOrEmpty(inverseNavigatorName) ? "none" : inverseNavigatorName , "; ",  inverseNavigatorIsList ? "is list" : "is reference" , ";\r\n");
#line 58 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} else { 
#line 59 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // no inverse navigator handling\r\n");
#line 60 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 61 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // PositionStorage=",  String.IsNullOrEmpty(positionPropertyName) ? "none" : positionPropertyName , ";\r\n");
this.WriteObjects("        // Target ",  relDataTypeExportable ? String.Empty : "not " , "exportable\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("		[System.Runtime.Serialization.IgnoreDataMember]\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
this.WriteObjects("        public ",  referencedInterface , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get { return ",  implName , "; }\r\n");
this.WriteObjects("            set { ",  implName , " = (",  referencedImplementation , ")value; }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        private int? ",  fkBackingName , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>ForeignKey Property for ",  UglyXmlEncode(name) , "'s id, used on APIs only</summary>\r\n");
this.WriteObjects("		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]\r\n");
this.WriteObjects("        public int? ",  publicFKBackingName , "\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			get { return ",  name , " != null ? ",  name , ".ID : (int?)null; }\r\n");
this.WriteObjects("			set { ",  fkBackingName , " = value; }\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
#line 84 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (relDataTypeExportable) { 
#line 85 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        private Guid? ",  fkGuidBackingName , " = null;\r\n");
#line 86 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 87 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
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
this.WriteObjects("                }\r\n");
this.WriteObjects("                __value = r.Value;\r\n");
#line 105 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (callGetterSetterEvents) { 
#line 106 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (",  eventName , "_Getter != null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var e = new PropertyGetterEventArgs<",  referencedInterface , ">(__value);\r\n");
this.WriteObjects("                    ",  eventName , "_Getter(this, e);\r\n");
this.WriteObjects("                    __value = (",  referencedImplementation , ")e.Result;\r\n");
this.WriteObjects("                }\r\n");
#line 112 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 113 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                return __value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (this.IsReadonly) throw new ReadOnlyObjectException();\r\n");
this.WriteObjects("                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();\r\n");
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
this.WriteObjects("                ",  referencedImplementation , " __oldValue = (",  referencedImplementation , ")r.Value;\r\n");
this.WriteObjects("                ",  referencedImplementation , " __newValue = (",  referencedImplementation , ")value;\r\n");
this.WriteObjects("\r\n");
#line 132 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (notifyInverseCollection && !String.IsNullOrEmpty(inverseNavigatorName)) { 
#line 133 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                // fetch collection proxy and attach change notifications\r\n");
this.WriteObjects("                if (__oldValue != null) __oldValue.Get",  inverseNavigatorName , "Collection();\r\n");
this.WriteObjects("                if (__newValue != null) __newValue.Get",  inverseNavigatorName , "Collection();\r\n");
this.WriteObjects("\r\n");
#line 137 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 138 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                // Changing Event fires before anything is touched\r\n");
this.WriteObjects("                NotifyPropertyChanging(\"",  name , "\", __oldValue, __newValue);\r\n");
#line 140 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (!notifyInverseCollection && !String.IsNullOrEmpty(inverseNavigatorName)) { 
#line 141 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (__oldValue != null) {\r\n");
this.WriteObjects("                    __oldValue.NotifyPropertyChanging(\"",  inverseNavigatorName , "\", null, null);\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                if (__newValue != null) {\r\n");
this.WriteObjects("                    __newValue.NotifyPropertyChanging(\"",  inverseNavigatorName , "\", null, null);\r\n");
this.WriteObjects("                }\r\n");
#line 147 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 148 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("\r\n");
#line 149 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (callGetterSetterEvents) { 
#line 150 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (",  eventName , "_PreSetter != null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var e = new PropertyPreSetterEventArgs<",  referencedInterface , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("                    ",  eventName , "_PreSetter(this, e);\r\n");
this.WriteObjects("                    __newValue = (",  referencedImplementation , ")e.Result;\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("\r\n");
#line 157 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 158 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                r.Value = (",  referencedImplementation , ")__newValue;\r\n");
this.WriteObjects("\r\n");
#line 160 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (callGetterSetterEvents) { 
#line 161 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (",  eventName , "_PostSetter != null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var e = new PropertyPostSetterEventArgs<",  referencedInterface , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("                    ",  eventName , "_PostSetter(this, e);\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("\r\n");
#line 167 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 168 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                // everything is done. fire the Changed event\r\n");
this.WriteObjects("                NotifyPropertyChanged(\"",  name , "\", __oldValue, __newValue);\r\n");
#line 170 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (!notifyInverseCollection && !String.IsNullOrEmpty(inverseNavigatorName)) { 
#line 171 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (__oldValue != null) {\r\n");
this.WriteObjects("                    __oldValue.NotifyPropertyChanged(\"",  inverseNavigatorName , "\", null, null);\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                if (__newValue != null) {\r\n");
this.WriteObjects("                    __newValue.NotifyPropertyChanged(\"",  inverseNavigatorName , "\", null, null);\r\n");
this.WriteObjects("                }\r\n");
#line 177 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 178 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if(IsAttached) UpdateChangedInfo = true;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public Zetbox.API.Async.ZbTask TriggerFetch",  name , "Async()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            return new Zetbox.API.Async.ZbTask<",  referencedInterface , ">(this.",  name , ");\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
#line 188 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
AddSerialization(serializationList, name, fkBackingName, fkGuidBackingName, referencedImplementation, associationName, targetRoleName);

    if (!String.IsNullOrEmpty(positionPropertyName))
    {
        Properties.NotifyingValueProperty.Call(
            Host, ctx, serializationList,
            "int?", positionPropertyName, moduleNamespace, false, disableExport);
    }

#line 197 "D:\Projects\zetbox.net4\Zetbox.DalProvider.EF.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // END ",  this.GetType() , " for ",  name , "\r\n");

        }

    }
}