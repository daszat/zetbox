// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Zetbox.App.GUI;
using Zetbox.Client.Forms.View;
using Zetbox.Client.Presentables;

namespace Zetbox.Client.Forms
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
