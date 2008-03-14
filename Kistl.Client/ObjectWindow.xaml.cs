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
using System.Windows.Markup;
using System.Xml;
using Kistl.Client.Controls;
using System.IO;

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
            txt.Context = this.ctx;

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
            DataContext = obj;

            BindDefaultProperties();

            List<Kistl.App.Base.Method> methods = new List<Kistl.App.Base.Method>();

            // Objektklassenhierarchie holen
            foreach (Kistl.App.Base.ObjectClass objClass in Helper.GetObjectHierarchie(ObjectType))
            {
                // Get Actions, late we'll bind them to our Menu
                methods.AddRange(objClass.Methods);

                #region Binden
                // Aus Metadaten holen
                foreach (Kistl.App.Base.BaseProperty p in objClass.Properties)
                {
                    if (p is Kistl.App.Base.BackReferenceProperty)
                    {
                        Controls.ObjectList lst = new Kistl.Client.Controls.ObjectList();
                        lst.SourceObjectType = this.ObjectType;
                        lst.Label = p.PropertyName;
                        lst.ToolTip = p.AltText;
                        lst.Context = this.ctx;

                        // aus Metadaten auslesen
                        lst.DestinationObjectType = new ObjectType(p.GetDataType());

                        Binding b = new Binding("ID");
                        b.Mode = BindingMode.TwoWay;
                        b.Source = obj;
                        b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                        b.NotifyOnSourceUpdated = true;
                        lst.SetBinding(Controls.ObjectList.ObjectIDProperty, b);

                        //lst.ObjectID = this.ObjectID;
                        lst.PropertyName = p.PropertyName;

                        data.Children.Add(lst);
                    }
                    else if (p is Kistl.App.Base.ObjectReferenceProperty && !((Kistl.App.Base.ObjectReferenceProperty)p).IsList)
                    {
                        Controls.EditPointerProperty pointer = new Kistl.Client.Controls.EditPointerProperty();
                        pointer.Label = p.PropertyName;
                        pointer.ToolTip = p.AltText;
                        pointer.Context = this.ctx;

                        pointer.ObjectType = new ObjectType(p.GetDataType());

                        // Set Binding, damit werden Änderungen automatisch übernommen.
                        Binding b = new Binding("fk_" + p.PropertyName);
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
                                obj.GetType().GetProperty("fk_" + p.PropertyName).SetValue(obj, SourceObjectID, new object[] { });
                            }
                        }
                    }
                    else if (p is Kistl.App.Base.ObjectReferenceProperty && ((Kistl.App.Base.ObjectReferenceProperty)p).IsList)
                    {
                        Controls.EditPointerPropertyList pointerList = new Kistl.Client.Controls.EditPointerPropertyList();
                        pointerList.Label = p.PropertyName;
                        pointerList.ToolTip = p.AltText;
                        pointerList.Context = this.ctx;

                        pointerList.ObjectType = new ObjectType(p.GetDataType());

                        // Set Binding, damit werden Änderungen automatisch übernommen.
                        Binding b = new Binding(p.PropertyName);
                        b.Mode = BindingMode.OneWay; //BindingMode.TwoWay;
                        b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                        b.NotifyOnSourceUpdated = true;
                        b.NotifyOnTargetUpdated = true;
                        pointerList.SetBinding(Controls.EditPointerPropertyList.ValueProperty, b);

                        data.Children.Add(pointerList);
                    }
                    else if (p is Kistl.App.Base.ValueTypeProperty && ((Kistl.App.Base.ValueTypeProperty)p).IsList)
                    {
                        Controls.EditSimplePropertyList list = new EditSimplePropertyList();

                        // Bezeichnung setzen
                        // TODO: sollte auch gebunden werden
                        list.Label = p.PropertyName;
                        list.ToolTip = p.AltText;
                        list.Context = this.ctx;

                        // Set Binding, damit werden Änderungen automatisch übernommen.
                        Binding b = new Binding(p.PropertyName);
                        b.Mode = BindingMode.OneWay;
                        b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                        b.NotifyOnSourceUpdated = true;
                        b.NotifyOnTargetUpdated = true;
                        list.SetBinding(Controls.EditSimplePropertyList.ValueProperty, b);

                        data.Children.Add(list);
                    }
                    else
                    {
                        PropertyControl control = (PropertyControl)XamlReader.Load(XmlReader.Create(new StringReader(p.GetGUIRepresentation())));
                        // Bezeichnung setzen
                        // TODO: sollte auch gebunden werden
                        control.Label = p.PropertyName;
                        control.ToolTip = p.AltText;
                        control.Context = this.ctx;

                        // Set Binding, damit werden Änderungen automatisch übernommen.
                        Binding b = new Binding(p.PropertyName);
                        b.Mode = BindingMode.TwoWay;
                        b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                        b.NotifyOnSourceUpdated = true;
                        b.NotifyOnTargetUpdated = true;
                        control.SetBinding(Controls.EditSimpleProperty.ValueProperty, b);

                        data.Children.Add(control);
                    }
                }
                #endregion
            }

            // Bind Actions to Menu
            mnuActions.ItemsSource = methods;
        }

        private void SetTitle()
        {
            if (obj.ObjectState != DataObjectState.Unmodified && !this.Title.StartsWith("*"))
            {
                this.Title = "* " + ObjectType.ToString();
            }
            else if (obj.ObjectState == DataObjectState.Unmodified)
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
                ctx = new KistlContext();
                obj = ctx.GetQuery(ObjectType).SingleOrDefault(o => o.ID == ObjectID);
                obj.PropertyChanged += new PropertyChangedEventHandler(obj_PropertyChanged);

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
                int count = ctx.SubmitChanges();
                ObjectID = obj.ID;
                // ReBind
                // Das muss sein, weil sich ja Properties geändert haben könnten
                // außerdem wird beim Kopieren kein Change gefeuert
                obj.NotifyChange();

                SetTitle();

                MessageBox.Show(string.Format("{0} Item(s) submitted", count), "Save", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
            }
        }

        /// <summary>
        /// Delete this object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure that you want to delete this Object?",
                    "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    ctx.DeleteObject(obj);
                    ctx.SubmitChanges();

                    this.Close();
                }
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

        void obj_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                SetTitle();
            }
            catch
            {
            }
        }


        private void mnuActions_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((MenuItem)e.OriginalSource).Header is Kistl.App.Base.Method)
                {
                    Kistl.App.Base.Method m = (Kistl.App.Base.Method)((MenuItem)e.OriginalSource).Header;
                    System.Reflection.MethodInfo mi = obj.GetType().GetMethod(m.MethodName);
                    if (mi != null)
                    {
                        // TODO: Nur Parameterlose Methoden zulassen!
                        mi.Invoke(obj, new object[] { });
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
            }
        }
    }
}
