using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Client.Presentables;
using System.Windows.Forms;
using System.Diagnostics;

namespace Kistl.Client.Forms
{
    internal class FormsThreadManager : IThreadManager
    {
        private Button _dispatcher = new Button();

        #region IThreadManager Members

        public void Verify()
        {
            if (_dispatcher.InvokeRequired)
                throw new InvalidOperationException("Off-Main Thread invocation!");
        }

        public void Queue(object lck, Action uiTask)
        {
            Debug.Assert(lck == this, "always pass the UI thread manager to UI.Queue() calls");
            _dispatcher.BeginInvoke(uiTask);
        }

        public void Queue(object lck, Action<object> uiTask, object data)
        {
            Debug.Assert(lck == this, "always pass the UI thread manager to UI.Queue() calls");
            _dispatcher.BeginInvoke(new Action(delegate() { uiTask(data); }));
        }

        #endregion
    }
}
