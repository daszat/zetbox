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
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    public partial class OnPropertyChange
    {
        public List<Property> GetRecalcProperties()
        {
            return dt.Properties.OfType<ValueTypeProperty>().Where(p => p.IsCalculated).Cast<Property>()
                .Concat(dt.Properties.OfType<CalculatedObjectReferenceProperty>().Cast<Property>())
                .OrderBy(p => p.Name)
                .ToList();
        }

        public List<Property> GetAuditProperties()
        {
            return dt.Properties
                .OfType<ValueTypeProperty>().Where(p => !p.IsList && !p.IsCalculated).Cast<Property>()
                .Concat(dt.Properties.OfType<ObjectReferenceProperty>().Where(p => !p.GetIsList()).Cast<Property>())
                .Concat(dt.Properties.OfType<CompoundObjectProperty>().Where(p => !p.IsList /* && !p.IsCalculated */).Cast<Property>())
                .OrderBy(p => p.Name)
                .ToList();
        }

        protected virtual List<Property> GetNonModifyingProperties()
        {
            return dt.Properties.OfType<ObjectReferenceProperty>()
                .Where(p => !p.RelationEnd.Parent.HasStorage(p.RelationEnd.GetRole()))
                .OrderBy(p => p.Name)
                .Cast<Property>()
                .ToList();
        }

        protected virtual void ApplyNotifyPropertyChanging(Property prop)
        {
            this.WriteLine("                    NotifyPropertyChanging(property, null, null);");
        }

        protected virtual void ApplyNotifyPropertyChanged(Property prop)
        {
            this.WriteLine("                    NotifyPropertyChanged(property, null, null);");
        }
    }
}
