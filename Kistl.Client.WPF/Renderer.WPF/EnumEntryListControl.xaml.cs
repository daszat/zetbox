using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

using Kistl.App.Base;

namespace Kistl.GUI.Renderer.WPF
{
    /// <summary>
    /// Interaktionslogik für StringListControl.xaml
    /// </summary>
    public partial class EnumEntryListControl : ListControl, IListControl<EnumerationEntry>
    {
        public EnumEntryListControl()
        {
            Values = new ObservableCollection<EnumerationEntry>();
            InitializeComponent();
        }

        #region Behaviours

        protected void AddItem(EnumerationEntry item)
        {
            Values.Add(item);
            OnUserChangedList(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, Values.Count - 1));
        }

        protected void RemoveItem(int idx)
        {
            EnumerationEntry oldItem = Values[idx];
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

        private void lst_MouseDoubleClick(object sender, MouseEventArgs e) {
            OnUserActivatedSelection(UserActivatedSelection, lst.SelectedItems);
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException("AddItem(inputBox.Text);");
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            IList<EnumerationEntry> selection = lst.SelectedItems.Cast<EnumerationEntry>().ToList();
            foreach (EnumerationEntry s in selection)
            {
                RemoveItem(Values.IndexOf(s));
            }
        }

        #endregion

        #region IListControl<string> Member

        IList<EnumerationEntry> IListControl<EnumerationEntry>.Values
        {
            get { return Values; }
        }

        public ObservableCollection<EnumerationEntry> Values
        {
            get { return (ObservableCollection<EnumerationEntry>)GetValue(ValuesProperty); }
            set { SetValue(ValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Values.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValuesProperty =
            DependencyProperty.Register("Values", typeof(ObservableCollection<EnumerationEntry>), typeof(EnumEntryListControl), new PropertyMetadata());

        public event NotifyCollectionChangedEventHandler UserChangedList;
        public event EventHandler<GenericEventArgs<IList<EnumerationEntry>>> UserActivatedSelection;

        #endregion

    }

}
