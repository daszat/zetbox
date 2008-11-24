using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

using Kistl.API;
using Kistl.GUI;
using Kistl.GUI.Renderer;
using Kistl.Client.ASPNET.Toolkit.Pages;

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
            if (HttpContext.Current.CurrentHandler is IWorkspaceView)
            {
                IWorkspaceView page = (IWorkspaceView)HttpContext.Current.CurrentHandler;
                page.ShowObject(obj);
            }
            else
            {
                throw new InvalidOperationException("ShowObject is only allowed on a IWorkspacePage");
            }
        }

        // TODO?
        protected override void ShowObject(IDataObject obj, IContainerLoader ctrl, IList<IControlLoader> menus)
        {
            throw new NotImplementedException();
        }

        protected override IControlLoader Setup(IControlLoader control, IList<IControlLoader> menus)
        {
            if (menus != null && menus.Count>0)
                throw new NotImplementedException("Cannot handle menus");
            return control;
        }

        protected override IContainerLoader Setup(IContainerLoader widget, IList<IControlLoader> list, IList<IControlLoader> menus)
        {
            if (menus != null && menus.Count > 0)
                throw new NotImplementedException("Cannot handle menus");

            list.ForEach<IControlLoader>(c => widget.AddChild(c));
            return widget;
        }

        public override IDataObject ChooseObject(IKistlContext ctx, Type klass, string prompt)
        {
            throw new NotImplementedException("This runs contrary to ASP.NET's runtime model. Avoid triggering Events that lead to presenters needing this method.");
        }

    }
}
