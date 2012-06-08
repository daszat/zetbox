using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Kistl.App.GUI;
using Kistl.Client.Forms.View;
using Kistl.Client.Presentables;

namespace Kistl.Client.Forms
{
    internal class Renderer
    {

        public void ShowModel(ViewModel mdl, Control parent)
        {
            // TODO: revive with new infrastructure
            //Layout lout = DataMocks.LookupDefaultLayout(mdl.GetType());
            //var vDesc = DataMocks.LookupViewDescriptor(Toolkit.WinForms, lout);
            //var formsView = (IFormsView)vDesc.ViewRef.Create();
            //formsView.SetRenderer(this);
            //formsView.SetDataContext(mdl);

            //var control = (Control)formsView;
            //// control.Dock = DockStyle.Fill;
            //parent.Controls.Add(control);
        }

    }
}
