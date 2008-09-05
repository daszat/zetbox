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
using Kistl.App.Base;
using System.Collections.ObjectModel;

namespace Kistl.GUI.Renderer.WPF
{
    /// <summary>
    /// Interaction logic for EnumListControl.xaml
    /// </summary>
    public partial class EnumControl : PropertyControl, IEnumControl
    {
        public EnumControl()
        {
            ItemsSource = new ObservableCollection<EnumerationEntry>();
            InitializeComponent();
        }

        #region IEnumControl Members

        public IList<EnumerationEntry> ItemsSource
        {
            get { return (IList<Kistl.App.Base.EnumerationEntry>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IList<EnumerationEntry>), typeof(EnumControl), new UIPropertyMetadata());

        #endregion

        #region IValueControl<EnumerationEntry> Members

        private bool _SupressUserInputEvent = false;
        EnumerationEntry IValueControl<EnumerationEntry>.Value
        {
            get { return (EnumerationEntry)GetValue(ValueProperty); }
            set
            {
                try
                {
                    _SupressUserInputEvent = true;
                    SetValue(ValueProperty, value);
                }
                finally
                {
                    _SupressUserInputEvent = false;
                }
            }
        }

        /// <summary>
        /// The actual Value of this Property
        /// </summary>
        public EnumerationEntry Value
        {
            get { return (EnumerationEntry)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(EnumerationEntry), typeof(EnumControl));


        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == ValueProperty)
            {
                OnUserInput(e);
            }
        }

        protected virtual void OnUserInput(DependencyPropertyChangedEventArgs e)
        {
            if (UserInput != null && !_SupressUserInputEvent)
            {
                UserInput(this, new EventArgs());
            }
        }

        public event EventHandler UserInput;

        #endregion
    }
}
