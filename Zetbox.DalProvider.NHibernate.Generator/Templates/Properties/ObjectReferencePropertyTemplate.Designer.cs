using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst")]
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
		protected string fkGuidBackingName;
		protected string referencedInterface;
		protected string referencedImplementation;
		protected string associationNameUnused;
		protected string targetRoleNameUnused;
		protected string positionPropertyName;
		protected string inverseNavigatorName;
		protected bool inverseNavigatorIsList;
		protected bool eagerLoading;
		protected bool relDataTypeExportable;
		protected bool callGetterSetterEvents;
		protected bool isCalculated;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace, string ownInterface, string name, string implNameUnused, string eventName, string fkBackingName, string fkGuidBackingName, string referencedInterface, string referencedImplementation, string associationNameUnused, string targetRoleNameUnused, string positionPropertyName, string inverseNavigatorName, bool inverseNavigatorIsList, bool eagerLoading, bool relDataTypeExportable, bool callGetterSetterEvents, bool isCalculated)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ObjectReferencePropertyTemplate", ctx, serializationList, moduleNamespace, ownInterface, name, implNameUnused, eventName, fkBackingName, fkGuidBackingName, referencedInterface, referencedImplementation, associationNameUnused, targetRoleNameUnused, positionPropertyName, inverseNavigatorName, inverseNavigatorIsList, eagerLoading, relDataTypeExportable, callGetterSetterEvents, isCalculated);
        }

        public ObjectReferencePropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace, string ownInterface, string name, string implNameUnused, string eventName, string fkBackingName, string fkGuidBackingName, string referencedInterface, string referencedImplementation, string associationNameUnused, string targetRoleNameUnused, string positionPropertyName, string inverseNavigatorName, bool inverseNavigatorIsList, bool eagerLoading, bool relDataTypeExportable, bool callGetterSetterEvents, bool isCalculated)
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
			this.associationNameUnused = associationNameUnused;
			this.targetRoleNameUnused = targetRoleNameUnused;
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
#line 17 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("");
#line 48 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , " for ",  name , "\n");
this.WriteObjects("        // fkBackingName=this.Proxy.",  name , "; fkGuidBackingName=",  fkGuidBackingName , ";\n");
this.WriteObjects("        // referencedInterface=",  referencedInterface , "; moduleNamespace=",  moduleNamespace , ";\n");
this.WriteObjects("        // inverse Navigator=",  String.IsNullOrEmpty(inverseNavigatorName) ? "none" : inverseNavigatorName , "; ",  inverseNavigatorIsList ? "is list" : "is reference" , ";\n");
this.WriteObjects("        // PositionStorage=",  String.IsNullOrEmpty(positionPropertyName) ? "none" : positionPropertyName , ";\n");
this.WriteObjects("        // Target ",  relDataTypeExportable ? String.Empty : "not " , "exportable; does ",  callGetterSetterEvents ? String.Empty : "not " , "call events\n");
this.WriteObjects("\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\n");
this.WriteObjects("        ",  GetModifiers() , " ",  referencedInterface , " ",  name , "\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get\n");
this.WriteObjects("            {\n");
this.WriteObjects("                if (!CurrentAccessRights.HasReadRights()) return null;\n");
this.WriteObjects("                ",  referencedImplementation , " __value = (",  referencedImplementation , ")OurContext.AttachAndWrap(this.Proxy.",  name , ");\n");
this.WriteObjects("\n");
#line 63 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (callGetterSetterEvents) { 
#line 64 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (",  eventName , "_Getter != null)\n");
this.WriteObjects("                {\n");
this.WriteObjects("                    var e = new PropertyGetterEventArgs<",  referencedInterface , ">(__value);\n");
this.WriteObjects("                    ",  eventName , "_Getter(this, e);\n");
this.WriteObjects("                    __value = (",  referencedImplementation , ")e.Result;\n");
this.WriteObjects("                }\n");
#line 70 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 71 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("\n");
this.WriteObjects("                return __value;\n");
this.WriteObjects("            }\n");
this.WriteObjects("            set\n");
this.WriteObjects("            {\n");
this.WriteObjects("                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();\n");
this.WriteObjects("                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();\n");
this.WriteObjects("\n");
this.WriteObjects("                // shortcut noop with nulls\n");
this.WriteObjects("                if (value == null && this.Proxy.",  name , " == null)\n");
this.WriteObjects("				{\n");
this.WriteObjects("					SetInitializedProperty(\"",  name , "\");\n");
this.WriteObjects("                    return;\n");
this.WriteObjects("				}\n");
this.WriteObjects("\n");
this.WriteObjects("                // cache old value to remove inverse references later\n");
this.WriteObjects("                var __oldValue = (",  referencedImplementation , ")OurContext.AttachAndWrap(this.Proxy.",  name , ");\n");
this.WriteObjects("                var __newValue = (",  referencedImplementation , ")value;\n");
this.WriteObjects("\n");
this.WriteObjects("                // shortcut noop on objects\n");
this.WriteObjects("                // can't use proxy's ID here, since that might be INVALIDID before persisting the first time.\n");
this.WriteObjects("                if (__oldValue == __newValue)\n");
this.WriteObjects("				{\n");
this.WriteObjects("					SetInitializedProperty(\"",  name , "\");\n");
this.WriteObjects("                    return;\n");
this.WriteObjects("				}\n");
this.WriteObjects("\n");
this.WriteObjects("                // Changing Event fires before anything is touched\n");
this.WriteObjects("                NotifyPropertyChanging(\"",  name , "\", __oldValue, __newValue);\n");
this.WriteObjects("\n");
#line 101 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (!String.IsNullOrEmpty(inverseNavigatorName) && inverseNavigatorIsList) { 
#line 102 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (__oldValue != null) {\n");
this.WriteObjects("                    __oldValue.NotifyPropertyChanging(\"",  inverseNavigatorName , "\", null, null);\n");
this.WriteObjects("                }\n");
this.WriteObjects("                if (__newValue != null) {\n");
this.WriteObjects("                    __newValue.NotifyPropertyChanging(\"",  inverseNavigatorName , "\", null, null);\n");
this.WriteObjects("                }\n");
this.WriteObjects("\n");
#line 109 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 110 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (callGetterSetterEvents) { 
#line 111 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (",  eventName , "_PreSetter != null && IsAttached)\n");
this.WriteObjects("                {\n");
this.WriteObjects("                    var e = new PropertyPreSetterEventArgs<",  referencedInterface , ">(__oldValue, __newValue);\n");
this.WriteObjects("                    ",  eventName , "_PreSetter(this, e);\n");
this.WriteObjects("                    __newValue = (",  referencedImplementation , ")e.Result;\n");
this.WriteObjects("                }\n");
this.WriteObjects("\n");
#line 118 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 119 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                // next, set the local reference\n");
this.WriteObjects("                if (__newValue == null)\n");
this.WriteObjects("                {\n");
this.WriteObjects("                    this.Proxy.",  name , " = null;\n");
this.WriteObjects("                }\n");
this.WriteObjects("                else\n");
this.WriteObjects("                {\n");
this.WriteObjects("                    this.Proxy.",  name , " = __newValue.Proxy;\n");
this.WriteObjects("                }\n");
this.WriteObjects("\n");
#line 129 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (!String.IsNullOrEmpty(inverseNavigatorName)) { 
#line 130 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                // now fixup redundant, inverse references\n");
this.WriteObjects("                // The inverse navigator will also fire events when changed, so should\n");
this.WriteObjects("                // only be touched after setting the local value above.\n");
this.WriteObjects("                // TODO: for complete correctness, the \"other\" Changing event should also fire\n");
this.WriteObjects("                //       before the local value is changed\n");
this.WriteObjects("                if (__oldValue != null)\n");
this.WriteObjects("                {\n");
#line 137 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (inverseNavigatorIsList) {
            // TODO: check whether __oldValue is loaded before potentially triggering a DB Call

#line 140 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    // remove from old list\n");
this.WriteObjects("                    (__oldValue.",  inverseNavigatorName , " as IRelationListSync<",  ownInterface , ">).RemoveWithoutClearParent(this);\n");
#line 142 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} else { 
#line 143 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    // unset old reference\n");
this.WriteObjects("                    __oldValue.",  inverseNavigatorName , " = null;\n");
#line 145 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 146 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                }\n");
this.WriteObjects("\n");
this.WriteObjects("                if (__newValue != null)\n");
this.WriteObjects("                {\n");
#line 150 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (inverseNavigatorIsList) { 
#line 151 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    // add to new list\n");
this.WriteObjects("                    (__newValue.",  inverseNavigatorName , " as IRelationListSync<",  ownInterface , ">).AddWithoutSetParent(this);\n");
#line 153 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} else { 
#line 154 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    // set new reference\n");
this.WriteObjects("                    __newValue.",  inverseNavigatorName , " = this;\n");
#line 156 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 157 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                }\n");
#line 158 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 159 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                // everything is done. fire the Changed event\n");
this.WriteObjects("                NotifyPropertyChanged(\"",  name , "\", __oldValue, __newValue);\n");
this.WriteObjects("\n");
#line 162 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (callGetterSetterEvents) { 
#line 163 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if (",  eventName , "_PostSetter != null && IsAttached)\n");
this.WriteObjects("                {\n");
this.WriteObjects("                    var e = new PropertyPostSetterEventArgs<",  referencedInterface , ">(__oldValue, __newValue);\n");
this.WriteObjects("                    ",  eventName , "_PostSetter(this, e);\n");
this.WriteObjects("                }\n");
#line 168 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 169 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("        /// <summary>Backing store for ",  UglyXmlEncode(name) , "'s id, used on dehydration only</summary>\n");
this.WriteObjects("        private int? ",  fkBackingName , " = null;\n");
this.WriteObjects("\n");
#line 175 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
if (relDataTypeExportable) { 
#line 176 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        /// <summary>Backing store for ",  UglyXmlEncode(name) , "'s guid, used on import only</summary>\n");
this.WriteObjects("        private Guid? ",  fkGuidBackingName , " = null;\n");
#line 178 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
} 
#line 179 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("\n");
#line 181 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
AddSerialization(serializationList, name, fkBackingName, fkGuidBackingName);

    if (!String.IsNullOrEmpty(positionPropertyName))
    {
        Properties.NotifyingValueProperty.Call(
            Host, ctx, serializationList,
            "int?", positionPropertyName, moduleNamespace, false);
    }

#line 190 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // END ",  this.GetType() , " for ",  name , "\n");

        }

    }
}