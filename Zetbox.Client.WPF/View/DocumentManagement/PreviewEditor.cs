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
using System.Windows;
using System.Windows.Controls;
using Zetbox.API.Utils;
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables.DocumentManagement;
using Zetbox.Client.WPF.Toolkit;
using System.Windows.Media.Imaging;
using Zetbox.Client.WPF.CustomControls;

namespace Zetbox.Client.WPF.View.DocumentManagement
{
    public abstract class PreviewEditor : UserControl, IHasViewModel<FileViewModel>
    {
        public PreviewEditor()
        {
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

                    try
                    {
                        var bmpImg = new BitmapImage();
                        bmpImg.BeginInit();
                        bmpImg.StreamSource = blob.GetStream();
                        bmpImg.EndInit();

                        var zoomCtrl = new ZoomBorder();
                        zoomCtrl.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                        zoomCtrl.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

                        var imgCtrl = new Image() { Source = bmpImg };

                        zoomCtrl.Child = imgCtrl;
                        PreviewControl.Content = zoomCtrl;

                        return;
                    }
                    catch (NotSupportedException)
                    {
                        // No image, try the Vista-Preview
                    }

                    if (vistaPreview == null)
                    {
                        vistaPreview = new WPFPreviewControl();
                    }
                    PreviewControl.Content = vistaPreview;
                    vistaPreview.PreviewFilePath = fi.FullName;
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
    }
}
