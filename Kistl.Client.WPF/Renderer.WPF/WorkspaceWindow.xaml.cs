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

namespace Kistl.GUI.Renderer.WPF
{
    /// <summary>
    /// Interaktionslogik für WorkspaceWindow.xaml
    /// </summary>
    public partial class WorkspaceWindow : Window
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
            Context.SubmitChanges();
        }

        /// <summary>
        /// Is called when the window wants to throwaway all work.
        /// </summary>
        protected virtual void OnAbort(RoutedEventArgs e)
        {
            Context.Dispose();
        }

        public void ShowObject(Kistl.API.IDataObject obj)
        {
            CheckContext("ShowObject");

            if (obj.Context != Context)
                throw new ArgumentOutOfRangeException("obj", "Object is not in this window's Context");
            var template = obj.FindTemplate(TemplateUsage.EditControl);
            ObjectTabItem oti = (ObjectTabItem)Manager.Renderer.CreateControl(obj, template.VisualTree);
            tabObjects.Items.Add(oti); 
            Objects.Add(obj);
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

        #region Context

        public IKistlContext Context { get; private set; }

        /// <summary>
        /// Set the context of this WorkspaceWindow. Can only be used once.
        /// </summary>
        /// <param name="ctx">The Context that shall be handled by this WsW. Must not be null. Must not be used by another WsW.</param>
        public void SetContext(IKistlContext ctx)
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

            Context = ctx;
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
        public static WorkspaceWindow FindWindow(IKistlContext ctx)
        {
            if (_workspaces.ContainsKey(ctx))
                return _workspaces[ctx];
            else
                return null;
        }

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
        #endregion
    }

    /*
     */

}
