using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst")]
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


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Serialization.SerializationMembersList serializationList, string moduleNamespace, string ownInterface, string name, string implName, string eventName, string fkBackingName, string publicFKBackingName, string fkGuidBackingName, string referencedInterface, string referencedImplementation, string associationName, string targetRoleName, string positionPropertyName, string inverseNavigatorName, bool inverseNavigatorIsList, bool notifyInverseCollection, bool eagerLoading, bool relDataTypeExportable, bool callGetterSetterEvents, bool isCalculated, bool disableExport)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ObjectReferencePropertyTemplate", ctx, serializationList, moduleNamespace, ownInterface, name, implName, eventName, fkBackingName, publicFKBackingName, fkGuidBackingName, referencedInterface, referencedImplementation, associationName, targetRoleName, positionPropertyName, inverseNavigatorName, inverseNavigatorIsList, notifyInverseCollection, eagerLoading, relDataTypeExportable, callGetterSetterEvents, isCalculated, disableExport);
        }

        public ObjectReferencePropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Serialization.SerializationMembersList serializationList, string moduleNamespace, string ownInterface, string name, string implName, string eventName, string fkBackingName, string publicFKBackingName, string fkGuidBackingName, string referencedInterface, string referencedImplementation, string associationName, string targetRoleName, string positionPropertyName, string inverseNavigatorName, bool inverseNavigatorIsList, bool notifyInverseCollection, bool eagerLoading, bool relDataTypeExportable, bool callGetterSetterEvents, bool isCalculated, bool disableExport)
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
#line 52 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
string taskName = "_triggerFetch" + name + "Task";

#line 54 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , " for ",  name , "\r\n");
this.WriteObjects("        // fkBackingName=",  fkBackingName , "; fkGuidBackingName=",  fkGuidBackingName , ";\r\n");
this.WriteObjects("        // referencedInterface=",  referencedInterface , "; moduleNamespace=",  moduleNamespace , ";\r\n");
#line 57 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (notifyInverseCollection && !String.IsNullOrEmpty(inverseNavigatorName)) { 
#line 58 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // will get inverse collection for notifications for ",  inverseNavigatorName , "\r\n");
#line 59 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} else if (!notifyInverseCollection && !String.IsNullOrEmpty(inverseNavigatorName)) { 
#line 60 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // inverse Navigator=",  String.IsNullOrEmpty(inverseNavigatorName) ? "none" : inverseNavigatorName , "; ",  inverseNavigatorIsList ? "is list" : "is reference" , ";\r\n");
#line 61 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} else { 
#line 62 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // no inverse navigator handling\r\n");
#line 63 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 64 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // PositionStorage=",  String.IsNullOrEmpty(positionPropertyName) ? "none" : positionPropertyName , ";\r\n");
this.WriteObjects("        // Target ",  relDataTypeExportable ? String.Empty : "not " , "exportable; does ",  callGetterSetterEvents ? String.Empty : "not " , "call events\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("		[System.Runtime.Serialization.IgnoreDataMember]\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
#line 71 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
DelegatingProperty.Call(Host, ctx, name, referencedInterface, implName, referencedImplementation); 
#line 72 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public System.Threading.Tasks.Task<",  referencedInterface , "> GetProp_",  name , "()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            return TriggerFetch",  name , "Async();\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public async System.Threading.Tasks.Task SetProp_",  name , "(",  referencedInterface , " newValue)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            await TriggerFetch",  name , "Async();\r\n");
this.WriteObjects("            ",  implName , " = (",  referencedImplementation , ")newValue;\r\n");
this.WriteObjects("        }\r\n");
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
this.WriteObjects("        /// <summary>ForeignKey Property for ",  UglyXmlEncode(name) , "'s id, used on APIs only</summary>\r\n");
this.WriteObjects("		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]\r\n");
this.WriteObjects("        public int? ",  publicFKBackingName , "\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			get { return ",  fkBackingName , "; }\r\n");
this.WriteObjects("			set { ",  fkBackingName , " = value; }\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
#line 107 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (relDataTypeExportable) { 
#line 108 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        private Guid? ",  fkGuidBackingName , " = null;\r\n");
#line 109 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 110 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        System.Threading.Tasks.Task<",  referencedInterface , "> ",  taskName , ";\r\n");
this.WriteObjects("        public System.Threading.Tasks.Task<",  referencedInterface , "> TriggerFetch",  name , "Async()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            if (",  taskName , " != null) return ",  taskName , ";\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            System.Threading.Tasks.Task<",  referencedInterface , "> task;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            if (",  fkBackingName , ".HasValue)\r\n");
this.WriteObjects("                task = Context.FindAsync<",  referencedInterface , ">(",  fkBackingName , ".Value);\r\n");
this.WriteObjects("            else\r\n");
this.WriteObjects("                task = System.Threading.Tasks.Task.FromResult<",  referencedInterface , ">(null);\r\n");
this.WriteObjects("\r\n");
#line 123 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (callGetterSetterEvents) {                                                                    
#line 124 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("            task.OnResult(t =>\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (",  eventName , "_Getter != null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var e = new PropertyGetterEventArgs<",  referencedInterface , ">(t.Result);\r\n");
this.WriteObjects("                    ",  eventName , "_Getter(this, e);\r\n");
this.WriteObjects("                    // TODO: t.Result = e.Result;\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("            });\r\n");
this.WriteObjects("\r\n");
#line 134 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}                                                                                                
#line 135 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("            return ",  taskName , " = task;\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        // internal implementation\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  referencedImplementation , " ",  implName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                var task = TriggerFetch",  name , "Async();\r\n");
this.WriteObjects("                task.TryRunSynchronously();\r\n");
this.WriteObjects("                return (",  referencedImplementation , ")task.Result;\r\n");
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
#line 167 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (notifyInverseCollection && !String.IsNullOrEmpty(inverseNavigatorName)) {                    
#line 168 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
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
#line 178 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}                                                                                                
#line 179 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (callGetterSetterEvents) {                                                                    
#line 180 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (",  eventName , "_PreSetter != null && IsAttached)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var e = new PropertyPreSetterEventArgs<",  referencedInterface , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("                    ",  eventName , "_PreSetter(this, e);\r\n");
this.WriteObjects("                    __newValue = (",  referencedImplementation , ")e.Result;\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("\r\n");
#line 187 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}                                                                                                
#line 188 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                // next, set the local reference\r\n");
this.WriteObjects("                ",  fkBackingName , " = __newValue == null ? (int?)null : __newValue.ID;\r\n");
this.WriteObjects("\r\n");
#line 191 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (notifyInverseCollection && !String.IsNullOrEmpty(inverseNavigatorName)) {                    
#line 192 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (__oldValue != null)\r\n");
this.WriteObjects("                    __oldValue.On",  inverseNavigatorName , "CollectionChanged();\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                if (__newValue != null)\r\n");
this.WriteObjects("                    __newValue.On",  inverseNavigatorName , "CollectionChanged();\r\n");
this.WriteObjects("\r\n");
#line 198 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}                                                                                                
#line 199 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (!notifyInverseCollection && !String.IsNullOrEmpty(inverseNavigatorName)) {                   
#line 200 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                // now fixup redundant, inverse references\r\n");
this.WriteObjects("                // The inverse navigator will also fire events when changed, so should\r\n");
this.WriteObjects("                // only be touched after setting the local value above.\r\n");
this.WriteObjects("                // TODO: for complete correctness, the \"other\" Changing event should also fire\r\n");
this.WriteObjects("                //       before the local value is changed\r\n");
this.WriteObjects("                if (__oldValue != null)\r\n");
this.WriteObjects("                {\r\n");
#line 207 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (inverseNavigatorIsList) {                                                               
#line 208 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
// TODO: check whether __oldValue is loaded before potentially triggering a DB Call     
#line 209 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    // remove from old list\r\n");
this.WriteObjects("                    (__oldValue.",  inverseNavigatorName , " as IRelationListSync<",  ownInterface , ">).RemoveWithoutClearParent(this);\r\n");
#line 211 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} else {                                                                                    
#line 212 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    // unset old reference\r\n");
this.WriteObjects("                    __oldValue.",  inverseNavigatorName , " = null;\r\n");
#line 214 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}                                                                                           
#line 215 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                if (__newValue != null)\r\n");
this.WriteObjects("                {\r\n");
#line 219 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (inverseNavigatorIsList) {                                                               
#line 220 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    // add to new list\r\n");
this.WriteObjects("                    (__newValue.",  inverseNavigatorName , " as IRelationListSync<",  ownInterface , ">).AddWithoutSetParent(this);\r\n");
#line 222 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} else {                                                                                    
#line 223 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    // set new reference\r\n");
this.WriteObjects("                    __newValue.",  inverseNavigatorName , " = this;\r\n");
#line 225 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}                                                                                           
#line 226 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                }\r\n");
#line 227 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}                                                                                                
#line 228 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                // everything is done. fire the Changed event\r\n");
this.WriteObjects("                NotifyPropertyChanged(\"",  name , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("                if(IsAttached) UpdateChangedInfo = true;\r\n");
#line 231 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (callGetterSetterEvents) {                                                                    
#line 232 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("                if (",  eventName , "_PostSetter != null && IsAttached)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var e = new PropertyPostSetterEventArgs<",  referencedInterface , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("                    ",  eventName , "_PostSetter(this, e);\r\n");
this.WriteObjects("                }\r\n");
#line 238 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}                                                                                                
#line 239 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
#line 242 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
AddSerialization(serializationList, name, fkBackingName, fkGuidBackingName);

    if (!String.IsNullOrEmpty(positionPropertyName))
    {
        Properties.NotifyingValueProperty.Call(
            Host, ctx, serializationList,
            "int?", positionPropertyName, moduleNamespace, false, disableExport);
    }

#line 251 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // END ",  this.GetType() , " for ",  name , "\r\n");

        }

    }
}