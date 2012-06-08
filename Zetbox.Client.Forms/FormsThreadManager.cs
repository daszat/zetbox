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
using Zetbox.Client.Presentables;
using System.Windows.Forms;
using System.Diagnostics;

namespace Zetbox.Client.Forms
{
    internal class FormsThreadManager : IThreadManager
    {
        private Button _dispatcher = new Button();

        #region IThreadManager Members

        public void Verify()
        {
            if (_dispatcher.InvokeRequired)
                throw new InvalidOperationException("Off-Main Thread invocation!");
        }

        public void Queue(object lck, Action uiTask)
        {
            Debug.Assert(lck == this, "always pass the UI thread manager to UI.Queue() calls");
            _dispatcher.BeginInvoke(uiTask);
        }

        public void Queue(object lck, Action<object> uiTask, object data)
        {
            Debug.Assert(lck == this, "always pass the UI thread manager to UI.Queue() calls");
            _dispatcher.BeginInvoke(new Action(delegate() { uiTask(data); }));
        }

        #endregion
    }
}
