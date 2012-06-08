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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst")]
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
#line 18 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("\r\n");
#line 20 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
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

#line 68 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("        public ",  overrideAndCallBase ? "override" : "virtual" , " void ",  methodName , "(",  argType , " ",  argName , "",  additionalArgs , ")\r\n");
this.WriteObjects("        {\r\n");
#line 70 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
if (overrideAndCallBase) { 
#line 71 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            base.",  methodName , "(",  argName , "",  callBaseWithAdditionalArgs , ");\r\n");
#line 72 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
} else if (direction == SerializerDirection.Export && !String.IsNullOrEmpty(exportGuidBackingStore)) { 
#line 73 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            xml.WriteAttributeString(\"ExportGuid\", ",  exportGuidBackingStore , ".ToString());\r\n");
#line 74 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
}                                                                                
#line 75 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            // it may be only an empty shell to stand-in for unreadable data\r\n");
this.WriteObjects("            if (!CurrentAccessRights.HasReadRights()) return;\r\n");
#line 77 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
foreach(var serMember in fieldList)        
#line 78 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
{                                                                                            
#line 79 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
if (direction == SerializerDirection.Export && serMember.XmlName == "ExportGuid")        
#line 80 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
{                                                                                        
#line 81 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
continue;                                                                            
#line 82 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
}                                                                                        
#line 83 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
ApplySerializer(direction, serMember, argName);                                            
#line 84 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
}                                                                                            
#line 85 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("        }\r\n");
#line 86 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
break;                                                                                
#line 87 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
case SerializerDirection.MergeImport:                                                    
#line 88 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("        public ",  overrideAndCallBase ? "override" : "virtual" , " void ",  methodName , "(",  argType , " ",  argName , "",  additionalArgs , ")\r\n");
this.WriteObjects("        {\r\n");
#line 90 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
if (overrideAndCallBase) {                                                        
#line 91 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            base.",  methodName , "(",  argName , "",  callBaseWithAdditionalArgs , ");\r\n");
#line 92 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
}                                                                                
#line 93 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            // it may be only an empty shell to stand-in for unreadable data\r\n");
this.WriteObjects("            if (!CurrentAccessRights.HasReadRights()) return;\r\n");
#line 95 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
if(fieldList.Count > 0) {                                                                        
#line 96 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            switch (xml.NamespaceURI + \"|\" + xml.LocalName) {\r\n");
#line 97 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
foreach(var serMember in fieldList)    
#line 98 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
{                                                                                        
#line 99 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
ApplySerializer(direction, serMember, argName);                                        
#line 100 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
}                                                                                        
#line 101 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            }\r\n");
#line 102 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
}                                                                                                
#line 103 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("        }\r\n");
#line 104 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
break;                                                                                    
#line 105 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
case SerializerDirection.FromStream:                                                        
#line 106 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("        public ",  overrideAndCallBase ? "override" : "virtual" , " IEnumerable<IPersistenceObject> ",  methodName , "(",  argType , " ",  argName , "",  additionalArgs , ")\r\n");
this.WriteObjects("        {\r\n");
#line 108 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
if (overrideAndCallBase) {                                                        
#line 109 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            var baseResult = base.",  methodName , "(",  argName , "",  callBaseWithAdditionalArgs , ");\r\n");
this.WriteObjects("            var result = new List<IPersistenceObject>();\r\n");
#line 111 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
} else {                                                                            
#line 112 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            object baseResult = null;\r\n");
this.WriteObjects("            var result = new List<IPersistenceObject>();\r\n");
#line 114 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
}                                                                                
#line 115 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            // it may be only an empty shell to stand-in for unreadable data\r\n");
this.WriteObjects("            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {\r\n");
#line 117 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
foreach(var serMember in fieldList)    
#line 118 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
{                                                                                        
#line 119 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
ApplySerializer(direction, serMember, argName);                                        
#line 120 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
}                                                                                        
#line 121 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            } // if (CurrentAccessRights != Zetbox.API.AccessRights.None)\r\n");
this.WriteObjects("            return baseResult == null\r\n");
this.WriteObjects("                ? result.Count == 0\r\n");
this.WriteObjects("                    ? null\r\n");
this.WriteObjects("                    : result\r\n");
this.WriteObjects("                : baseResult.Concat(result);\r\n");
this.WriteObjects("        }\r\n");
#line 129 "P:\Zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
break;
        }


        }

    }
}