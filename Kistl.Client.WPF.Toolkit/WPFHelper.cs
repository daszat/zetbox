using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace Kistl.Client.WPF.Toolkit
{
    public static class WPFHelper
    {
        /// <summary>
        /// Don't ask. WPF isn't able to handle FocusLost in an acceptable, simple way
        /// </summary>
        public static void MoveFocus()
        {
            // Gets the element with keyboard focus.
            UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

            // Change keyboard focus.
            if (elementWithFocus != null)
            {
                elementWithFocus.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }
    } 
}
