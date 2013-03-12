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

namespace Zetbox.Client.WPF.Toolkit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Windows.Threading;
    using Zetbox.API.Client;
    using Zetbox.Client.Presentables;

    /// <summary>
    /// Implements a delayed task using the WPF Dispatcher for the current Thread.
    /// </summary>
    public sealed class WpfDelayedTask : IDelayedTask
    {
        private readonly ViewModel _displayer;
        private readonly Action _task;

        public WpfDelayedTask(ViewModel displayer, Action task)
        {
            if (task == null) throw new ArgumentNullException("task");

            _displayer = displayer;
            _task = task;
        }

        public void Trigger()
        {
            if (_displayer != null)
                _displayer.SetBusy();

            Dispatcher.FromThread(Thread.CurrentThread).BeginInvoke(new Action(() =>
            {
                try
                {
                    _task();
                }
                finally
                {
                    if (_displayer != null)
                        _displayer.ClearBusy();
                }
            }), DispatcherPriority.Background); // prio must be Background to let fakeprogressoverlay be rendered
        }
    }
}
