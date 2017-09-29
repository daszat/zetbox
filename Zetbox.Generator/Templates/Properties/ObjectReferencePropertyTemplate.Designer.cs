using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst")]
    public partial class ObjectReferencePropertyTemplate : Zetbox.Generator.MemberTemplate
    {
		protected IZetboxContext ctx;
		protected Serialization.SerializationMembersList serializationList;
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
		protected bool notifyInverseCollection;
		protected bool eagerLoading;
		protected bool relDataTypeExportable;
		protected bool callGetterSetterEvents;
		protected bool isCalculated;
		protected bool disableExport;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Serialization.SerializationMembersList serializationList, string moduleNamespace, string ownInterface, string name, string implName, string eventName, string fkBackingName, string fkGuidBackingName, string referencedInterface, string referencedImplementation, string associationName, string targetRoleName, string positionPropertyName, string inverseNavigatorName, bool inverseNavigatorIsList, bool notifyInverseCollection, bool eagerLoading, bool relDataTypeExportable, bool callGetterSetterEvents, bool isCalculated, bool disableExport)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ObjectReferencePropertyTemplate", ctx, serializationList, moduleNamespace, ownInterface, name, implName, eventName, fkBackingName, fkGuidBackingName, referencedInterface, referencedImplementation, associationName, targetRoleName, positionPropertyName, inverseNavigatorName, inverseNavigatorIsList, notifyInverseCollection, eagerLoading, relDataTypeExportable, callGetterSetterEvents, isCalculated, disableExport);
        }

        public ObjectReferencePropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Serialization.SerializationMembersList serializationList, string moduleNamespace, string ownInterface, string name, string implName, string eventName, string fkBackingName, string fkGuidBackingName, string referencedInterface, string referencedImplementation, string associationName, string targetRoleName, string positionPropertyName, string inverseNavigatorName, bool inverseNavigatorIsList, bool notifyInverseCollection, bool eagerLoading, bool relDataTypeExportable, bool callGetterSetterEvents, bool isCalculated, bool disableExport)
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
			this.notifyInverseCollection = notifyInverseCollection;
			this.eagerLoading = eagerLoading;
			this.relDataTypeExportable = relDataTypeExportable;
			this.callGetterSetterEvents = callGetterSetterEvents;
			this.isCalculated = isCalculated;
			this.disableExport = disableExport;

        }

        public override void Generate()
        {
#line 51 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
string taskName = "_triggerFetch" + name + "Task";

#line 53 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , " for ",  name , "\r\n");
this.WriteObjects("        // fkBackingName=",  fkBackingName , "; fkGuidBackingName=",  fkGuidBackingName , ";\r\n");
this.WriteObjects("        // referencedInterface=",  referencedInterface , "; moduleNamespace=",  moduleNamespace , ";\r\n");
#line 56 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (notifyInverseCollection && !String.IsNullOrEmpty(inverseNavigatorName)) { 
#line 57 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // will get inverse collection for notifications for ",  inverseNavigatorName , "\r\n");
#line 58 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} else if (!notifyInverseCollection && !String.IsNullOrEmpty(inverseNavigatorName)) { 
#line 59 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // inverse Navigator=",  String.IsNullOrEmpty(inverseNavigatorName) ? "none" : inverseNavigatorName , "; ",  inverseNavigatorIsList ? "is list" : "is reference" , ";\r\n");
#line 60 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} else { 
#line 61 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // no inverse navigator handling\r\n");
#line 62 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 63 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // PositionStorage=",  String.IsNullOrEmpty(positionPropertyName) ? "none" : positionPropertyName , ";\r\n");
this.WriteObjects("        // Target ",  relDataTypeExportable ? String.Empty : "not " , "exportable; does ",  callGetterSetterEvents ? String.Empty : "not " , "call events\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
#line 69 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
DelegatingProperty.Call(Host, ctx, name, referencedInterface, implName, referencedImplementation); 
#line 70 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        private int? _",  fkBackingName , "Cache;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        private int? ",  fkBackingName , " {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                return _",  fkBackingName , "Cache;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                _",  fkBackingName , "Cache = value;\r\n");
this.WriteObjects("                // Recreate task to clear it's cache\r\n");
this.WriteObjects("                ",  taskName , " = null;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
#line 86 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (relDataTypeExportable) { 
#line 87 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        private Guid? ",  fkGuidBackingName , " = null;\r\n");
#line 88 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 89 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        Zetbox.API.Async.ZbTask<",  referencedInterface , "> ",  taskName , ";\r\n");
this.WriteObjects("        public Zetbox.API.Async.ZbTask<",  referencedInterface , "> TriggerFetch",  name , "Async()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            if (",  taskName , " != null) return ",  taskName , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            if (",  fkBackingName , ".HasValue)\r\n");
this.WriteObjects("                ",  taskName , " = Context.FindAsync<",  referencedInterface , ">(",  fkBackingName , ".Value);\r\n");
this.WriteObjects("            else\r\n");
this.WriteObjects("                ",  taskName , " = new Zetbox.API.Async.ZbTask<",  referencedInterface , ">(Zetbox.API.Async.ZbTask.Synchron, () => null);\r\n");
this.WriteObjects("\r\n");
#line 100 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (callGetterSetterEvents) {                                                                    
#line 101 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("            ",  taskName , ".OnResult(t =>\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (",  eventName , "_Getter != null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var e = new PropertyGetterEventArgs<",  referencedInterface , ">(t.Result);\r\n");
this.WriteObjects("                    ",  eventName , "_Getter(this, e);\r\n");
this.WriteObjects("                    t.Result = e.Result;\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("            });\r\n");
this.WriteObjects("\r\n");
#line 111 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}                                                                                                
#line 112 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("            return ",  taskName , ";\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        // internal implementation\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  referencedImplementation , " ",  implName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                return (",  referencedImplementation , ")TriggerFetch",  name , "Async().Result;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (this.IsReadonly) throw new ReadOnlyObjectException();\r\n");
this.WriteObjects("                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                // shortcut noops\r\n");
this.WriteObjects("                if ((value == null && ",  fkBackingName , " == null) || (value != null && value.ID == ",  fkBackingName , "))\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    SetInitializedProperty(\"",  name , "\");\r\n");
this.WriteObjects("                    return;\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                // cache old value to remove inverse references later\r\n");
this.WriteObjects("                var __oldValue = ",  implName , ";\r\n");
this.WriteObjects("                var __newValue = value;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                // Changing Event fires before anything is touched\r\n");
this.WriteObjects("                NotifyPropertyChanging(\"",  name , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("\r\n");
#line 142 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (notifyInverseCollection && !String.IsNullOrEmpty(inverseNavigatorName)) {                    
#line 143 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (__oldValue != null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    __oldValue.NotifyPropertyChanging(\"",  inverseNavigatorName , "\", null, null);\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                if (__newValue != null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    __newValue.NotifyPropertyChanging(\"",  inverseNavigatorName , "\", null, null);\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("\r\n");
#line 153 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}                                                                                                
#line 154 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (callGetterSetterEvents) {                                                                    
#line 155 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (",  eventName , "_PreSetter != null && IsAttached)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var e = new PropertyPreSetterEventArgs<",  referencedInterface , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("                    ",  eventName , "_PreSetter(this, e);\r\n");
this.WriteObjects("                    __newValue = (",  referencedImplementation , ")e.Result;\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("\r\n");
#line 162 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}                                                                                                
#line 163 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                // next, set the local reference\r\n");
this.WriteObjects("                ",  fkBackingName , " = __newValue == null ? (int?)null : __newValue.ID;\r\n");
this.WriteObjects("\r\n");
#line 166 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (notifyInverseCollection && !String.IsNullOrEmpty(inverseNavigatorName)) {                    
#line 167 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (__oldValue != null)\r\n");
this.WriteObjects("                    __oldValue.On",  inverseNavigatorName , "CollectionChanged();\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                if (__newValue != null)\r\n");
this.WriteObjects("                    __newValue.On",  inverseNavigatorName , "CollectionChanged();\r\n");
this.WriteObjects("\r\n");
#line 173 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}                                                                                                
#line 174 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (!notifyInverseCollection && !String.IsNullOrEmpty(inverseNavigatorName)) {                   
#line 175 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                // now fixup redundant, inverse references\r\n");
this.WriteObjects("                // The inverse navigator will also fire events when changed, so should\r\n");
this.WriteObjects("                // only be touched after setting the local value above.\r\n");
this.WriteObjects("                // TODO: for complete correctness, the \"other\" Changing event should also fire\r\n");
this.WriteObjects("                //       before the local value is changed\r\n");
this.WriteObjects("                if (__oldValue != null)\r\n");
this.WriteObjects("                {\r\n");
#line 182 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (inverseNavigatorIsList) {                                                               
#line 183 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
// TODO: check whether __oldValue is loaded before potentially triggering a DB Call     
#line 184 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    // remove from old list\r\n");
this.WriteObjects("                    (__oldValue.",  inverseNavigatorName , " as IRelationListSync<",  ownInterface , ">).RemoveWithoutClearParent(this);\r\n");
#line 186 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} else {                                                                                    
#line 187 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    // unset old reference\r\n");
this.WriteObjects("                    __oldValue.",  inverseNavigatorName , " = null;\r\n");
#line 189 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}                                                                                           
#line 190 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                if (__newValue != null)\r\n");
this.WriteObjects("                {\r\n");
#line 194 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (inverseNavigatorIsList) {                                                               
#line 195 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    // add to new list\r\n");
this.WriteObjects("                    (__newValue.",  inverseNavigatorName , " as IRelationListSync<",  ownInterface , ">).AddWithoutSetParent(this);\r\n");
#line 197 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} else {                                                                                    
#line 198 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    // set new reference\r\n");
this.WriteObjects("                    __newValue.",  inverseNavigatorName , " = this;\r\n");
#line 200 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}                                                                                           
#line 201 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                }\r\n");
#line 202 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}                                                                                                
#line 203 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                // everything is done. fire the Changed event\r\n");
this.WriteObjects("                NotifyPropertyChanged(\"",  name , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("                if(IsAttached) UpdateChangedInfo = true;\r\n");
#line 206 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (callGetterSetterEvents) {                                                                    
#line 207 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("                if (",  eventName , "_PostSetter != null && IsAttached)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var e = new PropertyPostSetterEventArgs<",  referencedInterface , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("                    ",  eventName , "_PostSetter(this, e);\r\n");
this.WriteObjects("                }\r\n");
#line 213 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}                                                                                                
#line 214 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
#line 217 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
AddSerialization(serializationList, name, fkBackingName, fkGuidBackingName);

    if (!String.IsNullOrEmpty(positionPropertyName))
    {
        Properties.NotifyingValueProperty.Call(
            Host, ctx, serializationList,
            "int?", positionPropertyName, moduleNamespace, false, disableExport);
    }

#line 226 "C:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // END ",  this.GetType() , " for ",  name , "\r\n");

        }

    }
}