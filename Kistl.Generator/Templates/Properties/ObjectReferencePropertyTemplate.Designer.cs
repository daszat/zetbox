using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst")]
    public partial class ObjectReferencePropertyTemplate : Kistl.Generator.MemberTemplate
    {
		protected IKistlContext ctx;
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
		protected bool eagerLoading;
		protected bool relDataTypeExportable;
		protected bool callGetterSetterEvents;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Serialization.SerializationMembersList serializationList, string moduleNamespace, string ownInterface, string name, string implName, string eventName, string fkBackingName, string fkGuidBackingName, string referencedInterface, string referencedImplementation, string associationName, string targetRoleName, string positionPropertyName, string inverseNavigatorName, bool inverseNavigatorIsList, bool eagerLoading, bool relDataTypeExportable, bool callGetterSetterEvents)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ObjectReferencePropertyTemplate", ctx, serializationList, moduleNamespace, ownInterface, name, implName, eventName, fkBackingName, fkGuidBackingName, referencedInterface, referencedImplementation, associationName, targetRoleName, positionPropertyName, inverseNavigatorName, inverseNavigatorIsList, eagerLoading, relDataTypeExportable, callGetterSetterEvents);
        }

        public ObjectReferencePropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Serialization.SerializationMembersList serializationList, string moduleNamespace, string ownInterface, string name, string implName, string eventName, string fkBackingName, string fkGuidBackingName, string referencedInterface, string referencedImplementation, string associationName, string targetRoleName, string positionPropertyName, string inverseNavigatorName, bool inverseNavigatorIsList, bool eagerLoading, bool relDataTypeExportable, bool callGetterSetterEvents)
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
#line 31 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , " for ",  name , "\r\n");
this.WriteObjects("        // fkBackingName=",  fkBackingName , "; fkGuidBackingName=",  fkGuidBackingName , ";\r\n");
this.WriteObjects("        // referencedInterface=",  referencedInterface , "; moduleNamespace=",  moduleNamespace , ";\r\n");
this.WriteObjects("        // inverse Navigator=",  String.IsNullOrEmpty(inverseNavigatorName) ? "none" : inverseNavigatorName , "; ",  inverseNavigatorIsList ? "is list" : "is reference" , ";\r\n");
this.WriteObjects("        // PositionStorage=",  String.IsNullOrEmpty(positionPropertyName) ? "none" : positionPropertyName , ";\r\n");
this.WriteObjects("        // Target ",  relDataTypeExportable ? String.Empty : "not " , "exportable; does ",  callGetterSetterEvents ? String.Empty : "not " , "call events\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
#line 41 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
DelegatingProperty.Call(Host, ctx, name, referencedInterface, implName, referencedImplementation); 
#line 42 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        private int? ",  fkBackingName , ";\r\n");
this.WriteObjects("\r\n");
#line 45 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (relDataTypeExportable) { 
#line 46 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        private Guid? ",  fkGuidBackingName , " = null;\r\n");
#line 47 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 48 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        // internal implementation\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  referencedImplementation , " ",  implName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                ",  referencedImplementation , " __value;\r\n");
this.WriteObjects("                if (",  fkBackingName , ".HasValue)\r\n");
this.WriteObjects("                    __value = (",  referencedImplementation , ")Context.Find<",  referencedInterface , ">(",  fkBackingName , ".Value);\r\n");
this.WriteObjects("                else\r\n");
this.WriteObjects("                    __value = null;\r\n");
this.WriteObjects("\r\n");
#line 62 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if(callGetterSetterEvents)
                {

#line 65 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if(",  eventName , "_Getter != null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var e = new PropertyGetterEventArgs<",  referencedInterface , ">(__value);\r\n");
this.WriteObjects("                    ",  eventName , "_Getter(this, e);\r\n");
this.WriteObjects("                    __value = (",  referencedImplementation , ")e.Result;\r\n");
this.WriteObjects("                }\r\n");
#line 72 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}

#line 74 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("                return __value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();\r\n");
this.WriteObjects("                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                // shortcut noops\r\n");
this.WriteObjects("                if (value == null && ",  fkBackingName , " == null)\r\n");
this.WriteObjects("                    return;\r\n");
this.WriteObjects("                else if (value != null && value.ID == ",  fkBackingName , ")\r\n");
this.WriteObjects("                    return;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                // cache old value to remove inverse references later\r\n");
this.WriteObjects("                var __oldValue = ",  implName , ";\r\n");
this.WriteObjects("                var __newValue = value;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                // Changing Event fires before anything is touched\r\n");
this.WriteObjects("                NotifyPropertyChanging(\"",  name , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("\r\n");
#line 96 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if(callGetterSetterEvents)
                {

#line 99 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if(",  eventName , "_PreSetter != null && IsAttached)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var e = new PropertyPreSetterEventArgs<",  referencedInterface , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("                    ",  eventName , "_PreSetter(this, e);\r\n");
this.WriteObjects("                    __newValue = (",  referencedImplementation , ")e.Result;\r\n");
this.WriteObjects("                }\r\n");
#line 106 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}

#line 108 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("                // next, set the local reference\r\n");
this.WriteObjects("                ",  fkBackingName , " = __newValue == null ? (int?)null : __newValue.ID;\r\n");
this.WriteObjects("\r\n");
#line 113 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (!String.IsNullOrEmpty(inverseNavigatorName))
    {


#line 117 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                // now fixup redundant, inverse references\r\n");
this.WriteObjects("                // The inverse navigator will also fire events when changed, so should\r\n");
this.WriteObjects("                // only be touched after setting the local value above.\r\n");
this.WriteObjects("                // TODO: for complete correctness, the \"other\" Changing event should also fire\r\n");
this.WriteObjects("                //       before the local value is changed\r\n");
this.WriteObjects("                if (__oldValue != null)\r\n");
this.WriteObjects("                {\r\n");
#line 125 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (inverseNavigatorIsList)
        {
            // TODO: check whether __oldValue is loaded before potentially triggering a DB Call

#line 129 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    // remove from old list\r\n");
this.WriteObjects("                    (__oldValue.",  inverseNavigatorName , " as OneNRelationList<",  ownInterface , ">).RemoveWithoutClearParent(this);\r\n");
#line 132 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}
        else
        {

#line 136 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    // unset old reference\r\n");
this.WriteObjects("                    __oldValue.",  inverseNavigatorName , " = null;\r\n");
#line 139 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}

#line 141 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                if (__newValue != null)\r\n");
this.WriteObjects("                {\r\n");
#line 146 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (inverseNavigatorIsList)
        {

#line 149 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    // add to new list\r\n");
this.WriteObjects("                    (__newValue.",  inverseNavigatorName , " as OneNRelationList<",  ownInterface , ">).AddWithoutSetParent(this);\r\n");
#line 152 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}
        else
        {

#line 156 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    // set new reference\r\n");
this.WriteObjects("                    __newValue.",  inverseNavigatorName , " = this;\r\n");
#line 159 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}

#line 161 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                }\r\n");
#line 163 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}

#line 165 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                // everything is done. fire the Changed event\r\n");
this.WriteObjects("                NotifyPropertyChanged(\"",  name , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("\r\n");
#line 169 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if(callGetterSetterEvents)
                {

#line 172 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if(",  eventName , "_PostSetter != null && IsAttached)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var e = new PropertyPostSetterEventArgs<",  referencedInterface , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("                    ",  eventName , "_PostSetter(this, e);\r\n");
this.WriteObjects("                }\r\n");
#line 178 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
}

#line 180 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
#line 183 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
AddSerialization(serializationList, name, fkBackingName);

    if (!String.IsNullOrEmpty(positionPropertyName))
    {
        Properties.NotifyingValueProperty.Call(
            Host, ctx, serializationList,
            "int?", positionPropertyName, moduleNamespace);
    }

#line 192 "P:\Kistl\Kistl.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // END ",  this.GetType() , " for ",  name , "\r\n");

        }

    }
}