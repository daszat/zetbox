using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Kistl.Client.Presentables;
using System.Windows;
using System.Windows.Data;

namespace Kistl.Client.WPF.CustomControls
{
    public class CommandButton : Button
    {
        public CommandButton()
        {
            {
                var bCmd = new Binding("CommandModel");
                bCmd.RelativeSource = RelativeSource.Self;
                bCmd.Converter = new Commands.Converter();
                this.SetBinding(CommandProperty, bCmd);
            }

            {
                var bLabel = new Binding("CommandModel.Label");
                bLabel.RelativeSource = RelativeSource.Self;
                this.SetBinding(ContentProperty, bLabel);
            }

            {
                var bTooltip = new Binding("CommandModel.ToolTip");
                bTooltip.RelativeSource = RelativeSource.Self;
                this.SetBinding(ToolTipProperty, bTooltip);
            }
        }

        public ICommand CommandModel
        {
            get { return (ICommand)GetValue(CommandModelProperty); }
            set { SetValue(CommandModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandModelProperty =
            DependencyProperty.Register("CommandModel", typeof(ICommand), typeof(CommandButton));

    }
}
