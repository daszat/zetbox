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
using System.Windows.Shapes;
using Kistl.API;
using System.Collections.ObjectModel;
using Kistl.GUI.DB;
using Kistl.Client;
using Kistl.App.Base;

namespace Kistl.GUI.Renderer.WPF
{
    /// <summary>
    /// Interaktionslogik für WorkspaceWindow.xaml
    /// </summary>
    public partial class WorkspaceWindow : Window, IWorkspaceControl
    {

        public WorkspaceWindow()
        {
            Objects = new ObservableCollection<ObjectTabItem>();
            DataContext = this;
            InitializeComponent();
        }

        #region Behaviours

        /// <summary>
        /// Is called when the window wants to save all work.
        /// </summary>
        protected virtual void OnSave(RoutedEventArgs e)
        {
            CheckContext("Saving");
            if (UserSaveRequest != null)
                UserSaveRequest(this, e);
        }

        /// <summary>
        /// Is called when the window wants to throwaway all work.
        /// </summary>
        protected virtual void OnAbort(RoutedEventArgs e)
        {
            if (UserAbortRequest != null)
                UserAbortRequest(this, e);
        }

        protected virtual void OnNew(RoutedEventArgs e)
        {
            CheckContext("OnNew");
            if (UserNewObjectRequest != null)
                UserNewObjectRequest(this, e);
        }

        protected virtual void OnDelete(RoutedEventArgs e)
        {
            CheckContext("OnDelete");
            if (UserDeleteObjectRequest != null)
            {
                UserDeleteObjectRequest(this, new GenericEventArgs<Kistl.API.IDataObject>() { Data = ((IObjectControl)tabObjects.SelectedItem).Value });
            }
            else
            {
                throw new InvalidOperationException("Delete requested, but nobody is listening.");
            }
        }

        /// <summary>
        /// select the tab of the given IDataObject
        /// </summary>
        /// <param name="obj"></param>
        public void ShowObject(Kistl.API.IDataObject obj)
        {
            var tabItem = Objects.Single(tab => ((IObjectControl)tab).Value.Equals(obj));
            ShowObject(obj, tabItem);
        }

        public bool IsDisplayingObjects(params Kistl.API.IDataObject[] objs)
        {
            return objs.All(obj => 
                Objects.SingleOrDefault(tab => ((IObjectControl)tab).Value.Equals(obj))
                != null);
        }

        public void ShowObject(Kistl.API.IDataObject obj, IBasicControl ctrl)
        {
            if (ctrl == null)
                throw new ArgumentNullException("ctrl", "must not be null");

            ObjectTabItem realCtrl = ctrl as ObjectTabItem;

            if (realCtrl == null)
                throw new ArgumentOutOfRangeException("ctrl", "must be a ObjectTabItem");

            ShowObject(obj, realCtrl);
        }

        public void ShowObject(Kistl.API.IDataObject obj, ObjectTabItem ctrl)
        {
            CheckContext("ShowObject");

            if (obj.Context != Context)
                throw new ArgumentOutOfRangeException("obj", "Object is not in this Workspace's Context");

            int idx = Objects.IndexOf(ctrl);
            if (idx != -1)
            {
                tabObjects.SelectedIndex = idx;
                return;
            }

            Objects.Add(ctrl);

            tabObjects.SelectedIndex = Objects.IndexOf(ctrl);
        }

        public void RemoveObject(Kistl.API.IDataObject dataObject)
        {
           // TODO: Objects.Remove(dataObject);
        }

        public ObservableCollection<ObjectTabItem> Objects
        {
            get { return (ObservableCollection<ObjectTabItem>)GetValue(ObjectsProperty); }
            set { SetValue(ObjectsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Objects.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectsProperty =
            DependencyProperty.Register("Objects", typeof(ObservableCollection<ObjectTabItem>), typeof(WorkspaceWindow), new UIPropertyMetadata());

        #endregion

        #region Event Handlers

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OnSave(e);
            }
            catch (Exception ex)
            {
                ClientHelper.HandleError(ex);
            }
        }

        private void SaveAndClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OnSave(e);
                this.Close();
            }
            catch (Exception ex)
            {
                ClientHelper.HandleError(ex);
            }
        }

        private void AbortAndClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OnAbort(e);
                this.Close();
            }
            catch (Exception ex)
            {
                ClientHelper.HandleError(ex);
            }
        }

        private void Self_Closed(object sender, EventArgs e)
        {
            try
            {
                OnAbort(null);
            }
            catch (Exception ex)
            {
                ClientHelper.HandleError(ex);
            }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OnNew(e);
            }
            catch (Exception ex)
            {
                ClientHelper.HandleError(ex);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OnDelete(e);
            }
            catch (Exception ex)
            {
                ClientHelper.HandleError(ex);
            }
        }

        #endregion

        #region Other Context Stuff

        /// <summary>
        /// Set the context of this WorkspaceWindow. Can only be used once.
        /// </summary>
        /// <param name="ctx">The Context that shall be handled by this WsW. Must not be null. Must not be used by another WsW.</param>
        protected void SetContext(IKistlContext ctx)
        {
            if (ctx == null)
                throw new ArgumentNullException("ctx", "SetContext needs a valid parameter");

            // idempotency: do nothing if User tries to set the same context again.
            if (Context == ctx)
                return;

            if (Context != null)
                throw new InvalidOperationException("Cannot change Context of WorkspaceWindow");

            // artificial barrier to reduce complexity for now
            if (Renderer.FindWorkspace(ctx) != null)
                throw new ArgumentOutOfRangeException("ctx", "Cannot set Context that is already handled by another WorkspaceWindow");

            _Context = ctx;
            CheckContext("SetContext");
        }

        /// <summary>
        /// Throws an exception if the context is invalid
        /// </summary>
        /// <param name="action">a string representing the action needing a Context. Has to fit into the sentence "Cannot perform {0} without a Context"</param>
        private void CheckContext(string action)
        {
            if (action == null || action == "")
                action = "this";

            if (Context == null)
                throw new InvalidOperationException(String.Format("Cannot perform {0} without Context", action));

            // TODO:
            // if (Context.IsDisposed)
            //    throw new InvalidOperationException(String.Format("Cannot perform {0} with disposed Context", action));
        }

        #endregion

        #region IWorkspaceControl Member

        public event EventHandler UserSaveRequest;
        public event EventHandler UserAbortRequest;
        public event EventHandler UserNewObjectRequest;
        public event EventHandler<GenericEventArgs<Kistl.API.IDataObject>> UserDeleteObjectRequest;

        #endregion

        #region IBasicControl Member

        public string ShortLabel { get; set; }
        public string Description { get; set; }
        public FieldSize Size { get; set; }

        private IKistlContext _Context;
        public IKistlContext Context { get { return _Context; } set { SetContext(value); } }

        #endregion



    }
}
