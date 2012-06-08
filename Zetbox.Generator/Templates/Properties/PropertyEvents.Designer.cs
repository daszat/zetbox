using System;
using Zetbox.API;


namespace Zetbox.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\Properties\PropertyEvents.cst")]
    public partial class PropertyEvents : Zetbox.Generator.MemberTemplate
    {
		protected IZetboxContext ctx;
		protected string eventName;
		protected string propType;
		protected string objType;
		protected bool hasGetters;
		protected bool hasSetters;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string eventName, string propType, string objType, bool hasGetters, bool hasSetters)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.PropertyEvents", ctx, eventName, propType, objType, hasGetters, hasSetters);
        }

        public PropertyEvents(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string eventName, string propType, string objType, bool hasGetters, bool hasSetters)
            : base(_host)
        {
			this.ctx = ctx;
			this.eventName = eventName;
			this.propType = propType;
			this.objType = objType;
			this.hasGetters = hasGetters;
			this.hasSetters = hasSetters;

        }

        public override void Generate()
        {
#line 29 "P:\zetbox\Zetbox.Generator\Templates\Properties\PropertyEvents.cst"
if (hasGetters) { 
#line 30 "P:\zetbox\Zetbox.Generator\Templates\Properties\PropertyEvents.cst"
this.WriteObjects("		",  GetModifiers() , " event PropertyGetterHandler<",  objType , ", ",  propType , "> ",  eventName , "_Getter;\r\n");
#line 31 "P:\zetbox\Zetbox.Generator\Templates\Properties\PropertyEvents.cst"
} 
#line 32 "P:\zetbox\Zetbox.Generator\Templates\Properties\PropertyEvents.cst"
if (hasSetters) { 
#line 33 "P:\zetbox\Zetbox.Generator\Templates\Properties\PropertyEvents.cst"
this.WriteObjects("		",  GetModifiers() , " event PropertyPreSetterHandler<",  objType , ", ",  propType , "> ",  eventName , "_PreSetter;\r\n");
this.WriteObjects("		",  GetModifiers() , " event PropertyPostSetterHandler<",  objType , ", ",  propType , "> ",  eventName , "_PostSetter;\r\n");
#line 35 "P:\zetbox\Zetbox.Generator\Templates\Properties\PropertyEvents.cst"
} 

        }

    }
}