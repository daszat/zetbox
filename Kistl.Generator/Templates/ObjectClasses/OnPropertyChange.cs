
namespace Kistl.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.App.Base;

    public partial class OnPropertyChange
    {
        public List<Property> GetRecalcProperties()
        {
            return dt.Properties.OfType<ValueTypeProperty>().Where(p => p.IsCalculated).Cast<Property>()
                .Concat(dt.Properties.OfType<CalculatedObjectReferenceProperty>().Cast<Property>())
                .OrderBy(p => p.Name).ToList();
        }

        public List<Property> GetAuditProperties()
        {
            return dt.Properties
                .OfType<ValueTypeProperty>().Where(p => !p.IsList && !p.IsCalculated).Cast<Property>()
                .Concat(dt.Properties.OfType<ObjectReferenceProperty>().Where(p => !p.GetIsList()).Cast<Property>())
                .Concat(dt.Properties.OfType<CompoundObjectProperty>().Where(p => !p.IsList /* && !p.IsCalculated */).Cast<Property>())
                .OrderBy(p => p.Name)
                .ToList();
        }

        protected virtual void ApplyNotifyPropertyChanging(Property prop)
        {
            this.WriteLine("                    NotifyPropertyChanging(property, null, null);");
        }

        protected virtual void ApplyNotifyPropertyChanged(Property prop)
        {
            this.WriteLine("                    NotifyPropertyChanged(property, null, null);");
        }
    }
}
