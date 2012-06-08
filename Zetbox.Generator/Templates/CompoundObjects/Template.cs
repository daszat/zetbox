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

namespace Zetbox.Generator.Templates.CompoundObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Templates = Zetbox.Generator.Templates;

    /// <summary>
    /// A template for "CompoundObject".
    /// </summary>
    public class Template
        : Zetbox.Generator.Templates.TypeBase
    {
        protected CompoundObject CompoundObjectType { get; private set; }

        public Template(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, CompoundObject cType)
            : base(_host, ctx, cType)
        {
            this.CompoundObjectType = cType;
        }

        protected override string[] GetInterfaces()
        {
            return base.GetInterfaces().Concat(new string[] { typeof(ICompoundObject).Name }).OrderBy(s => s).ToArray();
        }

        /// <returns>The base class to inherit from.</returns>
        protected override string GetBaseClass()
        {
            return "CompoundObjectDefaultImpl";
        }

        protected override void ApplyConstructorTemplate()
        {
            base.ApplyConstructorTemplate();

            ObjectClasses.Constructors.Call(
                Host,
                ctx,
                GetTypeName(),
                this.DataType
                    .Properties
                    .OfType<CompoundObjectProperty>());

            string clsName = this.GetTypeName();

            // attach compound to parent object
            this.WriteObjects("        public ", clsName, "(IPersistenceObject parent, string property) : this(false, parent, property) {}");
            this.WriteLine();
            this.WriteObjects("        public ", clsName, "(bool ignore, IPersistenceObject parent, string property)");
            this.WriteLine();
            this.WriteObjects("            : base(null) // TODO: pass parent's lazyCtx");
            this.WriteLine();
            this.WriteObjects("        {");
            this.WriteLine();
            this.WriteObjects("            AttachToObject(parent, property);");
            this.WriteLine();

            Properties.CompoundObjectPropertyInitialisation.Call(
                Host, ctx,
                this.DataType
                    .Properties
                    .OfType<CompoundObjectProperty>(),
                ImplementationSuffix,
                ImplementationPropertySuffix);

            this.WriteObjects("        }");
            this.WriteLine();
        }

        protected override void ApplyApplyChangesFromMethod()
        {
            base.ApplyApplyChangesFromMethod();
            ObjectClasses.ApplyChangesFromMethod.Call(Host, ctx, typeof(ICompoundObject).Name, DataType, GetTypeName());
        }

        protected override void ApplyClassHeadTemplate()
        {
            base.ApplyClassHeadTemplate();

            // Implement CompoundObjectID by using a static backing store
            this.WriteLine("        private static readonly Guid _compoundObjectID = new Guid(\"{0}\");", DataType.ExportGuid);
            this.WriteLine("        public override Guid CompoundObjectID { get { return _compoundObjectID; } }");
            this.WriteLine();
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();
            DefaultMethods.Call(Host, ctx, this.DataType);
        }

        protected override void ApplyPropertyIsValidEvent(Property p)
        {
            // No CustomTypeDescriptor, no events
        }
    }
}
