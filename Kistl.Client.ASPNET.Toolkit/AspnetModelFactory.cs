using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Client.Presentables;
using Kistl.Client.GUI;

namespace Kistl.Client.ASPNET.Toolkit
{
    public class AspnetModelFactory : ModelFactory
    {

        public AspnetModelFactory(GuiApplicationContext appCtx)
            : base(appCtx)
        {

        }

        protected override Kistl.App.GUI.Toolkit Toolkit
        {
            get { return Kistl.App.GUI.Toolkit.ASPNET; }
        }

        protected override object Renderer
        {
            get { return null; }
        }

        protected override void ShowInView(object renderer, PresentableModel mdl, IView view, bool activate)
        {
            throw new NotImplementedException();
        }
    }
}
