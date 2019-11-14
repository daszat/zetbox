using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ListProperty.cst")]
    public partial class ListProperty : Zetbox.Generator.MemberTemplate
    {
		protected IZetboxContext ctx;
		protected Serialization.SerializationMembersList serializationList;
		protected DataType containingType;
		protected String name;
		protected Property property;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Serialization.SerializationMembersList serializationList, DataType containingType, String name, Property property)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ListProperty", ctx, serializationList, containingType, name, property);
        }

        public ListProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Serialization.SerializationMembersList serializationList, DataType containingType, String name, Property property)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.containingType = containingType;
			this.name = name;
			this.property = property;

        }

        public override void Generate()
        {
#line 33 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ListProperty.cst"
ApplyAttributesTemplate();

	var backingName = BackingMemberFromName(name);

#line 37 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ListProperty.cst"
this.WriteObjects("   		// ",  this.GetType() , "\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  GetPropertyTypeString() , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (",  backingName , " == null)\r\n");
this.WriteObjects("                    ",  backingName , " = ",  GetInitialisationExpression() , ";\r\n");
this.WriteObjects("                return ",  backingName , ";\r\n");
this.WriteObjects("            }\r\n");
#line 47 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ListProperty.cst"
ApplySettor();

#line 49 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ListProperty.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("        private ",  GetBackingTypeString() , " ",  backingName , ";\r\n");
#line 52 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\ListProperty.cst"
AddSerialization(serializationList, name);


	ApplyRequisitesTemplate();


        }

    }
}