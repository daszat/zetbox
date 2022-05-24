using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.SchemaManagement;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst")]
    public partial class ApplyChangesFromMethod : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected string otherInterface;
		protected DataType cls;
		protected string clsName;
		protected string implName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string otherInterface, DataType cls, string clsName, string implName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.ApplyChangesFromMethod", ctx, otherInterface, cls, clsName, implName);
        }

        public ApplyChangesFromMethod(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string otherInterface, DataType cls, string clsName, string implName)
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
#line 35 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public override void ApplyChangesFrom(",  otherInterface , " obj)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.ApplyChangesFrom(obj);\r\n");
this.WriteObjects("            var other = (",  clsName , ")obj;\r\n");
this.WriteObjects("            var otherImpl = (",  implName , ")obj;\r\n");
this.WriteObjects("            var me = (",  clsName , ")this;\r\n");
this.WriteObjects("\r\n");
#line 43 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
foreach(var prop in cls.Properties.OfType<ValueTypeProperty>().Where(p => !p.IsCalculated && !p.IsList).OrderBy(p => p.Name)) { 
#line 44 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
var propName = prop.Name;                                                                                                  
#line 45 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            me.",  propName , " = other.",  propName , ";\r\n");
#line 46 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 47 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
foreach(var prop in cls.Properties.OfType<CompoundObjectProperty>().Where(p => !p.IsList /* && !p.IsCalculated */).OrderBy(p => p.Name)) { 
#line 48 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
var propName = prop.Name;                                                                                                  
#line 49 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            if (me.",  propName , " == null && other.",  propName , " != null) {\r\n");
this.WriteObjects("                me.",  propName , " = (",  prop.GetElementTypeString() , ")other.",  propName , ".Clone();\r\n");
this.WriteObjects("            } else if (me.",  propName , " != null && other.",  propName , " == null) {\r\n");
this.WriteObjects("                me.",  propName , " = null;\r\n");
this.WriteObjects("            } else if (me.",  propName , " != null && other.",  propName , " != null) {\r\n");
this.WriteObjects("                me.",  propName , ".ApplyChangesFrom(other.",  propName , ");\r\n");
this.WriteObjects("            }\r\n");
#line 56 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 57 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
foreach(var prop in cls.Properties.OfType<ObjectReferenceProperty>().Where(p => !p.IsList().Result).OrderBy(p => p.Name)) {
		var propName = prop.Name;
        if (prop.RelationEnd.HasPersistentOrder) {
            var positionPropertyName = Construct.ListPositionPropertyName(prop.RelationEnd);

#line 62 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            this.",  positionPropertyName , " = otherImpl.",  positionPropertyName , ";\r\n");
#line 63 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 64 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            this._fk_",  propName , " = otherImpl._fk_",  propName , ";\r\n");
#line 65 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 66 "D:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("        }\r\n");

        }

    }
}