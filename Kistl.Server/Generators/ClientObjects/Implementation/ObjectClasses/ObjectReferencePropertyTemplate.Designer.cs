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
		protected string referencedInterface;
		protected Relation rel;
		protected RelationEndRole endRole;
		protected bool hasInverseNavigator;
		protected bool hasPositionStorage;


        public ObjectReferencePropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList, string name, string efName, string fkName, string fkBackingName, string referencedInterface, Relation rel, RelationEndRole endRole, bool hasInverseNavigator, bool hasPositionStorage)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.name = name;
			this.efName = efName;
			this.fkName = fkName;
			this.fkBackingName = fkBackingName;
			this.referencedInterface = referencedInterface;
			this.rel = rel;
			this.endRole = endRole;
			this.hasInverseNavigator = hasInverseNavigator;
			this.hasPositionStorage = hasPositionStorage;

        }
        
        public override void Generate()
        {
#line 25 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
RelationEnd relEnd = rel.GetEnd(endRole);
    RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

#line 28 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
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
#line 45 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if (hasInverseNavigator)
    {
        var otherProp = otherEnd.Navigator;
        string otherName = otherProp.PropertyName;

#line 50 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("\r\n");
this.WriteObjects("                var oldValue = ",  name , ";\r\n");
this.WriteObjects("                \r\n");
this.WriteObjects("                // shortcut noops\r\n");
this.WriteObjects("                if (Object.Equals(oldValue, value))\r\n");
this.WriteObjects("					return;\r\n");
this.WriteObjects("                \r\n");
this.WriteObjects("                // fix up inverse reference\r\n");
this.WriteObjects("                if (value != null && value.ID != ",  fkName , ")\r\n");
this.WriteObjects("                {\r\n");
#line 61 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if (otherProp.IsList)
        {

#line 64 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("					if (oldValue != null)\r\n");
this.WriteObjects("						oldValue.",  otherName , ".Remove(this);\r\n");
this.WriteObjects("                    ",  fkName , " = value.ID;\r\n");
this.WriteObjects("                    value.",  otherName , ".Add(this);\r\n");
#line 69 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}
        else
        {

#line 73 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    ",  fkName , " = value.ID;\r\n");
this.WriteObjects("                    value.",  otherName , " = this;\r\n");
#line 76 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 78 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                }\r\n");
this.WriteObjects("                else\r\n");
this.WriteObjects("                {\r\n");
#line 82 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if (otherProp.IsList)
        {

#line 85 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("					if (oldValue != null)\r\n");
this.WriteObjects("	                    oldValue.",  otherName , ".Remove(this);\r\n");
this.WriteObjects("                    ",  fkName , " = null;\r\n");
#line 89 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}
        else
        {

#line 93 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    ",  fkName , " = null;\r\n");
this.WriteObjects("                    value.",  otherName , " = null;\r\n");
#line 96 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 98 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                }\r\n");
#line 100 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}
    else // has no inverse navigator
    {

#line 104 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                ",  fkName , " = value == null ? (int?)null : value.ID;\r\n");
#line 106 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 108 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
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
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (IsReadonly) throw new ReadOnlyObjectException();\r\n");
this.WriteObjects("                if (",  fkBackingName , " != value)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    NotifyPropertyChanging(\"",  name , "\");\r\n");
this.WriteObjects("                    ",  fkBackingName , " = value;\r\n");
this.WriteObjects("                    NotifyPropertyChanging(\"",  name , "\");\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        private int? ",  fkBackingName , ";\r\n");
#line 131 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
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