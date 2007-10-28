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

        public string ServerObjectType { get; set; }
        public string ClientObjectType { get; set; }
        public int ObjectID { get; set; }

        private IClientObject client = null;
        private Kistl.API.IDataObject obj = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            try
            {
                client = ClientObjectHelper.GetClientObject(ClientObjectType);
                obj = client.GetObjectFromXML(App.Service.GetObject(ServerObjectType, ObjectID));

                foreach (System.Reflection.PropertyInfo p in obj.GetType().GetProperties())
                {
                    if (p.PropertyType.GetInterface("System.Collections.ICollection") != null)
                    {
                        Controls.ObjectList lst = new Kistl.Client.Controls.ObjectList();
                        lst.SourceServerObjectType = this.ServerObjectType;
                        lst.SourceClientObjectType = this.ClientObjectType;
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
                        txt.Value = p.GetValue(obj, null);
                        txt.PropertyInfo = p;

                        data.Children.Add(txt);
                    }
                }
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
                foreach (object ctrl in data.Children)
                {
                    Controls.EditSimpleProperty txt = ctrl as Controls.EditSimpleProperty;
                    if (txt != null)
                    {
                        txt.PropertyInfo.SetValue(obj, txt.Value, null);
                    }
                }

                App.Service.SetObject(ServerObjectType, obj.ToXmlString());
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
