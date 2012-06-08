
namespace Kistl.Client.WPF.Toolkit
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// A <see cref="DataTemplateSelector"/> to choose the appropriate view for type of the item.
    /// </summary>
    public class TypeNameTemplateSelector
        : DataTemplateSelector
    {
        
        /// <summary>
        /// Initializes a new instance of the TypeNameTemplateSelector class.
        /// </summary>
        public TypeNameTemplateSelector()
        {
        }

        /// <inheritdoc/>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
            {
                return GetNullTemplate(container);
            }
            else
            {
                return (DataTemplate)((FrameworkElement)container).FindResource(item.GetType().Name);
            }
        }

        private static DataTemplate GetNullTemplate(DependencyObject container)
        {
            return (DataTemplate)((FrameworkElement)container).FindResource("nullTemplate");
        }
    }
}
