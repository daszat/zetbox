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
    /// Interaktionslogik für IntegerListControl.xaml
    /// </summary>
    public partial class IntegerListControl : ListControl, IListControl<int>
    {
        public IntegerListControl()
        {
            Values = new ObservableCollection<int>();
            InitializeComponent();
        }

        public int? SearchBoxValue
        {
            get { return (int?)GetValue(SearchBoxValueProperty); }
            set { SetValue(SearchBoxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchBoxValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchBoxValueProperty =
            DependencyProperty.Register("SearchBoxValue", typeof(int?), typeof(IntegerListControl), new UIPropertyMetadata(null));

        #region IListControl<int> Member

        IList<int> IListControl<int>.Values
        {
            get { return Values; }
        }

        public new ObservableCollection<int> Values
        {
            get { return (ObservableCollection<int>)base.Values; }
            set { base.Values = value; }
        }

        public event EventHandler<GenericEventArgs<IList<int>>> UserActivatedSelection;

        #endregion

        #region Event Handlers

        private void lst_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnUserActivatedSelection(UserActivatedSelection, lst.SelectedItems);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (SearchBoxValue.HasValue)
                AddItem(SearchBoxValue.Value);
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            IList<int> selection = lst.SelectedItems.Cast<int>().ToList();
            foreach (int s in selection)
            {
                RemoveItem(Values.IndexOf(s));
            }
        }

        #endregion

    }
}
