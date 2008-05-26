using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
    /// Zeigt eine Liste von Objekten an.
    /// Sie kann _alle_ Instanzen einer Klasse anzeigen &
    /// alle Instanzen einer Eigenschaft eines Objektes.
    /// </summary>
    public partial class ObjectListControl
        : PropertyControl, IObjectListControl
    {

        static ObjectListControl()
        {
            SelectionChangedEvent = EventManager.RegisterRoutedEvent("SelectionChanged", RoutingStrategy.Bubble,
                            typeof(SelectionChangedEventHandler), typeof(ObjectListControl));
        }

        public static RoutedEvent SelectionChangedEvent;
        public event SelectionChangedEventHandler SelectionChanged
        {
            add { AddHandler(SelectionChangedEvent, value); }
            remove { RemoveHandler(SelectionChangedEvent, value); }
        }

        public ObjectListControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// DoppelKlick -> öffnet das Objekt in einem neuen Fenster
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lst_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //TODO: OnUserSelectsObject(sender);
        }

        private void lst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChangedEventArgs args = new SelectionChangedEventArgs(
                SelectionChangedEvent, e.RemovedItems, e.AddedItems);
            RaiseEvent(args);
        }

        #region IReferenceControl<IList<IDataObject>> Members

        public Kistl.API.ObjectType ObjectType
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IList<Kistl.API.IDataObject> ItemsSource
        {
            get { return (IList<Kistl.API.IDataObject>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IList<Kistl.API.IDataObject>), typeof(ObjectListControl), new PropertyMetadata(null));

        #endregion

        #region IValueControl<IList<IDataObject>> Members

        public IList<Kistl.API.IDataObject> Value
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public event EventHandler UserInput;

        #endregion
    }
}
