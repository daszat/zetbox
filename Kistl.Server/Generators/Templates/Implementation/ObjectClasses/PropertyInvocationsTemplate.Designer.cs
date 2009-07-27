using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\PropertyInvocationsTemplate.cst")]
    public partial class PropertyInvocationsTemplate : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Property prop;


        public PropertyInvocationsTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Property prop)
            : base(_host)
        {
			this.ctx = ctx;
			this.prop = prop;

        }
        
        public override void Generate()
        {
#line 15 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\PropertyInvocationsTemplate.cst"
string eventName = "On" + prop.PropertyName;
	string propType = prop.ReferencedTypeAsCSharp();
	string objType = prop.ObjectClass.GetDataTypeString();


#line 20 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\PropertyInvocationsTemplate.cst"
this.WriteObjects("		public event PropertyGetterHandler<",  objType , ", ",  propType , "> ",  eventName , "_Getter;\r\n");
this.WriteObjects("		public event PropertyPreSetterHandler<",  objType , ", ",  propType , "> ",  eventName , "_PreSetter;\r\n");
this.WriteObjects("		public event PropertyPostSetterHandler<",  objType , ", ",  propType , "> ",  eventName , "_PostSetter;");

        }



    }
}