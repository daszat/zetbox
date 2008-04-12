using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Xml;

using Kistl.API.Client;
using Kistl.GUI.DB;
using Kistl.Client;

namespace Kistl.GUI.Renderer.WPF
{
    /// <summary>
    /// Interaction logic for WPFWindow.xaml
    /// </summary>
    public partial class WPFWindow : Window, IObjectControl
    {
        public WPFWindow()
        {
            _ObjChanged = new PropertyChangedEventHandler(obj_PropertyChanged);
            InitializeComponent();
        }

        /// <summary>
        /// Typ des Objektes, welches dieses Objekt geöffnet hat.
        /// </summary>
        public Kistl.API.ObjectType SourceObjectType { get; set; }
        /// <summary>
        /// ObjectID jenes Objektes, welches dieses Objekt geöffnet hat.
        /// </summary>
        public int SourceObjectID { get; set; }

        /// <summary>
        /// Datenobjekt, das angezeigt wird.
        /// </summary>
        private BaseClientDataObject _obj = null;

        private void SetTitle()
        {
            if (_obj.ObjectState != Kistl.API.DataObjectState.Unmodified && !this.Title.StartsWith("*"))
            {
                this.Title = "* " + _obj.Type.ToString();
            }
            else if (_obj.ObjectState == Kistl.API.DataObjectState.Unmodified)
            {
                this.Title = _obj.Type.ToString();
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
                int count = _obj.Context.SubmitChanges();
                // ReBind
                // Das muss sein, weil sich ja Properties geändert haben könnten
                // außerdem wird beim Kopieren kein Change gefeuert
                _obj.NotifyChange();

                SetTitle();

                MessageBox.Show(string.Format("{0} Item(s) submitted", count), "Save", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Kistl.Client.Helper.HandleError(ex);
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
                    _obj.Context.DeleteObject(_obj);
                    _obj.Context.SubmitChanges();

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
                    System.Reflection.MethodInfo mi = _obj.GetType().GetMethod(m.MethodName);
                    if (mi != null)
                    {
                        // TODO: Nur Parameterlose Methoden zulassen!
                        mi.Invoke(_obj, new object[] { });
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
            }
        }

        internal void Show(KistlContext kistlContext, BaseClientDataObject obj)
        {
            ((IObjectControl)this).Value = obj;

            // TODO: should come from DataType
            var template = obj.FindTemplate(TemplateUsage.EditControl);

            // the Meat
            this.Content = Renderer.WPF.CreateControl(obj, template.VisualTree);

            this.Show();
        }

        #region IObjectControl Members

        private PropertyChangedEventHandler _ObjChanged;
        BaseClientDataObject IObjectControl.Value
        {
            get { return _obj; }
            set
            {
                if (_obj != null)
                    _obj.PropertyChanged -= _ObjChanged;
                _obj = value;
                _obj.PropertyChanged += _ObjChanged;
                SetTitle();
            }
        }

        private EventHandler _UserInput;
        event EventHandler IObjectControl.UserInput
        {
            add { _UserInput += value; }
            remove { _UserInput -= value; }
        }

        #endregion

        #region IBasicControl Members

        // TODO: useful implementation missing, use DependencyProperties and bind to XAML
        string IBasicControl.ShortLabel { get; set; }
        string IBasicControl.Description { get; set; }
        FieldSize IBasicControl.Size { get; set; }

        #endregion
    }
}
