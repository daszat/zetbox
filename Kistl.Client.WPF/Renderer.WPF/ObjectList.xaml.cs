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
    public partial class ObjectList : PropertyControl, IObjectListControl
    {

        static ObjectList()
        {
            SelectionChangedEvent = EventManager.RegisterRoutedEvent("SelectionChanged", RoutingStrategy.Bubble,
                            typeof(SelectionChangedEventHandler), typeof(ObjectList));
        }

        public static RoutedEvent SelectionChangedEvent;
        public event SelectionChangedEventHandler SelectionChanged
        {
            add { AddHandler(SelectionChangedEvent, value); }
            remove { RemoveHandler(SelectionChangedEvent, value); }
        }

        public ObjectList()
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

        #region IObjectListControl Members

        IList<Kistl.API.IDataObject> IObjectListControl.Value
        {
            get
            {
                return (IList<Kistl.API.IDataObject>)lst.ItemsSource;
            }
            set
            {
                lst.ItemsSource = value;
            }
        }

        // TODO: has to be called when editing lst.ItemsSource
        protected virtual void OnUserInput(DependencyPropertyChangedEventArgs e)
        {
            if (_UserInput != null)
            {
                _UserInput(this, new EventArgs());
            }
        }

        private event EventHandler _UserInput;
        event EventHandler IObjectListControl.UserInput
        {
            add { _UserInput += value; }
            remove { _UserInput -= value; }
        }

        #endregion

    }
}
