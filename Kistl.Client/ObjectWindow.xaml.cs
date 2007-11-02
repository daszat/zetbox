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

namespace Kistl.Client
{
    /// <summary>
    /// Interaction logic for ObjectWindow.xaml
    /// </summary>
    public partial class ObjectWindow : Window
    {
        public ObjectWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Server BL Typ des Objektes, das angezeigt wird.
        /// </summary>
        public string ServerObjectType { get; set; }
        /// <summary>
        /// Client BL Typ des Objektes, das angezeigt wird.
        /// </summary>
        public string ClientObjectType { get; set; }
        /// <summary>
        /// ObjektID
        /// </summary>
        public int ObjectID { get; set; }

        /// <summary>
        /// Client BL Objekt instanz
        /// </summary>
        private IClientObject client = null;

        /// <summary>
        /// Datenobjekt, das angezeigt wird.
        /// </summary>
        private Kistl.API.IDataObject obj = null;

        /// <summary>
        /// Objekt _einmalig_ binden - das erzeugt "nur" die WPF Controls
        /// </summary>
        private void Bind()
        {
            data.DataContext = obj;

            // TODO: Aus Metadaten holen
            foreach (System.Reflection.PropertyInfo p in obj.GetType().GetProperties())
            {
                if (p.PropertyType.GetInterface("System.Collections.ICollection") != null)
                {
                    // TODO: Naja, das könnte besser sein
                    Controls.ObjectList lst = new Kistl.Client.Controls.ObjectList();
                    lst.SourceServerObjectType = this.ServerObjectType;
                    lst.SourceClientObjectType = this.ClientObjectType;

                    // TODO: aus Metadaten auslesen
                    lst.DestinationServerObjectType = ((API.ServerObjectAttribute)p.GetCustomAttributes(typeof(API.ServerObjectAttribute), true)[0]).FullName;
                    lst.DestinationClientObjectType = ((API.ClientObjectAttribute)p.GetCustomAttributes(typeof(API.ClientObjectAttribute), true)[0]).FullName;

                    lst.ObjectID = this.ObjectID;
                    lst.PropertyName = p.Name;

                    data.Children.Add(lst);
                }
                else
                {
                    // Neues Bearbeitungscontrol erzeugen
                    Controls.EditSimpleProperty txt = new Controls.EditSimpleProperty();

                    // Bezeichnung setzen
                    txt.Label = p.Name; 

                    // Set Binding, damit werden Änderungen automatisch übernommen.
                    Binding b = new Binding();
                    b.Path = new PropertyPath(p.Name);
                    b.Mode = BindingMode.TwoWay;
                    b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    txt.SetBinding(Controls.EditSimpleProperty.ValueProperty, b);
                    
                    txt.PropertyInfo = p;

                    data.Children.Add(txt);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Im Designer? -> raus
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            try
            {
                this.Title = ClientObjectType;

                // Client BL holen
                client = ObjectBrokerClient.GetClientObject(ClientObjectType);

                // Je nachdem, Objekt vom Server holen oder mittels BL erzeugen
                // TODO: Das holen solte auch in die BL rein & Typisiert werden.
                // allerdings brauchts dann zwei Methodenarten: Die generischen & typisierten
                if (ObjectID != API.Helper.INVALIDID)
                {
                    obj = client.GetObjectFromXML(App.Service.GetObject(ServerObjectType, ObjectID));
                }
                else
                {
                    obj = client.CreateNew();
                }

                // Einmalig Binden & WPF Controls erzeugen
                Bind();
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
            }
        }

        /// <summary>
        /// Objekt speichern
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_Save(object sender, RoutedEventArgs e)
        {
            try
            {
                // Objekt zum Server schicken & dann wieder auspacken
                string result = App.Service.SetObject(ServerObjectType, obj.ToXmlString());
                obj = client.GetObjectFromXML(result);
                // ReBind
                data.DataContext = obj;
                // Das muss sein, weil die Properties keine DependencyProperties sind
                // Nutzt aber nix, da ich ein neues Objekt zurück bekommen habe *grrr*
                // einfach den Datakontext neu setzen (siehe oben)
                // obj.NotifyChange(); 
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
            }
        }

        /// <summary>
        /// Auf Wiedersehen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
