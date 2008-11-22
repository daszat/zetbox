using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Kistl.GUI;
using System.Web;
using System.Web.UI.WebControls;
using Kistl.API;
using System.Collections.ObjectModel;
using Kistl.Client.GUI;

namespace Kistl.Client.ASPNET.Toolkit.View
{
    public abstract class ViewLoader : IViewLoader, IView
    {
        protected abstract string GetControlPath();

        public Control LoadControl(Page parent)
        {
            var ctrl = (IView)parent.LoadControl(GetControlPath());
            ctrl.SetModel(_mdl);
            return (Control)ctrl;
        }

        Presentables.PresentableModel _mdl;
        public void SetModel(Kistl.Client.Presentables.PresentableModel mdl)
        {
            _mdl = mdl;
        }
    }

    public class WorkspaceViewLoader : ViewLoader
    {
        protected override string GetControlPath()
        {
            return "~/View/WorkspaceView.ascx";
        }
    }

    public class DataObjectFullViewLoader : ViewLoader
    {
        protected override string GetControlPath()
        {
            return "~/View/DataObjectFullView.ascx";
        }
    }

    public class DataObjectReferenceViewLoader : ViewLoader
    {
        protected override string GetControlPath()
        {
            return "~/View/DataObjectReferenceView.ascx";
        }
    }
    public class DataObjectListViewLoader : ViewLoader
    {
        protected override string GetControlPath()
        {
            return "~/View/DataObjectListView.ascx";
        }
    }
    public class NullablePropertyTextBoxViewLoader : ViewLoader
    {
        protected override string GetControlPath()
        {
            return "~/View/NullablePropertyTextBoxView.ascx";
        }
    }

}

namespace Kistl.Client.ASPNET.Toolkit
{
    public interface IViewLoader
    {
        Control LoadControl(Page parent);
    }

    public interface IControlLoader
    {
    }

    public interface IContainerLoader : IControlLoader
    {
        void AddChild(IControlLoader child);
    }

    public abstract class BasicControlLoader<T> : TemplateControl, IBasicControl, IControlLoader where T : class, IBasicControl
    {
        protected T Ctrl { get; private set; }

        public BasicControlLoader()
        {
            Ctrl = this.LoadControl(GetControlPath()) as T;
            if (Ctrl == null) throw new InvalidOperationException(string.Format("UserControl \"{0}\" could not be loaded or converted to {1}", GetControlPath(), typeof(T).FullName));
            this.Controls.Add(Ctrl as Control);
        }

        protected abstract string GetControlPath();

        public string ShortLabel
        {
            get { return Ctrl.ShortLabel; }
            set { Ctrl.ShortLabel = value; }
        }

        public string Description
        {
            get { return Ctrl.Description; }
            set { Ctrl.Description = value; }
        }

        public FieldSize Size
        {
            get { return Ctrl.Size; }
            set { Ctrl.Size = value; }
        }

        IKistlContext IBasicControl.Context
        {
            get { return ((IBasicControl)Ctrl).Context; }
            set { ((IBasicControl)Ctrl).Context = value; }
        }
    }

    public abstract class BaseContainerLoader<T> : BasicControlLoader<T>, IContainerLoader where T : class, IBasicControl
    {
        public virtual void AddChild(IControlLoader child)
        {
            ((IContainerLoader)Ctrl).AddChild(child);
        }

    }

    public class ObjectPanelLoader : BaseContainerLoader<IObjectControl>, IObjectControl
    {
        protected override string GetControlPath()
        {
            return "~/Controls/ObjectPanel.ascx";
        }

        public Kistl.API.IDataObject Value
        {
            get { return Ctrl.Value; }
            set { Ctrl.Value = value; }
        }

        public event EventHandler UserSaveRequest
        {
            add { Ctrl.UserSaveRequest += value; }
            remove { Ctrl.UserSaveRequest -= value; }
        }

        public event EventHandler UserDeleteRequest
        {
            add { Ctrl.UserDeleteRequest += value; }
            remove { Ctrl.UserDeleteRequest -= value; }
        }
    }

    public abstract class BasicPropertyControlLoader<T> : BasicControlLoader<IValueControl<T>>, IValueControl<T>
    {
        #region IValueControl<T> Members

        public T Value
        {
            get { return Ctrl.Value; }
            set { Ctrl.Value = value; }
        }

        public bool IsValidValue
        {
            get { return Ctrl.IsValidValue; }
            set { Ctrl.IsValidValue = value; }
        }

        string IValueControl<T>.Error
        {
            get { return Ctrl.Error; }
            set { Ctrl.Error = value; }
        }

        public event EventHandler UserInput
        {
            add { Ctrl.UserInput += value; }
            remove { Ctrl.UserInput -= value; }
        }

        #endregion
    }

    public class StringPropertyControlLoader : BasicPropertyControlLoader<string>
    {
        protected override string GetControlPath()
        {
            return "~/Controls/StringPropertyControl.ascx";
        }
    }

    public class IntPropertyControlLoader : BasicPropertyControlLoader<int?>
    {
        protected override string GetControlPath()
        {
            return "~/Controls/IntPropertyControl.ascx";
        }
    }
    public class BoolPropertyControlLoader : BasicPropertyControlLoader<bool?>
    {
        protected override string GetControlPath()
        {
            return "~/Controls/BoolPropertyControl.ascx";
        }
    }

    public class DoublePropertyControlLoader : BasicPropertyControlLoader<double?>
    {
        protected override string GetControlPath()
        {
            return "~/Controls/DoublePropertyControl.ascx";
        }
    }
    public class DateTimePropertyControlLoader : BasicPropertyControlLoader<DateTime?>
    {
        protected override string GetControlPath()
        {
            return "~/Controls/DateTimePropertyControl.ascx";
        }
    }
    public class ObjectReferencePropertyControlLoader : BasicControlLoader<IReferenceControl>, IReferenceControl
    {
        protected override string GetControlPath()
        {
            return "~/Controls/ObjectReferencePropertyControl.ascx";
        }

        #region IReferenceControl<IDataObject> Members

        public Type ObjectType
        {
            get { return Ctrl.ObjectType; }
            set { Ctrl.ObjectType = value; }
        }

        public IList<Kistl.API.IDataObject> ItemsSource
        {
            get { return Ctrl.ItemsSource; }
            set { Ctrl.ItemsSource = value; }
        }

        #endregion

        #region IValueControl<IDataObject> Members

        public Kistl.API.IDataObject Value
        {
            get { return Ctrl.Value; }
            set { Ctrl.Value = value; }
        }

        public bool IsValidValue
        {
            get { return Ctrl.IsValidValue; }
            set { Ctrl.IsValidValue = value; }
        }

        string IValueControl<IDataObject>.Error
        {
            get { return Ctrl.Error; }
            set { Ctrl.Error = value; }
        }

        public event EventHandler UserInput
        {
            add { Ctrl.UserInput += value; }
            remove { Ctrl.UserInput -= value; }
        }

        #endregion
    }
    public class ObjectListControlLoader : BasicControlLoader<IReferenceListControl>, IReferenceListControl
    {
        protected override string GetControlPath()
        {
            return "~/Controls/ObjectListControl.ascx";
        }

        #region IReferenceListControl Member

        public Type ObjectType
        {
            get { return Ctrl.ObjectType; }
            set { Ctrl.ObjectType = value; }
        }

        public IList<IDataObject> ItemsSource
        {
            get { return Ctrl.ItemsSource; }
            set { Ctrl.ItemsSource = value; }
        }

        public event EventHandler UserAddRequest;
        public event System.Collections.Specialized.NotifyCollectionChangedEventHandler UserChangedListEvent;


        #endregion

        #region IValueControl<ObservableCollection<IDataObject>> Member

        public ObservableCollection<IDataObject> Value
        {
            get { return Ctrl.Value; }
            set { Ctrl.Value = value; }
        }

        public bool IsValidValue
        {
            get { return Ctrl.IsValidValue; }
            set { Ctrl.IsValidValue = value; }
        }

        string IValueControl<ObservableCollection<IDataObject>>.Error
        {
            get { return Ctrl.Error; }
            set { Ctrl.Error = value; }
        }

        public event EventHandler UserInput
        {
            add { Ctrl.UserInput += value; }
            remove { Ctrl.UserInput -= value; }
        }

        #endregion

    }

}
