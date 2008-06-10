using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Kistl.GUI;
using System.Web;
using System.Web.UI.WebControls;

namespace Kistl.Client.ASPNET.Toolkit
{
    public interface IASPNETControl
    {
    }

    public interface IASPNETContainer
    {
        void AddChild(BaseASPNETControl child);
    }

    public abstract class BaseASPNETControl : TemplateControl, IBasicControl
    {
        protected IBasicControl Ctrl { get; private set; }

        public BaseASPNETControl()
        {
            Ctrl = (IBasicControl)this.LoadControl(GetControlPath());
            this.Controls.Add((Control)Ctrl);
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

    public abstract class BaseASPNETContainer : BaseASPNETControl, IASPNETContainer
    {
        public virtual void AddChild(BaseASPNETControl child)
        {
            ((IASPNETContainer)Ctrl).AddChild(child);
        }
    }

    public class ObjectPanel : BaseASPNETContainer, IObjectControl
    {
        protected override string GetControlPath()
        {
            return "~/Controls/ObjectPanel.ascx";
        }

        public Kistl.API.IDataObject Value
        {
            get { return ((IObjectControl)Ctrl).Value; }
            set { ((IObjectControl)Ctrl).Value = value; }
        }

        public event EventHandler UserInput
        {
            add { ((IObjectControl)Ctrl).UserInput += value; }
            remove { ((IObjectControl)Ctrl).UserInput -= value; }
        }

        public event EventHandler UserSaveRequest
        {
            add { ((IObjectControl)Ctrl).UserSaveRequest += value; }
            remove { ((IObjectControl)Ctrl).UserSaveRequest -= value; }
        }
    }

    public class StringPropertyControl : BaseASPNETControl, IValueControl<string>
    {
        protected override string GetControlPath()
        {
            return "~/Controls/StringPropertyControl.ascx";
        }

        #region IValueControl<string> Members

        public string Value
        {
            get { return ((IValueControl<string>)Ctrl).Value; }
            set { ((IValueControl<string>)Ctrl).Value = value; }
        }

        public bool IsValidValue
        {
            get;
            set;
        }

        public event EventHandler UserInput
        {
            add { ((IValueControl<string>)Ctrl).UserInput += value; }
            remove { ((IValueControl<string>)Ctrl).UserInput -= value; }
        }

        #endregion
    }
    public class IntPropertyControl : BaseASPNETControl, IValueControl<int?>
    {
        protected override string GetControlPath()
        {
            return "~/Controls/NotImplementedControl.ascx";
        }

        #region IValueControl<int> Members

        public int? Value
        {
            get;
            set;
        }

        public bool IsValidValue
        {
            get;
            set;
        }

        public event EventHandler UserInput;
        #endregion
    }
    public class BoolPropertyControl : BaseASPNETControl, IValueControl<bool?>
    {
        protected override string GetControlPath()
        {
            return "~/Controls/NotImplementedControl.ascx";
        }

        #region IValueControl<bool> Members

        public bool? Value
        {
            get;
            set;
        }

        public bool IsValidValue
        {
            get;
            set;
        }

        public event EventHandler UserInput;

        #endregion
    }
    public class DoublePropertyControl : BaseASPNETControl, IValueControl<double?>
    {
        protected override string GetControlPath()
        {
            return "~/Controls/NotImplementedControl.ascx";
        }

        #region IValueControl<double> Members

        public double? Value
        {
            get;
            set;
        }

        public bool IsValidValue
        {
            get;
            set;
        }

        public event EventHandler UserInput;

        #endregion
    }
    public class DateTimePropertyControl : BaseASPNETControl, IValueControl<DateTime?>
    {
        protected override string GetControlPath()
        {
            return "~/Controls/NotImplementedControl.ascx";
        }

        #region IValueControl<DateTime> Members

        public DateTime? Value
        {
            get;
            set;
        }

        public bool IsValidValue
        {
            get;
            set;
        }

        public event EventHandler UserInput;

        #endregion
    }
    public class ObjectReferencePropertyControl : BaseASPNETControl, IObjectReferenceControl
    {
        protected override string GetControlPath()
        {
            return "~/Controls/NotImplementedControl.ascx";
        }

        #region IReferenceControl<IDataObject> Members

        public Kistl.API.ObjectType ObjectType
        {
            get;
            set;
        }

        public IList<Kistl.API.IDataObject> ItemsSource
        {
            get;
            set;
        }

        #endregion

        #region IValueControl<IDataObject> Members

        public Kistl.API.IDataObject Value
        {
            get;
            set;
        }

        public bool IsValidValue
        {
            get;
            set;
        }

        public event EventHandler UserInput;

        #endregion
    }
    public class ObjectListControl : BaseASPNETControl, IObjectListControl
    {
        protected override string GetControlPath()
        {
            return "~/Controls/ObjectListControl.ascx";
        }

        #region IReferenceControl<IList<IDataObject>> Members

        public Kistl.API.ObjectType ObjectType
        {
            get { return ((IObjectListControl)Ctrl).ObjectType; }
            set { ((IObjectListControl)Ctrl).ObjectType = value; }
        }

        public IList<Kistl.API.IDataObject> ItemsSource
        {
            get { return ((IObjectListControl)Ctrl).ItemsSource; }
            set { ((IObjectListControl)Ctrl).ItemsSource = value; }
        }

        #endregion

        #region IValueControl<IList<IDataObject>> Members

        public IList<Kistl.API.IDataObject> Value
        {
            get { return ((IObjectListControl)Ctrl).Value; }
            set { ((IObjectListControl)Ctrl).Value = value; }
        }

        public bool IsValidValue
        {
            get { return ((IObjectListControl)Ctrl).IsValidValue; }
            set { ((IObjectListControl)Ctrl).IsValidValue = value; }
        }

        public event EventHandler UserInput
        {
            add { ((IObjectListControl)Ctrl).UserInput += value; }
            remove { ((IObjectListControl)Ctrl).UserInput -= value; }
        }

        #endregion
    }

}
