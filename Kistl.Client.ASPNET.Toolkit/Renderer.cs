using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.GUI.Renderer;
using System.Web.UI;
using System.Web;
using Kistl.GUI;

namespace Kistl.Client.ASPNET.Toolkit
{
    public class Renderer : BasicRenderer<IControlLoader, IControlLoader, IContainerLoader>
    {
        public override Kistl.GUI.DB.Toolkit Platform
        {
            get { return Kistl.GUI.DB.Toolkit.ASPNET; }
        }

        public override void ShowMessage(string msg)
        {
            throw new NotImplementedException();
        }

        public override void ShowObject(Kistl.API.IDataObject obj)
        {
            HttpContext.Current.Response.Redirect(
                string.Format("~/ObjectPage.aspx?Type={0}&ID={1}", 
                    HttpUtility.UrlEncode(obj.GetType().FullName), obj.ID));
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

        public override IDataObject ChooseObject(IKistlContext ctx, Type klass)
        {
            throw new NotImplementedException("This runs contrary to ASP.NET's runtime model. Avoid triggering Events that lead to presenters needing this method.");
        }

        public override T ChooseObject<T>(IKistlContext ctx)
        {
            return (T)ChooseObject(ctx, typeof(T));
        }
    }
}
