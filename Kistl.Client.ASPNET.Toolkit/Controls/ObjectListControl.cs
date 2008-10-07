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
using System.Collections.Generic;
using Kistl.API;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;
using Kistl.Client.ASPNET.Toolkit;

[assembly: WebResource("Kistl.Client.ASPNET.Toolkit.Controls.ObjectListControl.js", "text/javascript")] 

namespace Kistl.Client.ASPNET.Toolkit.Controls
{
    public abstract class ObjectListControl : System.Web.UI.UserControl, IReferenceListControl, IScriptControl
    {
        protected abstract HiddenField HdItemsControl { get; }
        protected abstract AjaxDataControls.DataList LstItemsControl { get; }
        protected abstract Control ContainerControl { get; }
        protected abstract Control LnkAddControl { get; }
        protected abstract Control LnkNewControl { get; }

        public ObjectListControl()
        {
            this.Load += new EventHandler(ObjectListControl_Load);
        }

        void ObjectListControl_Load(object sender, EventArgs e)
        {
            // an empty array is posted as []
            // if the value is empty -> the Object is been displayed the first time
            if (!string.IsNullOrEmpty(HdItemsControl.Value))
            {
                var postedData = HdItemsControl.Value.FromJSONArray(((IBasicControl)this).Context).ToList();

                var added = postedData.Except(_Value).ToList();
                var deleted = _Value.Except(postedData).ToList();

                deleted.ForEach(d => _Value.Remove(d));
                added.ForEach(a => _Value.Add(a));
            }
        }

        public System.Collections.Generic.IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            ScriptControlDescriptor desc = new ScriptControlDescriptor("Kistl.Client.ASPNET.ObjectListControl",
                ContainerControl.ClientID);
            desc.AddComponentProperty("List", LstItemsControl.ClientID);
            desc.AddElementProperty("Items", HdItemsControl.ClientID);
            desc.AddElementProperty("LnkAdd", LnkAddControl.ClientID);
            desc.AddElementProperty("LnkNew", LnkNewControl.ClientID);
            desc.AddProperty("Type", new SerializableType(ObjectType));
            yield return desc;
        }

        public System.Collections.Generic.IEnumerable<ScriptReference> GetScriptReferences()
        {
            // typeof(thisclass) is important!
            // This is a UserControl. ASP.NET will derive from this class.
            // this.GetType() wont return a Type, where Assembly is set to this Assembly
            // -> use typeof(thisclass) instead
            yield return new ScriptReference(this.Page.ClientScript.GetWebResourceUrl(
                typeof(ObjectListControl), "Kistl.Client.ASPNET.Toolkit.Controls.ObjectListControl.js"));
        }


        IKistlContext IBasicControl.Context { get; set; }

        public Type ObjectType
        {
            get;
            set;
        }

        public System.Collections.Generic.IList<Kistl.API.IDataObject> ItemsSource
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

        public event EventHandler UserAddRequest;

        ObservableCollection<IDataObject> _Value = new ObservableCollection<IDataObject>();

        ObservableCollection<IDataObject> IValueControl<ObservableCollection<IDataObject>>.Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            HdItemsControl.Value = _Value.ToJSONArray();

            ScriptManager scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager == null)
            {
                throw new InvalidOperationException(
                  "ScriptManager required on the page.");
            }

            scriptManager.RegisterScriptControl(this);

            Page.ClientScript.RegisterOnSubmitStatement(this.GetType(), ContainerControl.ClientID,
                string.Format("$find('{0}').onSubmit();", ContainerControl.ClientID));
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);

            if (!DesignMode)
            {
                ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
            }
        }

        #region IValueControl<ObservableCollection<IDataObject>> Members


        public new string Error
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}