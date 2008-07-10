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
    public partial class StringListControl : PropertyControl, IListControl<string>
    {
        public StringListControl()
        {
            Values = new ObservableCollection<string>();
            InitializeComponent();
        }

        #region Behaviours

        protected void AddItem(string item)
        {
            Values.Add(item);
            OnUserChangedList(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, Values.Count - 1));
        }

        protected void RemoveItem(int idx)
        {
            string oldItem = Values[idx];
            Values.RemoveAt(idx);
            OnUserChangedList(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, oldItem, idx));
        }

        protected virtual void OnUserChangedList(NotifyCollectionChangedEventArgs e)
        {
            if (UserChangedList != null)
            {
                UserChangedList(this, e);
            }
        }

        #endregion

        #region Event Handlers

        private void lst_MouseDoubleClick(object sender, MouseEventArgs e) { }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddItem(inputBox.Text);
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

        #region IListControl<string> Member

        IList<string> IListControl<string>.Values
        {
            get { return Values; }
        }

        public ObservableCollection<string> Values
        {
            get { return (ObservableCollection<string>)GetValue(ValuesProperty); }
            set { SetValue(ValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Values.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValuesProperty =
            DependencyProperty.Register("Values", typeof(ObservableCollection<string>), typeof(StringListControl), new PropertyMetadata());

        public event NotifyCollectionChangedEventHandler UserChangedList;

        #endregion

    }
}
