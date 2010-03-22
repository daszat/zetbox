
namespace Kistl.DalProvider.Frozen
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
        public CustomPropertyDescriptor(
            Guid? propertyGuid,
            string name,
            Attribute[] attrs,
            Func<TComponent, TProperty> getter,
            Action<TComponent, TProperty> setter)
            : base(name, attrs, getter, setter)
        {
        }

        public override string[] GetValidationErrors(object component)
        {
            // Frozen objects are always valid
            return null;
        }
    }
}
