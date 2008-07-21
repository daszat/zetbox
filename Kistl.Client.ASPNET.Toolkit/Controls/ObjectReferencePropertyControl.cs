using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Kistl.GUI;
using Kistl.API;

namespace Kistl.Client.ASPNET.Toolkit.Controls
{
    public abstract class ObjectReferencePropertyControl : System.Web.UI.UserControl, IReferenceControl
    {
        protected abstract DropDownList cbListControl { get; }

        public ObjectReferencePropertyControl()
        {
            this.Init += new EventHandler(ObjectReferencePropertyControl_Init);
        }

        void ObjectReferencePropertyControl_Init(object sender, EventArgs e)
        {
            cbListControl.SelectedIndexChanged += new EventHandler(cbList_OnSelectedIndexChanged);
        }

        protected void cbList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.UserInput != null)
            {
                this.UserInput(this, EventArgs.Empty);
            }
        }

        #region IBasicControl Members

        IKistlContext IBasicControl.Context { get; set; }

        public string ShortLabel
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public FieldSize Size
        {
            get;
            set;
        }

        #endregion

        #region IReferenceControl<IDataObject> Members

        public Type ObjectType
        {
            get;
            set;
        }

        public System.Collections.Generic.IList<Kistl.API.IDataObject> ItemsSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                cbListControl.DataValueField = "ID";
                cbListControl.DataTextField = "Text";
                cbListControl.DataSource = value.Select(i => new { ID = i.ID, Text = i.ToString() });
                cbListControl.DataBind();
            }
        }

        #endregion

        #region IValueControl<IDataObject> Members

        public Kistl.API.IDataObject Value
        {
            get
            {
                return ((IBasicControl)this).Context.Find(ObjectType, Convert.ToInt32(cbListControl.SelectedValue));
            }
            set
            {
                if (value != null)
                {
                    cbListControl.SelectedValue = value.ID.ToString();
                }
            }
        }

        public bool IsValidValue
        {
            get;
            set;
        }

        public event EventHandler UserInput;

        #endregion
    }
}