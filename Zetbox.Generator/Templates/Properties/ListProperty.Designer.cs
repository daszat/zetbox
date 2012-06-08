using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\Properties\ListProperty.cst")]
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
#line 17 "P:\zetbox\Zetbox.Generator\Templates\Properties\ListProperty.cst"
this.WriteObjects("");
#line 33 "P:\zetbox\Zetbox.Generator\Templates\Properties\ListProperty.cst"
ApplyAttributesTemplate();

	var backingName = BackingMemberFromName(name);

#line 37 "P:\zetbox\Zetbox.Generator\Templates\Properties\ListProperty.cst"
this.WriteObjects("   		// ",  this.GetType() , "\n");
this.WriteObjects("        ",  GetModifiers() , " ",  GetPropertyTypeString() , " ",  name , "\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get\n");
this.WriteObjects("            {\n");
this.WriteObjects("                if (",  backingName , " == null)\n");
this.WriteObjects("                    ",  backingName , " = ",  GetInitialisationExpression() , ";\n");
this.WriteObjects("                return ",  backingName , ";\n");
this.WriteObjects("            }\n");
#line 47 "P:\zetbox\Zetbox.Generator\Templates\Properties\ListProperty.cst"
ApplySettor();

#line 49 "P:\zetbox\Zetbox.Generator\Templates\Properties\ListProperty.cst"
this.WriteObjects("        }\n");
this.WriteObjects("        private ",  GetBackingTypeString() , " ",  backingName , ";\n");
#line 52 "P:\zetbox\Zetbox.Generator\Templates\Properties\ListProperty.cst"
AddSerialization(serializationList, name);


	ApplyRequisitesTemplate();


        }

    }
}