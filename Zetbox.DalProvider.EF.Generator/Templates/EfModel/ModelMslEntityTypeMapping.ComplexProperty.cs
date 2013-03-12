// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arebis.CodeGeneration;
using Zetbox.API;
using Zetbox.API.SchemaManagement;
using Zetbox.App.Base;
using Zetbox.Generator;

namespace Zetbox.DalProvider.Ef.Generator.Templates.EfModel
{
    public class ModelMslEntityTypeMappingComplexProperty : Zetbox.Generator.ResourceTemplate
    {
        protected IZetboxContext ctx;
        protected CompoundObjectProperty prop;
        protected string propNameOverride;
        protected string parentName;

        public static void Call(IGenerationHost host, IZetboxContext ctx, Property prop, string parentName)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("EfModel.ModelMslEntityTypeMappingComplexProperty", ctx, prop, null, parentName);
        }

        public static void Call(IGenerationHost host, IZetboxContext ctx, Property prop, string propNameOverride, string parentName)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("EfModel.ModelMslEntityTypeMappingComplexProperty", ctx, prop, propNameOverride, parentName);
        }

        public ModelMslEntityTypeMappingComplexProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, CompoundObjectProperty prop, string propNameOverride, string parentName)
            : base(_host)
        {
            this.ctx = ctx;
            this.prop = prop;
            this.propNameOverride = propNameOverride;
            this.parentName = parentName;
        }

        public override void Generate()
        {
            this.WriteLine("          <ComplexProperty Name=\"{0}{1}\" TypeName=\"Model.{2}EfImpl\">",
                propNameOverride ?? prop.Name,
                ImplementationPropertySuffix,
                prop.CompoundObjectDefinition.Name
                );

            string newParent = Construct.ColumnName(prop, parentName);
            foreach (var subProp in prop.CompoundObjectDefinition.Properties.OfType<ValueTypeProperty>().Where(p => !p.IsList).OrderBy(p => p.Name))
            {
                ModelMslEntityTypeMappingScalarProperty.Call(Host, ctx, subProp, newParent);
            }

            foreach (var subProp in prop.CompoundObjectDefinition.Properties.OfType<CompoundObjectProperty>().Where(p => !p.IsList).OrderBy(p => p.Name))
            {
                ModelMslEntityTypeMappingComplexProperty.Call(Host, ctx, subProp, newParent);
            }

            this.WriteLine("          </ComplexProperty>");
        }
    }
}
