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

        private List<Kistl.App.Base.ObjectClass> GetObjectHierarchie()
        {
            Kistl.App.Base.ObjectClass objClass = Helper.ObjectClasses[ObjectType]; //.First(o => o.GetObject<Kistl.App.Base.Module>("Module").Namespace == ObjectType.Namespace && o.ClassName == ObjectType.Classname);
            List<Kistl.App.Base.ObjectClass> objClasses = new List<Kistl.App.Base.ObjectClass>();
            while (objClass != null)
            {
                objClasses.Add(objClass);

                if (objClass.fk_BaseObjectClass == API.Helper.INVALIDID)
                {
                    objClass = null;
                }
                else
                {
                    objClass = Helper.ObjectClasses.Values.First(o => o.ID == objClass.fk_BaseObjectClass);
                }
            }

            objClasses.Reverse();
            return objClasses;
        }

        /// <summary>
        /// Objekt _einmalig_ binden - das erzeugt "nur" die WPF Controls
        /// </summary>
        private void Bind()
        {
            data.DataContext = obj;

            BindDefaultProperties();
            
            // Objektklassenhierarchie holen
            //Kistl.App.Base.ObjectClassClient objClassClient = new Kistl.App.Base.ObjectClassClient();
            foreach (Kistl.App.Base.ObjectClass objClass in GetObjectHierarchie())
            {
                #region Binden
                // Aus Metadaten holen
                foreach (Kistl.App.Base.BaseProperty p in objClass.Properties)
                //foreach (Kistl.App.Base.BaseProperty p in objClassClient.GetListOfProperties(objClass.ID))
                {
                    if (p is Kistl.App.Base.BackReferenceProperty)
                    {
                        Controls.ObjectList lst = new Kistl.Client.Controls.ObjectList();
                        lst.SourceObjectType = this.ObjectType;
                        lst.Label = p.PropertyName;
                        lst.ToolTip = p.AltText;

                        // aus Metadaten auslesen
                        lst.DestinationObjectType = new ObjectType(p.GetDataType());

                        lst.ObjectID = this.ObjectID;
                        lst.PropertyName = p.PropertyName;

                        data.Children.Add(lst);
                    }
                    else if (p is Kistl.App.Base.ObjectReferenceProperty)
                    {
                        Controls.EditPointerProperty pointer = new Kistl.Client.Controls.EditPointerProperty();
                        pointer.Label = p.PropertyName;
                        pointer.ToolTip = p.AltText;

                        pointer.ObjectType = new ObjectType(p.GetDataType());

                        // Set Binding, damit werden Änderungen automatisch übernommen.
                        Binding b = new Binding(p.PropertyName);
                        b.Mode = BindingMode.TwoWay;
                        b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                        b.NotifyOnSourceUpdated = true;
                        b.NotifyOnTargetUpdated = true;
                        pointer.SetBinding(Controls.EditPointerProperty.ValueProperty, b);

                        data.Children.Add(pointer);

                        // fk setzten, falls vorhanden
                        if (SourceObjectID != API.Helper.INVALIDID)
                        {
                            if (pointer.ObjectType.Equals(SourceObjectType))
                            {
                                obj.GetType().GetProperty(p.PropertyName).SetValue(obj, SourceObjectID, new object[] { });
                            }
                        }
                    }
                    else if (p is Kistl.App.Base.BoolProperty)
                    {
                        // Neues Bearbeitungscontrol erzeugen
                        Controls.EditBoolProperty boolCtrl = new Controls.EditBoolProperty();

                        // Bezeichnung setzen
                        boolCtrl.Label = p.PropertyName;
                        boolCtrl.ToolTip = p.AltText;

                        // Set Binding, damit werden Änderungen automatisch übernommen.
                        Binding b = new Binding(p.PropertyName);
                        b.Mode = BindingMode.TwoWay;
                        b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                        b.NotifyOnSourceUpdated = true;
                        b.NotifyOnTargetUpdated = true;
                        boolCtrl.SetBinding(Controls.EditBoolProperty.ValueProperty, b);

                        data.Children.Add(boolCtrl);
                    }
                    else if (p is Kistl.App.Base.DateTimeProperty)
                    {
                        // Neues Bearbeitungscontrol erzeugen
                        Controls.EditDateTimeProperty dtCtrl = new Controls.EditDateTimeProperty();

                        // Bezeichnung setzen
                        dtCtrl.Label = p.PropertyName;
                        dtCtrl.ToolTip = p.AltText;

                        // Set Binding, damit werden Änderungen automatisch übernommen.
                        Binding b = new Binding(p.PropertyName);
                        b.Mode = BindingMode.TwoWay;
                        b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                        b.NotifyOnSourceUpdated = true;
                        b.NotifyOnTargetUpdated = true;
                        dtCtrl.SetBinding(Controls.EditDateTimeProperty.ValueProperty, b);

                        data.Children.Add(dtCtrl);
                    }
                    else
                    {
                        // Neues Bearbeitungscontrol erzeugen
                        Controls.EditSimpleProperty txt = new Controls.EditSimpleProperty();

                        // Bezeichnung setzen
                        txt.Label = p.PropertyName;
                        txt.ToolTip = p.AltText;

                        // Set Binding, damit werden Änderungen automatisch übernommen.
                        Binding b = new Binding(p.PropertyName);
                        b.Mode = BindingMode.TwoWay;
                        b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                        b.NotifyOnSourceUpdated = true;
                        b.NotifyOnTargetUpdated = true;
                        txt.SetBinding(Controls.EditSimpleProperty.ValueProperty, b);

                        data.Children.Add(txt);
                    }
                }
                #endregion
            }
        }

        private bool IsObjectDirty = false;
        private void SetTitle()
        {
            if (IsObjectDirty && !this.Title.StartsWith("*"))
            {
                this.Title = "* " + ObjectType.ToString();
            }
            else if (!IsObjectDirty)
            {
                this.Title = ObjectType.ToString();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Im Designer? -> raus
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            try
            {
                // Client BL holen
                //client = ClientObjectFactory.GetClientObject(ObjectType);
                ctx = new KistlContext();

                obj = ctx.GetQuery(ObjectType).SingleOrDefault(o => o.ID == ObjectID);

                /*
                // Je nachdem, Objekt vom Server holen oder mittels BL erzeugen
                if (ObjectID != API.Helper.INVALIDID)
                {
                    obj = client.GetObjectGeneric(ObjectID);
                }
                else
                {
                    obj = client.CreateNewGeneric();
                }
                 * */

                // Objekttype anpassen
                ObjectType = new ObjectType(obj);

                // Fensternamen setzen
                SetTitle();

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
                ctx.SubmitChanges();
                ObjectID = obj.ID;
                // ReBind
                // Das muss sein, weil die Properties (noch) keine DependencyProperties sind
                obj.NotifyChange(); 

                IsObjectDirty = false;
                SetTitle();
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

        private void Window_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            try
            {
                IsObjectDirty = true;
                SetTitle();
            }
            catch
            {
            }
        }
    }
}
