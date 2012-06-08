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

namespace Zetbox.Client.WPF.CustomControls
{
    /// <summary>
    /// Interaction logic for ErrorReporter.xaml
    /// </summary>
    public partial class ErrorReporter : UserControl
    {
        public ErrorReporter()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        /// <summary>
        /// Decides whether or not this ErrorReporter is currently displaying an error or not.
        /// This is basically Severity>0 || !IsNullOrEmpty(Text)
        /// </summary>
        public bool IsReportingError
        {
            get { return (bool)GetValue(IsReportingErrorProperty); }
            set { SetValue(IsReportingErrorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsReportingError.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsReportingErrorProperty =
            DependencyProperty.Register("IsReportingError", typeof(bool), typeof(ErrorReporter), new UIPropertyMetadata(false));

        private void SetIsReportingError()
        {
            this.IsReportingError = this.Severity > 0 || !String.IsNullOrEmpty(this.Text);
        }


        /// <summary>
        /// The severity of this error
        /// </summary>
        public int Severity
        {
            get { return (int)GetValue(SeverityProperty); }
            set { SetValue(SeverityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ErrorSeverity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeverityProperty =
            DependencyProperty.Register("Severity", typeof(int), typeof(ErrorReporter), new UIPropertyMetadata(0, SeverityOrTextChangedCallback), ValidateSeverityCallback);


        private static bool ValidateSeverityCallback(object value)
        {
            return (int)value >= 0;
        }

        /// <summary>
        /// The error text to display
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ErrorReporter), new UIPropertyMetadata(null, SeverityOrTextChangedCallback));


        private static void SeverityOrTextChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (ErrorReporter)d;
            self.SetIsReportingError();
        }

    }
}
