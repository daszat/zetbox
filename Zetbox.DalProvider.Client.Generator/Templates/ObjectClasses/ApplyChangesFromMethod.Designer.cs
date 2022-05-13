using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.SchemaManagement;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.Client.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst")]
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
#line 34 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public override void ApplyChangesFrom(",  otherInterface , " obj)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.ApplyChangesFrom(obj);\r\n");
this.WriteObjects("            var other = (",  clsName , ")obj;\r\n");
this.WriteObjects("            var otherImpl = (",  implName , ")obj;\r\n");
this.WriteObjects("            var me = (",  clsName , ")this;\r\n");
this.WriteObjects("\r\n");
#line 42 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
// Only Client and Menory objects are applying calculated properties. NH + EF are re-calculating those properties when a depended object has changed. 
#line 43 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
foreach(var prop in cls.Properties.OfType<ValueTypeProperty>().OrderBy(p => p.Name)) { 
#line 44 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
var propName = prop.Name;                                                     
#line 45 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
if (prop.IsList) { 
#line 46 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
if (prop.HasPersistentOrder) { 
#line 47 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            SynchronizeLists(this._",  propName , "Collection, otherImpl._",  propName , "Collection);\r\n");
#line 48 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} else { 
#line 49 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            SynchronizeCollections(this._",  propName , "Collection, otherImpl._",  propName , "Collection);\r\n");
#line 50 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 51 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} else { 
#line 52 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
if(prop.IsCalculated) { 
#line 53 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            this.",  propName , " = otherImpl.",  propName , ";\r\n");
#line 54 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} else { 
#line 55 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            me.",  propName , " = other.",  propName , ";\r\n");
#line 56 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 57 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 58 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 59 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
foreach(var prop in cls.Properties.OfType<CompoundObjectProperty>()/*.Where(p => !p.IsCalculated)*/.OrderBy(p => p.Name)) { 
#line 60 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
var propName = prop.Name;                                                     
#line 61 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
if (prop.IsList) { 
#line 62 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
if (prop.HasPersistentOrder) { 
#line 63 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            SynchronizeLists(this._",  propName , "Collection, otherImpl._",  propName , "Collection);\r\n");
#line 64 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} else { 
#line 65 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            SynchronizeCollections(this._",  propName , "Collection, otherImpl._",  propName , "Collection);\r\n");
#line 66 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 67 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} else { 
#line 68 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            if (me.",  propName , " == null && other.",  propName , " != null) {\r\n");
this.WriteObjects("                me.",  propName , " = (",  prop.GetElementTypeString() , ")other.",  propName , ".Clone();\r\n");
this.WriteObjects("            } else if (me.",  propName , " != null && other.",  propName , " == null) {\r\n");
this.WriteObjects("                me.",  propName , " = null;\r\n");
this.WriteObjects("            } else if (me.",  propName , " != null && other.",  propName , " != null) {\r\n");
this.WriteObjects("                me.",  propName , ".ApplyChangesFrom(other.",  propName , ");\r\n");
this.WriteObjects("            }\r\n");
#line 75 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 76 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 77 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
foreach(var prop in cls.Properties.OfType<ObjectReferenceProperty>().Where(p => !p.IsList()).OrderBy(p => p.Name)) {
        var propName = prop.Name;                                                     
        if (prop.RelationEnd.HasPersistentOrder) {
            var positionPropertyName = Construct.ListPositionPropertyName(prop.RelationEnd);

#line 82 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            this.",  positionPropertyName , " = otherImpl.",  positionPropertyName , ";\r\n");
#line 83 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 84 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            if(this._fk_",  propName , " != otherImpl._fk_",  propName , ")\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                // On the client side it is important to notify changes as _fk_",  propName , " is also the backingstore\r\n");
this.WriteObjects("                // so, there is no need for a ReloadReferences call\r\n");
this.WriteObjects("                var oldVal = this._fk_",  propName , ";\r\n");
this.WriteObjects("                var newVal = otherImpl._fk_",  propName , ";\r\n");
this.WriteObjects("                NotifyPropertyChanging(\"",  propName , "\", oldVal, newVal);\r\n");
this.WriteObjects("                this._fk_",  propName , " = otherImpl._fk_",  propName , ";\r\n");
this.WriteObjects("                NotifyPropertyChanged(\"",  propName , "\", oldVal, newVal);\r\n");
this.WriteObjects("            }\r\n");
#line 94 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 95 "D:\Projects\zetbox\Zetbox.DalProvider.Client.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("        }\r\n");

        }

    }
}