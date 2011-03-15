
namespace Kistl.Client.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;
    using System.Windows.Threading;
    using Kistl.API.Utils;

    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            Steps = 10;
            CurrentStep = 0;
            InitializeComponent();
            Log.Debug("Initialization complete");
        }

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Client.WPF.SplashScreen");
        private static readonly object _lock = new object();
        private static SplashScreen _current = null;
        private static Thread _thread = null;
        private static AutoResetEvent _created = new AutoResetEvent(false);

        private static void Run()
        {
            Log.Debug("Run: Start");

            _current = new SplashScreen();
            _current.Show();
            _current.Closed += (sender, e) => _current.Dispatcher.InvokeShutdown();

            Log.Debug("Run: Set");
            _created.Set();
            System.Windows.Threading.Dispatcher.Run();
        }

        public static void ShowSplashScreen(string message, string info, int steps)
        {
            using (Log.DebugTraceMethodCall("ShowSplashScreen"))
            {
                lock (_lock)
                {
                    Log.Debug("Acquired lock for ShowSplashScreen");
                    if (_current == null)
                    {
                        Log.Debug("Will create new Thread");
                        _thread = new Thread(new ThreadStart(Run));
                        _thread.SetApartmentState(ApartmentState.STA);
                        _thread.IsBackground = true; // do not block main process from closing
                        _thread.Start();

                        Log.Debug("Waiting for thread to proceed");
                        _created.WaitOne();

                        _current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate()
                        {
                            Log.Debug("Setting new message");
                            _current.Message = message;
                            _current.Info = info;
                            _current.Steps = steps;
                        }));
                    }
                    else
                    {
                        Log.Debug("Reusing existing dispatcher");
                        _current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate()
                        {
                            Log.Debug("Setting new message on reused thread");
                            _current.Message = message;
                            _current.Info = info;
                            _current.Steps = steps;

                            _current.Show();
                            _current.Activate();
                        }));
                    }
                }
            }
        }

        public static void HideSplashScreen()
        {
            using (Log.DebugTraceMethodCall("HideSplashScreen"))
            {
                lock (_lock)
                {
                    Log.Debug("Acquired lock for HideSplashScreen");
                    if (_current != null)
                    {
                        Log.Debug("Signalling Hide to the dispatcher");
                        _current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate()
                        {
                            Log.Debug("Committing Hide to the dispatcher");
                            _current.Hide();
                        }));
                    }
                    else
                    {
                        Log.Debug("No current dispatcher");
                    }
                }
            }
        }

        public static void SetInfo(string info)
        {
            using (Log.DebugTraceMethodCall("SetInfo"))
            {
                lock (_lock)
                {
                    Log.Debug("Acquired lock for SetInfo");
                    if (_current != null)
                    {
                        Log.Debug("Signalling SetInfo to the dispatcher");
                        _current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate()
                        {
                            Log.Debug("Set new Info and incremented CurrentStep");
                            _current.Info = info;
                            _current.CurrentStep++;
                        }));
                    }
                }
            }
        }

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Message.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(SplashScreen));

        public string Info
        {
            get { return (string)GetValue(InfoProperty); }
            set { SetValue(InfoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Info.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InfoProperty =
            DependencyProperty.Register("Info", typeof(string), typeof(SplashScreen));

        public int Steps
        {
            get { return (int)GetValue(StepsProperty); }
            set { SetValue(StepsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Steps.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StepsProperty =
            DependencyProperty.Register("Steps", typeof(int), typeof(SplashScreen));

        public int CurrentStep
        {
            get { return (int)GetValue(CurrentStepProperty); }
            set { SetValue(CurrentStepProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentStep.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentStepProperty =
            DependencyProperty.Register("CurrentStep", typeof(int), typeof(SplashScreen));
    }
}
