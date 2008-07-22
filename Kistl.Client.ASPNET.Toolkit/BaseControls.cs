using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.GUI;
using Kistl.API;

namespace Kistl.Client.ASPNET.Toolkit
{
    public abstract class BasicControl : System.Web.UI.UserControl, IBasicControl
    {
        public virtual string ShortLabel
        {
            get;
            set;
        }

        public virtual string Description
        {
            get;
            set;
        }

        public virtual FieldSize Size
        {
            get;
            set;
        }

        IKistlContext IBasicControl.Context
        {
            get;
            set;
        }
    }

    public abstract class ObjectPanel : BasicControl, IObjectControl
    {
        public virtual Kistl.API.IDataObject Value
        {
            get;
            set;
        }

        public event EventHandler UserSaveRequest;

        public event EventHandler UserDeleteRequest;
    }

    public abstract class BasicPropertyControl<T> : BasicControl, IValueControl<T>
    {
        public virtual T Value
        {
            get;
            set;
        }

        public virtual bool IsValidValue
        {
            get;
            set;
        }

        public event EventHandler UserInput;

        public void NotifyUserInput()
        {
            if (this.UserInput != null)
            {
                this.UserInput(this, EventArgs.Empty);
            }
        }
    }
}
