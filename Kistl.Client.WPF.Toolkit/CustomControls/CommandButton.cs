
namespace Kistl.Client.WPF.CustomControls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using Kistl.Client.Presentables;
    using System.ComponentModel;
    
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

            this.Loaded += new RoutedEventHandler(CommandButton_Loaded);
        }

        void CommandButton_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                this.Content = "Command Button";
            }
        }

        public ICommandViewModel CommandViewModel
        {
            get { return (ICommandViewModel)GetValue(CommandViewModelProperty); }
            set { SetValue(CommandViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandViewModelProperty =
            DependencyProperty.Register("CommandViewModel", typeof(ICommandViewModel), typeof(CommandButton));

    }
}
