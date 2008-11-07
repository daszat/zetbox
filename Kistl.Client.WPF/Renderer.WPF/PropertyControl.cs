using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

using Kistl.GUI;
using Kistl.GUI.Renderer.WPF.Controls;

namespace Kistl.GUI.Renderer.WPF
{
    /// <summary>
    /// Defines common (Dependency-)Properties for Controls displaying/editing (Object)Properties
    /// </summary>
    [ContentProperty("EditPart")]
    public class PropertyControl : UserControl, IBasicControl, IDataErrorInfo
    {
        private Grid _grid = new Grid();
        private ErrorReporter _errorReporter = new ErrorReporter();

        public const int LabelColumn = 0;
        public const int EditPartColumn = 2;

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

            _errorReporter.SetValue(Grid.ColumnProperty, EditPartColumn);
            _grid.Children.Add(_errorReporter);
            Content = _grid;

            // to simplify binding for everyone
            DataContext = this;

            this.Loaded += LoadedHandler;
        }

        private void LoadedHandler(object sender, RoutedEventArgs e)
        {
            // march through all bindings and update the source once to trigger pending validation checks after initialisation
            foreach (var values in this.WalkTree().Select(child => new { Child = child, Enumerator = child.GetLocalValueEnumerator() }))
            {
                // evaluate child.GetLocalValueEnumerator() only once.
                LocalValueEnumerator enumerator = values.Enumerator;
                while (enumerator.MoveNext())
                {
                    DependencyProperty dp = enumerator.Current.Property;
                    if (!dp.ReadOnly)
                    {
                        BindingExpressionBase bindingInfo = BindingOperations.GetBindingExpressionBase(values.Child, dp);

                        // don't try to update if no binding is there
                        if (bindingInfo != null)
                            bindingInfo.UpdateSource();
                    }
                }

            }
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

        public string Error
        {
            get { return (string)GetValue(ErrorProperty); }
            set { SetValue(ErrorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Error.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorProperty =
            DependencyProperty.Register("Error", typeof(string), typeof(PropertyControl), new UIPropertyMetadata(null, ErrorChangedCallback));

        private static void ErrorChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (PropertyControl)d;
            self._errorReporter.Severity = String.IsNullOrEmpty((string)e.NewValue) ? 0 : 2;
            self._errorReporter.Text = (string)e.NewValue;
        }

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
            p._errorReporter.Content = p.EditPart;
        }

        #endregion

        #endregion

        #region IDataErrorInfo Members

        public string this[string columnName]
        {
            get
            {
                Console.Out.WriteLine("Error checked for {0} on {1}", columnName, this);
                return Error;
            }
        }

        #endregion
    }

    public static class NavigationHelpers
    {
        public static IEnumerable<DependencyObject> WalkTree(this DependencyObject d)
        {
            yield return d;
            int childrenCount = System.Windows.Media.VisualTreeHelper.GetChildrenCount(d);
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = System.Windows.Media.VisualTreeHelper.GetChild(d, i);
                yield return child;
                foreach (var c in WalkTree(child))
                {
                    yield return c;
                }
            }
        }
    }
}
