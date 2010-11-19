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
using Kistl.Client.GUI;
using Kistl.Client.Presentables;
using System.ComponentModel;
using Kistl.Client.Presentables.ValueViewModels;

namespace Kistl.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for LabeledView.xaml
    /// </summary>
    public partial class LabeledView : UserControl, IHasViewModel<ILabeledViewModel>
    {
        public LabeledView()
        {
            InitializeComponent();
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

        public bool? IsReadOnly
        {
            get { return (bool?)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsReadOnly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool?), typeof(LabeledView), new UIPropertyMetadata(null, new PropertyChangedCallback(IsReadOnly_Changed)));

        private static void IsReadOnly_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            LabeledView v = (LabeledView)obj;

            if (v.ViewModel is BaseValueViewModel && e.NewValue != null)
            {
                ((BaseValueViewModel)v.ViewModel).IsReadOnly = (bool)e.NewValue;
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (ViewModel is BaseValueViewModel && e.Property == DataContextProperty && IsReadOnly != null)
            {
                ((BaseValueViewModel)ViewModel).IsReadOnly = IsReadOnly.Value;
            }
        }

        #region IHasViewModel<ILabeledViewModel> Members

        public ILabeledViewModel ViewModel
        {
            get { return (ILabeledViewModel)DataContext; }
        }

        #endregion
    }
}
