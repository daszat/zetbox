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
using Kistl.API.Client;

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
        /// Typ des Objektes, das angezeigt wird.
        /// </summary>
        public ObjectType ObjectType { get; set; }
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

        private void BindDefaultProperties()
        {
            // Object ID anzeigen - die kommt ja nicht mehr vor...
            // Neues Bearbeitungscontrol erzeugen
            Controls.EditSimpleProperty txt = new Controls.EditSimpleProperty();

            // Bezeichnung setzen
            txt.Label = "ID";
            txt.IsReadOnly = true;

            // Set Binding, damit werden Änderungen automatisch übernommen.
            Binding b = new Binding("ID");
            b.Mode = BindingMode.OneWay;
            b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            txt.SetBinding(Controls.EditSimpleProperty.ValueProperty, b);

            data.Children.Add(txt);
        }

        /// <summary>
        /// Objekt _einmalig_ binden - das erzeugt "nur" die WPF Controls
        /// </summary>
        private void Bind()
        {
            data.DataContext = obj;

            Kistl.App.Base.ObjectClassClient objClassClient = new Kistl.App.Base.ObjectClassClient();
            Kistl.App.Base.ObjectClass objClass = Helper.ObjectClasses.Single(o => o.Namespace == ObjectType.Namespace && o.ClassName == ObjectType.Classname);

            BindDefaultProperties();

            // Aus Metadaten holen
            foreach (Kistl.App.Base.ObjectProperty p in objClassClient.GetListOfProperties(objClass.ID))
            {
                if (p.IsList && p.IsAssociation)
                {
                    Controls.ObjectList lst = new Kistl.Client.Controls.ObjectList();
                    lst.SourceObjectType = this.ObjectType;

                    // aus Metadaten auslesen
                    lst.DestinationObjectType = new ObjectType(p.DataType);

                    lst.ObjectID = this.ObjectID;
                    lst.PropertyName = p.PropertyName;

                    data.Children.Add(lst);
                }
                else
                {
                    // Neues Bearbeitungscontrol erzeugen
                    Controls.EditSimpleProperty txt = new Controls.EditSimpleProperty();

                    // Bezeichnung setzen
                    txt.Label = p.PropertyName; 

                    // Set Binding, damit werden Änderungen automatisch übernommen.
                    Binding b = new Binding(p.PropertyName);
                    b.Mode = BindingMode.TwoWay;
                    b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    txt.SetBinding(Controls.EditSimpleProperty.ValueProperty, b);
                    
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
                this.Title = ObjectType.ToString();

                // Client BL holen
                client = Helper.GetClientObject(ObjectType);

                // Je nachdem, Objekt vom Server holen oder mittels BL erzeugen
                // TODO: Das holen solte auch in die BL rein & Typisiert werden.
                // allerdings brauchts dann zwei Methodenarten: Die generischen & typisierten
                if (ObjectID != API.Helper.INVALIDID)
                {
                    obj = client.GetObjectGeneric(ObjectID);
                }
                else
                {
                    obj = client.CreateNewGeneric();
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
                obj = client.SetObjectGeneric(obj);
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
