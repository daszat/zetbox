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

using Zetbox.App.Base;
using Zetbox.API;
using System.Threading.Tasks;

namespace Zetbox.App.Extensions
{
    public static class PropertyExtensions
    {
        /// <summary>
        /// Returns the association name for the given ValueTypeProperty or StructProperty
        /// </summary>
        public static async Task<string> GetAssociationName(this Property prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            if (!(prop is ValueTypeProperty || prop is CompoundObjectProperty)) throw new NotSupportedException("Property must be either a ValueTypeProperty or StructProperty");
            if (!(prop is ValueTypeProperty ? ((ValueTypeProperty)prop).IsList : ((CompoundObjectProperty)prop).IsList))
                throw new NotSupportedException("GetAssociationName is only valid for Lists");
            return String.Format("FK_{0}_{1}_{2}", (await prop.GetProp_ObjectClass()).Name, "value", prop.Name);
        }

        public static async Task<bool> IsReadOnly(this Property p)
        {
            if (p == null) { throw new ArgumentNullException("p"); }
            return p.IsCalculated() || (await p.GetProp_Constraints()).OfType<ReadOnlyConstraint>().Count() > 0;
        }

        public static async Task<bool> IsViewReadOnly(this Property p)
        {
            if (p == null) { throw new ArgumentNullException("p"); }
            return p.IsCalculated() || (await p.GetProp_Constraints()).OfType<ViewReadOnlyConstraint>().Count() > 0;
        }

        public static async Task<bool> IsClientReadOnly(this Property p)
        {
            if (p == null) { throw new ArgumentNullException("p"); }
            return p.IsCalculated() || (await p.GetProp_Constraints()).OfType<ClientReadOnlyConstraint>().Count() > 0;
        }

        public static async Task<bool> IsInitOnly(this Property p)
        {
            if (p == null) { throw new ArgumentNullException("p"); }
            return (await p.GetProp_Constraints()).OfType<InitOnlyConstraint>().Count() > 0;
        }

        public static async Task<bool> IsNullable(this Property p)
        {
            if (p == null) { throw new ArgumentNullException("p"); }
            if (p is ObjectReferenceProperty)
            {
                return await IsNullable((ObjectReferenceProperty)p);
            }
            else
            {
                return (await p.GetProp_Constraints()).OfType<NotNullableConstraint>().Count() == 0;
            }
        }

        private static async Task<bool> IsNullable(this ObjectReferenceProperty p)
        {
            if (p == null) { throw new ArgumentNullException("p"); }
            var relEnd = p.RelationEnd;
            var rel = relEnd.GetParent();
            var otherEnd = await rel.GetOtherEnd(relEnd);

            return otherEnd.IsNullable();
        }

        public static bool IsCalculated(this Property p)
        {
            return (p is ValueTypeProperty && ((ValueTypeProperty)p).IsCalculated)
                || (p is CalculatedObjectReferenceProperty)
                || (p is CompoundObjectProperty && false /* ((CompoundObjectProperty)p).IsCalculated*/ );
        }

        public static async Task<bool> HasLengthConstraint(this StringProperty prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            return (await prop.GetProp_Constraints()).OfType<StringRangeConstraint>().Count() > 0;
        }

        public static async Task<StringRangeConstraint> GetLengthConstraint(this StringProperty prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            return (await prop.GetProp_Constraints()).OfType<StringRangeConstraint>().FirstOrDefault();
        }

        public static async Task<int> GetSize(this Property prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            if (prop is StringProperty) return await GetMaxLength((StringProperty)prop);
            if (prop is DecimalProperty) return ((DecimalProperty)prop).Precision;
            return 0;
        }

        public static Task<int> GetScale(this Property prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            if (prop is DecimalProperty) return Task.FromResult(((DecimalProperty)prop).Scale);
            return Task.FromResult(0);
        }

        public static async Task<int> GetMaxLength(this StringProperty prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            StringRangeConstraint constraint = await prop.GetLengthConstraint();
            // create unconstrained maxLength if no constrain is specified
            return constraint == null ? Zetbox.API.Helper.DefaultStringPropertyLength : (constraint.MaxLength ?? int.MaxValue);
        }

        public static bool GetIsList(this Property prop)
        {
            if (prop is ValueTypeProperty)
            {
                return ((ValueTypeProperty)prop).IsList;
            }
            else if (prop is ObjectReferenceProperty)
            {
                var orp = (ObjectReferenceProperty)prop;
                RelationEnd relEnd = orp.RelationEnd;
                Relation rel = relEnd.GetParent();
                RelationEnd otherEnd = rel.A == relEnd ? rel.B : rel.A;

                return otherEnd.Multiplicity.UpperBound() > 1;
            }
            else if (prop is CompoundObjectProperty)
            {
                var cop = (CompoundObjectProperty)prop;
                return cop.IsList;
            }
            return false;
        }
    }
}
