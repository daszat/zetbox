using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\ObjectClasses\ReloadReferences.cst")]
    public partial class ReloadReferences : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected ObjectClass cls;


        public ReloadReferences(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls)
            : base(_host)
        {
			this.ctx = ctx;
			this.cls = cls;

        }
        
        public override void Generate()
        {
#line 15 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\ObjectClasses\ReloadReferences.cst"
this.WriteObjects("\r\n");
this.WriteObjects("		public override void ReloadReferences()\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			// Do not reload references if the current object has been deleted.\r\n");
this.WriteObjects("			// TODO: enable when MemoryContext uses MemoryDataObjects\r\n");
this.WriteObjects("			//if (this.ObjectState == DataObjectState.Deleted) return;\r\n");
#line 22 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\ObjectClasses\ReloadReferences.cst"
if (cls.BaseObjectClass != null) {

#line 24 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\ObjectClasses\ReloadReferences.cst"
this.WriteObjects("			base.ReloadReferences();\r\n");
this.WriteObjects("			\r\n");
#line 27 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\ObjectClasses\ReloadReferences.cst"
}

#line 29 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\ObjectClasses\ReloadReferences.cst"
this.WriteObjects("			// fix direct object references\r\n");
#line 31 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\ObjectClasses\ReloadReferences.cst"
foreach(var prop in cls.Properties.OfType<ObjectReferenceProperty>()
		.Where(orp => !orp.IsList())
		.OrderBy(orp => orp.ObjectClass.Name)
		.ThenBy(orp => orp.PropertyName))
	{
		Relation rel = Kistl.App.Extensions.RelationExtensions.Lookup(ctx, prop);
		RelationEnd relEnd = rel.GetEnd(prop);
        RelationEnd otherEnd = rel.GetOtherEnd(relEnd);
        
        string referencedInterface = otherEnd.Type.GetDataTypeString();
        string referencedImplementation = otherEnd.Type.GetDataTypeString() + Kistl.API.Helper.ImplementationSuffix;
        string name = prop.PropertyName;
		string efName = name + Kistl.API.Helper.ImplementationSuffix;
		string fkBackingName = "_fk_" + name;
		string fkGuidBackingName = "_fk_guid_" + name;

		ReloadOneReference.Call(Host, ctx, referencedInterface, referencedImplementation, name, efName, fkBackingName, fkGuidBackingName);
	}

#line 50 "P:\Kistl\Kistl.DalProvider.EF\Generator\Implementation\ObjectClasses\ReloadReferences.cst"
this.WriteObjects("		}");

        }



    }
}