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

namespace Kistl.Client.Controls
{
    /// <summary>
    /// Interaction logic for ObjectList.xaml
    /// </summary>
    public partial class ObjectList : UserControl
    {
        public ObjectList()
        {
            InitializeComponent();
        }

        public void Bind()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            try
            {
                IClientObject client = ObjectBroker.GetClientObject(SourceClientObjectType);
                if (string.IsNullOrEmpty(PropertyName))
                {
                    DestinationClientObjectType = SourceClientObjectType;
                    DestinationServerObjectType = SourceServerObjectType;
                    this.DataContext = client.GetArrayFromXML(App.Service.GetList(SourceServerObjectType));
                }
                else
                {
                    if (ObjectID != API.Helper.INVALIDID)
                    {
                        MethodInfo mi = client.GetType().GetMethod("GetArrayOf" + PropertyName + "FromXML");
                        if (mi != null)
                        {
                            string xml = App.Service.GetListOf(SourceServerObjectType, ObjectID, PropertyName);
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

        public string SourceServerObjectType { get; set; }
        public string SourceClientObjectType { get; set; }
        public string DestinationServerObjectType { get; set; }
        public string DestinationClientObjectType { get; set; }

        public int ObjectID { get; set; }
        public string PropertyName { get; set; }

        private void lst_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                API.IDataObject obj = lst.SelectedItem as API.IDataObject;
                ObjectWindow wnd = new ObjectWindow();
                wnd.ServerObjectType = this.DestinationServerObjectType;
                wnd.ClientObjectType = this.DestinationClientObjectType;
                wnd.ObjectID = obj.ID;

                wnd.Show();
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ObjectWindow wnd = new ObjectWindow();
                wnd.ServerObjectType = this.DestinationServerObjectType;
                wnd.ClientObjectType = this.DestinationClientObjectType;
                wnd.ObjectID = API.Helper.INVALIDID;

                wnd.Show();
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Bind();
        }
    }
}
