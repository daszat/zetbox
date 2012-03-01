using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using Kistl.Client.Presentables.DocumentManagement;
using Kistl.Client.GUI;
using Kistl.API.Utils;

namespace Kistl.Client.WPF.View.DocumentManagement
{
    public abstract class PreviewEditor : UserControl, IHasViewModel<FileViewModel>
    {
        public PreviewEditor()
        {
        }

        public FileViewModel ViewModel
        {
            get { return (FileViewModel)DataContext; }
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
            if (ViewModel.File.Blob != null)
            {
                if (vistaPreview == null)
                {
                    vistaPreview = new WPFPreviewControl();
                }
                PreviewControl.Content = vistaPreview;
                try
                {
                    vistaPreview.PreviewFilePath = ViewModel.File.Context.GetFileInfo(ViewModel.File.Blob.ID).FullName;
                }
                catch (Exception ex)
                {
                    Logging.Client.Error("Setting PreviewFilePath", ex);
                    PreviewControl.Content = new TextBlock() { Text = ex.Message };
                }
            }
            else
            {
                PreviewControl.Content = null;
            }
        }
    }
}
