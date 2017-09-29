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
    [Arebis.CodeGeneration.TemplateInfo(@"C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst")]
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
#line 34 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("\r\n");
#line 36 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
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

#line 84 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("        public ",  overrideAndCallBase ? "override" : "virtual" , " void ",  methodName , "(",  argType , " ",  argName , "",  additionalArgs , ")\r\n");
this.WriteObjects("        {\r\n");
#line 86 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
if (overrideAndCallBase) { 
#line 87 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            base.",  methodName , "(",  argName , "",  callBaseWithAdditionalArgs , ");\r\n");
#line 88 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
} else if (direction == SerializerDirection.Export && !String.IsNullOrEmpty(exportGuidBackingStore)) { 
#line 89 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            xml.WriteAttributeString(\"ExportGuid\", ",  exportGuidBackingStore , ".ToString());\r\n");
#line 90 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
}                                                                                
#line 91 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            // it may be only an empty shell to stand-in for unreadable data\r\n");
this.WriteObjects("            if (!CurrentAccessRights.HasReadRights()) return;\r\n");
#line 93 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
foreach(var serMember in fieldList)        
#line 94 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
{                                                                                            
#line 95 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
if (direction == SerializerDirection.Export && serMember.XmlName == "ExportGuid")        
#line 96 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
{                                                                                        
#line 97 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
continue;                                                                            
#line 98 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
}                                                                                        
#line 99 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
ApplySerializer(direction, serMember, argName);                                            
#line 100 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
}                                                                                            
#line 101 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("        }\r\n");
#line 102 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
break;                                                                                
#line 103 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
case SerializerDirection.MergeImport:                                                    
#line 104 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("        public ",  overrideAndCallBase ? "override" : "virtual" , " void ",  methodName , "(",  argType , " ",  argName , "",  additionalArgs , ")\r\n");
this.WriteObjects("        {\r\n");
#line 106 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
if (overrideAndCallBase) {                                                        
#line 107 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            base.",  methodName , "(",  argName , "",  callBaseWithAdditionalArgs , ");\r\n");
#line 108 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
}                                                                                
#line 109 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            // it may be only an empty shell to stand-in for unreadable data\r\n");
this.WriteObjects("            if (!CurrentAccessRights.HasReadRights()) return;\r\n");
#line 111 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
if(fieldList.Count > 0) {                                                                        
#line 112 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            switch (xml.NamespaceURI + \"|\" + xml.LocalName) {\r\n");
#line 113 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
foreach(var serMember in fieldList)    
#line 114 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
{                                                                                        
#line 115 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
ApplySerializer(direction, serMember, argName);                                        
#line 116 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
}                                                                                        
#line 117 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            }\r\n");
#line 118 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
}                                                                                                
#line 119 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("        }\r\n");
#line 120 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
break;                                                                                    
#line 121 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
case SerializerDirection.FromStream:                                                        
#line 122 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("        public ",  overrideAndCallBase ? "override" : "virtual" , " IEnumerable<IPersistenceObject> ",  methodName , "(",  argType , " ",  argName , "",  additionalArgs , ")\r\n");
this.WriteObjects("        {\r\n");
#line 124 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
if (overrideAndCallBase) {                                                        
#line 125 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            var baseResult = base.",  methodName , "(",  argName , "",  callBaseWithAdditionalArgs , ");\r\n");
this.WriteObjects("            var result = new List<IPersistenceObject>();\r\n");
#line 127 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
} else {                                                                            
#line 128 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            object baseResult = null;\r\n");
this.WriteObjects("            var result = new List<IPersistenceObject>();\r\n");
#line 130 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
}                                                                                
#line 131 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            // it may be only an empty shell to stand-in for unreadable data\r\n");
this.WriteObjects("            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {\r\n");
#line 133 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
foreach(var serMember in fieldList)    
#line 134 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
{                                                                                        
#line 135 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
ApplySerializer(direction, serMember, argName);                                        
#line 136 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
}                                                                                        
#line 137 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            } // if (CurrentAccessRights != Zetbox.API.AccessRights.None)\r\n");
this.WriteObjects("            return baseResult == null\r\n");
this.WriteObjects("                ? result.Count == 0\r\n");
this.WriteObjects("                    ? null\r\n");
this.WriteObjects("                    : result\r\n");
this.WriteObjects("                : baseResult.Concat(result);\r\n");
this.WriteObjects("        }\r\n");
#line 145 "C:\Projects\zetbox\Zetbox.Generator\Templates\Serialization\SerializerTemplate.cst"
break;
        }


        }

    }
}