using System;
using System.Collections.Generic;
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
using Kistl.Client.Presentables;
using System.ComponentModel;
using System.Windows.Markup;

namespace Kistl.Client.WPF.CustomControls
{
    /// <summary>
    /// Interaction logic for LabeledView.xaml
    /// </summary>
    [ContentProperty("LabeledContent")]
    public partial class LabeledContainer : UserControl
    {
        public LabeledContainer()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();            
        }


        [TypeConverter(typeof(LengthConverter))]
        public double LabelMinWidth
        {
            get { return (double)GetValue(LabelMinWidthProperty); }
            set { SetValue(LabelMinWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelMinWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelMinWidthProperty =
            DependencyProperty.Register("LabelMinWidth", typeof(double), typeof(LabeledContainer), new UIPropertyMetadata(150.0));


        [TypeConverter(typeof(LengthConverter))]
        public double LabelWidth
        {
            get { return (double)GetValue(LabelWidthProperty); }
            set { SetValue(LabelWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelWidthProperty =
            DependencyProperty.Register("LabelWidth", typeof(double), typeof(LabeledContainer), new UIPropertyMetadata(Double.NaN));



        public string LabelSharedSizeGroup
        {
            get { return (string)GetValue(LabelSharedSizeGroupProperty); }
            set { SetValue(LabelSharedSizeGroupProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelSharedSizeGroup.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelSharedSizeGroupProperty =
            DependencyProperty.Register("LabelSharedSizeGroup", typeof(string), typeof(LabeledContainer), new UIPropertyMetadata("LabeledViewLabel"));



        public object LabeledContent
        {
            get { return (object)GetValue(LabeledContentProperty); }
            set { SetValue(LabeledContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabeledContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabeledContentProperty =
            DependencyProperty.Register("LabeledContent", typeof(object), typeof(LabeledContainer));



        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Label.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(LabeledContainer), new UIPropertyMetadata(string.Empty));


        public bool Required
        {
            get { return (bool)GetValue(RequiredProperty); }
            set { SetValue(RequiredProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Required.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RequiredProperty =
            DependencyProperty.Register("Required", typeof(bool), typeof(LabeledContainer), new UIPropertyMetadata(false));

    }
}
