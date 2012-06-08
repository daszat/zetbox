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

namespace Zetbox.Client.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables;
using Zetbox.API.Client.PerfCounter;

    class TestViewModelFactory
        : ViewModelFactory
    {
        public TestViewModelFactory(Autofac.ILifetimeScope container,
            IFrozenContext frozenCtx,
            ZetboxConfig cfg, IPerfCounter perfCounter)
            : base(container, frozenCtx, cfg, perfCounter)
        {
        }

        public override Toolkit Toolkit
        {
            get { return Toolkit.TEST; }
        }

        protected override void ShowInView(ViewModel mdl, object view, bool activate, bool asDialog)
        {
        }

        public override void CreateTimer(TimeSpan tickLength, Action action)
        {
        }

        public override string GetSourceFileNameFromUser(params string[] filter)
        {
            return null;
        }

        public override string GetDestinationFileNameFromUser(string filename, params string[] filter)
        {
            return null;
        }

        public override bool GetDecisionFromUser(string message, string caption)
        {
            return false;
        }

        public override void ShowMessage(string message, string caption)
        {
        }
    }
}
