using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;

namespace Kistl.Client.WPF
{
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
        }

        private static SplashScreen _current = null;
        private static Thread _thread = null;
        private static AutoResetEvent _threadStarted = new AutoResetEvent(false);

        private static void RunSplashScreen()
        {
            _current = new SplashScreen();
            _threadStarted.Set();
            _current.ShowDialog();
        }

        public static void ShowSplashScreen(string message, string info, int steps)
        {
            try
            {
                if (_current == null)
                {
                    _thread = new Thread(new ThreadStart(RunSplashScreen));
                    _thread.SetApartmentState(ApartmentState.STA);
                    _thread.Start();

                    _threadStarted.WaitOne();

                    _current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate()
                    {
                        _current.Message = message;
                        _current.Info = info;
                        _current.Steps = steps;
                    }));
                }
            }
            catch (Exception ex)
            {
                ClientHelper.HandleError(ex);
            }
        }

        public static void HideSplashScreen()
        {
            try
            {
                if (_current != null)
                {
                    _current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate()
                    {
                        _current.Close();
                    }));
                    _current = null;
                    _thread = null;
                }
            }
            catch (Exception ex)
            {
                ClientHelper.HandleError(ex);
            }
        }

        public static void SetInfo(string info)
        {
            try
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
            catch (Exception ex)
            {
                ClientHelper.HandleError(ex);
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
