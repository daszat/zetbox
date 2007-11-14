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
using Kistl.API.Client;

namespace Kistl.Client
{
    /// <summary>
    /// Hauptfenster der KistApplikation
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // DesingMode? -> raus
            if (DesignerProperties.GetIsInDesignMode(this)) return;

            try
            {
                // Hole eine Liste aller ObjectClasses Objekte & zeige sie in der DropDown an
                Kistl.App.Base.ObjectClassClient client = new Kistl.App.Base.ObjectClassClient();
                lst.SourceObjectType = client.Type;

                this.DataContext = Helper.ObjectClasses;
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
            }
        }

        /// <summary>
        /// Und auf wieder sehen!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// DropDown Liste hat sich geändert -> Objektliste im unteren Bereich aktualisieren
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Kistl.App.Base.ObjectClass objClass = cbObjectTypes.SelectedItem as Kistl.App.Base.ObjectClass;
                if (objClass != null)
                {
                    // Neue Objekttypen setzen & neu Binden
                    // TODO: Man sollte gleich das ObjektClass Objekt übergeben.
                    lst.SourceObjectType = new ObjectType(objClass.Namespace, objClass.ClassName);
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
