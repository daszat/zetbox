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
		protected string fkName;
		protected string fkBackingName;
		protected string ownInterface;
		protected string referencedInterface;
		protected Relation rel;
		protected RelationEndRole endRole;
		protected bool hasInverseNavigator;
		protected bool hasPositionStorage;


        public ObjectReferencePropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList, string name, string efName, string fkName, string fkBackingName, string ownInterface, string referencedInterface, Relation rel, RelationEndRole endRole, bool hasInverseNavigator, bool hasPositionStorage)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.name = name;
			this.efName = efName;
			this.fkName = fkName;
			this.fkBackingName = fkBackingName;
			this.ownInterface = ownInterface;
			this.referencedInterface = referencedInterface;
			this.rel = rel;
			this.endRole = endRole;
			this.hasInverseNavigator = hasInverseNavigator;
			this.hasPositionStorage = hasPositionStorage;

        }
        
        public override void Generate()
        {
#line 26 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
RelationEnd relEnd = rel.GetEnd(endRole);
    RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

#line 29 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
this.WriteObjects("        public ",  referencedInterface , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (",  fkName , ".HasValue)\r\n");
this.WriteObjects("                    return Context.Find<",  referencedInterface , ">(",  fkName , ".Value);\r\n");
this.WriteObjects("                else\r\n");
this.WriteObjects("                    return null;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                // TODO: only accept objects from same Context\r\n");
this.WriteObjects("                if (IsReadonly) throw new ReadOnlyObjectException();\r\n");
this.WriteObjects("                \r\n");
this.WriteObjects("                // shortcut noops\r\n");
this.WriteObjects("                if (value == null && ",  fkBackingName , " == null)\r\n");
this.WriteObjects("					return;\r\n");
this.WriteObjects("                else if (value != null && value.ID == ",  fkBackingName , ")\r\n");
this.WriteObjects("					return;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("				// Changing Event fires before anything is touched\r\n");
this.WriteObjects("				NotifyPropertyChanging(\"",  name , "\");\r\n");
this.WriteObjects("				           \r\n");
#line 56 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if (hasInverseNavigator)
    {

#line 59 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("	            // cache old value to remove inverse references later\r\n");
this.WriteObjects("                var oldValue = ",  name , ";\r\n");
this.WriteObjects("                \r\n");
#line 63 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 65 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("				// next, set the local reference\r\n");
this.WriteObjects("                ",  fkBackingName , " = value == null ? (int?)null : value.ID;\r\n");
this.WriteObjects("				\r\n");
#line 69 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if (hasInverseNavigator)
    {
        var otherProp = otherEnd.Navigator;
        string otherName = otherProp.PropertyName;


#line 75 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("				// now fixup redundant, inverse references\r\n");
this.WriteObjects("				// The inverse navigator will also fire events when changed, so should \r\n");
this.WriteObjects("				// only be touched after setting the local value above. \r\n");
this.WriteObjects("				// TODO: for complete correctness, the \"other\" Changing event should also fire \r\n");
this.WriteObjects("				//       before the local value is changed\r\n");
this.WriteObjects("				if (oldValue != null)\r\n");
this.WriteObjects("				{\r\n");
#line 83 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if (otherProp.IsList)
        {
			// TODO: check whether oldValue is loaded before potentially triggering a DB Call

#line 87 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("					// remove from old list\r\n");
this.WriteObjects("					(oldValue.",  otherName , " as BackReferenceCollection<",  ownInterface , ">).RemoveWithoutClearParent(this);\r\n");
#line 90 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}
        else
        {

#line 94 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("					// unset old reference\r\n");
this.WriteObjects("					oldValue.",  otherName , " = null;\r\n");
#line 97 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 99 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("				}\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                if (value != null)\r\n");
this.WriteObjects("                {\r\n");
#line 104 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if (otherProp.IsList)
        {

#line 107 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("					// add to new list\r\n");
this.WriteObjects("					(value.",  otherName , " as BackReferenceCollection<",  ownInterface , ">).AddWithoutSetParent(this);\r\n");
#line 110 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}
        else
        {

#line 114 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("					// set new reference\r\n");
this.WriteObjects("                    value.",  otherName , " = this;\r\n");
#line 117 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 119 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                }\r\n");
#line 121 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 123 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("				// everything is done. fire the Changed event\r\n");
this.WriteObjects("				NotifyPropertyChanged(\"",  name , "\");\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        // provide a way to directly access the foreign key int\r\n");
this.WriteObjects("        public int? ",  fkName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                return ",  fkBackingName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            private set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (IsReadonly) throw new ReadOnlyObjectException();\r\n");
this.WriteObjects("                if (",  fkBackingName , " != value)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    NotifyPropertyChanging(\"",  name , "\");\r\n");
this.WriteObjects("                    ",  fkBackingName , " = value;\r\n");
this.WriteObjects("                    NotifyPropertyChanged(\"",  name , "\");\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        private int? ",  fkBackingName , ";\r\n");
#line 148 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
AddSerialization(serializationList, fkBackingName);

	string posStorageName = name + Kistl.API.Helper.PositionSuffix;

	if (hasPositionStorage)
	{
		CallTemplate("Implementation.ObjectClasses.NotifyingValueProperty", ctx,
		    serializationList,
			"int?", posStorageName);
	}


        }



    }
}