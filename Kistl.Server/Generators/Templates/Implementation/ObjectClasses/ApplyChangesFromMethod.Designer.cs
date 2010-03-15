using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\ApplyChangesFromMethod.cst")]
    public partial class ApplyChangesFromMethod : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected ObjectClass cls;


        public ApplyChangesFromMethod(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls)
            : base(_host)
        {
			this.ctx = ctx;
			this.cls = cls;

        }
        
        public override void Generate()
        {
#line 15 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("\r\n");
this.WriteObjects("		public override void ApplyChangesFrom(IPersistenceObject obj)\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			base.ApplyChangesFrom(obj);\r\n");
this.WriteObjects("			var other = (",  cls.Name , ")obj;\r\n");
this.WriteObjects("			var otherImpl = (",  cls.Name + Kistl.API.Helper.ImplementationSuffix , ")obj;\r\n");
this.WriteObjects("			var me = (",  cls.Name , ")this;\r\n");
this.WriteObjects("\r\n");
#line 24 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\ApplyChangesFromMethod.cst"
foreach(var prop in cls.Properties.OfType<ValueTypeProperty>().Where(p => !p.IsList).OrderBy(p => p.PropertyName))
			{

#line 27 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("			me.",  prop.PropertyName , " = other.",  prop.PropertyName , ";\r\n");
#line 29 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\ApplyChangesFromMethod.cst"
}

			foreach(var prop in cls.Properties.OfType<CompoundObjectProperty>().Where(p => !p.IsList).OrderBy(p => p.PropertyName))
			{

#line 34 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("			me.",  prop.PropertyName , " = other.",  prop.PropertyName , " != null ? (",  prop.GetPropertyTypeString() , ")other.",  prop.PropertyName , ".Clone() : null;\r\n");
#line 36 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\ApplyChangesFromMethod.cst"
}

			foreach(var prop in cls.Properties.OfType<ObjectReferenceProperty>().Where(p => !p.IsList()).OrderBy(p => p.PropertyName))
			{
				if (prop.RelationEnd.HasPersistentOrder) {

#line 42 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("			this.",  prop.PropertyName , "",  Kistl.API.Helper.PositionSuffix , " = otherImpl.",  prop.PropertyName , "",  Kistl.API.Helper.PositionSuffix , ";\r\n");
#line 44 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\ApplyChangesFromMethod.cst"
}

#line 46 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("			this._fk_",  prop.PropertyName , " = otherImpl._fk_",  prop.PropertyName , ";\r\n");
#line 48 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\ApplyChangesFromMethod.cst"
}

#line 50 "P:\Kistl\Kistl.Server\Generators\Templates\Implementation\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("		}\r\n");

        }



    }
}