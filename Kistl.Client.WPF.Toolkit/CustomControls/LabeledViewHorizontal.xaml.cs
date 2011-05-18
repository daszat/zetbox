
namespace Kistl.Client.WPF.CustomControls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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
    using Kistl.Client.Presentables.ValueViewModels;

    /// <summary>
    /// Interaction logic for LabeledViewHorizontal.xaml
    /// </summary>
    public partial class LabeledViewHorizontal : UserControl, IHasViewModel<ILabeledViewModel>
    {
        public LabeledViewHorizontal()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
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
            get { return (ILabeledViewModel)DataContext; }
        }

        #endregion
    }
}
