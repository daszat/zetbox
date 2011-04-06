
namespace Kistl.Generator.Templates.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Generator.Extensions;

    public partial class ValueCollectionEntry
        : CollectionEntryTemplate
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx, ValueTypeProperty prop)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("CollectionEntries.ValueCollectionEntry", ctx, prop);
        }

        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx, CompoundObjectProperty prop)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("CollectionEntries.ValueCollectionEntry", ctx, prop);
        }

        protected Property prop { get; private set; }

        public ValueCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Property prop)
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
                    prop.ReferencedTypeAsCSharp());
            }
            else
            {
                return String.Format("{0}.ValueCollectionEntry{1}<{2}, {2}{1}, {3}>",
                    ImplementationNamespace,
                    ImplementationSuffix,
                    prop.ObjectClass.GetDataTypeString(),
                    prop.ReferencedTypeAsCSharp());
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
                return new string[] { "Kistl.API.IExportableValueCollectionEntryInternal" };
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
            this.WriteLine("            me.Value = other.Value;");
        }
    }
}
