using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.GUI.Renderer;
using System.Web.UI;
using System.Web;

namespace Kistl.Client.ASPNET.Toolkit
{
    public class Renderer : BasicRenderer<BaseASPNETControl, BaseASPNETControl, BaseASPNETContainer>
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

        protected override BaseASPNETControl Setup(BaseASPNETControl control)
        {
            return control;
        }

        protected override BaseASPNETContainer Setup(BaseASPNETContainer widget, IList<BaseASPNETControl> list)
        {
            list.ForEach<BaseASPNETControl>(c => widget.AddChild(c));
            return widget;
        }
    }
}
