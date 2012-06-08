
namespace Zetbox.DalProvider.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;

    public class PropertyDescriptorBaseImpl<TComponent, TProperty>
        : BaseCustomPropertyDescriptor<TComponent, TProperty>
    {
        private readonly Property _property;

        public PropertyDescriptorBaseImpl(
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
