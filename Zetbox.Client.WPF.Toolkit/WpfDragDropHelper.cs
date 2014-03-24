using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Runtime.InteropServices;

namespace Zetbox.Client.WPF.Toolkit
{
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

        private Control _parent;
        private IDragDropTarget _target;
        private IDragDropSource _source;

        public WpfDragDropHelper(Control parent, object callback = null)
        {
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
            var editor = sender as Control;
            if (editor != null && e.LeftButton == MouseButtonState.Pressed)
            {
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

            var editor = sender as Control;
            if (editor != null && CanDrop(e))
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
                if (src != null && src.IsDescendantOf(_target as FrameworkElement))
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
            var editor = sender as Control;
            if (editor != null && CanDrop(e))
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
            var editor = sender as Control;
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
            var editor = sender as Control;
            if (editor != null && _previousFill != null)
            {
                editor.Background = _previousFill.Brush;
                _previousFill = null;
            }
        }
    }
}
