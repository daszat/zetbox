
namespace Kistl.API.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.App.Base;

    public class CustomPropertyDescriptor<TComponent, TProperty>
        : BaseCustomPropertyDescriptor<TComponent, TProperty>
    {
        private static readonly string[] NoErrors = new string[] { };
        private readonly Property _property;

        public CustomPropertyDescriptor(
            Func<IFrozenContext> lazyCtx,
            Guid? propertyGuid,
            string name,
            Attribute[] attrs,
            Func<TComponent, TProperty> getter,
            Action<TComponent, TProperty> setter)
            : base(name, attrs, getter, setter)
        {
            if (lazyCtx == null) { throw new ArgumentNullException("lazyCtx"); }

            if (propertyGuid.HasValue)
            {
                _property = lazyCtx().FindPersistenceObject<Kistl.App.Base.Property>(propertyGuid.Value);
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
                    .ToArray();
            }
            else
            {
                return NoErrors;
            }
        }
    }
}
