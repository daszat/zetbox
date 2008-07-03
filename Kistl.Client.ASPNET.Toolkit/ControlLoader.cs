using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Kistl.GUI;
using System.Web;
using System.Web.UI.WebControls;
using Kistl.API;

namespace Kistl.Client.ASPNET.Toolkit
{
    public interface IControlLoader
    {
    }

    public interface IContainerLoader : IControlLoader
    {
        void AddChild(IControlLoader child);
    }

    public abstract class BaseControlLoader<T> : TemplateControl, IBasicControl, IControlLoader where T : class, IBasicControl
    {
        protected T Ctrl { get; private set; }

        public BaseControlLoader()
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
    }

    public abstract class BaseContainerLoader<T> : BaseControlLoader<T>, IContainerLoader where T : class, IBasicControl
    {
        public virtual void AddChild(IControlLoader child)
        {
            ((IContainerLoader)Ctrl).AddChild(child);
        }
    }

    public class ObjectPanel : BaseContainerLoader<IObjectControl>, IObjectControl
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

    public abstract class BasicPropertyControl<T> : BaseControlLoader<IValueControl<T>>, IValueControl<T>
    {
        #region IValueControl<string> Members

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

        public event EventHandler UserInput
        {
            add { Ctrl.UserInput += value; }
            remove { Ctrl.UserInput -= value; }
        }

        #endregion
    }

    public class StringPropertyControl : BasicPropertyControl<string>
    {
        protected override string GetControlPath()
        {
            return "~/Controls/StringPropertyControl.ascx";
        }
    }

    public class IntPropertyControl : BasicPropertyControl<int?>
    {
        protected override string GetControlPath()
        {
            return "~/Controls/IntPropertyControl.ascx";
        }
    }
    public class BoolPropertyControl : BasicPropertyControl<bool?>
    {
        protected override string GetControlPath()
        {
            return "~/Controls/BoolPropertyControl.ascx";
        }
    }

    public class DoublePropertyControl : BasicPropertyControl<double?>
    {
        protected override string GetControlPath()
        {
            return "~/Controls/DoublePropertyControl.ascx";
        }
    }
    public class DateTimePropertyControl : BasicPropertyControl<DateTime?>
    {
        protected override string GetControlPath()
        {
            return "~/Controls/DateTimePropertyControl.ascx";
        }
    }
    public class ObjectReferencePropertyControl : BaseControlLoader<IReferenceControl>, IReferenceControl
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

        #region IValueControl<int> Members

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

        public event EventHandler UserInput
        {
            add { Ctrl.UserInput += value; }
            remove { Ctrl.UserInput -= value; }
        }

        #endregion
    }
    public class ObjectListControl : BaseControlLoader<IReferenceListControl>, IReferenceListControl
    {
        protected override string GetControlPath()
        {
            return "~/Controls/ObjectListControl.ascx";
        }

        #region IObjectListControl<IDataObject> Member

        public event EventHandler UserAddRequest;

        #endregion

        #region IReferenceListControl<IDataObject> Member

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

        #endregion

        #region IValueControl<IList<IDataObject>> Member

        public IList<IDataObject> Value
        {
            get { return Ctrl.Value; }
            set { Ctrl.Value = value; }
        }

        public bool IsValidValue
        {
            get { return Ctrl.IsValidValue; }
            set { Ctrl.IsValidValue = value; }
        }

        public event EventHandler UserInput
        {
            add { Ctrl.UserInput += value; }
            remove { Ctrl.UserInput -= value; }
        }

        #endregion
    }

}
