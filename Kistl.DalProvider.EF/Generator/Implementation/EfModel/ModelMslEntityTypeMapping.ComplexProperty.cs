using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Arebis.CodeGeneration;

namespace Kistl.DalProvider.EF.Generator.Implementation.EfModel
{
    public class ModelMslEntityTypeMappingComplexProperty : Kistl.Server.Generators.KistlCodeTemplate
    {
        protected IKistlContext ctx;
        protected CompoundObjectProperty prop;
        protected string propertyName;
        protected string parentName;

        public static void Call(IGenerationHost host, IKistlContext ctx, Property prop, string propertyName, string parentName)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.EfModel.ModelMslEntityTypeMappingComplexProperty", ctx, prop, propertyName, parentName);
        }

        public ModelMslEntityTypeMappingComplexProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, CompoundObjectProperty prop, string propertyName, string parentName)
            : base(_host)
        {
            this.ctx = ctx;
            this.prop = prop;
            this.propertyName = propertyName;
            this.parentName = parentName;
        }

        public override void Generate()
        {
            this.WriteLine("<ComplexProperty Name=\"{0}{1}\" TypeName=\"Model.{2}\">",
                propertyName,
                Kistl.API.Helper.ImplementationSuffix,
                prop.CompoundObjectDefinition.Name
                );

            this.WriteLine("  <ScalarProperty Name=\"CompoundObject_IsNull\" ColumnName=\"{0}\" />", Construct.NestedColumnName(prop, parentName));

            string newParent = Construct.NestedColumnName(prop, parentName);
            foreach (var subProp in prop.CompoundObjectDefinition.Properties.OfType<ValueTypeProperty>().Where(p => !p.IsList).OrderBy(p => p.Name))
            {
                ModelMslEntityTypeMappingScalarProperty.Call(Host, ctx, subProp, subProp.Name, newParent);
            }

            foreach (var subProp in prop.CompoundObjectDefinition.Properties.OfType<CompoundObjectProperty>().Where(p => !p.IsList).OrderBy(p => p.Name))
            {
                ModelMslEntityTypeMappingComplexProperty.Call(Host, ctx, subProp, subProp.Name, newParent);
            }

            this.WriteLine("</ComplexProperty>");
        }
    }
}
