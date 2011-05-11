
namespace Kistl.Client.WPF.Toolkit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Windows.Threading;
    using Kistl.API.Client;
    using Kistl.Client.Presentables;

    /// <summary>
    /// Implements a delayed task using the WPF Dispatcher for the current Thread.
    /// </summary>
    public sealed class WpfDelayedTask : IDelayedTask
    {
        private readonly ViewModel _displayer;
        private readonly Action _task;

        public WpfDelayedTask(ViewModel displayer, Action task)
        {
            if (task == null) throw new ArgumentNullException("task");

            _displayer = displayer;
            _task = task;
        }

        public void Trigger()
        {
            if (_displayer != null)
                _displayer.IsBusy = true;

            Dispatcher.FromThread(Thread.CurrentThread).BeginInvoke(new Action(() =>
            {
                try
                {
                    _task();
                }
                finally
                {
                    if (_displayer != null)
                        _displayer.IsBusy = false;
                }
            }), DispatcherPriority.Background); // prio must be Background to let fakeprogressoverlay be rendered
        }
    }
}
