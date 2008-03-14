using System;
using System.Collections;
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
using Kistl.API;
using System.ComponentModel;
using System.Reflection;
using Kistl.API.Client;

namespace Kistl.Client.Controls
{
    /// <summary>
    /// Zeigt eine Liste von Objekten an.
    /// Sie kann _alle_ Instanzen einer Klasse anzeigen &
    /// alle Instanzen einer Eigenschaft eines Objektes.
    /// </summary>
    public partial class ObjectList : UserControl
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

        public Kistl.API.Client.KistlContext Context
        {
            get { return (Kistl.API.Client.KistlContext)GetValue(ContextProperty); }
            set { SetValue(ContextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Context.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContextProperty =
            DependencyProperty.Register("Context", typeof(Kistl.API.Client.KistlContext), typeof(ObjectList));

        
        public ObjectList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Binden der Liste, kann auch mehrmals ausgelöst werden & von extern.
        /// </summary>
        public void Bind()
        {
            // Desingmode? -> raus
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            try
            {
                // Client BL holen
                using (KistlContext ctx = new KistlContext())
                {
                    if (string.IsNullOrEmpty(PropertyName))
                    {
                        // Wenn PropertyName nicht gesetzt ist, dann mein man die Liste _aller_
                        // Objekte einer Klasse
                        // Destination ist dann gleich Source
                        DestinationObjectType = SourceObjectType;

                        // Liste vom Server holen & den DataContext setzen.
                        this.DataContext = ctx.GetQuery(SourceObjectType).ToList();
                    }
                    else
                    {
                        // Wenn PropertyName gesetzt ist, dann meint man die Liste von Objekten
                        // zu einem Objekt
                        BaseClientDataObject obj = ctx.GetQuery(SourceObjectType).Single(o => o.ID == ObjectID);
                        this.DataContext = obj.GetPropertyValue<IEnumerable>(PropertyName);
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
            }
        }

        void ObjectList_Loaded(object sender, RoutedEventArgs e)
        {
            Bind();

            if (string.IsNullOrEmpty(Label))
            {
                lbLabel.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// ObjectType des Quellobjektes
        /// </summary>
        public ObjectType SourceObjectType { get; set; }
        
        /// <summary>
        /// Objecttype des Zielobjektes, falls eine Liste einer Property angezeigt werden soll.
        /// Dient nur der Übergabe an ein neues Fenster, welches beim Doppel-Klick
        /// bzw. "New" geöffnet wird.
        /// </summary>
        public ObjectType DestinationObjectType { get; set; }

        /// <summary>
        /// ObjektID
        /// </summary>
        public int ObjectID
        {
            get { return (int)GetValue(ObjectIDProperty); }
            set { SetValue(ObjectIDProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ObjectID.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectIDProperty =
            DependencyProperty.Register("ObjectID", typeof(int), typeof(ObjectList));



        /// <summary>
        /// Property des Objektes, dessen Liste dargestellt werden soll.
        /// ObjektID & Destination*ObjectType müssen auch ausgefüllt sein!
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Bezeichnung der Liste, wenn leer, dann wird's auch nicht angezeigt
        /// </summary>
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Label.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(ObjectList));


        /// <summary>
        /// DoppelKlick -> öffnet das Objekt in einem neuen Fenster
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lst_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (lst.SelectedItem == null) return;
                API.IDataObject obj = lst.SelectedItem as API.IDataObject;
                ObjectWindow wnd = new ObjectWindow();
                wnd.ObjectType = this.DestinationObjectType;
                wnd.ObjectID = obj.ID;

                wnd.Show();
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
            }
        }

        /// <summary>
        /// Erzeugt ein neues Objekt in einem neuen Fenster
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ObjectType resultObjectType = this.DestinationObjectType;

                Kistl.App.Base.ObjectClass objClass = Helper.ObjectClasses[DestinationObjectType];

                if (objClass.SubClasses.Count > 0)
                {
                    // TODO: Das ist noch nicht ganz konsistent
                    Dialogs.ChooseObjectClass dlg = new Kistl.Client.Dialogs.ChooseObjectClass();
                    dlg.BaseObjectClass = objClass;

                    if (dlg.ShowDialog() == true)
                    {
                        resultObjectType = new ObjectType(dlg.ResultObjectClass.Module.Namespace, dlg.ResultObjectClass.ClassName);
                    }
                    else
                    {
                        // Do nothing
                        return;
                    }
                }

                ObjectWindow wnd = new ObjectWindow();
                wnd.ObjectType = resultObjectType;
                wnd.ObjectID = API.Helper.INVALIDID;

                wnd.SourceObjectID = this.ObjectID;
                wnd.SourceObjectType = this.SourceObjectType;

                wnd.ShowDialog();

                if (wnd.ObjectID != API.Helper.INVALIDID)
                {
                    // ReBind
                    Bind();
                }
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
            }
        }

        /// <summary>
        /// Liste neu Laden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Bind();
        }

        private void lst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChangedEventArgs args = new SelectionChangedEventArgs(
                SelectionChangedEvent, e.RemovedItems, e.AddedItems);
            RaiseEvent(args);
        }
    }
}
