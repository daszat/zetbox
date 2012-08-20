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
    using System.Linq;
    using System.Text;
    using System.Windows;

    public static class Margin
    {
        #region Left
        public static Thickness GetLeft(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(LeftProperty);
        }

        public static void SetLeft(DependencyObject obj, Thickness value)
        {
            obj.SetValue(LeftProperty, value);
        }

        // Using a DependencyProperty as the backing store for Left.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.RegisterAttached("Left", typeof(Thickness), typeof(Margin), new PropertyMetadata(LeftChanged));

        public static void LeftChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;

            var fwelem = source as FrameworkElement;
            if (fwelem != null)
            {
                var margins = (Thickness)(fwelem.GetValue(FrameworkElement.MarginProperty) ?? new Thickness());
                fwelem.SetValue(FrameworkElement.MarginProperty, new Thickness() { Left = ((Thickness)e.NewValue).Left, Top = margins.Top, Right = margins.Right, Bottom = margins.Bottom });
            }
        }
        #endregion

        #region Top
        public static Thickness GetTop(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(TopProperty);
        }

        public static void SetTop(DependencyObject obj, Thickness value)
        {
            obj.SetValue(TopProperty, value);
        }

        // Using a DependencyProperty as the backing store for Top.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopProperty =
            DependencyProperty.RegisterAttached("Top", typeof(Thickness), typeof(Margin), new PropertyMetadata(TopChanged));

        public static void TopChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;

            var fwelem = source as FrameworkElement;
            if (fwelem != null)
            {
                var margins = (Thickness)(fwelem.GetValue(FrameworkElement.MarginProperty) ?? new Thickness());
                fwelem.SetValue(FrameworkElement.MarginProperty, new Thickness() { Left = margins.Left, Top = ((Thickness)e.NewValue).Top, Right = margins.Right, Bottom = margins.Bottom });
            }
        }
        #endregion

        #region Right
        public static Thickness GetRight(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(RightProperty);
        }

        public static void SetRight(DependencyObject obj, Thickness value)
        {
            obj.SetValue(RightProperty, value);
        }

        // Using a DependencyProperty as the backing store for Right.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightProperty =
            DependencyProperty.RegisterAttached("Right", typeof(Thickness), typeof(Margin), new PropertyMetadata(RightChanged));

        public static void RightChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;

            var fwelem = source as FrameworkElement;
            if (fwelem != null)
            {
                var margins = (Thickness)(fwelem.GetValue(FrameworkElement.MarginProperty) ?? new Thickness());
                fwelem.SetValue(FrameworkElement.MarginProperty, new Thickness() { Left = margins.Left, Top = margins.Top, Right = ((Thickness)e.NewValue).Right, Bottom = margins.Bottom });
            }
        }
        #endregion

        #region Bottom
        public static Thickness GetBottom(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(BottomProperty);
        }

        public static void SetBottom(DependencyObject obj, Thickness value)
        {
            obj.SetValue(BottomProperty, value);
        }

        // Using a DependencyProperty as the backing store for Bottom.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomProperty =
            DependencyProperty.RegisterAttached("Bottom", typeof(Thickness), typeof(Margin), new PropertyMetadata(BottomChanged));

        public static void BottomChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;

            var fwelem = source as FrameworkElement;
            if (fwelem != null)
            {
                var margins = (Thickness)(fwelem.GetValue(FrameworkElement.MarginProperty) ?? new Thickness());
                fwelem.SetValue(FrameworkElement.MarginProperty, new Thickness() { Left = margins.Left, Top = margins.Top, Right = margins.Right, Bottom = ((Thickness)e.NewValue).Bottom });
            }
        }
        #endregion
    }
}
