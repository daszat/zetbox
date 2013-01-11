using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zetbox.ConfigEditor.ViewModels
{
    public class WindowViewModel : ViewModel
    {        
        public event EventHandler Close;
        protected void OnClose()
        {
            var temp = Close;
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }
    }
}
