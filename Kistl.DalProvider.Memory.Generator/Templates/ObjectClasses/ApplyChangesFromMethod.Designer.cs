using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.Memory.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst")]
    public partial class ApplyChangesFromMethod : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected string otherInterface;
		protected DataType cls;
		protected string clsName;
		protected string implName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string otherInterface, DataType cls, string clsName, string implName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.ApplyChangesFromMethod", ctx, otherInterface, cls, clsName, implName);
        }

        public ApplyChangesFromMethod(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string otherInterface, DataType cls, string clsName, string implName)
            : base(_host)
        {
			this.ctx = ctx;
			this.otherInterface = otherInterface;
			this.cls = cls;
			this.clsName = clsName;
			this.implName = implName;

        }

        public override void Generate()
        {
#line 17 "P:\Kistl\Kistl.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public override void ApplyChangesFrom(",  otherInterface , " obj)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.ApplyChangesFrom(obj);\r\n");
this.WriteObjects("            var other = (",  clsName , ")obj;\r\n");
this.WriteObjects("            var otherImpl = (",  implName , ")obj;\r\n");
this.WriteObjects("            var me = (",  clsName , ")this;\r\n");
this.WriteObjects("\r\n");
#line 25 "P:\Kistl\Kistl.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
foreach(var prop in cls.Properties.OfType<ValueTypeProperty>().Where(p => !p.IsCalculated).OrderBy(p => p.Name)) { 
#line 26 "P:\Kistl\Kistl.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
if (prop.IsList) { 
#line 27 "P:\Kistl\Kistl.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            SynchronizeLists(this._",  prop.Name , "Collection, otherImpl._",  prop.Name , "Collection);\r\n");
#line 28 "P:\Kistl\Kistl.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} else { 
#line 29 "P:\Kistl\Kistl.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            me.",  prop.Name , " = other.",  prop.Name , ";\r\n");
#line 30 "P:\Kistl\Kistl.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 31 "P:\Kistl\Kistl.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 32 "P:\Kistl\Kistl.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
foreach(var prop in cls.Properties.OfType<CompoundObjectProperty>()/*.Where(p => !p.IsCalculated)*/.OrderBy(p => p.Name)) { 
#line 33 "P:\Kistl\Kistl.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
if (prop.IsList) { 
#line 34 "P:\Kistl\Kistl.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            SynchronizeLists(this._",  prop.Name , "Collection, otherImpl._",  prop.Name , "Collection);\r\n");
#line 35 "P:\Kistl\Kistl.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} else { 
#line 36 "P:\Kistl\Kistl.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            if (me.",  prop.Name , " == null && other.",  prop.Name , " != null) {\r\n");
this.WriteObjects("                me.",  prop.Name , " = (",  prop.GetPropertyTypeString() , ")other.",  prop.Name , ".Clone();\r\n");
this.WriteObjects("            } else if (me.",  prop.Name , " != null && other.",  prop.Name , " == null) {\r\n");
this.WriteObjects("                me.",  prop.Name , " = null;\r\n");
this.WriteObjects("            } else if (me.",  prop.Name , " != null && other.",  prop.Name , " != null) {\r\n");
this.WriteObjects("                me.",  prop.Name , ".ApplyChangesFrom(other.",  prop.Name , ");\r\n");
this.WriteObjects("            }\r\n");
#line 43 "P:\Kistl\Kistl.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 44 "P:\Kistl\Kistl.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 45 "P:\Kistl\Kistl.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
foreach(var prop in cls.Properties.OfType<ObjectReferenceProperty>().Where(p => !p.IsList()).OrderBy(p => p.Name)) {
        if (prop.RelationEnd.HasPersistentOrder) {
            var positionPropertyName = Construct.ListPositionPropertyName(prop.RelationEnd);

#line 49 "P:\Kistl\Kistl.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            this.",  positionPropertyName , " = otherImpl.",  positionPropertyName , ";\r\n");
#line 50 "P:\Kistl\Kistl.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 51 "P:\Kistl\Kistl.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            this._fk_",  prop.Name , " = otherImpl._fk_",  prop.Name , ";\r\n");
#line 52 "P:\Kistl\Kistl.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 53 "P:\Kistl\Kistl.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("        }\r\n");

        }

    }
}