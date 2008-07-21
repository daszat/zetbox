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
    public partial class StringListControl : ListControl, IListControl<string>
    {
        public StringListControl()
        {
            Values = new ObservableCollection<string>();
            InitializeComponent();
        }

        public string SearchBoxValue
        {
            get { return (string)GetValue(SearchBoxValueProperty); }
            set { SetValue(SearchBoxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchboxValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchBoxValueProperty =
            DependencyProperty.Register("SearchBoxValue", typeof(string), typeof(StringListControl), new UIPropertyMetadata(null));


        #region IListControl<string> Member

        IList<string> IListControl<string>.Values
        {
            get { return Values; }
        }

        public new ObservableCollection<string> Values
        {
            get { return (ObservableCollection<string>)base.Values; }
            set { base.Values = value; }
        }

        public event EventHandler<GenericEventArgs<IList<string>>> UserActivatedSelection;

        #endregion

        #region Event Handlers

        private void lst_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnUserActivatedSelection(UserActivatedSelection, lst.SelectedItems);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (SearchBoxValue != null)
                AddItem(SearchBoxValue);
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            IList<string> selection = lst.SelectedItems.Cast<string>().ToList();
            foreach (string s in selection)
            {
                RemoveItem(Values.IndexOf(s));
            }
        }

        #endregion

    }
}
