using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst")]
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
#line 17 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public override void ApplyChangesFrom(",  otherInterface , " obj)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.ApplyChangesFrom(obj);\r\n");
this.WriteObjects("            var other = (",  clsName , ")obj;\r\n");
this.WriteObjects("            var otherImpl = (",  implName , ")obj;\r\n");
this.WriteObjects("            var me = (",  clsName , ")this;\r\n");
this.WriteObjects("\r\n");
#line 25 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
foreach(var prop in cls.Properties.OfType<ValueTypeProperty>().Where(p => !p.IsCalculated).OrderBy(p => p.Name)) { 
#line 26 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
if (prop.IsList && prop.HasPersistentOrder) { 
#line 27 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            SynchronizeLists(this.",  prop.Name , "Collection, otherImpl.",  prop.Name , "Collection);\r\n");
#line 28 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} else if (prop.IsList && !prop.HasPersistentOrder) { 
#line 29 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            SynchronizeCollections(this.",  prop.Name , "Collection, otherImpl.",  prop.Name , "Collection);\r\n");
#line 30 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} else { 
#line 31 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            me.",  prop.Name , " = other.",  prop.Name , ";\r\n");
#line 32 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 33 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 34 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
foreach(var prop in cls.Properties.OfType<CompoundObjectProperty>()/*.Where(p => !p.IsCalculated)*/.OrderBy(p => p.Name)) { 
#line 35 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
if (prop.IsList && prop.HasPersistentOrder) { 
#line 36 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            SynchronizeLists(this.",  prop.Name , "Collection, otherImpl.",  prop.Name , "Collection);\r\n");
#line 37 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} else if (prop.IsList && !prop.HasPersistentOrder) { 
#line 38 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            SynchronizeCollections(this.",  prop.Name , "Collection, otherImpl.",  prop.Name , "Collection);\r\n");
#line 39 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} else { 
#line 40 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            if (me.",  prop.Name , " == null && other.",  prop.Name , " != null) {\r\n");
this.WriteObjects("                me.",  prop.Name , " = (",  prop.GetElementTypeString() , ")other.",  prop.Name , ".Clone();\r\n");
this.WriteObjects("            } else if (me.",  prop.Name , " != null && other.",  prop.Name , " == null) {\r\n");
this.WriteObjects("                me.",  prop.Name , " = null;\r\n");
this.WriteObjects("            } else if (me.",  prop.Name , " != null && other.",  prop.Name , " != null) {\r\n");
this.WriteObjects("                me.",  prop.Name , ".ApplyChangesFrom(other.",  prop.Name , ");\r\n");
this.WriteObjects("            }\r\n");
#line 47 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 48 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 49 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
foreach(var prop in cls.Properties.OfType<ObjectReferenceProperty>().Where(p => !p.IsList()).OrderBy(p => p.Name)) {
        if (prop.RelationEnd.HasPersistentOrder) {
            var positionPropertyName = Construct.ListPositionPropertyName(prop.RelationEnd);

#line 53 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            this.",  positionPropertyName , " = otherImpl.",  positionPropertyName , ";\r\n");
#line 54 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 55 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("            this._fk_",  prop.Name , " = otherImpl._fk_",  prop.Name , ";\r\n");
#line 56 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
} 
#line 57 "P:\Zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\ObjectClasses\ApplyChangesFromMethod.cst"
this.WriteObjects("        }\r\n");

        }

    }
}