
namespace Kistl.Client.WPF.View.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using Kistl.Client.GUI;
    using Kistl.Client.WPF.CustomControls;
    
    public class StringDisplay : PropertyEditor
    {
        private TextBlock txtStringDisplay;

//<ctrls:PropertyEditor x:Class="Kistl.Client.WPF.View.KistlBase.StringDisplay"
//                      xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
//                      xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
//                      xmlns:ctrls="clr-namespace:Kistl.Client.WPF.CustomControls;assembly=Kistl.Client.WPF.Toolkit"
//                      xmlns:view="clr-namespace:Kistl.Client.WPF.View.KistlBase"
//                      ToolTip="{Binding ToolTip}">
//    <TextBlock x:Name="txtStringDisplay"
//               Text="{Binding FormattedValue, Mode=OneWay}"
//               ToolTip="{Binding ToolTip}" />
//</ctrls:PropertyEditor>


        public StringDisplay()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;

            // InitializeComponent
            txtStringDisplay = new TextBlock();
            Content = txtStringDisplay;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            {
                var b = new Binding("FormattedValue");
                b.Mode = BindingMode.OneWay;
                BindingOperations.SetBinding(txtStringDisplay, TextBlock.TextProperty, b);
            }

            {
                var b = new Binding("ToolTip");
                b.Mode = BindingMode.OneWay;
                BindingOperations.SetBinding(txtStringDisplay, TextBlock.ToolTipProperty, b);
            }

            {
                var b = new Binding("ToolTip");
                b.Mode = BindingMode.OneWay;
                BindingOperations.SetBinding(txtStringDisplay, ContentControl.ToolTipProperty, b);
            }
        }

        protected override FrameworkElement MainControl
        {
            get { return txtStringDisplay; }
        }
    }
}
