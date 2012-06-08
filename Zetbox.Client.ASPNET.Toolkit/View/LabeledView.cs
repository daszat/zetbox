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
using System.Web.UI.WebControls;
using System.Web.UI;

using Zetbox.Client.Presentables;
using Zetbox.Client.GUI;

namespace Zetbox.Client.ASPNET.Toolkit.View
{
    [ControlLocation("~/View/LabeledView.ascx")]
    public abstract class LabeledView : ModelUserControl<ILabeledViewModel>
    {
        protected abstract Label lbCtrl { get; }
        protected abstract Control containerCtrl { get; }

        public LabeledView()
        {
            this.Init += new EventHandler(LabeledView_Init);
        }

        void LabeledView_Init(object sender, EventArgs e)
        {
            InititalizeView();
        }

        public override void SetModel(ViewModel mdl)
        {
            base.SetModel(mdl);
            InititalizeView();
        }

        private bool _initialized = false;
        private void InititalizeView()
        {
            if (!_initialized && Model != null)
            {
                ZetboxContextManagerModule.ViewModelFactory.CreateSpecificView(Model.Model, Model.RequestedKind, containerCtrl);
                _initialized = true;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            lbCtrl.Text = Model.Label;
            lbCtrl.ToolTip = Model.ToolTip;
            base.OnPreRender(e);
        }
    }
}