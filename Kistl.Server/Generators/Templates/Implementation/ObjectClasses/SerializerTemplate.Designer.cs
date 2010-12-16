using System;
using System.Linq;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst")]
    public partial class SerializerTemplate : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected SerializerDirection direction;
		protected SerializationMembersList fields;
		protected bool overrideAndCallBase;
		protected bool writeExportGuidAttribute;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, SerializationMembersList fields, bool overrideAndCallBase, bool writeExportGuidAttribute)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Implementation.ObjectClasses.SerializerTemplate", ctx, direction, fields, overrideAndCallBase, writeExportGuidAttribute);
        }

        public SerializerTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, SerializationMembersList fields, bool overrideAndCallBase, bool writeExportGuidAttribute)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.fields = fields;
			this.overrideAndCallBase = overrideAndCallBase;
			this.writeExportGuidAttribute = writeExportGuidAttribute;

        }

        public override void Generate()
        {
#line 18 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
this.WriteObjects("\r\n");
#line 20 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
string methodName = direction.ToString();
    string argName;
    string argType;
    string additionalArgs = String.Empty;
    string callBaseWithAdditionalArgs = String.Empty;
    SerializerType serType;
    
    switch(direction){
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
    

#line 73 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
this.WriteObjects("        public ",  overrideAndCallBase ? "override" : "virtual" , " void ",  methodName , "(",  argType , " ",  argName , "",  additionalArgs , ")\r\n");
this.WriteObjects("        {\r\n");
#line 76 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
if(overrideAndCallBase)
    {

#line 78 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
this.WriteObjects("            \r\n");
this.WriteObjects("            base.",  methodName , "(",  argName , "",  callBaseWithAdditionalArgs , ");\r\n");
#line 81 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
}
    else if(direction == SerializerDirection.Export && writeExportGuidAttribute)
    {

#line 84 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
this.WriteObjects("            \r\n");
this.WriteObjects("            xml.WriteAttributeString(\"ExportGuid\", this._ExportGuid.ToString());\r\n");
#line 87 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
}

    foreach(var serMember in fields.Where(f => (f.SerializerType & serType) == serType))
    {
        if (direction == SerializerDirection.Export && serMember.XmlName == "ExportGuid")
        {
            continue;
        }
        ApplySerializer(direction, serMember, argName);
    }

#line 98 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
this.WriteObjects("        }\r\n");

        }

    }
}