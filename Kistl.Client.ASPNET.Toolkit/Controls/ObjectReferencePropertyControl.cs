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
    public abstract class ObjectReferencePropertyControl : ValueControl<IDataObject>, IReferenceControl
    {
        protected abstract DropDownList cbListControl { get; }
        protected abstract HtmlControl btnNewControl { get; }
        protected abstract HtmlControl btnOpenControl { get; }

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
            NotifyUserInput();
        }

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

                cbListControl.Items.Insert(0, new ListItem("", Helper.INVALIDID.ToString()));
            }
        }

        #endregion

        public override Kistl.API.IDataObject Value
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

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            btnNewControl.Attributes.Add("onclick", string.Format("javascript: Kistl.JavascriptRenderer.showObject(Kistl.JavascriptRenderer.newObject({0}));", 
                ObjectType.ToJSON()));
            btnOpenControl.Attributes.Add("onclick", string.Format("javascript: Kistl.JavascriptRenderer.showObject({0});",
                Value.ToJSON()));
        }
    }
}