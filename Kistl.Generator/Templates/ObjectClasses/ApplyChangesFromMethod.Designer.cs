using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/ApplyChangesFromMethod.cst")]
    public partial class ApplyChangesFromMethod : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected DataType cls;
		protected string clsName;
		protected string implName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, DataType cls, string clsName, string implName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.ApplyChangesFromMethod", ctx, cls, clsName, implName);
        }

        public ApplyChangesFromMethod(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, DataType cls, string clsName, string implName)
            : base(_host)
        {
			this.ctx = ctx;
			this.cls = cls;
			this.clsName = clsName;
			this.implName = implName;

        }

        public override void Generate()
        {
#line 17 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/ApplyChangesFromMethod.cst"
this.WriteObjects("\r\n");
this.WriteObjects("		public override void ApplyChangesFrom(IPersistenceObject obj)\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			base.ApplyChangesFrom(obj);\r\n");
this.WriteObjects("			var other = (",  clsName , ")obj;\r\n");
this.WriteObjects("			var otherImpl = (",  implName , ")obj;\r\n");
this.WriteObjects("			var me = (",  clsName , ")this;\r\n");
this.WriteObjects("\r\n");
#line 26 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/ApplyChangesFromMethod.cst"
foreach(var prop in cls.Properties.OfType<ValueTypeProperty>().Where(p => !p.IsList).OrderBy(p => p.Name))
			{

#line 29 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/ApplyChangesFromMethod.cst"
this.WriteObjects("			me.",  prop.Name , " = other.",  prop.Name , ";\r\n");
#line 31 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/ApplyChangesFromMethod.cst"
}

			foreach(var prop in cls.Properties.OfType<CompoundObjectProperty>().Where(p => !p.IsList).OrderBy(p => p.Name))
			{

#line 36 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/ApplyChangesFromMethod.cst"
this.WriteObjects("			me.",  prop.Name , " = other.",  prop.Name , " != null ? (",  prop.GetPropertyTypeString() , ")other.",  prop.Name , ".Clone() : null;\r\n");
#line 38 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/ApplyChangesFromMethod.cst"
}

			foreach(var prop in cls.Properties.OfType<ObjectReferenceProperty>().Where(p => !p.IsList()).OrderBy(p => p.Name))
			{
				if (prop.RelationEnd.HasPersistentOrder) {
					var positionPropertyName = Construct.ListPositionPropertyName(prop.RelationEnd);

#line 45 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/ApplyChangesFromMethod.cst"
this.WriteObjects("			this.",  positionPropertyName , " = otherImpl.",  positionPropertyName , ";\r\n");
#line 47 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/ApplyChangesFromMethod.cst"
}

#line 49 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/ApplyChangesFromMethod.cst"
this.WriteObjects("			this._fk_",  prop.Name , " = otherImpl._fk_",  prop.Name , ";\r\n");
#line 51 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/ApplyChangesFromMethod.cst"
}

#line 53 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/ApplyChangesFromMethod.cst"
this.WriteObjects("		}\r\n");

        }

    }
}