using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using Kistl.API;

namespace Kistl.GUI.Renderer.WPF
{
    public partial class ObjectTabItem : ContentControl, IObjectControl
    {
        public ObjectTabItem()
        {
            Menus = new List<UIElement>();
        }

        #region Property: Menus

        public List<UIElement> Menus
        {
            get { return (List<UIElement>)GetValue(MenusKey.DependencyProperty); }
            private set { SetValue(MenusKey, value); }
        }

        // Using a DependencyProperty as the backing store for Menus.  This enables animation, styling, binding, etc...
        private static readonly DependencyPropertyKey MenusKey =
            DependencyProperty.RegisterReadOnly("Menus", typeof(List<UIElement>), typeof(ObjectTabItem), new UIPropertyMetadata());

        #endregion

        #region IObjectControl Member

        Kistl.API.IDataObject IObjectControl.Value { get; set; }

        public event EventHandler UserSaveRequest;

        public event EventHandler UserDeleteRequest;

        #endregion

        #region IBasicControl Member

        public string ShortLabel
        {
            get { return (string)GetValue(ShortLabelProperty); }
            set { SetValue(ShortLabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShortLabel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShortLabelProperty =
            DependencyProperty.Register("ShortLabel", typeof(string), typeof(ObjectTabItem), new UIPropertyMetadata(""));

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Description.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(ObjectTabItem), new UIPropertyMetadata(""));


        public FieldSize Size
        {
            get { return (FieldSize)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Size.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register("Size", typeof(FieldSize), typeof(ObjectTabItem), new UIPropertyMetadata(null));

        public IKistlContext Context
        {
            get { return (IKistlContext)GetValue(ContextProperty); }
            set { SetValue(ContextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Context.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContextProperty =
            DependencyProperty.Register("Context", typeof(IKistlContext), typeof(ObjectTabItem), new UIPropertyMetadata());


        #endregion
    }
}
