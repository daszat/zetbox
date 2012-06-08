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
    using Zetbox.App.Base;

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
            return "_ExportGuid";
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
