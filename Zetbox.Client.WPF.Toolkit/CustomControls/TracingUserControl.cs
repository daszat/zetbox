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

namespace Zetbox.Client.WPF.CustomControls
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Windows.Controls;

    public class TracingUserControl : UserControl
    {
        private Stopwatch _watch;
        public TracingUserControl()
        {
            _watch = new Stopwatch();
            _watch.Start();
            this.Initialized += (s, e) =>
            {
                Debug.WriteLine(string.Format("{0}: Initialised after {1:N2}ms", this.GetType().Name, _watch.ElapsedMilliseconds));
            };
            this.Loaded += (s, e) =>
            {
                Debug.WriteLine(string.Format("{0}: Loaded after {1:N2}ms", this.GetType().Name, _watch.ElapsedMilliseconds));
                _watch.Stop();
            };
            Debug.WriteLine(string.Format("{0}: Constructed", this.GetType().Name));
        }
    }
}
