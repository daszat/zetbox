using System;
using System.Diagnostics;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;


namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst")]
    public partial class ObjectReferencePropertyTemplate : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Templates.Implementation.SerializationMembersList serializationList;
		protected ObjectReferenceProperty prop;


        public ObjectReferencePropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList, ObjectReferenceProperty prop)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.prop = prop;

        }
        
        public override void Generate()
        {
#line 17 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
Debug.Assert(!prop.IsList);

    string name = prop.PropertyName;
    string efName = name + Kistl.API.Helper.ImplementationSuffix;
    string fkName = "fk_" + name;
    string fkBackingName = "_fk_" + name;

    string referencedInterface = prop.ReferenceObjectClass.Module.Namespace + "." + prop.ReferenceObjectClass.ClassName;

    var rel = NewRelation.Lookup(ctx, prop);
    var relEnd = rel.GetEnd(prop);
    var otherEnd = relEnd.Other;
    bool hasInverseNavigator = otherEnd.Navigator != null;
    

#line 32 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
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
#line 49 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if (hasInverseNavigator)
    {
        var otherProp = otherEnd.Navigator;
        string otherName = otherProp.PropertyName;

#line 54 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                // fix up inverse reference\r\n");
this.WriteObjects("                var oldValue = ",  name , ";\r\n");
this.WriteObjects("                if (value != null && value.ID != ",  fkName , ")\r\n");
this.WriteObjects("                {\r\n");
#line 59 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if (otherProp.IsList)
        {

#line 62 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    oldValue.",  otherName , ".Remove(this);\r\n");
this.WriteObjects("                    ",  fkName , " = value.ID;\r\n");
this.WriteObjects("                    value.",  otherName , ".Add(this);\r\n");
#line 66 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}
        else
        {

#line 70 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    ",  fkName , " = value.ID;\r\n");
this.WriteObjects("                    value.",  otherName , " = this;\r\n");
#line 73 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 75 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                }\r\n");
this.WriteObjects("                else\r\n");
this.WriteObjects("                {\r\n");
#line 79 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
if (otherProp.IsList)
        {

#line 82 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    oldValue.",  otherName , ".Remove(this);\r\n");
this.WriteObjects("                    ",  fkName , " = null;\r\n");
#line 85 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}
        else
        {

#line 89 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                    ",  fkName , " = null;\r\n");
this.WriteObjects("                    value.",  otherName , " = null;\r\n");
#line 92 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 94 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                }\r\n");
#line 96 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}
    else // has no inverse navigator
    {

#line 100 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("                ",  fkName , " = value == null ? (int?)null : value.ID;\r\n");
#line 102 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
}

#line 104 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
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
#line 127 "P:\Kistl\Kistl.Server\Generators\ClientObjects\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
AddSerialization(serializationList, fkBackingName);


        }



    }
}