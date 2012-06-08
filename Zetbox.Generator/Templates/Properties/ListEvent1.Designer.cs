using System;
using Zetbox.API;


namespace Zetbox.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Zetbox\Zetbox.Generator\Templates\Properties\PropertyListChangedEvent.cst")]
    public partial class PropertyListChangedEvent : Zetbox.Generator.MemberTemplate
    {
		protected IZetboxContext ctx;
		protected string eventName;
		protected string objType;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string eventName, string objType)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.PropertyListChangedEvent", ctx, eventName, objType);
        }

        public PropertyListChangedEvent(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string eventName, string objType)
            : base(_host)
        {
			this.ctx = ctx;
			this.eventName = eventName;
			this.objType = objType;

        }

        public override void Generate()
        {
#line 10 "P:\Zetbox\Zetbox.Generator\Templates\Properties\PropertyListChangedEvent.cst"
this.WriteObjects("",  GetModifiers() , " event PropertyListChangedHandler<",  objType , "> ",  eventName , ";\r\n");

        }

    }
}