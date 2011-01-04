using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Input;

namespace Kistl.Client.WPF.CustomControls
{
    /// <summary>
    /// http://social.msdn.microsoft.com/Forums/en-US/wpf/thread/c5b6a94b-2cdc-4cfb-8ccb-0f7e680586d7
    /// If the ToolBars ItemsSource is DataBound, all buttons will appear in the overflow list until a manual window resize.
    /// This is an Workaround Control which invalidates it's Measure after Templates are applied.
    /// </summary>
    public class WorkaroundToolBar : ToolBar
    {
        public WorkaroundToolBar()
            : base()
        {
            KeyboardNavigation.SetTabNavigation(this, KeyboardNavigationMode.Continue);
        }

        private delegate void MethodeInvoke();

        public override void OnApplyTemplate()
        {
            Dispatcher.BeginInvoke(new MethodeInvoke(InvalidateMeasure), DispatcherPriority.Background, null);
            base.OnApplyTemplate();
        }
    }
}
