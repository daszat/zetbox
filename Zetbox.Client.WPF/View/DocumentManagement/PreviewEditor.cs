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

namespace Zetbox.Client.WPF.View.DocumentManagement
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.Client.GUI;
    using Zetbox.Client.Presentables.DocumentManagement;
    using Zetbox.Client.WPF.CustomControls;
    using Zetbox.Client.WPF.Toolkit;

    public abstract class PreviewEditor : UserControl, IHasViewModel<FileViewModel>, IDragDropTarget
    {
        private WpfDragDropHelper _dragDrop;

        public PreviewEditor()
        {
            _dragDrop = new WpfDragDropHelper(this);
        }

        public FileViewModel ViewModel
        {
            get { return (FileViewModel)WPFHelper.SanitizeDataContext(DataContext); }
        }

        protected abstract ContentPresenter PreviewControl { get; }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == DataContextProperty)
            {
                if (ViewModel != null)
                {
                    PreviewDocument();
                    ViewModel.Object.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(vmdl_PropertyChanged);
                }
            }
        }

        void vmdl_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Blob")
            {
                PreviewDocument();
            }
        }

        WPFPreviewControl vistaPreview;
        protected void PreviewDocument()
        {
            try
            {
                var blob = ViewModel.File.Blob;
                if (blob != null)
                {
                    var fi = ViewModel.File.Context.GetFileInfo(blob.ID);

                    if (TryText(blob)) return;
                    if (TryImage(blob)) return;
                    if (TryVistaPreview(fi)) return;
                }
                else
                {
                    PreviewControl.Content = null;
                }
            }
            catch (Exception ex)
            {
                Logging.Client.Error("Setting PreviewFilePath", ex);
                PreviewControl.Content = new TextBlock() { Text = ex.Message };
            }
        }

        #region Try *
        private bool TryVistaPreview(System.IO.FileInfo fi)
        {
            if (vistaPreview == null)
            {
                vistaPreview = new WPFPreviewControl();
            }
            PreviewControl.Content = vistaPreview;
            vistaPreview.PreviewFilePath = fi.FullName;
            return true;
        }

        private bool TryImage(Blob blob)
        {
            try
            {
                var bmpImg = new BitmapImage();
                bmpImg.BeginInit();
                bmpImg.StreamSource = blob.GetStream();
                bmpImg.EndInit();

                PreviewControl.Content = new ZoomBorder() { Child = new Image() { Source = bmpImg } };
                return true;
            }
            catch (NotSupportedException)
            {
                // No image, try next one
                return false;
            }
        }

        private bool TryText(Blob blob)
        {
            if ("text/plain".Equals(blob.MimeType, StringComparison.InvariantCultureIgnoreCase))
            {
                using (var s = blob.GetStream())
                {
                    var isUtf8 = Utf8Checker.IsUtf8(s);
                    s.Seek(0, SeekOrigin.Begin);
                    var sr = new StreamReader(s, isUtf8 ? Encoding.UTF8 : Encoding.Default);

                    PreviewControl.Content = new TextBox()
                    {
                        Text = sr.ReadToEnd(),
                        IsReadOnly = true,
                        TextWrapping = TextWrapping.Wrap,
                        FontFamily = new System.Windows.Media.FontFamily("Consolas, Courier New"),
                        HorizontalScrollBarVisibility = ScrollBarVisibility.Visible,
                        VerticalScrollBarVisibility = ScrollBarVisibility.Visible,
                        VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
                    };
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region DragDrop
        string[] IDragDropTarget.AcceptableDataFormats
        {
            get { return new[] { "FileDrop" }; }
        }

        bool IDragDropTarget.CanDrop
        {
            get
            {
                return ViewModel != null && ViewModel.CanUpload;
            }
        }

        bool IDragDropTarget.OnDrop(string format, object data)
        {
            if (ViewModel == null) return false;
            switch (format)
            {
                case "FileDrop":
                    var files = (string[])data;
                    ViewModel.Upload(files.First());
                    return true;
            }
            return false;
        }
        #endregion
    }
}
