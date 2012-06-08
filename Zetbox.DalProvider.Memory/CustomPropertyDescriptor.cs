
namespace Zetbox.DalProvider.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;

    public class CustomPropertyDescriptor<TComponent, TProperty>
        : BaseCustomPropertyDescriptor<TComponent, TProperty>
    {
        private static readonly string[] NoErrors = new string[] { };

        private readonly Func<IFrozenContext> _lazyCtx;
        private readonly Guid? _propertyGuid;

        public CustomPropertyDescriptor(
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

        public override string[] GetValidationErrors(object component)
        {
            IReadOnlyZetboxContext ctx;
            if (_lazyCtx != null && _propertyGuid != null && (ctx = _lazyCtx()) != null)
            {
                var property = ctx.FindPersistenceObject<Zetbox.App.Base.Property>(_propertyGuid.Value);
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
