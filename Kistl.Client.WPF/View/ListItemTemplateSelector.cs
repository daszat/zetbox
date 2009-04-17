using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.App.GUI;
using Kistl.Client.Presentables;

namespace Kistl.Client.WPF.View
{
    class ListItemTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is DataObjectModel)
            {
                PresentableModelDescriptor pmd = item.GetType().ToRef(FrozenContext.Single).GetPresentableModelDescriptor(); ;

                var vDesc = pmd.GetViewDescriptor(Toolkit.WPF, VisualType.ObjectListEntry);

                DataTemplate result = new DataTemplate();
                result.VisualTree = new FrameworkElementFactory(vDesc.ControlRef.AsType(true));
                return result;

            }

            return (DataTemplate)((FrameworkElement)container).FindResource("emptyTemplate");
        }
    }
}
