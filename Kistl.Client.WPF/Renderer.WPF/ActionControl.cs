using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

using Kistl.GUI;

namespace Kistl.GUI.Renderer.WPF
{
    public class ActionControl : Button, IActionControl
    {

        public ActionControl()
        {
            this.Click += new System.Windows.RoutedEventHandler(ActionControl_Click);
        }

        #region Event Handlers

        void ActionControl_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OnActionActivated();
        }

        #endregion

        #region Behaviours

        protected virtual void OnActionActivated()
        {
            if (ActionActivatedEvent != null)
            {
                ActionActivatedEvent(this, EventArgs.Empty);
            }
        }

        #endregion

        #region IActionControl Member

        public event EventHandler ActionActivatedEvent;

        #endregion

        #region IBasicControl Members

        string IBasicControl.ShortLabel
        {
            get { return Content.ToString(); }
            set { Content = value; }
        }

        string IBasicControl.Description
        {
            get { return ToolTip.ToString(); }
            set { ToolTip = value; }
        }

        FieldSize IBasicControl.Size
        {
            get { return FieldSize.Full; }
            set { }
        }

        // No context needed
        public Kistl.API.IKistlContext Context { get; set; }

        #endregion

    }
}
