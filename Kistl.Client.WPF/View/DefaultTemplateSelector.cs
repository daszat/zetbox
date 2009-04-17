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
    public class DefaultTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is PresentableModel)
            {
                TypeRef mdlType = item.GetType().ToRef(FrozenContext.Single);
                PresentableModelDescriptor pmd = FrozenContext.Single
                    .GetQuery<PresentableModelDescriptor>()
                    .Single(obj => obj.PresentableModelRef == mdlType);

                var vDesc = pmd.GetDefaultViewDescriptor(Toolkit.WPF);

                DataTemplate result = new DataTemplate();
                result.VisualTree = new FrameworkElementFactory(vDesc.ControlRef.AsType(true));
                return result;
            }

            return (DataTemplate)((FrameworkElement)container).FindResource("emptyTemplate");
        }
    }
}
