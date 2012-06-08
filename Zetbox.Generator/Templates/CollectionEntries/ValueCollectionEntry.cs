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

namespace Zetbox.Generator.Templates.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator.Extensions;

    public partial class ValueCollectionEntry
        : CollectionEntryTemplate
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IZetboxContext ctx, ValueTypeProperty prop)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("CollectionEntries.ValueCollectionEntry", ctx, prop);
        }

        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IZetboxContext ctx, CompoundObjectProperty prop)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("CollectionEntries.ValueCollectionEntry", ctx, prop);
        }

        protected Property prop { get; private set; }

        public ValueCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Property prop)
            : base(_host, ctx)
        {
            if (!(prop is ValueTypeProperty || prop is CompoundObjectProperty))
            {
                throw new ArgumentOutOfRangeException("prop",
                    String.Format("Property has to be either ValueTypeProperty or CompoundObjectProperty, but is {0}", prop.GetType().FullName));
            }
            this.prop = prop;
        }

        protected override string GetCeClassName()
        {
            return prop.GetCollectionEntryClassName() + ImplementationSuffix;
        }

        protected override string GetCeBaseClassName()
        {
            if (prop is CompoundObjectProperty)
            {
                return String.Format("{0}.CompoundCollectionEntry{1}<{2}, {2}{1}, {3}, {3}{1}>",
                    ImplementationNamespace,
                    ImplementationSuffix,
                    prop.ObjectClass.GetDataTypeString(),
                    prop.GetElementTypeString());
            }
            else
            {
                return String.Format("{0}.ValueCollectionEntry{1}<{2}, {2}{1}, {3}>",
                    ImplementationNamespace,
                    ImplementationSuffix,
                    prop.ObjectClass.GetDataTypeString(),
                    prop.GetElementTypeString());
            }
        }

        protected override bool IsExportable()
        {
            var oc = prop.ObjectClass as ObjectClass;
            return oc != null && oc.ImplementsIExportable();
        }

        protected override string[] GetIExportableInterfaces()
        {
            if (IsExportable())
            {
                return new string[] { "Zetbox.API.IExportableValueCollectionEntryInternal" };
            }
            else
            {
                return new string[0];
            }
        }

        protected override void ApplyExportGuidPropertyTemplate()
        {
            // Do nothing, export guid will not be saved
            // value collection entries are always streamed/exported in-place
        }

        protected override string GetExportGuidBackingStoreReference()
        {
            return string.Empty;
        }

        protected override string GetCeInterface()
        {
            return prop.GetCollectionEntryClassName();
        }

        protected override bool IsOrdered()
        {
            var vp = prop as ValueTypeProperty;
            if (vp != null)
            {
                return vp.HasPersistentOrder;
            }

            var cp = prop as CompoundObjectProperty;
            if (cp != null)
            {
                return cp.HasPersistentOrder;
            }

            // should not happen (see constructor)
            return false;
        }

        protected override void ApplyReloadReferenceBody()
        {
            base.ApplyReloadReferenceBody();

            string referencedInterface = prop.ObjectClass.Module.Namespace + "." + prop.ObjectClass.Name;
            string referencedImplementation = referencedInterface + ImplementationSuffix;
            ObjectClasses.ReloadOneReference.Call(
                Host,
                ctx,
                referencedInterface,
                referencedImplementation,
                "Parent",
                "Parent",
                "_fk_Parent",
                null,
                false // value collection entries are always streamed/exported in-place
                );
        }

        protected override void ApplyChangesFromBody()
        {
            base.ApplyChangesFromBody();

            this.WriteLine("            me._fk_Parent = other._fk_Parent;");
            if (prop is CompoundObjectProperty)
            {
                this.WriteLine("            if (me.Value == null && other.Value != null) {");
                this.WriteLine("                me.Value = ({0})other.Value.Clone();", prop.GetElementTypeString());
                this.WriteLine("            } else if (me.Value != null && other.Value == null) {");
                this.WriteLine("                me.Value = null;");
                this.WriteLine("            } else if (me.Value != null && other.Value != null) {");
                this.WriteLine("                me.Value.ApplyChangesFrom(other.Value);");
                this.WriteLine("            }");
            }
            else
            {
                this.WriteLine("            me.Value = other.Value;");
            }
        }

        protected override void ApplyClassHeadTemplate()
        {
            base.ApplyClassHeadTemplate();

            // Implement PropertyID by using a static backing store
            this.WriteLine("        private static readonly Guid _propertyID = new Guid(\"{0}\");", prop.ExportGuid);
            this.WriteLine("        public override Guid PropertyID { get { return _propertyID; } }");
            this.WriteLine();
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();

            ApplyUpdateParentTemplate();
        }

        protected virtual void ApplyUpdateParentTemplate()
        {
            var interfaceType = prop.ObjectClass.Module.Namespace + "." + prop.ObjectClass.Name;
            ObjectClasses.UpdateParentTemplate.Call(Host, new[] { new ObjectClasses.UpdateParentTemplateParams() { PropertyName = "Parent", IfType = interfaceType } }.ToList());
        }
    }
}
