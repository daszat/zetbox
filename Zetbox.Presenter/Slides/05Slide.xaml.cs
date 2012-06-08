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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPFPresenter.Slides
{
    public partial class _05Slide : Page
    {
        private DispatcherTimer timer;

        public _05Slide()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(Timer_Tick);

            SetupControl();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            SetupControl();
        }

        private Zetbox.Client.MainWindowCtrl zetbox;

        private void SetupControl()
        {
            if (zetbox != null) return;
            if (App.ZetboxStarted)
            {
                zetbox = new Zetbox.Client.MainWindowCtrl();
                zetbox.Width = 640;
                zetbox.Height = 480;
                RootPanel.Children.Clear();
                RootPanel.Children.Add(zetbox);
            }
            else
            {
                TextBlock txt = new TextBlock();
                txt.Text = ".";
                WaitPanel.Children.Add(txt);
                timer.Start();
            }
        }
    }
}
