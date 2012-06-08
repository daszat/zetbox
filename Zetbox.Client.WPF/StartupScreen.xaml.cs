namespace Zetbox.Client.WPF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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
    using Zetbox.API.Utils;

    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class StartupScreen : Window
    {
        public StartupScreen()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;


            Steps = 10;
            CurrentStep = 0;
            InitializeComponent();
            DataContext = this;
            Log.Debug("Initialization complete");
        }

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Client.WPF.SplashScreen");
        private static readonly object _lock = new object();
        private static StartupScreen _current = null;
        private static Thread _thread = null;
        private static AutoResetEvent _created = new AutoResetEvent(false);

        private static void Run()
        {
            Log.Debug("Run: Start");

            _current = new StartupScreen();
            _current.Show();
            _current.Closed += (sender, e) => _current.Dispatcher.InvokeShutdown();

            Log.Debug("Run: Set");
            _created.Set();
            System.Windows.Threading.Dispatcher.Run();
            _current = null;
            _thread = null;
        }

        /// <summary>
        /// Closes the splash screen on first window loaded event
        /// </summary>
        /// <remarks>
        /// Trick 17, close splash screen, when Application Dispatcher is ready to dispatch messages after the app has been loaded
        /// http://www.olsonsoft.com/blogs/stefanolson/post/A-better-WPF-splash-screen.aspx
        /// </remarks>
        public static void CanCloseOnWindowLoaded()
        {
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Loaded,
                new ThreadStart(() =>
                {
                    lock (_lock)
                    {
                        if (_current != null) _current.Dispatcher.InvokeShutdown();
                    }
                })
            );
        }

        public static void ShowSplashScreen(string message, string info, int steps)
        {
            lock (_lock)
            {
                if (_current == null)
                {
                    _thread = new Thread(new ThreadStart(Run));
                    _thread.SetApartmentState(ApartmentState.STA);
                    _thread.IsBackground = true; // do not block main process from closing
                    _thread.Start();

                    _created.WaitOne();

                    _current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate()
                    {
                        _current.Message = message;
                        _current.Info = info;
                        _current.Steps = steps;
                    }));
                }
                else
                {
                    Log.Warn("There is already a SplashScreen shown");
                }
            }
        }

        public static void SetInfo(string info)
        {
            lock (_lock)
            {
                if (_current != null)
                {
                    _current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate()
                    {
                        _current.Info = info;
                        _current.CurrentStep++;
                    }));
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
            DependencyProperty.Register("Message", typeof(string), typeof(StartupScreen));

        public string Info
        {
            get { return (string)GetValue(InfoProperty); }
            set { SetValue(InfoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Info.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InfoProperty =
            DependencyProperty.Register("Info", typeof(string), typeof(StartupScreen));

        public int Steps
        {
            get { return (int)GetValue(StepsProperty); }
            set { SetValue(StepsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Steps.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StepsProperty =
            DependencyProperty.Register("Steps", typeof(int), typeof(StartupScreen));

        public int CurrentStep
        {
            get { return (int)GetValue(CurrentStepProperty); }
            set { SetValue(CurrentStepProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentStep.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentStepProperty =
            DependencyProperty.Register("CurrentStep", typeof(int), typeof(StartupScreen));

        public class ExitCommandImpl : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            protected void OnCanExecuteChanged()
            {
                var temp = CanExecuteChanged;
                if (temp != null)
                {
                    temp(this, EventArgs.Empty);
                }
            }

            public void Execute(object parameter)
            {
                System.Environment.Exit(1);
            }

            public string Label
            {
                get
                {
                    return Zetbox.Client.Properties.Resources.ExitButton;
                }
            }
        }

        private ICommand _ExitCommand;
        public ICommand ExitCommand
        {
            get
            {
                if (_ExitCommand == null)
                {
                    _ExitCommand = new ExitCommandImpl();
                }
                return _ExitCommand;
            }
        }
    }
}