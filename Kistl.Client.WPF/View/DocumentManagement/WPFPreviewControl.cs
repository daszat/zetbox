// http://www.codeproject.com/KB/WPF/wpf_vista_preview_handler.aspx

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace Kistl.Client.WPF.View.DocumentManagement
{
    public class WPFPreviewControl : ContentPresenter
    {
        Rect actualRect = new Rect();
        System.Windows.Forms.Integration.WindowsFormsHost host;

        public WPFPreviewControl()
        {
            this.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.VerticalAlignment = VerticalAlignment.Stretch;

            host = new System.Windows.Forms.Integration.WindowsFormsHost();
            host.Child = new System.Windows.Forms.Panel();
            host.Loaded += new RoutedEventHandler(host_Loaded);
            this.Content = host;
        }

        void host_Loaded(object sender, RoutedEventArgs e)
        {
            AttachPreview();
        }

        public string PreviewContent
        {
            get { return (string)GetValue(PreviewContentProperty); }
            set { SetValue(PreviewContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreviewContentProperty =
            DependencyProperty.Register("PreviewContent", typeof(string), typeof(WPFPreviewControl));

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == ContentControl.ActualHeightProperty || e.Property == ContentControl.ActualWidthProperty)
            {
                actualRect = new Rect(0, 0, this.ActualWidth, this.ActualHeight);
                if(host != null && host.Handle != IntPtr.Zero)
                    host.Handle.InvalidateAttachedPreview(actualRect);
            }
            if (e.Property == PreviewContentProperty)
            {
                AttachPreview();
            }
            base.OnPropertyChanged(e);
        }

        private void AttachPreview()
        {
            if (host != null && host.Handle != IntPtr.Zero && !string.IsNullOrEmpty(PreviewContent))
                host.Handle.AttachPreview(PreviewContent, actualRect);
        }
    }
}
