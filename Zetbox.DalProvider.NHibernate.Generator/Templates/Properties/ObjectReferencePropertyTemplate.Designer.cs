using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst")]
    public partial class ObjectReferencePropertyTemplate : Zetbox.Generator.MemberTemplate
    {
		protected IZetboxContext ctx;
		protected Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList;
		protected string moduleNamespace;
		protected string ownInterface;
		protected string name;
		protected string implNameUnused;
		protected string eventName;
		protected string fkBackingName;
		protected string publicFKBackingName;
		protected string fkGuidBackingName;
		protected string referencedInterface;
		protected string referencedImplementation;
		protected string associationNameUnused;
		protected string targetRoleNameUnused;
		protected string positionPropertyName;
		protected string inverseNavigatorName;
		protected bool inverseNavigatorIsList;
		protected bool notifyInverseCollection;
		protected bool eagerLoading;
		protected bool relDataTypeExportable;
		protected bool callGetterSetterEvents;
		protected bool isCalculated;
		protected bool disableExport;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace, string ownInterface, string name, string implNameUnused, string eventName, string fkBackingName, string publicFKBackingName, string fkGuidBackingName, string referencedInterface, string referencedImplementation, string associationNameUnused, string targetRoleNameUnused, string positionPropertyName, string inverseNavigatorName, bool inverseNavigatorIsList, bool notifyInverseCollection, bool eagerLoading, bool relDataTypeExportable, bool callGetterSetterEvents, bool isCalculated, bool disableExport)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ObjectReferencePropertyTemplate", ctx, serializationList, moduleNamespace, ownInterface, name, implNameUnused, eventName, fkBackingName, publicFKBackingName, fkGuidBackingName, referencedInterface, referencedImplementation, associationNameUnused, targetRoleNameUnused, positionPropertyName, inverseNavigatorName, inverseNavigatorIsList, notifyInverseCollection, eagerLoading, relDataTypeExportable, callGetterSetterEvents, isCalculated, disableExport);
        }

        public ObjectReferencePropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace, string ownInterface, string name, string implNameUnused, string eventName, string fkBackingName, string publicFKBackingName, string fkGuidBackingName, string referencedInterface, string referencedImplementation, string associationNameUnused, string targetRoleNameUnused, string positionPropertyName, string inverseNavigatorName, bool inverseNavigatorIsList, bool notifyInverseCollection, bool eagerLoading, bool relDataTypeExportable, bool callGetterSetterEvents, bool isCalculated, bool disableExport)
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
			this.publicFKBackingName = publicFKBackingName;
			this.fkGuidBackingName = fkGuidBackingName;
			this.referencedInterface = referencedInterface;
			this.referencedImplementation = referencedImplementation;
			this.associationNameUnused = associationNameUnused;
			this.targetRoleNameUnused = targetRoleNameUnused;
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
#line 51 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , " for ",  name , "\r\n");
this.WriteObjects("        // fkBackingName=this.Proxy.",  name , "; fkGuidBackingName=",  fkGuidBackingName , ";\r\n");
this.WriteObjects("        // referencedInterface=",  referencedInterface , "; moduleNamespace=",  moduleNamespace , ";\r\n");
#line 54 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (notifyInverseCollection && !String.IsNullOrEmpty(inverseNavigatorName)) { 
#line 55 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // will get inverse collection for notifications for ",  inverseNavigatorName , "\r\n");
#line 56 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} else if (!notifyInverseCollection && !String.IsNullOrEmpty(inverseNavigatorName)) { 
#line 57 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // inverse Navigator=",  String.IsNullOrEmpty(inverseNavigatorName) ? "none" : inverseNavigatorName , "; ",  inverseNavigatorIsList ? "is list" : "is reference" , ";\r\n");
#line 58 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} else { 
#line 59 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // no inverse navigator handling\r\n");
#line 60 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 61 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // PositionStorage=",  String.IsNullOrEmpty(positionPropertyName) ? "none" : positionPropertyName , ";\r\n");
this.WriteObjects("        // Target ",  relDataTypeExportable ? String.Empty : "not " , "exportable; does ",  callGetterSetterEvents ? String.Empty : "not " , "call events\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        public System.Threading.Tasks.Task<",  referencedInterface , "> GetProp_",  name , "()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            return System.Threading.Tasks.Task.FromResult(",  name , ");\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public async System.Threading.Tasks.Task SetProp_",  name , "(",  referencedInterface , " newValue)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            await TriggerFetch",  name , "Async();\r\n");
this.WriteObjects("            ",  name , " = newValue;\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
this.WriteObjects("		[System.Runtime.Serialization.IgnoreDataMember]\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  referencedInterface , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                ",  referencedImplementation , " __value = (",  referencedImplementation , ")OurContext.AttachAndWrap(this.Proxy.",  name , ");\r\n");
this.WriteObjects("\r\n");
#line 83 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (callGetterSetterEvents) { 
#line 84 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (",  eventName , "_Getter != null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var e = new PropertyGetterEventArgs<",  referencedInterface , ">(__value);\r\n");
this.WriteObjects("                    ",  eventName , "_Getter(this, e);\r\n");
this.WriteObjects("                    __value = (",  referencedImplementation , ")e.Result;\r\n");
this.WriteObjects("                }\r\n");
#line 90 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 91 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("                return __value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (this.IsReadonly) throw new ReadOnlyObjectException();\r\n");
this.WriteObjects("                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                // shortcut noop with nulls\r\n");
this.WriteObjects("                if (value == null && this.Proxy.",  name , " == null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    SetInitializedProperty(\"",  name , "\");\r\n");
this.WriteObjects("                    return;\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                // cache old value to remove inverse references later\r\n");
this.WriteObjects("                var __oldValue = (",  referencedImplementation , ")OurContext.AttachAndWrap(this.Proxy.",  name , ");\r\n");
this.WriteObjects("                var __newValue = (",  referencedImplementation , ")value;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                // shortcut noop on objects\r\n");
this.WriteObjects("                // can't use proxy's ID here, since that might be INVALIDID before persisting the first time.\r\n");
this.WriteObjects("                if (__oldValue == __newValue)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    SetInitializedProperty(\"",  name , "\");\r\n");
this.WriteObjects("                    return;\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                // Changing Event fires before anything is touched\r\n");
this.WriteObjects("                NotifyPropertyChanging(\"",  name , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("\r\n");
#line 121 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (!String.IsNullOrEmpty(inverseNavigatorName) && inverseNavigatorIsList) { 
#line 122 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (__oldValue != null) {\r\n");
this.WriteObjects("                    __oldValue.NotifyPropertyChanging(\"",  inverseNavigatorName , "\", null, null);\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                if (__newValue != null) {\r\n");
this.WriteObjects("                    __newValue.NotifyPropertyChanging(\"",  inverseNavigatorName , "\", null, null);\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("\r\n");
#line 129 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 130 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (callGetterSetterEvents) { 
#line 131 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (",  eventName , "_PreSetter != null && IsAttached)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var e = new PropertyPreSetterEventArgs<",  referencedInterface , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("                    ",  eventName , "_PreSetter(this, e);\r\n");
this.WriteObjects("                    __newValue = (",  referencedImplementation , ")e.Result;\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("\r\n");
#line 138 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 139 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
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
#line 149 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (!String.IsNullOrEmpty(inverseNavigatorName)) { 
#line 150 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                // now fixup redundant, inverse references\r\n");
this.WriteObjects("                // The inverse navigator will also fire events when changed, so should\r\n");
this.WriteObjects("                // only be touched after setting the local value above.\r\n");
this.WriteObjects("                // TODO: for complete correctness, the \"other\" Changing event should also fire\r\n");
this.WriteObjects("                //       before the local value is changed\r\n");
this.WriteObjects("                if (__oldValue != null)\r\n");
this.WriteObjects("                {\r\n");
#line 157 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (inverseNavigatorIsList) {
            // TODO: check whether __oldValue is loaded before potentially triggering a DB Call

#line 160 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    // remove from old list\r\n");
this.WriteObjects("                    (__oldValue.",  inverseNavigatorName , " as IRelationListSync<",  ownInterface , ">).RemoveWithoutClearParent(this);\r\n");
#line 162 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} else { 
#line 163 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    // unset old reference\r\n");
this.WriteObjects("                    __oldValue.",  inverseNavigatorName , " = null;\r\n");
#line 165 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 166 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                if (__newValue != null)\r\n");
this.WriteObjects("                {\r\n");
#line 170 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (inverseNavigatorIsList) { 
#line 171 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    // add to new list\r\n");
this.WriteObjects("                    (__newValue.",  inverseNavigatorName , " as IRelationListSync<",  ownInterface , ">).AddWithoutSetParent(this);\r\n");
#line 173 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} else { 
#line 174 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    // set new reference\r\n");
this.WriteObjects("                    __newValue.",  inverseNavigatorName , " = this;\r\n");
#line 176 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 177 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                }\r\n");
#line 178 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 179 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                // everything is done. fire the Changed event\r\n");
this.WriteObjects("                NotifyPropertyChanged(\"",  name , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("                if(IsAttached) UpdateChangedInfo = true;\r\n");
this.WriteObjects("\r\n");
#line 183 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (callGetterSetterEvents) { 
#line 184 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (",  eventName , "_PostSetter != null && IsAttached)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var e = new PropertyPostSetterEventArgs<",  referencedInterface , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("                    ",  eventName , "_PostSetter(this, e);\r\n");
this.WriteObjects("                }\r\n");
#line 189 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 190 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>Backing store for ",  UglyXmlEncode(name) , "'s id, used on dehydration only</summary>\r\n");
this.WriteObjects("        private int? ",  fkBackingName , " = null;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        /// <summary>ForeignKey Property for ",  UglyXmlEncode(name) , "'s id, used on APIs only</summary>\r\n");
this.WriteObjects("		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]\r\n");
this.WriteObjects("        public int? ",  publicFKBackingName , "\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			get { return ",  name , " != null ? ",  name , ".ID : (int?)null; }\r\n");
this.WriteObjects("			set { ",  fkBackingName , " = value; }\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("\r\n");
#line 204 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (relDataTypeExportable) { 
#line 205 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        /// <summary>Backing store for ",  UglyXmlEncode(name) , "'s guid, used on import only</summary>\r\n");
this.WriteObjects("        private Guid? ",  fkGuidBackingName , " = null;\r\n");
#line 207 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 208 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    public System.Threading.Tasks.Task TriggerFetch",  name , "Async()\r\n");
this.WriteObjects("    {\r\n");
this.WriteObjects("        return System.Threading.Tasks.Task.FromResult<",  referencedInterface , ">(this.",  name , ");\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("\r\n");
#line 215 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
AddSerialization(serializationList, name, fkBackingName, fkGuidBackingName);

    if (!String.IsNullOrEmpty(positionPropertyName))
    {
        Properties.NotifyingValueProperty.Call(
            Host, ctx, serializationList,
            "int?", positionPropertyName, moduleNamespace, false, disableExport);
    }

#line 224 "C:\projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // END ",  this.GetType() , " for ",  name , "\r\n");

        }

    }
}