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

namespace WPFPresenter.Controls
{
    /// <summary>
    /// Interaction logic for Listing.xaml
    /// </summary>
    public partial class Listing : UserControl
    {
        public Listing()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == IndentionProperty)
            {
                this.Margin = new Thickness(Indention * 60, 0, 0, 0);
            }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(Listing));

        public int Indention
        {
            get { return (int)GetValue(IndentionProperty); }
            set { SetValue(IndentionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Indention.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IndentionProperty =
            DependencyProperty.Register("Indention", typeof(int), typeof(Listing), new UIPropertyMetadata(0));
    }
}
