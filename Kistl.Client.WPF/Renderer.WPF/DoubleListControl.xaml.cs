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
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Kistl.GUI.Renderer.WPF
{
    /// <summary>
    /// Interaktionslogik für StringListControl.xaml
    /// </summary>
    public partial class DoubleListControl : ListControl, IListControl<Double>
    {
        public DoubleListControl()
        {
            Values = new ObservableCollection<Double>();
            InitializeComponent();
        }

        public Double? SearchBoxValue
        {
            get { return (Double?)GetValue(SearchBoxValueProperty); }
            set { SetValue(SearchBoxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchboxValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchBoxValueProperty =
            DependencyProperty.Register("SearchBoxValue", typeof(Double?), typeof(DoubleListControl), new UIPropertyMetadata(null));


        #region IListControl<Double> Member

        IList<Double> IListControl<Double>.Values
        {
            get { return Values; }
        }

        public new ObservableCollection<Double> Values
        {
            get { return (ObservableCollection<Double>)base.Values; }
            set { base.Values = value; }
        }

        #endregion

        #region Event Handlers

        private void lst_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (SearchBoxValue != null)
                AddItem(SearchBoxValue);
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            IList<Double> selection = lst.SelectedItems.Cast<Double>().ToList();
            foreach (Double s in selection)
            {
                RemoveItem(Values.IndexOf(s));
            }
        }

        #endregion

    }
}
