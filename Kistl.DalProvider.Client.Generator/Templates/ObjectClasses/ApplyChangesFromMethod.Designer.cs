using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.Client.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst")]
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
#line 17 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public override void ApplyChangesFrom(",  otherInterface , " obj)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.ApplyChangesFrom(obj);\r\n");
this.WriteObjects("            var other = (",  clsName , ")obj;\r\n");
this.WriteObjects("            var otherImpl = (",  implName , ")obj;\r\n");
this.WriteObjects("            var me = (",  clsName , ")this;\r\n");
this.WriteObjects("\r\n");
#line 25 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
// Only Client and Menory objects are applying calculated properties. NH + EF are re-calculating those properties when a depended object has changed. 
#line 26 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
foreach(var prop in cls.Properties.OfType<ValueTypeProperty>().Where(p => !p.IsCalculated).OrderBy(p => p.Name)) { 
#line 27 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
if (prop.IsList) { 
#line 28 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
if (prop.HasPersistentOrder) { 
#line 29 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            SynchronizeLists(this._",  prop.Name , "Collection, otherImpl._",  prop.Name , "Collection);\r\n");
#line 30 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} else { 
#line 31 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            SynchronizeCollections(this._",  prop.Name , "Collection, otherImpl._",  prop.Name , "Collection);\r\n");
#line 32 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 33 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} else { 
#line 34 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
if(prop.IsCalculated) { 
#line 35 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            this._",  prop.Name , " = otherImpl._",  prop.Name , ";\r\n");
this.WriteObjects("			this._",  prop.Name , "_IsDirty = false;\r\n");
#line 37 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} else { 
#line 38 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            me.",  prop.Name , " = other.",  prop.Name , ";\r\n");
#line 39 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 40 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 41 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 42 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
foreach(var prop in cls.Properties.OfType<CompoundObjectProperty>()/*.Where(p => !p.IsCalculated)*/.OrderBy(p => p.Name)) { 
#line 43 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
if (prop.IsList) { 
#line 44 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
if (prop.HasPersistentOrder) { 
#line 45 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            SynchronizeLists(this._",  prop.Name , "Collection, otherImpl._",  prop.Name , "Collection);\r\n");
#line 46 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} else { 
#line 47 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            SynchronizeCollections(this._",  prop.Name , "Collection, otherImpl._",  prop.Name , "Collection);\r\n");
#line 48 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 49 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} else { 
#line 50 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            if (me.",  prop.Name , " == null && other.",  prop.Name , " != null) {\r\n");
this.WriteObjects("                me.",  prop.Name , " = (",  prop.GetElementTypeString() , ")other.",  prop.Name , ".Clone();\r\n");
this.WriteObjects("            } else if (me.",  prop.Name , " != null && other.",  prop.Name , " == null) {\r\n");
this.WriteObjects("                me.",  prop.Name , " = null;\r\n");
this.WriteObjects("            } else if (me.",  prop.Name , " != null && other.",  prop.Name , " != null) {\r\n");
this.WriteObjects("                me.",  prop.Name , ".ApplyChangesFrom(other.",  prop.Name , ");\r\n");
this.WriteObjects("            }\r\n");
#line 57 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 58 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 59 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
foreach(var prop in cls.Properties.OfType<ObjectReferenceProperty>().Where(p => !p.IsList()).OrderBy(p => p.Name)) {
        if (prop.RelationEnd.HasPersistentOrder) {
            var positionPropertyName = Construct.ListPositionPropertyName(prop.RelationEnd);

#line 63 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            this.",  positionPropertyName , " = otherImpl.",  positionPropertyName , ";\r\n");
#line 64 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 65 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            this._fk_",  prop.Name , " = otherImpl._fk_",  prop.Name , ";\r\n");
#line 66 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 67 "P:\Kistl\Kistl.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("        }\r\n");

        }

    }
}