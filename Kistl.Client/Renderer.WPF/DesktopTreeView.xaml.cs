using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Kistl.API.Client;

namespace Kistl.GUI.Renderer.WPF
{
    /// <summary>
    /// Interaction logic for DesktopTreeView.xaml
    /// </summary>
    public partial class DesktopTreeView : UserControl
    {
        static DesktopTreeView()
        {
            SelectionChangedEvent = EventManager.RegisterRoutedEvent("SelectionChanged", RoutingStrategy.Bubble,
                            typeof(RoutedPropertyChangedEventHandler<BaseClientDataObject>), typeof(DesktopTreeView));
        }

        public static RoutedEvent SelectionChangedEvent;
        public event RoutedPropertyChangedEventHandler<BaseClientDataObject> SelectionChanged
        {
            add { AddHandler(SelectionChangedEvent, value); }
            remove { RemoveHandler(SelectionChangedEvent, value); }
        }

        public DesktopTreeView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Bind();
        }

        private void Bind()
        {
            // Desingmode? -> raus
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;

            try
            {
                List<ModuleNode> modules = new List<ModuleNode>();
                Kistl.Client.Helper.Modules.Values.ToList().ForEach(m => modules.Add(new ModuleNode(m)));

                this.DataContext = modules;
            }
            catch (Exception ex)
            {
                Kistl.Client.Helper.HandleError(ex);
            }
        }

        #region Nodes
        public interface INode
        {
            Kistl.API.Client.BaseClientDataObject DataObject { get; }
            void RefreshChildren();
        }

        public class ModuleNode : INode
        {
            public Kistl.App.Base.Module Module { get; set; }

            public ModuleNode(Kistl.App.Base.Module m)
            {
                Module = m;
            }

            private ObservableCollection<ObjectClassNode> _ObjectClassedNodes = null;
            public ObservableCollection<ObjectClassNode> ObjectClassedNodes
            {
                get
                {
                    if (_ObjectClassedNodes == null)
                    {
                        _ObjectClassedNodes = new ObservableCollection<ObjectClassNode>();
                        Module.DataTypes.ForEach(o => _ObjectClassedNodes.Add(new ObjectClassNode(o)));
                    }

                    return _ObjectClassedNodes;
                }
            }

            #region INode Members

            public Kistl.API.Client.BaseClientDataObject DataObject
            {
                get { return Module; }
            }

            public void RefreshChildren()
            {
            }

            #endregion
        }

        public class ObjectClassNode : INode
        {
            public Kistl.App.Base.DataType ObjectClass { get; set; }

            public ObjectClassNode(Kistl.App.Base.DataType o)
            {
                ObjectClass = o;
            }

            private ObservableCollection<InstanceNode> _InstancesNodes = null;
            public ObservableCollection<InstanceNode> InstancesNodes
            {
                get
                {
                    if (_InstancesNodes == null)
                    {
                        _InstancesNodes = new ObservableCollection<InstanceNode>();
                        RefreshChildren();
                    }

                    return _InstancesNodes;
                }
            }

            #region INode Members

            public Kistl.API.Client.BaseClientDataObject DataObject
            {
                get { return ObjectClass; }
            }

            public void RefreshChildren()
            {
                try
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    _InstancesNodes.Clear();
                    if (ObjectClass is Kistl.App.Base.ObjectClass)
                    {
                        ObjectClass.Context.GetQuery(new Kistl.API.ObjectType(ObjectClass.Module.Namespace, ObjectClass.ClassName)).ToList()
                            .ForEach(i => _InstancesNodes.Add(new InstanceNode(i)));
                    }
                }
                finally
                {
                    Mouse.OverrideCursor = Cursors.Arrow;
                }
            }
            #endregion
        }

        public class InstanceNode : INode
        {
            public Kistl.API.Client.BaseClientDataObject Object { get; set; }

            public InstanceNode(Kistl.API.Client.BaseClientDataObject obj)
            {
                Object = obj;
            }

            #region INode Members

            public Kistl.API.Client.BaseClientDataObject DataObject
            {
                get { return Object; }
            }

            public void RefreshChildren()
            {
            }
            #endregion
        }
        #endregion

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (treeView.SelectedItem == null) return;

                ObjectType resultObjectType = null;
                INode n = null;
                if (treeView.SelectedItem is ObjectClassNode)
                {
                    n = (ObjectClassNode)treeView.SelectedItem;
                    resultObjectType = new ObjectType(((ObjectClassNode)n).ObjectClass.Module.Namespace, ((ObjectClassNode)n).ObjectClass.ClassName);
                    Kistl.App.Base.ObjectClass objClass = Kistl.Client.Helper.ObjectClasses[resultObjectType];

                    if (objClass.SubClasses.Count > 0)
                    {
                        // TODO: Das ist noch nicht ganz konsistent
                        Kistl.Client.Dialogs.ChooseObjectClass dlg = new Kistl.Client.Dialogs.ChooseObjectClass();
                        dlg.BaseObjectClass = objClass;

                        if (dlg.ShowDialog() == true)
                        {
                            resultObjectType = new ObjectType(dlg.ResultObjectClass.Module.Namespace, dlg.ResultObjectClass.ClassName);
                        }
                        else
                        {
                            // Do nothing
                            return;
                        }
                    }
                }
                else if (treeView.SelectedItem is ModuleNode)
                {
                    n = (ModuleNode)treeView.SelectedItem;
                    resultObjectType = new ObjectType("Kistl.App.Base", "ObjectClass");
                }

                if (resultObjectType != null && n != null)
                {
                    Renderer.WPF.ShowObject((BaseClientDataObject)resultObjectType.NewDataObject());
                    n.RefreshChildren();
                }
            }
            catch (Exception ex)
            {
                Kistl.Client.Helper.HandleError(ex);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Bind();
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is INode)
            {
                RoutedPropertyChangedEventArgs<Kistl.API.Client.BaseClientDataObject> args =
                    new RoutedPropertyChangedEventArgs<Kistl.API.Client.BaseClientDataObject>(
                       null, ((INode)e.NewValue).DataObject, SelectionChangedEvent);
                RaiseEvent(args);
            }
        }

        private void treeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (treeView.SelectedItem == null) return;

                INode n = treeView.SelectedItem as INode;
                Renderer.WPF.ShowObject(n.DataObject);
            }
            catch (Exception ex)
            {
                Kistl.Client.Helper.HandleError(ex);
            }
        }

        private void ctxmnu_Refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (treeView.SelectedItem == null) return;
                INode n = treeView.SelectedItem as INode;
                n.RefreshChildren();
            }
            catch (Exception ex)
            {
                Kistl.Client.Helper.HandleError(ex);
            }
        }
    }
}
