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
    }
}
