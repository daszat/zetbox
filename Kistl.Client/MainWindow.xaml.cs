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
using System.Xml.Serialization;
using Kistl.API;
using System.ComponentModel;

namespace Kistl.Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (DesignerProperties.GetIsInDesignMode(this)) return;

            try
            {
                // TODO: Das muss einfacher gehen!
                // Es muss über den blöden ObjectBroker gehen, weil 
                // sonst die Custom Actions nicht angehängt werden (zur Zeit)
                Kistl.App.Base.ObjectClassClient client = (Kistl.App.Base.ObjectClassClient)ObjectBroker.GetClientObject(typeof(Kistl.App.Base.ObjectClassClient).AssemblyQualifiedName);
                this.DataContext = client.GetArrayFromXML(App.Service.GetList("Kistl.App.Base.ObjectClassServer, Kistl.App.Projekte"));
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
            }
        }

        private void menu_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Kistl.App.Base.ObjectClass objClass = cbObjectTypes.SelectedItem as Kistl.App.Base.ObjectClass;
                if (objClass != null)
                {
                    lst.SourceServerObjectType = objClass.ServerObject;
                    lst.SourceClientObjectType= objClass.ClientObject;
                    lst.Bind();
                }
            }
            catch(Exception ex)
            {
                Helper.HandleError(ex);
            }
        }
    }
}
