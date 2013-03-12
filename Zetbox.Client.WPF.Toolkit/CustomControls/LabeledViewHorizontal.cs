using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables;
using Zetbox.Client.WPF.Toolkit;

namespace Zetbox.Client.WPF.CustomControls
{
    public class LabeledViewHorizontal : Control, IHasViewModel<ILabeledViewModel>
    {
        static LabeledViewHorizontal()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(LabeledViewHorizontal),
                new FrameworkPropertyMetadata(typeof(LabeledViewHorizontal)));
            FocusableProperty.OverrideMetadata(
                typeof(LabeledViewHorizontal),
                new FrameworkPropertyMetadata(false));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var presenter = (ContentPresenter)GetTemplateChild("PART_ContentPresenter");
            presenter.ContentTemplateSelector = (DataTemplateSelector)FindResource("defaultTemplateSelector");
        }

        // Using a DependencyProperty as the backing store for LabelSharedSizeGroup.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RequestedKindProperty =
            DependencyProperty.Register("RequestedKind", typeof(object), typeof(LabeledViewHorizontal), new UIPropertyMetadata(null));
        public object RequestedKind
        {
            get { return GetValue(RequestedKindProperty) ?? (ViewModel != null ? ViewModel.RequestedKind : null); }
            set { SetValue(RequestedKindProperty, value); }
        }

        #region IHasViewModel<ILabeledViewModel> Members

        public ILabeledViewModel ViewModel
        {
            get { return (ILabeledViewModel)WPFHelper.SanitizeDataContext(DataContext); }
        }

        #endregion
    }
}
