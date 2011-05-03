using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API.Client;
using Kistl.Client.Presentables;

namespace Kistl.Client.WPF.Toolkit
{
    public class WpfDelayedTask : IDelayedTask
    {
        private ViewModel _displayer;
        private Action _task;

        public WpfDelayedTask(ViewModel displayer, Action task)
        {
            if (displayer == null) throw new ArgumentNullException("displayer");
            if (task == null) throw new ArgumentNullException("task");

            _displayer = displayer;
            _task = task;
        }

        public void Trigger()
        {
            _displayer.IsBusy = true;
            System.Windows.Threading.Dispatcher.FromThread(System.Threading.Thread.CurrentThread).BeginInvoke(new Action(() =>
            {
                try
                {
                    _task();
                }
                finally
                {
                    _displayer.IsBusy = false;
                }
            }), System.Windows.Threading.DispatcherPriority.Background); // prio must be Background to let fakeprogressoverlay be rendered
        }
    }
}
