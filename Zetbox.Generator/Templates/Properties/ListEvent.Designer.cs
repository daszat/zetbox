using System;
using Zetbox.API;


namespace Zetbox.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Zetbox\Zetbox.Generator\Templates\Properties\ListEvent.cst")]
    public partial class ListEvent : Zetbox.Generator.MemberTemplate
    {
		protected IZetboxContext ctx;
		protected string eventName;
		protected string propType;
		protected string objType;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string eventName, string propType, string objType)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ListEvent", ctx, eventName, propType, objType);
        }

        public ListEvent(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string eventName, string propType, string objType)
            : base(_host)
        {
			this.ctx = ctx;
			this.eventName = eventName;
			this.propType = propType;
			this.objType = objType;

        }

        public override void Generate()
        {
#line 11 "P:\Zetbox\Zetbox.Generator\Templates\Properties\ListEvent.cst"
this.WriteObjects("",  GetModifiers() , " event PropertyPostSetterHandler<",  objType , ", ",  propType , "> ",  eventName , "_PostSetter;\r\n");

        }

    }
}