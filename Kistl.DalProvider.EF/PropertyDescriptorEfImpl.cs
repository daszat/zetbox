
namespace Kistl.DalProvider.Ef
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    // server implementation has to delay property lookup until validation
    // to avoid bootstrapping issues and threading failures when initialising
    public sealed class PropertyDescriptorEfImpl<TComponent, TProperty>
         : BaseCustomPropertyDescriptor<TComponent, TProperty>
    {
        private static readonly string[] NoErrors = new string[] { };

        private readonly Func<IFrozenContext> _lazyCtx;
        private readonly Guid? _propertyGuid;

        public PropertyDescriptorEfImpl(
            Func<IFrozenContext> lazyCtx,
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

        public PropertyDescriptorEfImpl(
            Guid? propertyGuid,
            string name,
            Attribute[] attrs,
            Func<TComponent, TProperty> getter,
            Action<TComponent, TProperty> setter)
            : base(name, attrs, getter, setter)
        {
            _propertyGuid = propertyGuid;
        }

        public override string[] GetValidationErrors(object component)
        {
            IReadOnlyKistlContext ctx;
            if (_lazyCtx != null && _propertyGuid != null && (ctx = _lazyCtx()) != null)
            {
                var property = ctx.FindPersistenceObject<Kistl.App.Base.Property>(_propertyGuid.Value);
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
