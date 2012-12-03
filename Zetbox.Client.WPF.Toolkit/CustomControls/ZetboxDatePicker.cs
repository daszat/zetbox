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
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using Zetbox.Client.WPF.Toolkit;

    public class ZetboxDatePicker : DatePicker
    {
        public ZetboxDatePicker()
        {
            this.Loaded += new System.Windows.RoutedEventHandler(ZetboxDatePicker_Loaded);
        }

        void ZetboxDatePicker_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var tb = WPFHelper.FindVisualChild<DatePickerTextBox>(this);
            if (tb == null) return;

            var wm = tb.Template.FindName("PART_Watermark", tb) as ContentControl;
            if (wm == null) return;

            wm.Content = CustomControlsResources.DatePicker_WatermarkText;
        }
    }
}
