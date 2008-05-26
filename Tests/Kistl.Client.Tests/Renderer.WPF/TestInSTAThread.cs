using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;
using NUnit.Framework;

namespace Kistl.Client.Renderer.WPF
{
    /// <summary>
    /// Starting from Josh Smith's fine article on unit testing DispatcherTimers at
    /// http://www.codeproject.com/KB/WPF/UnitTestDispatcherTimer.aspx this class implements
    /// testing WPF Controls on a seperate Thread.
    /// </summary>
    /// 
    public class TestInSTAThread
    {
        public static string WORKER_THREAD_NAME = "STAWorkerThread";
        public void AssertWorkerThread(bool onThread)
        {
            Assert.AreEqual(onThread, Thread.CurrentThread.Name == WORKER_THREAD_NAME,
                onThread ? String.Format("Should run on the worker thread, but runs on {0}", Thread.CurrentThread.Name)
                    : "this should not run on the worker thread"
                    );
        }

        protected virtual void SetupWorker()
        {
            AssertWorkerThread(true);
            Dispatcher.Run();
        }

        protected virtual void CleanWorker()
        {
            AssertWorkerThread(true);
        }

        private Thread _WorkerThread;

        /// <summary>
        /// The Dispatcher from the worker thread
        /// </summary>
        protected Dispatcher WorkerDispatcher
        {
            get
            {
                return Dispatcher.FromThread(_WorkerThread);
            }
        }

        public void StartThread()
        {
            AssertWorkerThread(false);
            _WorkerThread = new Thread(SetupWorker);
            _WorkerThread.SetApartmentState(ApartmentState.STA);
            _WorkerThread.Name = WORKER_THREAD_NAME;
            _WorkerThread.Start();
        }

        public void RunOnWorker(Action test)
        {
            AssertWorkerThread(false);
            WorkerDispatcher.Invoke(DispatcherPriority.Normal, test);
        }
        
        /// <summary>
        /// See http://msdn.microsoft.com/en-us/library/system.windows.threading.dispatcherframe.aspx 
        /// for details on the DoEvents() method.
        /// </summary>
        public void DoEvents()
        {
            AssertWorkerThread(true);
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
                new DispatcherOperationCallback(ExitFrames), frame);
            Dispatcher.PushFrame(frame);
        }

        private object ExitFrames(object f)
        {
            ((DispatcherFrame)f).Continue = false;
            return null;
        }

        public void JoinThread()
        {
            AssertWorkerThread(false);
            WorkerDispatcher.InvokeShutdown();
            _WorkerThread.Join();
        }

        public void KillThread()
        {
            AssertWorkerThread(false);
            Assert.IsTrue(WorkerDispatcher.HasShutdownFinished,
                "Worker thread should have already been shut down by calling JoinThread()");
        }
    }
}
