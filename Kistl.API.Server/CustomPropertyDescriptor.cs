
namespace Kistl.API.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.App.Base;
    
    public class CustomPropertyDescriptor<TComponent, TProperty>
        : BaseCustomPropertyDescriptor<TComponent, TProperty>
    {
        private readonly Property _property;

        public CustomPropertyDescriptor(
            Guid propertyGuid,
            string name,
            Attribute[] attrs,
            Func<TComponent, TProperty> getter,
            Action<TComponent, TProperty> setter)
            : base(name, attrs, getter, setter)
        {
            _property = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(propertyGuid);
        }

        public override string[] GetValidationErrors(object component)
        {
            var self = (TComponent)component;
            var val = getter(self);
            return _property
                .Constraints
                .Where(c => !c.IsValid(self, val))
                .Select(c => c.GetErrorText(self, val))
                .ToArray();
        }
    }
}
