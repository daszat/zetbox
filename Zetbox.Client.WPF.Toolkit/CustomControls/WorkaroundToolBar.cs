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
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Input;

namespace Zetbox.Client.WPF.CustomControls
{
    /// <summary>
    /// http://social.msdn.microsoft.com/Forums/en-US/wpf/thread/c5b6a94b-2cdc-4cfb-8ccb-0f7e680586d7
    /// If the ToolBars ItemsSource is DataBound, all buttons will appear in the overflow list until a manual window resize.
    /// This is an Workaround Control which invalidates it's Measure after Templates are applied.
    /// </summary>
    public class WorkaroundToolBar : ToolBar
    {
        public WorkaroundToolBar()
            : base()
        {
            this.Height = 33; // Fixed height
        }

        private delegate void MethodeInvoke();

        public override void OnApplyTemplate()
        {
            Dispatcher.BeginInvoke(new MethodeInvoke(InvalidateMeasure), DispatcherPriority.Background, null);
            base.OnApplyTemplate();
        }
    }
}
