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
using Zetbox.API;
using Zetbox.App.Base;
using Zetbox.Generator;
using Arebis.CodeGeneration;

namespace Zetbox.DalProvider.Ef.Generator.Templates.EfModel
{
    public class ModelMslEntityTypeMappingScalarProperty : Zetbox.Generator.ResourceTemplate
    {
        protected IZetboxContext ctx;
        protected Property prop;
        protected string propertyName;
        protected string parentName;

        public static void Call(IGenerationHost host, IZetboxContext ctx, Property prop, string propertyName, string parentName)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("EfModel.ModelMslEntityTypeMappingScalarProperty", ctx, prop, propertyName, parentName);
        }

        public ModelMslEntityTypeMappingScalarProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Property prop, string propertyName, string parentName)
            : base(_host)
        {
            this.ctx = ctx;
            this.prop = prop;
            this.propertyName = propertyName;
            this.parentName = parentName;
        }

        public override void Generate()
        {
            string columnName;
            string name = propertyName;

            if (prop is EnumerationProperty)
            {
                columnName = Construct.NestedColumnName(prop, parentName);
                name += ImplementationPropertySuffix;
            }
            else if (prop is ValueTypeProperty)
            {
                columnName = Construct.NestedColumnName(prop, parentName);
            }
            else if (prop is ObjectReferenceProperty)
            {
                throw new ArgumentOutOfRangeException("prop", "cannot apply ObjectReferenceProperty as scalar");
            }
            else
            {
                return;
            }

            this.WriteLine("<ScalarProperty Name=\"{0}\" ColumnName=\"{1}\" />", name, columnName);
        }
    }
}
