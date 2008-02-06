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
using Kistl.API.Client;
using Kistl.API;
using System.ComponentModel;

namespace Kistl.Client
{
    /// <summary>
    /// Interaction logic for ObjectControl.xaml
    /// </summary>
    public partial class ObjectControl : UserControl
    {
        public ObjectControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Typ des Objektes, das angezeigt wird.
        /// </summary>
        public ObjectType ObjectType { get; set; }
        /// <summary>
        /// Typ des Objektes, welches dieses Objekt geöffnet hat.
        /// </summary>
        public ObjectType SourceObjectType { get; set; }
        /// <summary>
        /// ObjektID
        /// </summary>
        public int ObjectID { get; set; }
        /// <summary>
        /// ObjectID jenes Objektes, welches dieses Objekt geöffent hat.
        /// </summary>
        public int SourceObjectID { get; set; }

        /// <summary>
        /// Client BL Objekt instanz
        /// </summary>
        private KistlContext ctx = null;
        /// <summary>
        /// Datenobjekt, das angezeigt wird.
        /// </summary>
        private Kistl.API.Client.BaseClientDataObject obj = null;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Im Designer? -> raus
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            try
            {
                // Client BL holen
                ctx = new KistlContext();
                obj = ctx.GetQuery(ObjectType).SingleOrDefault(o => o.ID == ObjectID);

                // Objekttype anpassen
                ObjectType = new ObjectType(obj);

                // Fensternamen setzen
                // SetTitle();

                // Einmalig Binden & WPF Controls erzeugen
                // Bind();
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
            }

        }

        private void UserControl_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }
    }
}
