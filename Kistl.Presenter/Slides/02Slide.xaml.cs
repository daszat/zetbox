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
    /// <summary>
    /// Interaction logic for _02Slide.xaml
    /// </summary>
    public partial class _02Slide : Page
    {
        private DispatcherTimer timer;

        public _02Slide()
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

        private Kistl.Client.MainWindowCtrl kistl;

        private void SetupControl()
        {
            if (kistl != null) return;
            if (App.KistlStarted)
            {
                kistl = new Kistl.Client.MainWindowCtrl();
                RootPanel.Children.Clear();
                RootPanel.Children.Add(kistl);
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
