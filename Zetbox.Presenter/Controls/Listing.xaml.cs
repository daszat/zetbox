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
