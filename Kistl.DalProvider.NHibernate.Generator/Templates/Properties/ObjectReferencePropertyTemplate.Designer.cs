using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.NHibernate.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst")]
    public partial class ObjectReferencePropertyTemplate : Kistl.Generator.MemberTemplate
    {
		protected IKistlContext ctx;
		protected Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList;
		protected string moduleNamespace;
		protected string ownInterface;
		protected string name;
		protected string implNameUnused;
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


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace, string ownInterface, string name, string implNameUnused, string eventName, string fkBackingName, string fkGuidBackingName, string referencedInterface, string referencedImplementation, string associationName, string targetRoleName, string positionPropertyName, string inverseNavigatorName, bool inverseNavigatorIsList, bool eagerLoading, bool relDataTypeExportable, bool callGetterSetterEvents)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ObjectReferencePropertyTemplate", ctx, serializationList, moduleNamespace, ownInterface, name, implNameUnused, eventName, fkBackingName, fkGuidBackingName, referencedInterface, referencedImplementation, associationName, targetRoleName, positionPropertyName, inverseNavigatorName, inverseNavigatorIsList, eagerLoading, relDataTypeExportable, callGetterSetterEvents);
        }

        public ObjectReferencePropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace, string ownInterface, string name, string implNameUnused, string eventName, string fkBackingName, string fkGuidBackingName, string referencedInterface, string referencedImplementation, string associationName, string targetRoleName, string positionPropertyName, string inverseNavigatorName, bool inverseNavigatorIsList, bool eagerLoading, bool relDataTypeExportable, bool callGetterSetterEvents)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.moduleNamespace = moduleNamespace;
			this.ownInterface = ownInterface;
			this.name = name;
			this.implNameUnused = implNameUnused;
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
#line 31 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , " for ",  name , "\r\n");
this.WriteObjects("        // fkBackingName=this.Proxy.",  name , "; fkGuidBackingName=",  fkGuidBackingName , ";\r\n");
this.WriteObjects("        // referencedInterface=",  referencedInterface , "; moduleNamespace=",  moduleNamespace , ";\r\n");
this.WriteObjects("        // inverse Navigator=",  String.IsNullOrEmpty(inverseNavigatorName) ? "none" : inverseNavigatorName , "; ",  inverseNavigatorIsList ? "is list" : "is reference" , ";\r\n");
this.WriteObjects("        // PositionStorage=",  String.IsNullOrEmpty(positionPropertyName) ? "none" : positionPropertyName , ";\r\n");
this.WriteObjects("        // Target ",  relDataTypeExportable ? String.Empty : "not " , "exportable; does ",  callGetterSetterEvents ? String.Empty : "not " , "call events\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  referencedInterface , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                ",  referencedImplementation , " __value = (",  referencedImplementation , ")OurContext.AttachAndWrap(this.Proxy.",  name , ");\r\n");
this.WriteObjects("\r\n");
#line 45 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (callGetterSetterEvents) { 
#line 46 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (",  eventName , "_Getter != null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var e = new PropertyGetterEventArgs<",  referencedInterface , ">(__value);\r\n");
this.WriteObjects("                    ",  eventName , "_Getter(this, e);\r\n");
this.WriteObjects("                    __value = (",  referencedImplementation , ")e.Result;\r\n");
this.WriteObjects("                }\r\n");
#line 52 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 53 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("                return __value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();\r\n");
this.WriteObjects("                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                // shortcut noop with nulls\r\n");
this.WriteObjects("                if (value == null && this.Proxy.",  name , " == null)\r\n");
this.WriteObjects("                    return;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                // cache old value to remove inverse references later\r\n");
this.WriteObjects("                var __oldValue = (",  referencedImplementation , ")OurContext.AttachAndWrap(this.Proxy.",  name , ");\r\n");
this.WriteObjects("                var __newValue = (",  referencedImplementation , ")value;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                // shortcut noop on objects\r\n");
this.WriteObjects("                // can't use proxy's ID here, since that might be INVALIDID before persisting the first time.\r\n");
this.WriteObjects("                if (__oldValue == __newValue)\r\n");
this.WriteObjects("                    return;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                // Changing Event fires before anything is touched\r\n");
this.WriteObjects("                NotifyPropertyChanging(\"",  name , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("\r\n");
#line 77 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (callGetterSetterEvents) { 
#line 78 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (",  eventName , "_PreSetter != null && IsAttached)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var e = new PropertyPreSetterEventArgs<",  referencedInterface , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("                    ",  eventName , "_PreSetter(this, e);\r\n");
this.WriteObjects("                    __newValue = (",  referencedImplementation , ")e.Result;\r\n");
this.WriteObjects("                }\r\n");
#line 84 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 85 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("                // next, set the local reference\r\n");
this.WriteObjects("                if (__newValue == null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    this.Proxy.",  name , " = null;\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                else\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    this.Proxy.",  name , " = __newValue.Proxy;\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("\r\n");
#line 96 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (!String.IsNullOrEmpty(inverseNavigatorName)) { 
#line 97 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                // now fixup redundant, inverse references\r\n");
this.WriteObjects("                // The inverse navigator will also fire events when changed, so should\r\n");
this.WriteObjects("                // only be touched after setting the local value above.\r\n");
this.WriteObjects("                // TODO: for complete correctness, the \"other\" Changing event should also fire\r\n");
this.WriteObjects("                //       before the local value is changed\r\n");
this.WriteObjects("                if (__oldValue != null)\r\n");
this.WriteObjects("                {\r\n");
#line 104 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (inverseNavigatorIsList) {
            // TODO: check whether __oldValue is loaded before potentially triggering a DB Call

#line 107 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    // remove from old list\r\n");
this.WriteObjects("                    (__oldValue.",  inverseNavigatorName , " as OneNRelationList<",  ownInterface , ">).RemoveWithoutClearParent(this);\r\n");
#line 109 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} else { 
#line 110 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    // unset old reference\r\n");
this.WriteObjects("                    __oldValue.",  inverseNavigatorName , " = null;\r\n");
#line 112 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 113 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                if (__newValue != null)\r\n");
this.WriteObjects("                {\r\n");
#line 117 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (inverseNavigatorIsList) { 
#line 118 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    // add to new list\r\n");
this.WriteObjects("                    (__newValue.",  inverseNavigatorName , " as OneNRelationList<",  ownInterface , ">).AddWithoutSetParent(this);\r\n");
#line 120 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} else { 
#line 121 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    // set new reference\r\n");
this.WriteObjects("                    __newValue.",  inverseNavigatorName , " = this;\r\n");
#line 123 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 124 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                }\r\n");
#line 125 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 126 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                // everything is done. fire the Changed event\r\n");
this.WriteObjects("                NotifyPropertyChanged(\"",  name , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("\r\n");
#line 129 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (callGetterSetterEvents) { 
#line 130 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (",  eventName , "_PostSetter != null && IsAttached)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var e = new PropertyPostSetterEventArgs<",  referencedInterface , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("                    ",  eventName , "_PostSetter(this, e);\r\n");
this.WriteObjects("                }\r\n");
#line 135 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 136 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>Backing store for ",  name , "'s id, used on dehydration only</summary>\r\n");
this.WriteObjects("        private int? ",  fkBackingName , " = null;\r\n");
this.WriteObjects("\r\n");
#line 142 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (relDataTypeExportable) { 
#line 143 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        /// <summary>Backing store for ",  name , "'s guid, used on import only</summary>\r\n");
this.WriteObjects("        private Guid? ",  fkGuidBackingName , " = null;\r\n");
#line 145 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 146 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("\r\n");
#line 148 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
AddSerialization(serializationList, name, fkBackingName);

    if (!String.IsNullOrEmpty(positionPropertyName))
    {
        Properties.NotifyingValueProperty.Call(
            Host, ctx, serializationList,
            "int?", positionPropertyName, moduleNamespace);
    }

#line 157 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // END ",  this.GetType() , " for ",  name , "\r\n");

        }

    }
}