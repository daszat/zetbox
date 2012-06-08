using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.Memory.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst")]
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
#line 17 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("");
#line 33 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public override void ApplyChangesFrom(",  otherInterface , " obj)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.ApplyChangesFrom(obj);\r\n");
this.WriteObjects("            var other = (",  clsName , ")obj;\r\n");
this.WriteObjects("            var otherImpl = (",  implName , ")obj;\r\n");
this.WriteObjects("            var me = (",  clsName , ")this;\r\n");
this.WriteObjects("\r\n");
#line 41 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
// Only Client and Menory objects are applying calculated properties. NH + EF are re-calculating those properties when a depended object has changed. 
#line 42 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
foreach(var prop in cls.Properties.OfType<ValueTypeProperty>().OrderBy(p => p.Name)) { 
#line 43 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
if (prop.IsList) { 
#line 44 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
if (prop.HasPersistentOrder) { 
#line 45 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            SynchronizeLists(this._",  prop.Name , "Collection, otherImpl._",  prop.Name , "Collection);\r\n");
#line 46 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} else { 
#line 47 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            SynchronizeCollections(this._",  prop.Name , "Collection, otherImpl._",  prop.Name , "Collection);\r\n");
#line 48 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 49 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} else { 
#line 50 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
if(prop.IsCalculated) { 
#line 51 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            this.",  prop.Name , " = otherImpl.",  prop.Name , ";\r\n");
#line 52 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} else { 
#line 53 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            me.",  prop.Name , " = other.",  prop.Name , ";\r\n");
#line 54 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 55 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 56 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 57 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
foreach(var prop in cls.Properties.OfType<CompoundObjectProperty>()/*.Where(p => !p.IsCalculated)*/.OrderBy(p => p.Name)) { 
#line 58 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
if (prop.IsList) { 
#line 59 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
if (prop.HasPersistentOrder) { 
#line 60 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            SynchronizeLists(this._",  prop.Name , "Collection, otherImpl._",  prop.Name , "Collection);\r\n");
#line 61 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} else { 
#line 62 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            SynchronizeCollections(this._",  prop.Name , "Collection, otherImpl._",  prop.Name , "Collection);\r\n");
#line 63 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 64 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} else { 
#line 65 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            if (me.",  prop.Name , " == null && other.",  prop.Name , " != null) {\r\n");
this.WriteObjects("                me.",  prop.Name , " = (",  prop.GetElementTypeString() , ")other.",  prop.Name , ".Clone();\r\n");
this.WriteObjects("            } else if (me.",  prop.Name , " != null && other.",  prop.Name , " == null) {\r\n");
this.WriteObjects("                me.",  prop.Name , " = null;\r\n");
this.WriteObjects("            } else if (me.",  prop.Name , " != null && other.",  prop.Name , " != null) {\r\n");
this.WriteObjects("                me.",  prop.Name , ".ApplyChangesFrom(other.",  prop.Name , ");\r\n");
this.WriteObjects("            }\r\n");
#line 72 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 73 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 74 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
foreach(var prop in cls.Properties.OfType<ObjectReferenceProperty>().Where(p => !p.IsList()).OrderBy(p => p.Name)) {
        if (prop.RelationEnd.HasPersistentOrder) {
            var positionPropertyName = Construct.ListPositionPropertyName(prop.RelationEnd);

#line 78 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            this.",  positionPropertyName , " = otherImpl.",  positionPropertyName , ";\r\n");
#line 79 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 80 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            this._fk_",  prop.Name , " = otherImpl._fk_",  prop.Name , ";\r\n");
#line 81 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 82 "P:\zetbox\Zetbox.DalProvider.Memory.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("        }\r\n");

        }

    }
}