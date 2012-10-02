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

namespace Zetbox.Client.WPF.View.ZetboxBase
{
    /// <summary>
    /// Interaction logic for PropertiesPrewiewDisplay.xaml
    /// </summary>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public partial class PropertiesPrewiewDisplay : UserControl, IHasViewModel<PropertiesPrewiewViewModel>
    {
        public PropertiesPrewiewDisplay()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ViewModel != null && e.Property == FrameworkElement.DataContextProperty)
            {
                ViewModel.PropertyChanged += (s, pce) =>
                {
                    if (pce.PropertyName == "DisplayedColumns")
                    {
                        ViewModel.DisplayedColumns.Columns.CollectionChanged += (sncc, ncc) => ApplyColumns();
                        ApplyColumns();
                    }
                };
                ViewModel.DisplayedColumns.Columns.CollectionChanged += (s, ncc) => ApplyColumns();
                ApplyColumns();
            }
        }

        private void ApplyColumns()
        {
            WPFHelper.RefreshGridView(lst, ViewModel.DisplayedColumns, WpfSortHelper.SortPropertyNameProperty);
        }

        public PropertiesPrewiewViewModel ViewModel
        {
            get { return (PropertiesPrewiewViewModel)WPFHelper.SanitizeDataContext(DataContext); }
        }
    }
}
