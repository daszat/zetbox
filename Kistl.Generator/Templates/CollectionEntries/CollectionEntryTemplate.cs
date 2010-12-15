
namespace Kistl.Generator.Templates.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.App.Base;

    public abstract partial class CollectionEntryTemplate
    {
        protected virtual void ApplyClassAttributeTemplate() { }

        protected abstract string GetCeClassName();

        /// <returns>a string defining the inheritance relations of this class</returns>
        protected virtual string GetInheritance()
        {
            string baseClass = GetCeBaseClassName();
            string[] interfaces = GetInterfaces().OrderBy(s => s).ToArray();
            if (!String.IsNullOrEmpty(baseClass) && interfaces.Length > 0)
            {
                return ": " + baseClass + ", " + String.Join(", ", interfaces);
            }
            else if (!String.IsNullOrEmpty(baseClass) && interfaces.Length == 0)
            {
                return ": " + baseClass;
            }
            else if (String.IsNullOrEmpty(baseClass) && interfaces.Length > 0)
            {
                return ": " + String.Join(", ", interfaces);
            }
            else
            {
                return String.Empty;
            }
        }

        /// <returns>The base class to inherit from.</returns>
        protected abstract string GetCeBaseClassName();

        /// <returns>which CollectionEntry interface to implement.</returns>
        protected abstract string GetCeInterface();

        protected virtual void ApplyClassHeadTemplate()
        {
            ApplyIdPropertyTemplate();
            if (IsExportable())
            {
                ApplyExportGuidPropertyTemplate();
            }
        }

        protected virtual void ApplyConstructorTemplate()
        {
            ObjectClasses.Constructors.Call(Host, ctx, GetCeClassName(), new CompoundObjectProperty[0]);
        }

        protected abstract bool IsExportable();

        protected abstract string[] GetIExportableInterfaces();

        protected virtual string GetExportGuidBackingStoreReference()
        {
            return "this._ExportGuid";
        }

        /// <returns>The interfaces this class implements</returns>
        protected virtual string[] GetInterfaces()
        {
            if (IsExportable())
            {
                return new string[] { GetCeInterface() }.Concat(GetIExportableInterfaces()).ToArray();
            }
            return new string[] { GetCeInterface() };
        }

        /// <returns>true, if any side is ordered</returns>
        protected abstract bool IsOrdered();

        protected virtual void ApplyReloadReferenceBody() { }

        protected virtual void ApplyClassTailTemplate() { }

        protected virtual void ApplyChangesFromBody() { }

        protected virtual void ApplyAttachToContextBody() { }
    }
}
