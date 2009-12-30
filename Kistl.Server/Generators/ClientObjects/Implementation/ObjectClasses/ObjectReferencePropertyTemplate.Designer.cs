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
		protected string efName;
		protected string fkBackingName;
		protected string fkGuidBackingName;
		protected string ownInterface;
		protected string referencedInterface;
		protected Relation rel;
		protected RelationEndRole endRole;
		protected bool hasInverseNavigator;
		protected bool hasPositionStorage;
		protected bool callGetterSetterEvents;


        public ObjectReferencePropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList, string name, string efName, string fkBackingName, string fkGuidBackingName, string ownInterface, string referencedInterface, Relation rel, RelationEndRole endRole, bool hasInverseNavigator, bool hasPositionStorage, bool callGetterSetterEvents)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.name = name;
			this.efName = efName;
			this.fkBackingName = fkBackingName;
			this.fkGuidBackingName = fkGuidBackingName;
			this.ownInterface = ownInterface;
			this.referencedInterface = referencedInterface;
			this.rel = rel;
			this.endRole = endRole;
			this.hasInverseNavigator = hasInverseNavigator;
			this.hasPositionStorage = hasPositionStorage;
			this.callGetterSetterEvents = callGetterSetterEvents;

        }
        
        public override void Generate()
        {
#line 27 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
RelationEnd relEnd = rel.GetEndFromRole(endRole);
    RelationEnd otherEnd = rel.GetOtherEnd(relEnd);
	string eventName = "On" + name;

#line 31 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("		// ",  this.GetType() , "\r\n");
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
#line 46 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if(callGetterSetterEvents)
				{

#line 49 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("				if(",  eventName , "_Getter != null)\r\n");
this.WriteObjects("				{\r\n");
this.WriteObjects("					var e = new PropertyGetterEventArgs<",  referencedInterface , ">(__value);\r\n");
this.WriteObjects("					",  eventName , "_Getter(this, e);\r\n");
this.WriteObjects("					__value = e.Result;\r\n");
this.WriteObjects("				}\r\n");
#line 56 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 58 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
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
#line 80 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if(callGetterSetterEvents)
				{

#line 83 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if(",  eventName , "_PreSetter != null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("					var e = new PropertyPreSetterEventArgs<",  referencedInterface , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("					",  eventName , "_PreSetter(this, e);\r\n");
this.WriteObjects("					__newValue = e.Result;\r\n");
this.WriteObjects("                }\r\n");
#line 90 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 92 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                \r\n");
this.WriteObjects("				// next, set the local reference\r\n");
this.WriteObjects("                ",  fkBackingName , " = __newValue == null ? (int?)null : __newValue.ID;\r\n");
this.WriteObjects("				\r\n");
#line 97 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if (hasInverseNavigator)
    {
        var otherProp = otherEnd.Navigator;
        string otherName = otherProp.PropertyName;


#line 103 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("				// now fixup redundant, inverse references\r\n");
this.WriteObjects("				// The inverse navigator will also fire events when changed, so should \r\n");
this.WriteObjects("				// only be touched after setting the local value above. \r\n");
this.WriteObjects("				// TODO: for complete correctness, the \"other\" Changing event should also fire \r\n");
this.WriteObjects("				//       before the local value is changed\r\n");
this.WriteObjects("				if (__oldValue != null)\r\n");
this.WriteObjects("				{\r\n");
#line 111 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if (otherProp.IsList())
        {
			// TODO: check whether __oldValue is loaded before potentially triggering a DB Call

#line 115 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("					// remove from old list\r\n");
this.WriteObjects("					(__oldValue.",  otherName , " as OneNRelationList<",  ownInterface , ">).RemoveWithoutClearParent(this);\r\n");
#line 118 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}
        else
        {

#line 122 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("					// unset old reference\r\n");
this.WriteObjects("					__oldValue.",  otherName , " = null;\r\n");
#line 125 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 127 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("				}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                if (__newValue != null)\r\n");
this.WriteObjects("                {\r\n");
#line 132 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if (otherProp.IsList())
        {

#line 135 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("					// add to new list\r\n");
this.WriteObjects("					(__newValue.",  otherName , " as OneNRelationList<",  ownInterface , ">).AddWithoutSetParent(this);\r\n");
#line 138 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}
        else
        {

#line 142 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("					// set new reference\r\n");
this.WriteObjects("                    __newValue.",  otherName , " = this;\r\n");
#line 145 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 147 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                }\r\n");
#line 149 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 151 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("				// everything is done. fire the Changed event\r\n");
this.WriteObjects("				NotifyPropertyChanged(\"",  name , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("\r\n");
#line 155 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if(callGetterSetterEvents)
				{

#line 158 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                if(",  eventName , "_PostSetter != null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("					var e = new PropertyPostSetterEventArgs<",  referencedInterface , ">(__oldValue, __newValue);\r\n");
this.WriteObjects("					",  eventName , "_PostSetter(this, e);\r\n");
this.WriteObjects("                }\r\n");
#line 164 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 165 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                \r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        private int? ",  fkBackingName , ";\r\n");
#line 171 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
AddSerialization(serializationList, fkBackingName);

	string posStorageName = name + Kistl.API.Helper.PositionSuffix;

	if (hasPositionStorage)
	{
		Templates.Implementation.ObjectClasses.NotifyingValueProperty.Call(Host, ctx,
		    serializationList,
			"int?", posStorageName, "http://dasz.at/Kistl");
	}


        }



    }
}