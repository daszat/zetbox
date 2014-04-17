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
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public interface IDragDropTarget
    {
        bool CanDrop { get; }
        string[] AcceptableDataFormats { get; }
        bool OnDrop(string format, object data);
    }

    public interface IDragDropSource
    {
        object GetData();
    }

    public class WpfDragDropHelper
    {
        public const string ZetboxObjectDataFormat = "Zetbox.API.IDataObject[]";
        public const string ZetboxDragSourceDataFormat = "ZetboxDragSource";

        #region NoDragSource
        public static bool GetNoDragSource(DependencyObject obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            return (bool)obj.GetValue(NoDragSourceProperty);
        }

        public static void SetNoDragSource(DependencyObject obj, bool value)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            obj.SetValue(NoDragSourceProperty, value);
        }

        // Using a DependencyProperty as the backing store for NoDrag.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoDragSourceProperty =
            DependencyProperty.RegisterAttached("NoDragSource", typeof(bool), typeof(WpfDragDropHelper), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
        #endregion

        public static readonly string[] ZetboxObjectDataFormats = new[] 
        { 
            ZetboxObjectDataFormat,
        };
        public static readonly string[] AllAcceptableDataFormats = new[] 
        {
            WpfDragDropHelper.ZetboxObjectDataFormat, 
            "FileDrop", 
            DataFormats.Bitmap, 
            DataFormats.Dib, 
            DataFormats.EnhancedMetafile, 
            DataFormats.Rtf, 
            DataFormats.Html, 
            DataFormats.UnicodeText,
            DataFormats.OemText, 
            DataFormats.Text, 
        };

        private FrameworkElement _parent;
        private IDragDropTarget _target;
        private IDragDropSource _source;

        public WpfDragDropHelper(FrameworkElement parent, object callback = null)
        {
            if (parent == null) throw new ArgumentNullException("parent");

            _parent = parent;
            _target = (callback ?? parent) as IDragDropTarget;
            _source = (callback ?? parent) as IDragDropSource;

            if (_target != null)
            {
                _parent.AllowDrop = true;
                _parent.PreviewDragEnter += OnDragEnter;
                _parent.PreviewDragLeave += OnDragLeave;
                _parent.PreviewDragOver += OnDragOver;
                _parent.PreviewDrop += OnDrop;
            }
            if (_source != null)
            {
                _parent.MouseMove += new System.Windows.Input.MouseEventHandler(OnMouseMove);
            }
        }

        void OnMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var editor = sender as FrameworkElement;
            if (editor != null && e.LeftButton == MouseButtonState.Pressed)
            {
                if (e.OriginalSource is DependencyObject && GetNoDragSource((DependencyObject)e.OriginalSource) == true) return;

                var data = _source.GetData();
                if (data != null)
                {
                    _parent.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            var dragData = new DataObject(data);
                            dragData.SetData(ZetboxDragSourceDataFormat, editor);
                            DragDrop.DoDragDrop(editor, dragData, DragDropEffects.Copy | DragDropEffects.Link);
                        }
                        catch (COMException)
                        {
                            // LA LA LA LA LA LA, I can't here you....
                        }
                    }));
                }
            }
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            ResetBackground(sender);

            if (CanDrop(e))
            {
                foreach (var format in _target.AcceptableDataFormats)
                {
                    if (e.Data.GetDataPresent(format))
                    {
                        if (_target.OnDrop(format, e.Data.GetData(format)))
                            break;
                    }
                }
            }

            // the default implementation should be called
        }

        private bool CanDrop(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(ZetboxDragSourceDataFormat))
            {
                var src = e.Data.GetData(ZetboxDragSourceDataFormat) as FrameworkElement;
                if (src != null && src.IsDescendantOf(_target as DependencyObject))
                {
                    // Prevent drag'n'drop to self
                    return false;
                }
            }

            var accetableFomats = _target.AcceptableDataFormats;
            return _target.CanDrop && e.Data.GetFormats().Any(f => accetableFomats.Contains(f));
        }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            if (CanDrop(e))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }

            e.Handled = true; // Tell WPF that I've handled the effect
        }

        private class PreviousFill
        {
            public Brush Brush = null;
        }

        private PreviousFill _previousFill = null;
        private static Brush _dragEnderFill = null;
        private void OnDragEnter(object sender, DragEventArgs e)
        {
            var element = sender as FrameworkElement;
            if (element == null) return;

            var editor = element as Control ?? element.FindVisualParent<Control>();

            if (editor != null && _previousFill == null && CanDrop(e))
            {
                if (_dragEnderFill == null)
                {
                    _dragEnderFill = new SolidColorBrush() { Color = (Color)_parent.FindResource(Zetbox.Client.WPF.Styles.Defaults.SecondaryBackgroundKey) };
                }
                _previousFill = new PreviousFill() { Brush = editor.Background };
                editor.Background = _dragEnderFill;
            }

            // the default implementation should be called
        }

        private void OnDragLeave(object sender, DragEventArgs e)
        {
            ResetBackground(sender);

            // the default implementation should be called
        }

        private void ResetBackground(object sender)
        {
            var element = sender as FrameworkElement;
            if (element == null) return;

            var editor = element as Control ?? element.FindVisualParent<Control>();
            if (editor != null && _previousFill != null)
            {
                editor.Background = _previousFill.Brush;
                _previousFill = null;
            }
        }
    }
}
