
namespace Kistl.API.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.App.Base;

    // server implementation has to delay property lookup until validation
    // to avoid bootstrapping issues and threading failures when initialising

    public class CustomPropertyDescriptor<TComponent, TProperty>
        : BaseCustomPropertyDescriptor<TComponent, TProperty>
    {
        private readonly Guid? _propertyGuid;
        private static readonly string[] NoErrors = new string[] { };

        public CustomPropertyDescriptor(
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
            if (_propertyGuid != null && FrozenContext.Single != null)
            {
                var property = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(_propertyGuid.Value);
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
