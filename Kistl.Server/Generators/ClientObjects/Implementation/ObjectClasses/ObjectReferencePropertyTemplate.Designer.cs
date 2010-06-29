using System;
using System.Diagnostics;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst")]
    public partial class ObjectReferencePropertyTemplate : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Templates.Implementation.SerializationMembersList serializationList;
		protected string name;
		protected string implName;
		protected string fkBackingName;
		protected string fkGuidBackingName;
		protected string ownInterface;
		protected string referencedInterface;
		protected Relation rel;
		protected RelationEndRole endRole;
		protected bool hasInverseNavigator;
		protected bool hasPositionStorage;
		protected string positionPropertyName;
		protected bool relDataTypeExportable;
		protected string moduleNamespace;
		protected bool callGetterSetterEvents;
		protected bool isReloadable;


        public ObjectReferencePropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList, string name, string implName, string fkBackingName, string fkGuidBackingName, string ownInterface, string referencedInterface, Relation rel, RelationEndRole endRole, bool hasInverseNavigator, bool hasPositionStorage, string positionPropertyName, bool relDataTypeExportable, string moduleNamespace, bool callGetterSetterEvents, bool isReloadable)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.name = name;
			this.implName = implName;
			this.fkBackingName = fkBackingName;
			this.fkGuidBackingName = fkGuidBackingName;
			this.ownInterface = ownInterface;
			this.referencedInterface = referencedInterface;
			this.rel = rel;
			this.endRole = endRole;
			this.hasInverseNavigator = hasInverseNavigator;
			this.hasPositionStorage = hasPositionStorage;
			this.positionPropertyName = positionPropertyName;
			this.relDataTypeExportable = relDataTypeExportable;
			this.moduleNamespace = moduleNamespace;
			this.callGetterSetterEvents = callGetterSetterEvents;
			this.isReloadable = isReloadable;

        }
        
        public override void Generate()
        {
#line 31 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
RelationEnd relEnd = rel.GetEndFromRole(endRole);
    RelationEnd otherEnd = rel.GetOtherEnd(relEnd);
	string eventName = "On" + name;

#line 35 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("		// BEGIN ",  this.GetType() , " for ",  name , "\r\n");
this.WriteObjects("		// rel(",  endRole , "): ",  rel.A.RoleName , " ",  rel.Verb , " ",  rel.B.RoleName , "\r\n");
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
this.WriteObjects("        public ",  referencedInterface , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("				",  referencedInterface , " __value;\r\n");
this.WriteObjects("                if (",  fkBackingName , ".HasValue)\r\n");
this.WriteObjects("                    __value = Context.Find<",  referencedInterface , ">(",  fkBackingName , ".Value);\r\n");
this.WriteObjects("                else\r\n");
this.WriteObjects("                    __value = null;\r\n");
this.WriteObjects("\r\n");
#line 51 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if(callGetterSetterEvents)
				{

#line 54 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("				if(",  eventName , "_Getter != null)\r\n");
this.WriteObjects("				{\r\n");
this.WriteObjects("					var e = new PropertyGetterEventArgs<",  referencedInterface , ">(__value);\r\n");
this.WriteObjects("					",  eventName , "_Getter(this, e);\r\n");
this.WriteObjects("					__value = e.Result;\r\n");
this.WriteObjects("				}\r\n");
#line 61 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 63 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    \r\n");
this.WriteObjects("                return __value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();\r\n");
this.WriteObjects("                if(value != null && value.Context != this.Context) throw new WrongKistlContextException();\r\n");
this.WriteObjects("                \r\n");
this.WriteObjects("                // shortcut noops\r\n");
this.WriteObjects("                if (value == null && ",  fkBackingName , " == null)\r\n");
this.WriteObjects("					return;\r\n");
this.WriteObjects("                else if (value != null && value.ID == ",  fkBackingName , ")\r\n");
this.WriteObjects("					return;\r\n");
this.WriteObjects("			           \r\n");
this.WriteObjects("	            // cache old value to remove inverse references later\r\n");
this.WriteObjects("                var __oldValue = ",  name , ";\r\n");
this.WriteObjects("				var __newValue = value;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("				// Changing Event fires before anything is touched\r\n");
this.WriteObjects("				NotifyPropertyChanging(\"",  name , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("				\r\n");
#line 85 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if(callGetterSetterEvents)
				{

#line 88 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if(",  eventName , "_PreSetter != null && IsAttached)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("					var e = new PropertyPreSetterEventArgs<",  referencedInterface , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("					",  eventName , "_PreSetter(this, e);\r\n");
this.WriteObjects("					__newValue = e.Result;\r\n");
this.WriteObjects("                }\r\n");
#line 95 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 97 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                \r\n");
this.WriteObjects("				// next, set the local reference\r\n");
this.WriteObjects("                ",  fkBackingName , " = __newValue == null ? (int?)null : __newValue.ID;\r\n");
this.WriteObjects("				\r\n");
#line 102 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if (hasInverseNavigator)
    {
        var otherProp = otherEnd.Navigator;
        string otherName = otherProp.Name;


#line 108 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("				// now fixup redundant, inverse references\r\n");
this.WriteObjects("				// The inverse navigator will also fire events when changed, so should \r\n");
this.WriteObjects("				// only be touched after setting the local value above. \r\n");
this.WriteObjects("				// TODO: for complete correctness, the \"other\" Changing event should also fire \r\n");
this.WriteObjects("				//       before the local value is changed\r\n");
this.WriteObjects("				if (__oldValue != null)\r\n");
this.WriteObjects("				{\r\n");
#line 116 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if (otherProp.IsList())
        {
			// TODO: check whether __oldValue is loaded before potentially triggering a DB Call

#line 120 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("					// remove from old list\r\n");
this.WriteObjects("					(__oldValue.",  otherName , " as OneNRelationList<",  ownInterface , ">).RemoveWithoutClearParent(this);\r\n");
#line 123 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}
        else
        {

#line 127 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("					// unset old reference\r\n");
this.WriteObjects("					__oldValue.",  otherName , " = null;\r\n");
#line 130 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 132 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("				}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                if (__newValue != null)\r\n");
this.WriteObjects("                {\r\n");
#line 137 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if (otherProp.IsList())
        {

#line 140 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("					// add to new list\r\n");
this.WriteObjects("					(__newValue.",  otherName , " as OneNRelationList<",  ownInterface , ">).AddWithoutSetParent(this);\r\n");
#line 143 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}
        else
        {

#line 147 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("					// set new reference\r\n");
this.WriteObjects("                    __newValue.",  otherName , " = this;\r\n");
#line 150 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 152 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                }\r\n");
#line 154 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 156 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("				// everything is done. fire the Changed event\r\n");
this.WriteObjects("				NotifyPropertyChanged(\"",  name , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("\r\n");
#line 160 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if(callGetterSetterEvents)
				{

#line 163 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if(",  eventName , "_PostSetter != null && IsAttached)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("					var e = new PropertyPostSetterEventArgs<",  referencedInterface , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("					",  eventName , "_PostSetter(this, e);\r\n");
this.WriteObjects("                }\r\n");
#line 169 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 170 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                \r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        // normalize namespace for Templates\r\n");
this.WriteObjects("        private ",  referencedInterface , " ",  implName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("			get\r\n");
this.WriteObjects("			{\r\n");
this.WriteObjects("				return ",  name , ";\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("			set\r\n");
this.WriteObjects("			{\r\n");
this.WriteObjects("				",  name , " = value;\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("		}\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        private int? ",  fkBackingName , ";\r\n");
#line 190 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if (isReloadable)
	{

#line 193 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        private Guid? ",  fkGuidBackingName , " = null;\r\n");
#line 195 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}
	
	AddSerialization(serializationList, name, fkBackingName);

	if (hasPositionStorage)
	{
		Templates.Implementation.ObjectClasses.NotifyingValueProperty.Call(Host, ctx,
		    serializationList,
			"int?", positionPropertyName, "http://dasz.at/Kistl");
	}

#line 206 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("		// END ",  this.GetType() , " for ",  name , "\r\n");

        }



    }
}