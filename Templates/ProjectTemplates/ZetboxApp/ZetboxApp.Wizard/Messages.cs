using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvDTE;

namespace ZetboxApp.Wizard
{
    public class Messages
    {
        private DTE _dte;
        private OutputWindowPane _pane;

        public Messages(DTE dte)
        {
            _dte = dte;
        }

        private void EnsurePane()
        {
            if (_pane != null) return;
            if (_dte == null) return;

            Window window = _dte.Windows.Item(EnvDTE.Constants.vsWindowKindOutput);
            OutputWindow outputWindow = (OutputWindow)window.Object;

            _pane = outputWindow.OutputWindowPanes.OfType<OutputWindowPane>().FirstOrDefault(p => p.Name == "zetbox");
            if (_pane == null)
            {
                _pane = outputWindow.OutputWindowPanes.Add("zetbox");
            }
        }

        public void Write(string message)
        {
            EnsurePane();
            if (_pane != null)
            {
                _pane.Activate();
                _pane.OutputString(message);
                System.Windows.Forms.Application.DoEvents();
            }
        }

        public void WriteLine(string message)
        {
            Write(message + "\n");
        }

        public void WriteLine(string format, params object[] v)
        {
            WriteLine(string.Format(format, v));
        }
    }
}
