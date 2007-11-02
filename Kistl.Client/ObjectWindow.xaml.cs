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

        private IClientObject client = null;
        private Kistl.API.IDataObject obj = null;

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
                    Controls.EditSimpleProperty txt = new Controls.EditSimpleProperty();
                    txt.Label = p.Name;

                    // Set Binding
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
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            try
            {
                this.Title = ClientObjectType;

                client = ObjectBrokerClient.GetClientObject(ClientObjectType);

                if (ObjectID != API.Helper.INVALIDID)
                {
                    obj = client.GetObjectFromXML(App.Service.GetObject(ServerObjectType, ObjectID));
                }
                else
                {
                    obj = client.CreateNew();
                }

                Bind();
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
            }
        }

        private void MenuItem_Click_Save(object sender, RoutedEventArgs e)
        {
            try
            {
                string result = App.Service.SetObject(ServerObjectType, obj.ToXmlString());
                obj = client.GetObjectFromXML(result);
                // ReBind
                data.DataContext = obj;
                // Das muss sein, weil die Properties keine DependencyProperties sind
                // Nutzt aber nix, da ich ein neues Objekt zurück bekommen habe *grrr*
                //obj.NotifyChange(); 
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
            }
        }

        private void MenuItem_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
