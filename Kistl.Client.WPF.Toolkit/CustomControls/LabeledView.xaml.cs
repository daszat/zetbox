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

namespace Kistl.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for LabeledView.xaml
    /// </summary>
    public partial class LabeledView : UserControl, IHasViewModel<ILabeledViewModel>, INotifyPropertyChanged
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

        private object _requestedKind = null;
        public object RequestedKind
        {
            get { return _requestedKind ?? (ViewModel != null ? ViewModel.RequestedKind : null); }
            set 
            { 
                _requestedKind = value;
                PropertyChangedEventHandler temp = PropertyChanged;
                if (temp != null)
                {
                    temp(this, new PropertyChangedEventArgs("RequestedKind"));
                }
            }
        }

        #region IHasViewModel<ILabeledViewModel> Members

        public ILabeledViewModel ViewModel
        {
            get { return (ILabeledViewModel)DataContext; }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
