using System;
using System.Linq;
using System.Collections.Generic;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst")]
    public partial class SerializerTemplate : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected SerializerDirection direction;
		protected SerializationMembersList fields;
		protected bool overrideAndCallBase;
		protected string exportGuidBackingStore;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, SerializationMembersList fields, bool overrideAndCallBase, string exportGuidBackingStore)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Serialization.SerializerTemplate", ctx, direction, fields, overrideAndCallBase, exportGuidBackingStore);
        }

        public SerializerTemplate(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, SerializerDirection direction, SerializationMembersList fields, bool overrideAndCallBase, string exportGuidBackingStore)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.fields = fields;
			this.overrideAndCallBase = overrideAndCallBase;
			this.exportGuidBackingStore = exportGuidBackingStore;

        }

        public override void Generate()
        {
#line 17 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("");
#line 34 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("\n");
#line 36 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
string methodName = direction.ToString();
    string argName;
    string argType;
    string additionalArgs = String.Empty;
    string callBaseWithAdditionalArgs = String.Empty;
    SerializerType serType;

    switch(direction)
    {
        case SerializerDirection.ToStream:
            argType = "Zetbox.API.ZetboxStreamWriter";
            argName = "binStream";
            methodName = "ToStream";
            serType = SerializerType.Binary;
            additionalArgs = ", HashSet<IStreamable> auxObjects, bool eagerLoadLists";
            callBaseWithAdditionalArgs = ", auxObjects, eagerLoadLists";
            break;
        case SerializerDirection.FromStream:
            argType = "Zetbox.API.ZetboxStreamReader";
            argName = "binStream";
            methodName = "FromStream";
            serType = SerializerType.Binary;
            break;
        case SerializerDirection.Export:
            argType = "System.Xml.XmlWriter";
            additionalArgs = ", string[] modules";
            callBaseWithAdditionalArgs = ", modules";
            argName = "xml";
            methodName = "Export";
            serType = SerializerType.ImportExport;
            break;
        case SerializerDirection.MergeImport:
            argType = "System.Xml.XmlReader";
            argName = "xml";
            methodName = "MergeImport";
            serType = SerializerType.ImportExport;
            break;
        default:
            throw new ArgumentOutOfRangeException("direction");
    }

    var fieldList = fields.Where(f => (f.SerializerType & serType) == serType).ToList();

    switch(direction)
    {
        case SerializerDirection.ToStream:
        case SerializerDirection.Export:

#line 84 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("        public ",  overrideAndCallBase ? "override" : "virtual" , " void ",  methodName , "(",  argType , " ",  argName , "",  additionalArgs , ")\n");
this.WriteObjects("        {\n");
#line 86 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
if (overrideAndCallBase) { 
#line 87 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            base.",  methodName , "(",  argName , "",  callBaseWithAdditionalArgs , ");\n");
#line 88 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
} else if (direction == SerializerDirection.Export && !String.IsNullOrEmpty(exportGuidBackingStore)) { 
#line 89 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            xml.WriteAttributeString(\"ExportGuid\", ",  exportGuidBackingStore , ".ToString());\n");
#line 90 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
}                                                                                
#line 91 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            // it may be only an empty shell to stand-in for unreadable data\n");
this.WriteObjects("            if (!CurrentAccessRights.HasReadRights()) return;\n");
#line 93 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
foreach(var serMember in fieldList)        
#line 94 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
{                                                                                            
#line 95 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
if (direction == SerializerDirection.Export && serMember.XmlName == "ExportGuid")        
#line 96 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
{                                                                                        
#line 97 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
continue;                                                                            
#line 98 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
}                                                                                        
#line 99 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
ApplySerializer(direction, serMember, argName);                                            
#line 100 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
}                                                                                            
#line 101 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("        }\n");
#line 102 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
break;                                                                                
#line 103 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
case SerializerDirection.MergeImport:                                                    
#line 104 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("        public ",  overrideAndCallBase ? "override" : "virtual" , " void ",  methodName , "(",  argType , " ",  argName , "",  additionalArgs , ")\n");
this.WriteObjects("        {\n");
#line 106 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
if (overrideAndCallBase) {                                                        
#line 107 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            base.",  methodName , "(",  argName , "",  callBaseWithAdditionalArgs , ");\n");
#line 108 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
}                                                                                
#line 109 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            // it may be only an empty shell to stand-in for unreadable data\n");
this.WriteObjects("            if (!CurrentAccessRights.HasReadRights()) return;\n");
#line 111 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
if(fieldList.Count > 0) {                                                                        
#line 112 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            switch (xml.NamespaceURI + \"|\" + xml.LocalName) {\n");
#line 113 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
foreach(var serMember in fieldList)    
#line 114 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
{                                                                                        
#line 115 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
ApplySerializer(direction, serMember, argName);                                        
#line 116 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
}                                                                                        
#line 117 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            }\n");
#line 118 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
}                                                                                                
#line 119 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("        }\n");
#line 120 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
break;                                                                                    
#line 121 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
case SerializerDirection.FromStream:                                                        
#line 122 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("        public ",  overrideAndCallBase ? "override" : "virtual" , " IEnumerable<IPersistenceObject> ",  methodName , "(",  argType , " ",  argName , "",  additionalArgs , ")\n");
this.WriteObjects("        {\n");
#line 124 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
if (overrideAndCallBase) {                                                        
#line 125 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            var baseResult = base.",  methodName , "(",  argName , "",  callBaseWithAdditionalArgs , ");\n");
this.WriteObjects("            var result = new List<IPersistenceObject>();\n");
#line 127 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
} else {                                                                            
#line 128 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            object baseResult = null;\n");
this.WriteObjects("            var result = new List<IPersistenceObject>();\n");
#line 130 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
}                                                                                
#line 131 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            // it may be only an empty shell to stand-in for unreadable data\n");
this.WriteObjects("            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {\n");
#line 133 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
foreach(var serMember in fieldList)    
#line 134 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
{                                                                                        
#line 135 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
ApplySerializer(direction, serMember, argName);                                        
#line 136 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
}                                                                                        
#line 137 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            } // if (CurrentAccessRights != Zetbox.API.AccessRights.None)\n");
this.WriteObjects("            return baseResult == null\n");
this.WriteObjects("                ? result.Count == 0\n");
this.WriteObjects("                    ? null\n");
this.WriteObjects("                    : result\n");
this.WriteObjects("                : baseResult.Concat(result);\n");
this.WriteObjects("        }\n");
#line 145 "P:\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
break;
        }


        }

    }
}