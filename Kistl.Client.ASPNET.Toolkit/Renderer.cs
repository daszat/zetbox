using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

using Kistl.API;
using Kistl.GUI;
using Kistl.GUI.Renderer;

namespace Kistl.Client.ASPNET.Toolkit
{
    public class Renderer : BasicRenderer<IControlLoader, IControlLoader, IContainerLoader>
    {
        public override Kistl.App.GUI.Toolkit Platform
        {
            get { return Kistl.App.GUI.Toolkit.ASPNET; }
        }

        public override void ShowMessage(string msg)
        {
            throw new NotImplementedException();
        }

        public override void ShowObject(Kistl.API.IDataObject obj)
        {
            if (HttpContext.Current.CurrentHandler is IWorkspaceControl)
            {
                IWorkspaceControl page = (IWorkspaceControl)HttpContext.Current.CurrentHandler;
                page.ShowObject(obj, null);
            }
            else
            {
                HttpContext.Current.Response.Redirect(
                    string.Format("~/ObjectPage.aspx?Type={0}&ID={1}",
                        HttpUtility.UrlEncode(obj.GetType().FullName), obj.ID));
            }
        }

        // TODO?
        protected override void ShowObject(IDataObject obj, IContainerLoader ctrl)
        {
            throw new NotImplementedException();
        }

        protected override IControlLoader Setup(IControlLoader control)
        {
            return control;
        }

        protected override IContainerLoader Setup(IContainerLoader widget, IList<IControlLoader> list)
        {
            list.ForEach<IControlLoader>(c => widget.AddChild(c));
            return widget;
        }

        public override IDataObject ChooseObject(IKistlContext ctx, Type klass, string prompt)
        {
            throw new NotImplementedException("This runs contrary to ASP.NET's runtime model. Avoid triggering Events that lead to presenters needing this method.");
        }

    }
}
