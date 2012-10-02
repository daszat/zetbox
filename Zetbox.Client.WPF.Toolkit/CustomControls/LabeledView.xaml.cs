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

namespace Zetbox.Client.WPF.CustomControls
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
    using Zetbox.Client.GUI;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.Client.WPF.Toolkit;

    /// <summary>
    /// Interaction logic for LabeledView.xaml
    /// </summary>
    public partial class LabeledView : UserControl, IHasViewModel<ILabeledViewModel>
    {
        static LabeledView()
        {
            UserControl.VerticalContentAlignmentProperty.OverrideMetadata(
                typeof(LabeledView),
                new FrameworkPropertyMetadata(VerticalAlignment.Center));
        }

        public LabeledView()
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
            DependencyProperty.Register("LabelMinWidth", typeof(double), typeof(LabeledView), new UIPropertyMetadata(150.0));

        [TypeConverter(typeof(LengthConverter))]
        public double LabelWidth
        {
            get { return (double)GetValue(LabelWidthProperty); }
            set { SetValue(LabelWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelWidthProperty =
            DependencyProperty.Register("LabelWidth", typeof(double), typeof(LabeledView), new UIPropertyMetadata(Double.NaN));

        public string LabelSharedSizeGroup
        {
            get { return (string)GetValue(LabelSharedSizeGroupProperty); }
            set { SetValue(LabelSharedSizeGroupProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelSharedSizeGroup.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelSharedSizeGroupProperty =
            DependencyProperty.Register("LabelSharedSizeGroup", typeof(string), typeof(LabeledView), new UIPropertyMetadata("LabeledViewLabel"));

        // Using a DependencyProperty as the backing store for LabelSharedSizeGroup.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RequestedKindProperty =
            DependencyProperty.Register("RequestedKind", typeof(object), typeof(LabeledView), new UIPropertyMetadata(null));
        public object RequestedKind
        {
            get { return GetValue(RequestedKindProperty) ?? (ViewModel != null ? ViewModel.RequestedKind : null); }
            set { SetValue(RequestedKindProperty, value); }
        }

        #region IHasViewModel<ILabeledViewModel> Members

        public ILabeledViewModel ViewModel
        {
            get { return (ILabeledViewModel)WPFHelper.SanitizeDataContext(DataContext); }
        }

        #endregion
    }
}
