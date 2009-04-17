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


        public SerializerTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, SerializationMembersList fields)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.fields = fields;

        }
        
        public override void Generate()
        {
#line 16 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
this.WriteObjects("\r\n");
#line 18 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
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
			serType = SerializerType.Binary;
			break;
		case SerializerDirection.FromStream:
			argType = "System.IO.BinaryReader";
			argName = "binStream";
			serType = SerializerType.Binary;
			break;
		case SerializerDirection.ToXmlStream:
			argType = "System.Xml.XmlWriter";
			argModules = ", string[] modules";
			callBaseWithModules = ", modules";
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
		default:
			throw new ArgumentOutOfRangeException("direction");
	}
	

#line 55 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
this.WriteObjects("        public override void ",  methodName , "(",  argType , " ",  argName , "",  argModules , ")\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.",  methodName , "(",  argName , "",  callBaseWithModules , ");\r\n");
#line 59 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
foreach(var serMember in fields.Where(f => (f.SerializerType & serType) == serType))
	{
	    ApplySerializer(direction, serMember, argName);
	}

#line 64 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\SerializerTemplate.cst"
this.WriteObjects("        }\r\n");

        }



    }
}