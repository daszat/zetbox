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
                IClientObject client = Helper.GetClientObject(SourceObjectType);

                if (string.IsNullOrEmpty(PropertyName))
                {
                    // Wenn PropertyName nicht gesetzt ist, dann mein man die Liste _aller_
                    // Objekte einer Klasse
                    // Destination ist dann gleich Source
                    DestinationObjectType = SourceObjectType;

                    // Liste vom Server holen & den DataContext setzen.
                    this.DataContext = client.GetArrayFromXML(App.Service.GetList(SourceObjectType));
                }
                else
                {
                    // Wenn PropertyName gesetzt ist, dann meint man die Liste von Objekten
                    // zu einem Objekt
                    if (ObjectID != API.Helper.INVALIDID)
                    {
                        // Client Methode holen
                        MethodInfo mi = client.GetType().GetMethod("GetArrayOf" + PropertyName + "FromXML");
                        if (mi != null)
                        {
                            // Liste vom Server holen & den DataContext setzen.
                            string xml = App.Service.GetListOf(SourceObjectType, ObjectID, PropertyName);
                            this.DataContext = mi.Invoke(client, new object[] { xml });
                        }
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
        public int ObjectID { get; set; }

        /// <summary>
        /// Property des Objektes, dessen Liste dargestellt werden soll.
        /// ObjektID & Destination*ObjectType müssen auch ausgefüllt sein!
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// DoppelKlick -> öffnet das Objekt in einem neuen Fenster
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lst_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
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
                ObjectWindow wnd = new ObjectWindow();
                wnd.ObjectType = this.DestinationObjectType;
                wnd.ObjectID = API.Helper.INVALIDID;

                wnd.Show();
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
    }
}
