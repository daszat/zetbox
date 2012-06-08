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

namespace Zetbox.DalProvider.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;

    public sealed class PropertyDescriptorClientImpl<TComponent, TProperty>
         : BaseCustomPropertyDescriptor<TComponent, TProperty>
    {
        private readonly Property _property;

        public PropertyDescriptorClientImpl(
            Func<IFrozenContext> lazyCtx,
            Guid? propertyGuid,
            string name,
            Attribute[] attrs,
            Func<TComponent, TProperty> getter,
            Action<TComponent, TProperty> setter,
            Func<TComponent, PropertyIsValidHandler<TComponent>> isValid)
            : base(name, attrs, getter, setter, isValid)
        {
            if (lazyCtx == null) { throw new ArgumentNullException("lazyCtx"); }

            if (propertyGuid.HasValue)
            {
                _property = lazyCtx().FindPersistenceObject<Zetbox.App.Base.Property>(propertyGuid.Value);
            }
            else
            {
                _property = null;
            }
        }

        public override string[] GetValidationErrors(object component)
        {
            if (_property != null)
            {
                var self = (TComponent)component;
                var val = getter(self);
                return _property
                    .Constraints
                    .Where(c => !c.IsValid(self, val))
                    .Select(c => c.GetErrorText(self, val))
                    .Concat(TryExecuteIsValidEvent(self))
                    .ToArray();
            }
            else
            {
                return NoErrors;
            }
        }
    }
}
