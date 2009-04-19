using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Kistl.API;
using Kistl.Client.GUI;

namespace Kistl.Client.ASPNET.Toolkit.View
{
    public interface IViewLoader
    {
        Control LoadControl(Page parent);
    }

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
