using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Kistl.GUI;

namespace Kistl.GUI.Renderer.WPF
{
    /// <summary>
    /// Defines common (Dependency-)Properties for Controls displaying/editing (Object)Properties
    /// </summary>
    public class PropertyControl : UserControl, IBasicControl
    {
        private Grid _grid = new Grid();

        public static readonly int LabelColumn = 0;
        public static readonly int EditPartColumn = 2;

        public PropertyControl()
        {
            MinWidth = 400;
            Margin = new Thickness(4);

            _grid.ColumnDefinitions.Add(new ColumnDefinition()
            {
                MinWidth = 150,
                Width = new GridLength(1, GridUnitType.Auto),
            });
            _grid.ColumnDefinitions.Add(new ColumnDefinition()
            {
                Width = new GridLength(5, GridUnitType.Pixel),
            });
            _grid.ColumnDefinitions.Add(new ColumnDefinition()
            {
                Width = new GridLength(1, GridUnitType.Star)
            });

            Content = _grid;
        }

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

        #endregion

        #region further infrastructure

        public bool IsValidValue
        {
            get { return (bool)GetValue(IsValidValueProperty); }
            set { SetValue(IsValidValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsValidValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsValidValueProperty =
            DependencyProperty.Register("IsValidValue", typeof(bool), typeof(PropertyControl), new PropertyMetadata(true));


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
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(PropertyControl), new PropertyMetadata(false));

        public Kistl.API.IKistlContext Context
        {
            get { return (Kistl.API.IKistlContext)GetValue(ContextProperty); }
            set { SetValue(ContextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Context.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContextProperty =
            DependencyProperty.Register("Context", typeof(Kistl.API.IKistlContext), typeof(PropertyControl));

        #endregion

        #region Design

        #region Label

        public UIElement Label
        {
            get { return (UIElement)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Label.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(UIElement), typeof(PropertyControl), new UIPropertyMetadata(LabelChangedCallback));

        private static void LabelChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PropertyControl p = (PropertyControl)d;
            p.Label.SetValue(Grid.ColumnProperty, LabelColumn);
            p._grid.Children.Remove((UIElement)e.OldValue);
            p._grid.Children.Add(p.Label);
        }

        #endregion

        #region EditPart

        public UIElement EditPart
        {
            get { return (UIElement)GetValue(EditPartProperty); }
            set { SetValue(EditPartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EditPart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditPartProperty =
            DependencyProperty.Register("EditPart", typeof(UIElement), typeof(PropertyControl), new UIPropertyMetadata(EditPartChangedCallback));

        private static void EditPartChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PropertyControl p = (PropertyControl)d;
            p.EditPart.SetValue(Grid.ColumnProperty, EditPartColumn);
            p._grid.Children.Remove((UIElement)e.OldValue);
            p._grid.Children.Add(p.EditPart);
        }

        #endregion

        #endregion

    }
}
