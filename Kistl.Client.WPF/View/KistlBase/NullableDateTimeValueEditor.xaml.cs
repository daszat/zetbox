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
using Kistl.Client.Presentables.ValueViewModels;
using Kistl.Client.WPF.View.KistlBase;

namespace Kistl.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for NullableDateTimeValueEditor.xaml
    /// </summary>
    [ViewDescriptor(Kistl.App.GUI.Toolkit.WPF)]
    public partial class NullableDateTimeValueEditor : PropertyEditor, IHasViewModel<NullableDateTimePropertyViewModel>
    {
        public NullableDateTimeValueEditor()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ViewModel != null && e.Property == FrameworkElement.DataContextProperty)
            {
                FixTimePart();
                ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            }
        }

        void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DatePartVisible")
            {
                FixTimePart();
            }
        }

        private void FixTimePart()
        {
            // Fix lonely Time Textbox
            txtTime.SetValue(DockPanel.DockProperty, ViewModel.DatePartVisible ? Dock.Right : Dock.Top);
            InitializeFocusManager();
        }

        #region IHasViewModel<NullableDateTimePropertyViewModel> Members

        public NullableDateTimePropertyViewModel ViewModel
        {
            get { return (NullableDateTimePropertyViewModel)DataContext; }
        }

        #endregion

        protected override FrameworkElement MainControl
        {
            get 
            {
                if (ViewModel == null) return null;
                return ViewModel.DatePartVisible ? (FrameworkElement)txtDate : (FrameworkElement)txtTime;
            }
        }
    }
}
