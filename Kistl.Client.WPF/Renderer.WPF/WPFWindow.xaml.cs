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
using Kistl.API;
using Kistl.Client.WPF;

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
        private Kistl.API.IDataObject _obj = null;

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
                if (UserSaveRequest != null)
                {
                    UserSaveRequest(this, EventArgs.Empty);
                }

                SetTitle();
            }
            catch (Exception ex)
            {
                ClientHelper.HandleError(ex);
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
                    if (UserDeleteRequest != null)
                    {
                        UserDeleteRequest(this, EventArgs.Empty);
                    }

                    // TODO: Frage: Wer mach das Fenster wieder zu?
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ClientHelper.HandleError(ex);
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
                ClientHelper.HandleError(ex);
            }
        }

        internal void Show(IKistlContext kistlContext, Kistl.API.IDataObject obj)
        {
            ((IObjectControl)this).Value = obj;

            // TODO: should come from DataType
            var template = obj.FindTemplate(TemplateUsage.EditControl);

            // the Meat
            this.Content = Manager.Renderer.CreateControl(obj, template.VisualTree);

            this.Show();
        }

        #region IObjectControl Members

        private PropertyChangedEventHandler _ObjChanged;
        Kistl.API.IDataObject IObjectControl.Value
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

        public event EventHandler UserInput;
        public event EventHandler UserSaveRequest;
        public event EventHandler UserDeleteRequest;


        #endregion

        #region IBasicControl Members

        // TODO: useful implementation missing, use DependencyProperties and bind to XAML
        string IBasicControl.ShortLabel { get; set; }
        string IBasicControl.Description { get; set; }
        FieldSize IBasicControl.Size { get; set; }

        #endregion
    }
}
