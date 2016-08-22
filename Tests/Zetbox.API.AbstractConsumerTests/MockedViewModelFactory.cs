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
    using Zetbox.Client.GUI;

    public class MockedViewModelFactory : ViewModelFactory
    {
        public MockedViewModelFactory(ILifetimeScopeFactory scopeFactory, Autofac.ILifetimeScope scope, IFrozenContext frozenCtx, ZetboxConfig cfg, IPerfCounter perfCounter, DialogCreator.Factory dialogFactory)
            : base(scopeFactory, scope, frozenCtx, cfg, perfCounter, dialogFactory)
        {
        }

        public override void CreateTimer(TimeSpan tickLength, Action action)
        {
            throw new NotImplementedException();
        }

        private static bool _decisionFromUser = false;
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

        private static ViewModel _lastShownModel;
        public ViewModel LastShownModel
        {
            get
            {
                return _lastShownModel;
            }
        }

        protected override void ShowInView(ViewModel mdl, object view, bool activate, bool asDialog, ViewModel ownerMdl)
        {
            _lastShownModel = mdl;
        }

        private static string _lastShownCaption;
        private static string _lastShownMessage;

        public string LastShownCaption { get { return _lastShownCaption; } }
        public string LastShownMessage { get { return _lastShownMessage; } }

        public override void ShowMessage(string message, string caption)
        {
            _lastShownCaption = caption;
            _lastShownMessage = message;
        }

        public void ResetMock()
        {
            _lastShownMessage = _lastShownCaption = string.Empty;
            _lastShownModel = null;
        }

        public override Zetbox.App.GUI.Toolkit Toolkit
        {
            get { return Zetbox.App.GUI.Toolkit.TEST; }
        }

        public T GetLastShownModel<T>()
            where T : ViewModel
        {
            return (T)LastShownModel;
        }
    }
}
