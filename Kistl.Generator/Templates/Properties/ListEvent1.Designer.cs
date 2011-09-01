using System;
using Kistl.API;


namespace Kistl.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\Properties\PropertyListChangedEvent.cst")]
    public partial class PropertyListChangedEvent : Kistl.Generator.MemberTemplate
    {
		protected IKistlContext ctx;
		protected string eventName;
		protected string objType;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string eventName, string objType)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.PropertyListChangedEvent", ctx, eventName, objType);
        }

        public PropertyListChangedEvent(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string eventName, string objType)
            : base(_host)
        {
			this.ctx = ctx;
			this.eventName = eventName;
			this.objType = objType;

        }

        public override void Generate()
        {
#line 10 "P:\Kistl\Kistl.Generator\Templates\Properties\PropertyListChangedEvent.cst"
this.WriteObjects("",  GetModifiers() , " event PropertyListChangedHandler<",  objType , "> ",  eventName , ";\r\n");

        }

    }
}