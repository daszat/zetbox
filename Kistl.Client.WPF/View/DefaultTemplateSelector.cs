using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using Kistl.App.GUI;
using Kistl.Client.GUI.DB;

namespace Kistl.Client.WPF.View
{
    public class DefaultTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null)
            {
                Layout lout = DataMocks.LookupDefaultLayout(item.GetType());
                var vDesc = DataMocks.LookupViewDescriptor(Toolkit.WPF, lout);
                DataTemplate result = new DataTemplate();
                result.VisualTree = new FrameworkElementFactory(vDesc.ViewRef.AsType(true));
                return result;
            }

            return (DataTemplate)((FrameworkElement)container).FindResource("emptyTemplate");
        }
    }
}
