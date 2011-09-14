using System;
using System.Linq;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.Serialization
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst")]
    public partial class SerializerTemplate : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected SerializerDirection direction;
		protected SerializationMembersList fields;
		protected bool overrideAndCallBase;
		protected string exportGuidBackingStore;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, SerializationMembersList fields, bool overrideAndCallBase, string exportGuidBackingStore)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Serialization.SerializerTemplate", ctx, direction, fields, overrideAndCallBase, exportGuidBackingStore);
        }

        public SerializerTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, SerializationMembersList fields, bool overrideAndCallBase, string exportGuidBackingStore)
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
#line 18 "P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("\r\n");
#line 20 "P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst"
string methodName = direction.ToString();
    string argName;
    string argType;
    string additionalArgs = String.Empty;
    string callBaseWithAdditionalArgs = String.Empty;
    SerializerType serType;

    switch(direction)
    {
        case SerializerDirection.ToStream:
            argType = "System.IO.BinaryWriter";
            argName = "binStream";
            methodName = "ToStream";
            serType = SerializerType.Binary;
            additionalArgs = ", HashSet<IStreamable> auxObjects, bool eagerLoadLists";
            callBaseWithAdditionalArgs = ", auxObjects, eagerLoadLists";
            break;
        case SerializerDirection.FromStream:
            argType = "System.IO.BinaryReader";
            argName = "binStream";
            methodName = "FromStream";
            serType = SerializerType.Binary;
            break;
        case SerializerDirection.ToXmlStream:
            argType = "System.Xml.XmlWriter";
            argName = "xml";
            methodName = "ToStream";
            serType = SerializerType.Xml;
            break;
        case SerializerDirection.FromXmlStream:
            argType = "System.Xml.XmlReader";
            argName = "xml";
            methodName = "FromStream";
            serType = SerializerType.Xml;
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

    switch(direction)
    {
        case SerializerDirection.ToStream:
        case SerializerDirection.ToXmlStream:
        case SerializerDirection.Export:
        case SerializerDirection.MergeImport:

#line 80 "P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("        public ",  overrideAndCallBase ? "override" : "virtual" , " void ",  methodName , "(",  argType , " ",  argName , "",  additionalArgs , ")\r\n");
this.WriteObjects("        {\r\n");
#line 83 "P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst"
if (overrideAndCallBase)
    {

#line 86 "P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            base.",  methodName , "(",  argName , "",  callBaseWithAdditionalArgs , ");\r\n");
#line 88 "P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst"
}
    else if (direction == SerializerDirection.Export && !String.IsNullOrEmpty(exportGuidBackingStore))
    {

#line 92 "P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            xml.WriteAttributeString(\"ExportGuid\", ",  exportGuidBackingStore , ".ToString());\r\n");
#line 94 "P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst"
}

#line 96 "P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            if (!CurrentAccessRights.HasReadRights()) return;\r\n");
#line 98 "P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst"
foreach(var serMember in fields.Where(f => (f.SerializerType & serType) == serType))
    {
        if (direction == SerializerDirection.Export && serMember.XmlName == "ExportGuid")
        {
            continue;
        }
        ApplySerializer(direction, serMember, argName);
    }

#line 107 "P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("        }\r\n");
#line 109 "P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst"
break;
        case SerializerDirection.FromStream:
        case SerializerDirection.FromXmlStream:

#line 113 "P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("        public ",  overrideAndCallBase ? "override" : "virtual" , " IEnumerable<IPersistenceObject> ",  methodName , "(",  argType , " ",  argName , "",  additionalArgs , ")\r\n");
this.WriteObjects("        {\r\n");
#line 116 "P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst"
if (overrideAndCallBase)
    {

#line 119 "P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            var baseResult = base.",  methodName , "(",  argName , "",  callBaseWithAdditionalArgs , ");\r\n");
this.WriteObjects("            var result = new List<IPersistenceObject>();\r\n");
#line 122 "P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst"
}
    else
    {

#line 126 "P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            object baseResult = null;\r\n");
this.WriteObjects("            var result = new List<IPersistenceObject>();\r\n");
#line 129 "P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst"
}
    
    if (!overrideAndCallBase && direction == SerializerDirection.Export && !String.IsNullOrEmpty(exportGuidBackingStore))
    {

#line 134 "P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            xml.WriteAttributeString(\"ExportGuid\", ",  exportGuidBackingStore , ".ToString());\r\n");
#line 136 "P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst"
}

#line 138 "P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            if (CurrentAccessRights != Kistl.API.AccessRights.None) {\r\n");
#line 140 "P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst"
foreach(var serMember in fields.Where(f => (f.SerializerType & serType) == serType))
    {
        if (direction == SerializerDirection.Export && serMember.XmlName == "ExportGuid")
        {
            continue;
        }
        ApplySerializer(direction, serMember, argName);
    }

#line 149 "P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst"
this.WriteObjects("            } // if (CurrentAccessRights != Kistl.API.AccessRights.None)\r\n");
this.WriteObjects("			return baseResult == null\r\n");
this.WriteObjects("                ? result.Count == 0\r\n");
this.WriteObjects("                    ? null\r\n");
this.WriteObjects("                    : result\r\n");
this.WriteObjects("                : baseResult.Concat(result);\r\n");
this.WriteObjects("        }\r\n");
#line 157 "P:\Kistl\Kistl.Generator\Templates\Serialization\SerializerTemplate.cst"
break;
        }


        }

    }
}