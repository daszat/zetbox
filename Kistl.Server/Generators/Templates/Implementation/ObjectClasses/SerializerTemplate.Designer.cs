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
	string argModules = "";
	string callBaseWithModules = "";
	SerializerType serType;
	
	switch(direction){
		case SerializerDirection.ToStream:
			argType = "System.IO.BinaryWriter";
			argName = "binStream";
			methodName = "ToStream";
			serType = SerializerType.Binary;
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
			argModules = ", string[] modules";
			callBaseWithModules = ", modules";
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
	

#line 71 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
this.WriteObjects("        public ",  overrideAndCallBase ? "override" : "virtual" , " void ",  methodName , "(",  argType , " ",  argName , "",  argModules , ")\r\n");
this.WriteObjects("        {\r\n");
#line 74 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
if(overrideAndCallBase)
	{

#line 76 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
this.WriteObjects("			\r\n");
this.WriteObjects("            base.",  methodName , "(",  argName , "",  callBaseWithModules , ");\r\n");
#line 79 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
}
	else if(direction == SerializerDirection.Export && writeExportGuidAttribute)
	{

#line 82 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
this.WriteObjects("			\r\n");
this.WriteObjects("			xml.WriteAttributeString(\"ExportGuid\", this.ExportGuid.ToString());\r\n");
#line 85 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
}

	foreach(var serMember in fields.Where(f => (f.SerializerType & serType) == serType))
	{
	    ApplySerializer(direction, serMember, argName);
	}

#line 92 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
this.WriteObjects("        }\r\n");

        }



    }
}