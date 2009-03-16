using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using Kistl.App.GUI;
using Kistl.Client.GUI.DB;
using Kistl.Client.Presentables;

namespace Kistl.Client.WPF.View
{
    class ListItemTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null && item is DataObjectModel)
            {
                // retrieve specific line layout
                var lout = new DataObjectLineLayout()
                {
                    SourceModelType = typeof(DataObjectModel)
                };
                ViewDescriptor vDesc = DataMocks.LookupViewDescriptor(Toolkit.WPF, lout);
                DataTemplate result = new DataTemplate();
                result.VisualTree = new FrameworkElementFactory(vDesc.ViewRef.AsType(true));
                return result;
            }

            return (DataTemplate)((FrameworkElement)container).FindResource("emptyTemplate");
        }
    }
}
