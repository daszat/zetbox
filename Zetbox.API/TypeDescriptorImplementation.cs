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
namespace Zetbox.API
{
    using System;
    using System.Linq;
    using System.ComponentModel;
    using System.Collections.Generic;

    public interface IValidatingPropertyDescriptor
    {
        string[] GetValidationErrors(object component);
        PropertyDescriptor UnderlyingDescriptor { get; }
    }

    public class BaseCustomPropertyDescriptor<TComponent, TProperty>
        : PropertyDescriptor, IValidatingPropertyDescriptor
    {
        protected static readonly string[] NoErrors = new string[] { };

        protected readonly Func<TComponent, TProperty> getter;
        protected readonly Action<TComponent, TProperty> setter;

        protected readonly Func<TComponent, PropertyIsValidHandler<TComponent>> isValid;

        public BaseCustomPropertyDescriptor(
            string name,
            Attribute[] attrs,
            Func<TComponent, TProperty> getter,
            Action<TComponent, TProperty> setter,
            Func<TComponent, PropertyIsValidHandler<TComponent>> isValid)
            : base(name, attrs)
        {
            if (getter == null) { throw new ArgumentNullException("getter"); }
            // setter may be null if readonly

            this.getter = getter;
            this.setter = setter;
            this.isValid = isValid;
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override Type ComponentType
        {
            get { return typeof(TComponent); }
        }

        public override object GetValue(object component)
        {
            return getter((TComponent)component);
        }

        public override bool IsReadOnly
        {
            get { return setter == null; }
        }

        public override Type PropertyType
        {
            get { return typeof(TProperty); }
        }

        public override void ResetValue(object component)
        {
            throw new NotImplementedException();
        }

        public override void SetValue(object component, object value)
        {
            setter((TComponent)component, (TProperty)value);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }

        #region IValidatingPropertyDescriptor Members

        public virtual string[] GetValidationErrors(object component)
        {
            return null;
        }

        public PropertyDescriptor UnderlyingDescriptor
        {
            get { return this; }
        }

        #endregion

        protected string[] TryExecuteIsValidEvent(TComponent self)
        {
            if (isValid != null)
            {
                var e = isValid(self);
                if (e != null)
                {
                    var args = new PropertyIsValidEventArgs();
                    e(self, args);
                    return new[] { args }
                        .Where(i => !i.IsValid)
                        .Select(i => i.Error).ToArray();
                }
            }
            return NoErrors;
        }
    }

    public class CustomEventDescriptor<TComponent>
        : EventDescriptor
    {
        private readonly Action<TComponent, Delegate> _remover;
        private readonly Action<TComponent, Delegate> _adder;

        public CustomEventDescriptor(
            string name,
            Attribute[] attrs,
            Action<TComponent, Delegate> remover,
            Action<TComponent, Delegate> adder)
            : base(name, attrs)
        {
            if (remover == null) { throw new ArgumentNullException("remover"); }
            if (adder == null) { throw new ArgumentNullException("adder"); }

            this._remover = remover;
            this._adder = adder;
        }

        public override void AddEventHandler(object component, Delegate value)
        {
            _adder((TComponent)component, value);
        }

        public override Type ComponentType
        {
            get { return typeof(TComponent); }
        }

        public override Type EventType
        {
            get { return typeof(Delegate); }
        }

        public override bool IsMulticast
        {
            get { return true; }
        }

        public override void RemoveEventHandler(object component, Delegate value)
        {
            _remover((TComponent)component, value);
        }
    }
}