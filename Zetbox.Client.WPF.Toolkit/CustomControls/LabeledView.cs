
namespace Zetbox.Client.WPF.CustomControls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using Zetbox.Client.GUI;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.WPF.Toolkit;

    public class LabeledView : Control, IHasViewModel<ILabeledViewModel>
    {
        static LabeledView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(LabeledView),
                new FrameworkPropertyMetadata(typeof(LabeledView)));
            FocusableProperty.OverrideMetadata(
                typeof(LabeledView),
                new FrameworkPropertyMetadata(false));
            VerticalContentAlignmentProperty.OverrideMetadata(
                typeof(LabeledView),
                new FrameworkPropertyMetadata(VerticalAlignment.Center));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var presenter = (ContentPresenter)GetTemplateChild("PART_ContentPresenter");
            presenter.ContentTemplateSelector = (DataTemplateSelector)FindResource("defaultTemplateSelector");
        }

        [TypeConverter(typeof(LengthConverter))]
        public double LabelMinWidth
        {
            get { return (double)GetValue(LabelMinWidthProperty); }
            set { SetValue(LabelMinWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelMinWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelMinWidthProperty =
            DependencyProperty.Register("LabelMinWidth", typeof(double), typeof(LabeledView), new UIPropertyMetadata(150.0));

        [TypeConverter(typeof(LengthConverter))]
        public double LabelWidth
        {
            get { return (double)GetValue(LabelWidthProperty); }
            set { SetValue(LabelWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelWidthProperty =
            DependencyProperty.Register("LabelWidth", typeof(double), typeof(LabeledView), new UIPropertyMetadata(Double.NaN));

        public string LabelSharedSizeGroup
        {
            get { return (string)GetValue(LabelSharedSizeGroupProperty); }
            set { SetValue(LabelSharedSizeGroupProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelSharedSizeGroup.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelSharedSizeGroupProperty =
            DependencyProperty.Register("LabelSharedSizeGroup", typeof(string), typeof(LabeledView), new UIPropertyMetadata("LabeledViewLabel"));

        // Using a DependencyProperty as the backing store for LabelSharedSizeGroup.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RequestedKindProperty =
            DependencyProperty.Register("RequestedKind", typeof(object), typeof(LabeledView), new UIPropertyMetadata(null));
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
