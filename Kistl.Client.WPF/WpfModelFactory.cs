using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using Kistl.Client.Presentables;
using Kistl.Client.WPF.View;
using System.Windows;

namespace Kistl.Client.WPF
{
    public class Renderer
    {
        private static Renderer _current;
        public static Renderer Current
        {
            get
            {
                if (_current == null)
                    _current = new Renderer();

                return _current;
            }
        }

    }

    public class WpfModelFactory : ModelFactory
    {
        public WpfModelFactory(IGuiApplicationContext appCtx)
            : base(appCtx)
        {
        }

        protected override Kistl.App.GUI.Toolkit Toolkit
        {
            get { return Kistl.App.GUI.Toolkit.WPF; }
        }

        protected override void ShowInView(object renderer, PresentableModel mdl, object view, bool activate)
        {
            if (view is Window)
            {
                var viewControl = (Window)view;
                viewControl.DataContext = mdl;
                viewControl.ShowActivated = activate;
                viewControl.Show();
            }
            else
            {
                // TODO: what should be done here, really?
                throw new NotImplementedException(String.Format("Cannot show view of type {0}", view.GetType()));
            }
        }

        protected override object Renderer { get { return null; } }
    }

}
