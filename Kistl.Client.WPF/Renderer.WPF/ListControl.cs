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

namespace Kistl.GUI.Renderer.WPF
{
    /// <summary>
    /// Interaktionslogik für StringListControl.xaml
    /// </summary>
    public partial class ListControl : PropertyControl
    {
        public ListControl()
        {
            Values = new ObservableCollection<string>();
        }

        public event NotifyCollectionChangedEventHandler UserChangedList;

        public System.Collections.IList Values
        {
            get { return (System.Collections.IList)GetValue(ValuesProperty); }
            set { SetValue(ValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Values.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValuesProperty =
            DependencyProperty.Register("Values", typeof(System.Collections.IList), typeof(ListControl), new PropertyMetadata());

        #region Behaviours

        /// <summary>
        /// Adds an item to the Values List
        /// </summary>
        protected virtual void AddItem(object item)
        {
            Values.Add(item);
            OnUserChangedList(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, Values.Count - 1));
        }

        protected virtual void RemoveItem(int idx)
        {
            object oldItem = Values[idx];
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

    }
}
