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
    public partial class MainWindow : Window, IMainUI
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region IMainUI Members

        void IMainUI.Show()
        {
            Show();
        }

        void IMainUI.Close()
        {
            Close();
        }

        #endregion

        #region IBasicControl Members

        string IBasicControl.ShortLabel
        {
            get { return Title; }
            set { Title = value; }
        }

        string IBasicControl.Description { get; set; }
        FieldSize IBasicControl.Size { get; set; }

        #endregion
    }
}
