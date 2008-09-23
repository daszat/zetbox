using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

using Kistl.GUI;

namespace Kistl.GUI.Renderer.WPF
{
    public class ToolBarWrapper : ToolBar, IBasicControl
    {

        #region IBasicControl Members

        string IBasicControl.ShortLabel
        {
            get { return Header.ToString(); }
            set { Header = value; }
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
