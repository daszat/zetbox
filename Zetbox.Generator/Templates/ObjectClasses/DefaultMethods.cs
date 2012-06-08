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

namespace Zetbox.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator.Extensions;

    public partial class DefaultMethods
    {
        protected virtual void ApplyRequisites() { }
        protected virtual void ApplyPrePreSaveTemplate() { }
        protected virtual void ApplyPostPreSaveTemplate() { }
        protected virtual void ApplyPrePostSaveTemplate() { }
        protected virtual void ApplyPostPostSaveTemplate() { }

        protected virtual void ApplyPreCreatedTemplate()
        {
            foreach (var prop in dt.Properties.Where(p => !p.IsList() && p.DefaultValue == null && !p.IsCalculated()).OrderBy(p => p.Name))
            {
                this.WriteObjects("            SetNotInitializedProperty(\"", prop.Name, "\");\r\n");
            }
            foreach (var prop in dt.Properties.Where(p => p.IsCalculated()).OrderBy(p => p.Name))
            {
                this.WriteObjects("            _", prop.Name, "_IsDirty = true;\r\n");
            }
        }

        protected virtual void ApplyPostCreatedTemplate() { }
        protected virtual void ApplyPreDeletingTemplate() { }
        protected virtual void ApplyPostDeletingTemplate()
        {
            // TODO: implement containment/delete cascading
            foreach (var prop in dt.Properties.Where(p => p.IsList() && !p.IsCalculated()).OrderBy(p => p.Name))
            {
                this.WriteObjects("            ", prop.Name, ".Clear();\r\n");
            }
            foreach (var prop in dt.Properties.OfType<ObjectReferenceProperty>().Where(p => !p.IsList() && !p.IsCalculated() && p.IsNullable()).OrderBy(p => p.Name))
            {
                this.WriteObjects("            ", prop.Name, " = null;\r\n");
            }
        }
    }
}
