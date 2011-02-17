using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Properties/ListProperty.cst")]
    public partial class ListProperty : Kistl.Generator.MemberTemplate
    {
		protected IKistlContext ctx;
		protected Serialization.SerializationMembersList serializationList;
		protected DataType containingType;
		protected String name;
		protected Property property;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Serialization.SerializationMembersList serializationList, DataType containingType, String name, Property property)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ListProperty", ctx, serializationList, containingType, name, property);
        }

        public ListProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Serialization.SerializationMembersList serializationList, DataType containingType, String name, Property property)
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
#line 17 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Properties/ListProperty.cst"
ApplyAttributesTemplate();

	var backingName = BackingMemberFromName(name);

#line 21 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Properties/ListProperty.cst"
this.WriteObjects("   		// ",  this.GetType() , "\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  GetPropertyTypeString() , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (",  backingName , " == null)\r\n");
this.WriteObjects("                    ",  backingName , " = ",  GetInitialisationExpression() , ";\r\n");
this.WriteObjects("                return ",  backingName , ";\r\n");
this.WriteObjects("            }\r\n");
#line 31 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Properties/ListProperty.cst"
ApplySettor();

#line 33 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Properties/ListProperty.cst"
this.WriteObjects("        }\r\n");
this.WriteObjects("        private ",  GetBackingTypeString() , " ",  backingName , ";\r\n");
#line 36 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Properties/ListProperty.cst"
AddSerialization(serializationList, name);


	ApplyRequisitesTemplate();


        }

    }
}