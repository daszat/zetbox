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

namespace Zetbox.Client.WPF.Converter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Data;
    using Zetbox.Client.Presentables;
    using System.Windows;

    [ValueConversion(typeof(Highlight), typeof(object))]
    public abstract class HighlightConverter : IValueConverter
    {
        protected abstract string GetValue(Highlight h);
        protected abstract string GetValueType();

        public object Convert(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            var h = value as Highlight;
            if (h == null) return parameter ?? Binding.DoNothing;
            var val = GetValue(h);
            if (!string.IsNullOrEmpty(val))
            {
                return val;
            }
            else
            {
                var resource = Application.Current.TryFindResource(string.Format("HighlightState_{0}_{1}", h.State.ToString(), GetValueType()));
                return resource ?? parameter ?? Binding.DoNothing;
            }
        }

        public object ConvertBack(object value, Type targetType,
                            object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    [ValueConversion(typeof(Highlight), typeof(object))]
    public class HighlightGridBackgroundConverter : HighlightConverter
    {
        protected override string GetValue(Highlight h)
        {
            return h.GridBackground;
        }

        protected override string GetValueType()
        {
            return "GridBackground";
        }
    }

    [ValueConversion(typeof(Highlight), typeof(object))]
    public class HighlightGridForegroundConverter : HighlightConverter
    {
        protected override string GetValue(Highlight h)
        {
            return h.GridForeground;
        }

        protected override string GetValueType()
        {
            return "GridForeground";
        }
    }

    [ValueConversion(typeof(Highlight), typeof(object))]
    public class HighlightGridFontStyleConverter : HighlightConverter
    {
        protected override string GetValue(Highlight h)
        {
            switch(h.GridFontStyle)
            {
                case System.Drawing.FontStyle.Italic:
                    return "Italic";
                default:
                    return string.Empty;
            }
        }

        protected override string GetValueType()
        {
            return "GridFontStyle";
        }
    }

    [ValueConversion(typeof(Highlight), typeof(object))]
    public class HighlightGridFontWeightConverter : HighlightConverter
    {
        protected override string GetValue(Highlight h)
        {
            switch (h.GridFontStyle)
            {
                case System.Drawing.FontStyle.Bold:
                    return "Bold";
                default:
                    return string.Empty;
            }
        }

        protected override string GetValueType()
        {
            return "GridFontWeight";
        }
    }


    [ValueConversion(typeof(Highlight), typeof(object))]
    public class HighlightPanelBackgroundConverter : HighlightConverter
    {
        protected override string GetValue(Highlight h)
        {
            return h.PanelBackground;
        }

        protected override string GetValueType()
        {
            return "PanelBackground";
        }
    }

}
