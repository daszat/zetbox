using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Kistl.GUI;

namespace Kistl.Client.Controls
{
    /// <summary>
    /// Defines common (Dependency-)Properties for Controls displaying/editing (Object)Properties
    /// </summary>
    public class PropertyControl : UserControl, IBasicControl
    {
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
            DependencyProperty.Register("ShortLabel", typeof(string), typeof(PropertyControl), new PropertyMetadata("short label"));

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Description.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(PropertyControl), new PropertyMetadata("long text (lore ipsum etc)"));

        public FieldSize Size
        {
            get { return (FieldSize)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Size.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register("Size", typeof(FieldSize), typeof(PropertyControl), new UIPropertyMetadata(FieldSize.Full));

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
