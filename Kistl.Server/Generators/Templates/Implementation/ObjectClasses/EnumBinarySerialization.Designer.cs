using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;


namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\EnumBinarySerialization.cst")]
    public partial class EnumBinarySerialization : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected SerializerDirection direction;
		protected string streamName;
		protected EnumerationProperty prop;


        public EnumBinarySerialization(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializerDirection direction, string streamName, EnumerationProperty prop)
            : base(_host)
        {
			this.ctx = ctx;
			this.direction = direction;
			this.streamName = streamName;
			this.prop = prop;

        }
        
        public override void Generate()
        {
#line 18 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\EnumBinarySerialization.cst"
// always use the interface to get the "right" property
	string interfaceName = prop.ObjectClass.ClassName;
    string memberName = prop.PropertyName;
    string enumName =  prop.Enumeration.Module.Namespace + "." +  prop.Enumeration.ClassName;
	string methodName = direction.ToString();
	
	switch(direction){
		case SerializerDirection.ToStream:

#line 27 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\EnumBinarySerialization.cst"
this.WriteObjects("            BinarySerializer.",  methodName , "((int)((",  interfaceName , ")this).",  memberName , ", ",  streamName , ");\r\n");
#line 29 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\EnumBinarySerialization.cst"
break;
		case SerializerDirection.FromStream:

#line 32 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\EnumBinarySerialization.cst"
this.WriteObjects("            BinarySerializer.",  methodName , "Converter(v => ((",  interfaceName , ")this).",  memberName , " = (",  enumName , ")v, ",  streamName , ");\r\n");
#line 34 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\EnumBinarySerialization.cst"
break;
		default:
			throw new ArgumentOutOfRangeException("direction");
	}


        }



    }
}