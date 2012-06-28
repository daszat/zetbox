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

namespace Zetbox.API.AbstractConsumerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Client.PerfCounter;
    using Zetbox.API.Configuration;
    using Zetbox.Client.Presentables;

    public class MockedViewModelFactory : ViewModelFactory
    {
        public MockedViewModelFactory(ILifetimeScope container, IFrozenContext frozenCtx, ZetboxConfig cfg, IPerfCounter perfCounter)
            : base(container, frozenCtx, cfg, perfCounter)
        {
        }

        public override void CreateTimer(TimeSpan tickLength, Action action)
        {
            throw new NotImplementedException();
        }

        private bool _decisionFromUser = false;
        public void SetDecisionFromUser(bool d)
        {
            _decisionFromUser = d;
        }

        public override bool GetDecisionFromUser(string message, string caption)
        {
            return _decisionFromUser;
        }

        public override string GetDestinationFileNameFromUser(string filename, params string[] filter)
        {
            throw new NotImplementedException();
        }

        public override string GetSourceFileNameFromUser(params string[] filter)
        {
            throw new NotImplementedException();
        }

        public ViewModel LastShownModel { get; private set; }
        protected override void ShowInView(ViewModel mdl, object view, bool activate, bool asDialog)
        {
            LastShownModel = mdl;
        }

        public string LastShownCaption { get; private set; }
        public string LastShownMessage { get; private set; }
        public override void ShowMessage(string message, string caption)
        {
            LastShownCaption = caption;
            LastShownMessage = message;
        }

        public void ResetMock()
        {
            LastShownMessage = LastShownCaption = string.Empty;
            LastShownModel = null;
        }

        public override Zetbox.App.GUI.Toolkit Toolkit
        {
            get { return Zetbox.App.GUI.Toolkit.TEST; }
        }

        public T GetLastShownModel<T>()
            where T : ViewModel
        {
            return (T)this.LastShownModel;
        }
    }
}
