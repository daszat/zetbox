
namespace Kistl.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.App.Base;

    public partial class OnPropertyChange
    {
        public List<ValueTypeProperty> GetRecalcProperties()
        {
            return dt.Properties.OfType<ValueTypeProperty>().Where(p => !p.IsList && p.IsCalculated).ToList();
        }

        public List<Property> GetAuditProperties()
        {
            var result = dt.Properties.OfType<ValueTypeProperty>().Where(p => !p.IsList && !p.IsCalculated).Cast<Property>().ToList();
            result.AddRange(
                dt.Properties.OfType<ObjectReferenceProperty>().Where(p => !p.GetIsList()).Cast<Property>()
                );
            result.AddRange(
                dt.Properties.OfType<CompoundObjectProperty>().Where(p => !p.IsList /* && !p.IsCalculated */).Cast<Property>()
                );

            return result;
        }
    }
}
