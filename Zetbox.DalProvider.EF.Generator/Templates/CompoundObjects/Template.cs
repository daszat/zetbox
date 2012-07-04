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

namespace Zetbox.DalProvider.Ef.Generator.Templates.CompoundObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.Generator.Extensions;
    using Templates = Zetbox.Generator.Templates;

    public class Template
        : Templates.CompoundObjects.Template
    {
        public Template(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, CompoundObject s)
            : base(_host, ctx, s)
        {
        }

        protected override void ApplyClassAttributeTemplate()
        {
            WriteLine("    [EdmComplexType(NamespaceName=\"Model\", Name=\"{0}\")]", this.CompoundObjectType.Name);
        }

        /// <returns>The base class to inherit from.</returns>
        protected override string GetBaseClass()
        {
            return "BaseServerCompoundObject_EntityFramework";
        }

        protected override void ApplyConstructorTemplate()
        {
            // avoid base constructor not implementing bool isNull
            // base.ApplyExtraConstructorTemplate();
            string clsName = this.GetTypeName();

            // default constructor used for de-serialization
            this.WriteObjects("        public ", clsName, "()");
            this.WriteLine();
            this.WriteObjects("            : base(null) // TODO: pass parent's lazyCtx");
            this.WriteLine();
            this.WriteObjects("        {");
            this.WriteLine();
            Templates.Properties.CompoundObjectPropertyInitialisation.Call(
                Host, ctx,
                this.DataType
                    .Properties
                    .OfType<CompoundObjectProperty>(),
                ImplementationSuffix,
                ImplementationPropertySuffix);

            this.WriteLine();
            this.WriteObjects("        }");
            this.WriteLine();


            this.WriteObjects("        public ", clsName, "(IPersistenceObject parent, string property)");
            this.WriteLine();
            this.WriteObjects("            : base(null) // TODO: pass parent's lazyCtx");
            this.WriteLine();
            this.WriteObjects("        {");
            this.WriteLine();
            this.WriteObjects("            AttachToObject(parent, property);");
            this.WriteLine();
            Templates.Properties.CompoundObjectPropertyInitialisation.Call(
                Host, ctx,
                this.DataType
                    .Properties
                    .OfType<CompoundObjectProperty>(),
                ImplementationSuffix,
                ImplementationPropertySuffix);

            this.WriteObjects("        }");
            this.WriteLine();

            // default constructor used for de-serialization
            this.WriteObjects("        public ", clsName, "(Func<IFrozenContext> lazyCtx)");
            this.WriteLine();
            this.WriteObjects("            : base(lazyCtx)");
            this.WriteLine();
            this.WriteObjects("        {");
            this.WriteLine();
            Templates.Properties.CompoundObjectPropertyInitialisation.Call(
                Host, ctx,
                this.DataType
                    .Properties
                    .OfType<CompoundObjectProperty>(),
                ImplementationSuffix,
                ImplementationPropertySuffix);

            this.WriteLine();
            this.WriteObjects("        }");
            this.WriteLine();


            this.WriteObjects("        public ", clsName, "(Func<IFrozenContext> lazyCtx, IPersistenceObject parent, string property)");
            this.WriteLine();
            this.WriteObjects("            : base(lazyCtx)");
            this.WriteLine();
            this.WriteObjects("        {");
            this.WriteLine();
            this.WriteObjects("            AttachToObject(parent, property);");
            this.WriteLine();
            Templates.Properties.CompoundObjectPropertyInitialisation.Call(
                Host, ctx,
                this.DataType
                    .Properties
                    .OfType<CompoundObjectProperty>(),
                ImplementationSuffix,
                ImplementationPropertySuffix);

            this.WriteObjects("        }");
            this.WriteLine();
        }

        protected override void ApplyEnumerationPropertyTemplate(EnumerationProperty prop)
        {
            this.WriteLine("        // enumeration property");
            Properties.EnumerationPropertyTemplate.Call(Host, ctx,
                this.MembersToSerialize,
                prop, true);
        }
    }
}
