
namespace Kistl.DalProvider.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;

    public class CustomPropertyDescriptor<TComponent, TProperty>
        : BaseCustomPropertyDescriptor<TComponent, TProperty>
    {
        private static readonly string[] NoErrors = new string[] { };

        private readonly Func<IReadOnlyKistlContext> _lazyCtx;
        private readonly Guid? _propertyGuid;

        public CustomPropertyDescriptor(
            Func<IReadOnlyKistlContext> lazyCtx,
            Guid? propertyGuid,
            string name,
            Attribute[] attrs,
            Func<TComponent, TProperty> getter,
            Action<TComponent, TProperty> setter)
            : base(name, attrs, getter, setter)
        {
            _lazyCtx = lazyCtx;
            _propertyGuid = propertyGuid;
        }

        public override string[] GetValidationErrors(object component)
        {
            if (_lazyCtx != null && _propertyGuid != null && FrozenContext.Single != null)
            {
                var property = _lazyCtx().FindPersistenceObject<Kistl.App.Base.Property>(_propertyGuid.Value);
                var self = (TComponent)component;
                var val = getter(self);
                return property
                    .Constraints
                    .Where(c => !c.IsValid(self, val))
                    .Select(c => c.GetErrorText(self, val))
                    .ToArray();
            }
            else
            {
                return NoErrors;
            }
        }
    }
}
