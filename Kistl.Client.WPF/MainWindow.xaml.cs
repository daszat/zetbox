using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Xml.Serialization;
using Kistl.API;
using Kistl.GUI;

namespace Kistl.Client
{
    /// <summary>
    /// Hauptfenster der KistlApplikation
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Primary = this;
        }

        public static MainWindow Primary
        {
            get;
            private set;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Get the debugger and re-parent him to the mainwindow
            // TODO: Use ServiceDiscoveryService to get to the debugger instance
            var debugger = (Kistl.Client.WPF.Debugger.KistlContextDebuggerWPF)KistlContextDebugger.GetDebugger();
            debugger.Owner = this;
            // Register as MainWindow for correctness.
            Application.Current.MainWindow = this;
        }
    }
}
