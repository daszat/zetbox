
namespace Kistl.Client.WPF.CustomControls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Permissions;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Media;
    using System.Windows.Threading;

    // from http://blogs.msdn.com/b/nathannesbit/archive/2008/11/05/creating-adorner-content-in-xaml.aspx
    public class ContentAdorner : Adorner
    {
        private static Action EmptyDelegate = delegate() { };

        public static void ShowWaitDialog(FrameworkElement target)
        {
            var overlayTemplate = target.FindResource("FakeProgressOverlay");
            // windows do not have an adorner layer, try their first (and only) child
            // else this is not working
            // hooray for voodoo
            if (target is Window)
                target = LogicalTreeHelper.GetChildren(target).OfType<FrameworkElement>().FirstOrDefault();
            target.SetValue(ContentAdorner.AdornerContentTemplateProperty, overlayTemplate);
        }

        public static void HideWaitDialog(FrameworkElement target)
        {
            // windows do not have an adorner layer, try their first (and only) child
            // else this is not working
            // hooray for voodoo
            if (target is Window)
                target = LogicalTreeHelper.GetChildren(target).OfType<FrameworkElement>().FirstOrDefault();
            target.SetValue(ContentAdorner.AdornerContentTemplateProperty, null);
        }

        VisualCollection children;
        FrameworkElement child;

        // Be sure to call the base class constructor.
        public ContentAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            this.children = new VisualCollection(this);

            //
            // Create the content control
            //
            var contentControl = new ContentControl();

            //
            // Bind the content control to the Adorner's ContentTemplate property, so we know what to display
            //
            var contentTemplateBinding = new Binding();
            contentTemplateBinding.Path = new PropertyPath(AdornerContentTemplateProperty);
            contentTemplateBinding.Source = adornedElement;
            contentControl.SetBinding(ContentControl.ContentTemplateProperty, contentTemplateBinding);

            //
            // Add the ContentControl as a child
            //
            this.child = contentControl;
            this.children.Add(this.child);
            this.AddLogicalChild(this.child);
        }

        //
        // The AdornerContentTemplate property is used to attach a template for adorner content to a given item.
        //

        public static DependencyProperty AdornerContentTemplateProperty = DependencyProperty.RegisterAttached("AdornerContentTemplate", typeof(DataTemplate), typeof(ContentAdorner), new PropertyMetadata(null, OnAdornerContentTemplatePropertyChanged));

        public static DataTemplate GetAdornerContentTemplate(DependencyObject element)
        {
            return (DataTemplate)element.GetValue(AdornerContentTemplateProperty);
        }

        public static void SetAdornerContentTemplate(DependencyObject element, DataTemplate value)
        {
            element.SetValue(AdornerContentTemplateProperty, value);
        }

        static void OnAdornerContentTemplatePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var target = (FrameworkElement)sender;

            //
            // TODO: Remove old template
            //

            if (e.NewValue != null)
            {
                if (target.IsLoaded)
                {
                    ApplyContentAdorner(target);
                }
                else
                {
                    //
                    // Controls not loaded don't have an adorner layer yet.
                    //
                    target.Loaded += OnAdornerTargetLoaded;
                }
            }
        }

        static void OnAdornerTargetLoaded(object sender, RoutedEventArgs e)
        {
            var target = (FrameworkElement)sender;
            target.Loaded -= OnAdornerTargetLoaded;
            ApplyContentAdorner(target);
        }

        private static void ApplyContentAdorner(FrameworkElement target)
        {
            var adornerLayer = AdornerLayer.GetAdornerLayer(target);
            if (adornerLayer == null)
            {
                // windows do not have an adorner layer, try their first (and only) child
                target = LogicalTreeHelper.GetChildren(target).OfType<FrameworkElement>().FirstOrDefault();
                if (target == null) return; // no children found
                adornerLayer = AdornerLayer.GetAdornerLayer(target);
                if (adornerLayer == null) return; // nothing adornable found
            }
            var adorner = new ContentAdorner(target);

            adornerLayer.Add(adorner);
        }

        protected override Visual GetVisualChild(int index)
        {
            return this.children[index];
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return this.children.Count;
            }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            this.child.Measure(constraint);
            return AdornedElement.RenderSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Point location = new Point(0, 0);
            Rect rect = new Rect(location, finalSize);
            this.child.Arrange(rect);
            return this.child.RenderSize;
        }
    }
}
