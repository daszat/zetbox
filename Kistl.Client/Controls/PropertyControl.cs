using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Kistl.Client.Controls
{
    /// <summary>
    /// Defines common (Dependency-)Properties for Controls displaying/editing (Object)Properties
    /// </summary>
    public class PropertyControl : UserControl
    {
        /// <summary>
        /// A descriptive Label for this Property
        /// </summary>
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Label.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(PropertyControl));

        /// <summary>
        /// The actual Value of this Property
        /// </summary>
        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(PropertyControl));

        /// <summary>
        /// Only display the Value, but do not allow to modify it
        /// </summary>
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsReadOnly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(PropertyControl), new UIPropertyMetadata(false));



        public Kistl.API.Client.KistlContext Context
        {
            get { return (Kistl.API.Client.KistlContext)GetValue(ContextProperty); }
            set { SetValue(ContextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Context.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContextProperty =
            DependencyProperty.Register("Context", typeof(Kistl.API.Client.KistlContext), typeof(PropertyControl));
    }
}
