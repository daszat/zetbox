
namespace Kistl.Client.WPF.View
{
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

    public class VisualTypeTemplateSelector
        : DataTemplateSelector
    {
        public static DataTemplate SelectTemplate(PresentableModel mdl, VisualType? vType)
        {
            PresentableModelDescriptor pmd = mdl.GetType().ToRef(FrozenContext.Single).GetPresentableModelDescriptor();

            var vDesc = vType.HasValue ? pmd.GetViewDescriptor(Toolkit.WPF, vType.Value) : pmd.GetDefaultViewDescriptor(Toolkit.WPF);

            DataTemplate result = new DataTemplate();
            if (vDesc != null)
            {
                result.VisualTree = new FrameworkElementFactory(vDesc.ControlRef.AsType(true));
            }
            return result;
        }

        public VisualType? RequestedType
        {
            get;
            set;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var model = item as PresentableModel;
            if (model != null)
            {
                return VisualTypeTemplateSelector.SelectTemplate(model, this.RequestedType);
            }
            else
            {
                return (DataTemplate)((FrameworkElement)container).FindResource("emptyTemplate");
            }
        }
    }
}
