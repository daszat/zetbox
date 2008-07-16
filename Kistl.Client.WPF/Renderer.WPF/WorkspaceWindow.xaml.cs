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
            Objects = new ObservableCollection<Kistl.API.IDataObject>();
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

            int idx = Objects.IndexOf(obj);
            if (idx != -1)
            {
                tabObjects.SelectedIndex = idx;
                return;
            }

            tabObjects.Items.Add(ctrl);
            Objects.Add(obj);

            idx = Objects.IndexOf(obj);
            tabObjects.SelectedIndex = idx;
        }

        public ObservableCollection<Kistl.API.IDataObject> Objects
        {
            get { return (ObservableCollection<Kistl.API.IDataObject>)GetValue(ObjectsProperty); }
            set { SetValue(ObjectsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Objects.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectsProperty =
            DependencyProperty.Register("Objects", typeof(ObservableCollection<Kistl.API.IDataObject>), typeof(WorkspaceWindow), new UIPropertyMetadata());

        #endregion

        #region Event Handlers

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            OnSave(e);
        }

        private void SaveAndClose_Click(object sender, RoutedEventArgs e)
        {
            OnSave(e);
            this.Close();
        }

        private void AbortAndClose_Click(object sender, RoutedEventArgs e)
        {
            OnAbort(e);
            this.Close();
        }

        private void Self_Closed(object sender, EventArgs e)
        {
            OnAbort(null);
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            OnNew(e);
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

            if (_workspaces.ContainsKey(ctx))
                throw new ArgumentOutOfRangeException("ctx", "Cannot set Context from another WorkspaceWindow");

            _Context = ctx;
            _workspaces[ctx] = this;
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

        /// <summary>
        /// _workspaces[ctx].Context == ctx
        /// </summary>
        private static Dictionary<IKistlContext, WorkspaceWindow> _workspaces = new Dictionary<IKistlContext, WorkspaceWindow>();

        /// <summary>
        /// Returns the WorkspaceWindow that is associated with the given Context or null if there is no association.
        /// </summary>
        public static WorkspaceWindow FindOrCreateWindow(IKistlContext ctx)
        {
            if (_workspaces.ContainsKey(ctx))
            {
                return _workspaces[ctx];
            }
            else
            {
                WorkspaceWindow wc = new WorkspaceWindow();
                wc.Context = ctx;

                WorkspacePresenter wp = new WorkspacePresenter();
                wp.InitializeComponent(null, null, wc);
                return wc;
            }
        }

        #endregion

        #region IWorkspaceControl Member

        public event EventHandler UserSaveRequest;
        public event EventHandler UserAbortRequest;
        public event EventHandler UserNewObjectRequest;

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
