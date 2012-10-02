// With my thanks from http://stackoverflow.com/a/6782715/178517
// Wiesław Šoltés

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;

namespace Zetbox.Client.WPF.CustomControls
{
    public class ZoomBorder : Border
    {
        private UIElement _child = null;
        private Point origin;
        private Point start;

        private TranslateTransform GetTranslateTransform(UIElement element)
        {
            return (TranslateTransform)((TransformGroup)element.RenderTransform).Children.First(tr => tr is TranslateTransform);
        }

        private ScaleTransform GetScaleTransform(UIElement element)
        {
            return (ScaleTransform)((TransformGroup)element.RenderTransform).Children.First(tr => tr is ScaleTransform);
        }

        public override UIElement Child
        {
            get
            {
                return this._child;
            }
            set
            {
                if (value != null && value != this.Child)
                {
                    this.Initialize(value);
                }
            }
        }

        public void Initialize(UIElement element)
        {
            this._child = element;
            if (_child != null)
            {
                TransformGroup group = new TransformGroup();

                ScaleTransform st = new ScaleTransform();
                group.Children.Add(st);

                TranslateTransform tt = new TranslateTransform();

                group.Children.Add(tt);

                _child.RenderTransform = group;
                _child.RenderTransformOrigin = new Point(0.0, 0.0);

                _child.MouseWheel += child_MouseWheel;
                _child.MouseLeftButtonDown += child_MouseLeftButtonDown;
                _child.MouseLeftButtonUp += child_MouseLeftButtonUp;
                _child.MouseMove += child_MouseMove;
                _child.PreviewMouseRightButtonDown += new MouseButtonEventHandler(child_PreviewMouseRightButtonDown);

                this.ClipToBounds = true;
                base.Child = _child;
            }
        }

        public void Reset()
        {
            if (_child != null)
            {
                // reset zoom
                var st = GetScaleTransform(_child);
                st.ScaleX = 1.0;
                st.ScaleY = 1.0;

                // reset pan
                var tt = GetTranslateTransform(_child);
                tt.X = 0.0;
                tt.Y = 0.0;
            }
        }

        #region Child Events

        private void child_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (_child != null)
            {
                var st = GetScaleTransform(_child);
                var tt = GetTranslateTransform(_child);

                double zoom = e.Delta > 0 ? .2 : -.2;
                if (!(e.Delta > 0) && (st.ScaleX < .4 || st.ScaleY < .4))
                    return;

                Point relative = e.GetPosition(_child);
                double abosuluteX;
                double abosuluteY;

                abosuluteX = relative.X * st.ScaleX + tt.X;
                abosuluteY = relative.Y * st.ScaleY + tt.Y;

                st.ScaleX += zoom;
                st.ScaleY += zoom;

                tt.X = abosuluteX - relative.X * st.ScaleX;
                tt.Y = abosuluteY - relative.Y * st.ScaleY;
            }
        }

        private void child_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_child != null)
            {
                var tt = GetTranslateTransform(_child);
                start = e.GetPosition(this);
                origin = new Point(tt.X, tt.Y);
                this.Cursor = Cursors.Hand;
                _child.CaptureMouse();
            }
        }

        private void child_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_child != null)
            {
                _child.ReleaseMouseCapture();
                this.Cursor = Cursors.Arrow;
            }
        }

        void child_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Reset();
        }

        private void child_MouseMove(object sender, MouseEventArgs e)
        {
            if (_child != null)
            {
                if (_child.IsMouseCaptured)
                {
                    var tt = GetTranslateTransform(_child);
                    Vector v = start - e.GetPosition(this);
                    tt.X = origin.X - v.X;
                    tt.Y = origin.Y - v.Y;
                }
            }
        }

        #endregion
    }
}
