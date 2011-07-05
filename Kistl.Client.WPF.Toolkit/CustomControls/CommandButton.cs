
namespace Kistl.Client.WPF.CustomControls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;
    using Kistl.Client.Presentables;
    using Kistl.Client.WPF.Converter;
    using Kistl.Client.WPF.Toolkit;

    public class CommandButton
        : Button
    {
        public CommandButton()
        {
            var bCmd = new Binding("CommandViewModel");
            bCmd.RelativeSource = RelativeSource.Self;
            bCmd.Converter = new Commands.Converter();
            this.SetBinding(CommandProperty, bCmd);

            var bLabel = new Binding("CommandViewModel.Label");
            bLabel.RelativeSource = RelativeSource.Self;
            this.SetBinding(ContentProperty, bLabel);

            var bTooltip = new Binding("CommandViewModel.ToolTip");
            bTooltip.RelativeSource = RelativeSource.Self;
            this.SetBinding(ToolTipProperty, bTooltip);

            var bImage = new Binding("CommandViewModel.Icon");
            bImage.RelativeSource = RelativeSource.Self;
            bImage.Converter = (IValueConverter)Application.Current.Resources["IconConverter"];
            this.SetBinding(ImageProperty, bImage);

            this.Loaded += new RoutedEventHandler(CommandButton_Loaded);
        }

        void CommandButton_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                this.Content = "Command Button";
            }
            this.CommandTarget = this;
        }

        public ICommandViewModel CommandViewModel
        {
            get { return (ICommandViewModel)GetValue(CommandViewModelProperty); }
            set { SetValue(CommandViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandViewModelProperty =
            DependencyProperty.Register("CommandViewModel", typeof(ICommandViewModel), typeof(CommandButton), new PropertyMetadata(OnCommandViewModelChanged));

        private static void OnCommandViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = d as FrameworkElement;
            var value = e.NewValue as ViewModel;
            if (self != null && value != null)
            {
                WPFHelper.ApplyIsBusyBehaviour(self, value);
            }
        }

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Image.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(CommandButton));


    }
}
