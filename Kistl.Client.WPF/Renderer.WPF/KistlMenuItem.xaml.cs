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

namespace Kistl.GUI.Renderer.WPF
{
    /// <summary>
    /// Interaction logic for MenuItem.xaml
    /// </summary>
    public partial class KistlMenuItem : MenuItem, IActionControl
    {
        public KistlMenuItem()
        {
            InitializeComponent();
        }

        protected override void OnClick()
        {
            base.OnClick();
            OnActionActivatedEvent();
        }

        #region IActionControl Members

        public event EventHandler ActionActivatedEvent;
        protected virtual void OnActionActivatedEvent()
        {
            if (ActionActivatedEvent != null)
            {
                ActionActivatedEvent(this, EventArgs.Empty);
            }
        }

        #endregion

        #region IBasicControl Members

        /// <summary>
        /// A descriptive Label for this Property
        /// </summary>
        public string ShortLabel
        {
            get { return (string)GetValue(ShortLabelProperty); }
            set { SetValue(ShortLabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Label.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShortLabelProperty =
            DependencyProperty.Register("ShortLabel", typeof(string), typeof(KistlMenuItem), new PropertyMetadata("short label"));

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Description.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(KistlMenuItem), new PropertyMetadata("long text (lore ipsum etc)"));

        /// <summary>
        /// ignored
        /// </summary>
        public FieldSize Size { get; set; }

        public Kistl.API.IKistlContext Context
        {
            get { return (Kistl.API.IKistlContext)GetValue(ContextProperty); }
            set { SetValue(ContextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Context.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContextProperty =
            DependencyProperty.Register("Context", typeof(Kistl.API.IKistlContext), typeof(PropertyControl));

        #endregion

    }
}
