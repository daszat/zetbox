using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Input;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Microsoft.Samples.KMoore.WPFSamples.Zap
{
    public class ZapItemsControl : ItemsControl
    {
        static ZapItemsControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZapItemsControl), new FrameworkPropertyMetadata(typeof(ZapItemsControl)));
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is ContentPresenter;
        }
    }
}
