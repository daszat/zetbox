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

namespace Zetbox.Client.WPF.Styles
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Threading;
    using Zetbox.Client.Presentables;
    using Zetbox.App.GUI;
using System.Windows.Data;

    public static class Controls
    {
        #region HeaderBackground

        public static Brush GetHeaderBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(HeaderBackgroundProperty);
        }

        public static void SetHeaderBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(HeaderBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.RegisterAttached("HeaderBackground", typeof(Brush), typeof(Controls));

        #endregion

        #region HeaderForeground

        public static Brush GetHeaderForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(HeaderForegroundProperty);
        }

        public static void SetHeaderForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(HeaderForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderForegroundProperty =
            DependencyProperty.RegisterAttached("HeaderForeground", typeof(Brush), typeof(Controls));

        #endregion

        #region HoverBackground

        public static Brush GetHoverBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(HoverBackgroundProperty);
        }

        public static void SetHoverBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(HoverBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for HoverBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.RegisterAttached("HoverBackground", typeof(Brush), typeof(Controls));

        #endregion

        #region HoverForeground

        public static Brush GetHoverForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(HoverForegroundProperty);
        }

        public static void SetHoverForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(HoverForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for HoverForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoverForegroundProperty =
            DependencyProperty.RegisterAttached("HoverForeground", typeof(Brush), typeof(Controls));

        #endregion

        #region PressedBackground

        public static Brush GetPressedBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(PressedBackgroundProperty);
        }

        public static void SetPressedBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(PressedBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for PressedBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PressedBackgroundProperty =
            DependencyProperty.RegisterAttached("PressedBackground", typeof(Brush), typeof(Controls));

        #endregion

        #region PressedForeground

        public static Brush GetPressedForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(PressedForegroundProperty);
        }

        public static void SetPressedForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(PressedForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for PressedForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PressedForegroundProperty =
            DependencyProperty.RegisterAttached("PressedForeground", typeof(Brush), typeof(Controls));

        #endregion

        #region Header

        public static String GetHeader(DependencyObject obj)
        {
            return (String)obj.GetValue(HeaderProperty);
        }

        public static void SetHeader(DependencyObject obj, String value)
        {
            obj.SetValue(HeaderProperty, value);
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.RegisterAttached("Header", typeof(String), typeof(Controls));

        #endregion

        #region BorderStyle

        public static Style GetBorderStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(BorderStyleProperty);
        }

        public static void SetBorderStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(BorderStyleProperty, value);
        }

        // Using a DependencyProperty as the backing store for BorderStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderStyleProperty =
            DependencyProperty.RegisterAttached("BorderStyle", typeof(Style), typeof(Controls));

        #endregion

        #region IsSecondary

        public static bool GetIsSecondary(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsSecondaryProperty);
        }

        public static void SetIsSecondary(DependencyObject obj, bool value)
        {
            obj.SetValue(IsSecondaryProperty, value);
        }

        /// <summary>
        /// Designates a control as secondary. It should be rendered less intense.
        /// </summary>
        public static readonly DependencyProperty IsSecondaryProperty =
            DependencyProperty.RegisterAttached("IsSecondary", typeof(bool), typeof(Controls), new UIPropertyMetadata(false));
        #endregion

        #region Highlight
        public static Highlight? GetHighlight(DependencyObject obj)
        {
            return (Highlight?)obj.GetValue(HighlightProperty);
        }

        public static void SetHighlight(DependencyObject obj, Highlight? value)
        {
            obj.SetValue(HighlightProperty, value);
        }

        // Using a DependencyProperty as the backing store for Highlight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HighlightProperty =
            DependencyProperty.RegisterAttached("Highlight", typeof(Highlight?), typeof(Controls), new PropertyMetadata(highlight_Changed));

        public static void highlight_Changed(object sender, DependencyPropertyChangedEventArgs e)
        {
            ApplyHighlight(sender, e);
        }

        public static bool GetStopHighlight(DependencyObject obj)
        {
            return (bool)obj.GetValue(StopHighlightProperty);
        }

        public static void SetStopHighlight(DependencyObject obj, bool value)
        {
            obj.SetValue(StopHighlightProperty, value);
        }

        // Using a DependencyProperty as the backing store for StopHighlight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StopHighlightProperty =
            DependencyProperty.RegisterAttached("StopHighlight", typeof(bool), typeof(Controls), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, stopHighlight_Changed));

        public static void stopHighlight_Changed(object sender, DependencyPropertyChangedEventArgs e)
        {
            ApplyHighlight(sender, e);
        }

        private static void ApplyHighlight(object sender, DependencyPropertyChangedEventArgs e)
        {
            var depObj = sender as DependencyObject;
            if (depObj == null) return;

            var stop = GetStopHighlight(depObj);
            var highlight = GetHighlight(depObj);

            if (highlight != null)
            {
                if (stop)
                {
                    depObj.ClearValue(Control.ForegroundProperty);
                    depObj.ClearValue(Control.BackgroundProperty);
                    depObj.ClearValue(Control.FontStyleProperty);
                    depObj.ClearValue(Control.FontWeightProperty);

                    depObj.ClearValue(TextBlock.ForegroundProperty);
                    depObj.ClearValue(TextBlock.BackgroundProperty);
                    depObj.ClearValue(TextBlock.FontStyleProperty);
                    depObj.ClearValue(TextBlock.FontWeightProperty);
                }
                else
                {
                    depObj.SetValue(Control.ForegroundProperty, GetHighlightValue(highlight.Value.State, highlight.Value.GridForeground, "GridForeground"));
                    depObj.SetValue(Control.BackgroundProperty, GetHighlightValue(highlight.Value.State, highlight.Value.GridBackground, "GridBackground"));
                    depObj.SetValue(Control.FontStyleProperty, GetHighlightValue(highlight.Value.State, ConvertFontStyle(highlight.Value.GridFontStyle), "GridFontStyle"));
                    depObj.SetValue(Control.FontWeightProperty, GetHighlightValue(highlight.Value.State, ConvertFontWeight(highlight.Value.GridFontStyle), "GridFontWeight"));

                    depObj.SetValue(TextBlock.ForegroundProperty, GetHighlightValue(highlight.Value.State, highlight.Value.GridForeground, "GridForeground"));
                    depObj.SetValue(TextBlock.BackgroundProperty, GetHighlightValue(highlight.Value.State, highlight.Value.GridBackground, "GridBackground"));
                    depObj.SetValue(TextBlock.FontStyleProperty, GetHighlightValue(highlight.Value.State, ConvertFontStyle(highlight.Value.GridFontStyle), "GridFontStyle"));
                    depObj.SetValue(TextBlock.FontWeightProperty, GetHighlightValue(highlight.Value.State, ConvertFontWeight(highlight.Value.GridFontStyle), "GridFontWeight"));
                }
            }
        }

        private static string ConvertFontWeight(System.Drawing.FontStyle fontStyle)
        {
            switch (fontStyle)
            {
                case System.Drawing.FontStyle.Bold:
                    return "Bold";
                default:
                    return string.Empty;
            }
        }

        private static string ConvertFontStyle(System.Drawing.FontStyle fontStyle)
        {
            switch (fontStyle)
            {
                case System.Drawing.FontStyle.Italic:
                    return "Italic";
                default:
                    return string.Empty;
            }
        }

        public static object GetHighlightValue(HighlightState state, string explicitValue, string valueKind)
        {
            if (!string.IsNullOrEmpty(explicitValue))
            {
                return explicitValue;
            }
            else
            {
                var resource = System.Windows.Application.Current.TryFindResource(string.Format("HighlightState_{0}_{1}", state.ToString(), valueKind));
                return resource ?? DependencyProperty.UnsetValue;
            }
        }
        #endregion
    }
}
